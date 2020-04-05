using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using Hondee.Common.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace HD.MeteringPayment.DAL.DBContractBoq
{
    public class DBContractBoqChange : IContractBoqChange
    {
        /// <summary>
        /// 查找Change的SQL,传入参数@ProjectNo
        /// </summary>
        const String CHANGE_BY_PROJECTNO = "SELECT * FROM [ERP_BoQ].[dbo].[v_gl_cntrct_boqchange] WHERE ProjectNo=@ProjectNo AND EDIT=1";
        const String CHANGE_BY_BOQNO = "SELECT * FROM [ERP_BoQ].[dbo].[v_gl_cntrct_boqchange] WHERE BoqNo=@BoqNo AND EDIT=1";
        const String CHANGE_BY_CHANGENO = "SELECT * FROM [ERP_BoQ].[dbo].[v_gl_cntrct_boqchange] WHERE ChangeNo=@ChangeNo AND EDIT=1";
        const String CHANGE_DETAIL_BY_CHANGENO = "SELECT * FROM [ERP_BoQ].[dbo].[Gl_Cntrct_boqchangeDetail] WHERE ChangeNo=@ChangeNo AND EDIT=1";
        /// <summary>
        /// 获取清单项变更记录的SQL
        /// </summary>
        const String CHANGE_LOG_BYITEMNO = "SELECT * FROM [ERP_BoQ].[dbo].[v_gl_cntrct_boqchangelog] WHERE ItemNo=@ItemNo Order by AdjustDate desc";
        public List<ContractBoqChangeInfo> GetList(string BoqNo)
        {
            return HdDbCmdManager.GetInstance().QueryForList<ContractBoqChangeInfo>(CHANGE_BY_BOQNO, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@BoqNo",BoqNo)});
        }
        public List<ContractBoqChangeInfo> GetListByProjectNo(string ProjectNo)
        {
            return HdDbCmdManager.GetInstance().QueryForList<ContractBoqChangeInfo>(CHANGE_BY_PROJECTNO, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@ProjectNo",ProjectNo)});
        }

        public ContractBoqChange Get(string ChangeNo)
        {
            ContractBoqChange boqResult = HdDbCmdManager.GetInstance().QueryForFirstRow<ContractBoqChange>(CHANGE_BY_CHANGENO, System.Data.CommandType.Text, new CmdParameter[] {
                                                new CmdParameter("@ChangeNo",ChangeNo)});
            if (boqResult != null)
            {
                boqResult.Details = HdDbCmdManager.GetInstance().QueryForList<ContractBoqChangeDetail>(CHANGE_DETAIL_BY_CHANGENO, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@ChangeNo",boqResult.ChangeNo)});
            }
            return boqResult;
        }

        public void Delete(string ChangeNo)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ChangeNo", ChangeNo));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Change_Delete", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });

        }

        public ContractBoqChange Save(ContractBoqChange BoqChange)
        {
            if (String.IsNullOrEmpty(BoqChange.ChangeNo))
            {
                //新增
                HdDbCmdManager.GetInstance().ExecuteTran(Tran=> {
                    Insert(BoqChange, ServiceContext.LoginName,Tran);
                    BoqChange.Details.ForEach(m=> {
                        m.ChangeNo = BoqChange.ChangeNo;
                        InsertDetail(m, ServiceContext.LoginName, Tran);
                    });
                });
            }
            else {
                //临时变量
                ContractBoqChangeDetail temp = null;
                //修改
                ContractBoqChange oldData = Get(BoqChange.ChangeNo);
                HdDbCmdManager.GetInstance().ExecuteTran(Tran => {
                    Update(BoqChange, ServiceContext.LoginName, Tran);
                    List<ContractBoqChangeDetail> lstDelete= oldData.Details.FindAll(m => !BoqChange.Details.Exists(n => n.ChangeDetailNo == m.ChangeDetailNo));
                    List<ContractBoqChangeDetail> lstAdd = BoqChange.Details.FindAll(m=>String.IsNullOrEmpty(m.ChangeDetailNo));
                    List<ContractBoqChangeDetail> lstDisable = BoqChange.Details.FindAll(m => (temp = oldData.Details.FirstOrDefault(n => n.ChangeDetailNo == m.ChangeDetailNo)) != null && temp.StatId == 1 && m.StatId == 0);
                    List<ContractBoqChangeDetail> lstEnable = BoqChange.Details.FindAll(m => (temp = oldData.Details.FirstOrDefault(n => n.ChangeDetailNo == m.ChangeDetailNo)) != null && temp.StatId == 0 && m.StatId == 1);
                    List<ContractBoqChangeDetail> lstUpdate = BoqChange.Details.FindAll(m => (temp = oldData.Details.FirstOrDefault(n => n.ChangeDetailNo == m.ChangeDetailNo)) != null && temp.StatId ==m.StatId);
                    lstDelete.ForEach(m => { 
                        DeleteDetail(m.ChangeDetailNo, ServiceContext.LoginName, Tran);
                    });
                    lstAdd.ForEach(m =>
                    {
                        m.ChangeNo = BoqChange.ChangeNo;
                        InsertDetail(m, ServiceContext.LoginName, Tran);
                    });
                    lstUpdate.ForEach(m =>
                    {
                        UpdateDetail(m, ServiceContext.LoginName, Tran);
                    });
                    lstEnable.ForEach(m =>
                    {
                        ChangeDataStat(m.ChangeDetailNo,1, ServiceContext.LoginName, Tran);
                    });
                    lstDisable.ForEach(m =>
                    {
                        ChangeDataStat(m.ChangeDetailNo, 0, ServiceContext.LoginName, Tran);
                    });
                });

            }
            return Get(BoqChange.ChangeNo);
        }

        public int Commit(string ChangeNo)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ChangeNo",  ChangeNo));
            cmds.Add(new CmdParameter("@OperationBy", ServiceContext.LoginName));
            cmds.Add(new CmdParameter("@Version", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Change_Commit", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return Convert.ToInt32(pResult["@Version"]);
        }

        public void Fixed(string ChangeNo, bool Fixed)
        {
            Commit(ChangeNo);
            if (Fixed)
            {
                List<CmdParameter> cmds = new List<CmdParameter>();
                cmds.Add(new CmdParameter("@ChangeNo", ChangeNo));
                cmds.Add(new CmdParameter("@ReleaseBy", ServiceContext.UserName));
                cmds.Add(new CmdParameter("@ReleaseDate", DateTime.Now));
                cmds.Add(new CmdParameter("@OperationBy", ServiceContext.LoginName));
                cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
                cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

                ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
                HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Change_Release", CommandType.StoredProcedure, pResult.Parameters);
                if (!Convert.ToBoolean(pResult["@Ok"]))
                    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                    {
                        ErrorMessage = pResult["@Infor"].ToString()
                    });
            }
            else
            {
                List<CmdParameter> cmds = new List<CmdParameter>();
                cmds.Add(new CmdParameter("@ChangeNo", ChangeNo));
                cmds.Add(new CmdParameter("@OperationBy", ServiceContext.LoginName));
                cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
                cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

                ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
                HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Change_UnFix", CommandType.StoredProcedure, pResult.Parameters);
                if (!Convert.ToBoolean(pResult["@Ok"]))
                    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                    {
                        ErrorMessage = pResult["@Infor"].ToString()
                    });
            }
        }

        /// <summary>
        /// 获取Boq
        /// </summary>
        /// <param name="ProjectNo">项目No</param>
        /// <returns></returns>
        public ContractBoq GetByProjectNo(string ProjectNo)
        {
            ContractBoq boqResult= HdDbCmdManager.GetInstance().QueryForFirstRow<ContractBoq>(CHANGE_BY_PROJECTNO, System.Data.CommandType.Text, new CmdParameter[] {
                                                new CmdParameter("@ProjectNo",ProjectNo)});
            if (boqResult != null)
            {
                boqResult.BoiList = HdDbCmdManager.GetInstance().QueryForList<ContractBoi>(CHANGE_BY_CHANGENO, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@BoqNo",boqResult.BoQNo)});
            }
            return boqResult;
        }
        #region 清单的数据库操作
        private void ChangeDataStat(String ChangeDetailNo, int StatId,String OperationBy,SqlTransaction Tran)
        {

            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ChangeDetailNo", ChangeDetailNo)); 
            cmds.Add(new CmdParameter("@StatId", StatId));
            cmds.Add(new CmdParameter("@RecordValidity", StatId));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_ChangeDetail_ChangeDataStat", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        } 
        /// <summary>
        /// 插入清单
        /// </summary>
       ///<param name="ChangeData">变更对象</param>
        /// <param name="OperationBy">操作人</param>
        private String Insert(ContractBoqChange ChangeData, String OperationBy, SqlTransaction Tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", ChangeData.ProjectNo)); 
            cmds.Add(new CmdParameter("@PrepareBy", ChangeData.PrepareBy));
            cmds.Add(new CmdParameter("@PrepareDate", ChangeData.PrepareDate));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ChangeNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ChangeCode", ChangeData.ChangeCode));
            cmds.Add(new CmdParameter("@ChangeName", ChangeData.ChangeName));
            cmds.Add(new CmdParameter("@Type", ChangeData.Type));
            cmds.Add(new CmdParameter("@ChangeDate", ChangeData.ChangeDate));
            cmds.Add(new CmdParameter("@ChangeAmount", ChangeData.ChangeAmount));
            cmds.Add(new CmdParameter("@BoiNum", ChangeData.BoiNum));
            cmds.Add(new CmdParameter("@Description", ChangeData.Description));
            cmds.Add(new CmdParameter("@Remark", ChangeData.Remark));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Change_Add", CommandType.StoredProcedure, pResult.Parameters,Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            ChangeData.ChangeNo = pResult["@ChangeNo"].ToString();
            return ChangeData.ChangeNo;
        }
        /// <summary>
        /// 更新清单头
        /// </summary>
        /// <param name="BoqNo"></param>
        /// <param name="BoqName"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="Tran"></param>
        private void Update(ContractBoqChange ChangeData, String OperationBy, SqlTransaction Tran)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ChangeNo", ChangeData.ChangeNo));
            cmds.Add(new CmdParameter("@Remark", ChangeData.Remark));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ProjectNo", ChangeData.ProjectNo));
            cmds.Add(new CmdParameter("@ChangeCode", ChangeData.ChangeCode));
            cmds.Add(new CmdParameter("@ChangeName", ChangeData.ChangeName));
            cmds.Add(new CmdParameter("@Type", ChangeData.Type));
            cmds.Add(new CmdParameter("@ChangeDate", ChangeData.ChangeDate));
            cmds.Add(new CmdParameter("@ChangeAmount", ChangeData.ChangeAmount));
            cmds.Add(new CmdParameter("@BoiNum", ChangeData.BoiNum));
            cmds.Add(new CmdParameter("@Description", ChangeData.Description));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Change_Update", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        /// <summary>
        /// 插入清单项
        /// </summary>
        /// <param name="BoqNo"></param>
        /// <param name="Detail"></param>
        /// <param name="OperationBy">操作人</param>
        /// <returns>No信息</returns>
        private ContractBoqChangeDetail InsertDetail(ContractBoqChangeDetail Detail, String OperationBy,SqlTransaction Tran=null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", Detail.ProjectNo));
            cmds.Add(new CmdParameter("@ChangeNo", Detail.ChangeNo));
            cmds.Add(new CmdParameter("@Type", Detail.Type));
            cmds.Add(new CmdParameter("@IsUpInfo", Detail.IsUpInfo));
            cmds.Add(new CmdParameter("@IsUpQty", Detail.IsUpQty));
            cmds.Add(new CmdParameter("@IsUpPrice", Detail.IsUpPrice));

            cmds.Add(new CmdParameter("@ItemNo", Detail.ItemNo));
            cmds.Add(new CmdParameter("@ItemCode", Detail.ItemCode));
            cmds.Add(new CmdParameter("@IItemCoe", Detail.IItemCoe));
            cmds.Add(new CmdParameter("@ItemName", Detail.ItemName));

            cmds.Add(new CmdParameter("@ParentCode", Detail.ParentCode));
            cmds.Add(new CmdParameter("@Currency", Detail.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", Detail.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", Detail.ExchangeRate));
            cmds.Add(new CmdParameter("@Uom", Detail.Uom));
            cmds.Add(new CmdParameter("@BefQty", Detail.BefQty));
            cmds.Add(new CmdParameter("@BefPrjPrice", Detail.BefPrjPrice));
            cmds.Add(new CmdParameter("@BefAmount", Detail.BefAmount));
            cmds.Add(new CmdParameter("@AfQty", Detail.AfQty));
            cmds.Add(new CmdParameter("@AfPrice", Detail.AfPrice));
            cmds.Add(new CmdParameter("@AfAmount", Detail.AfAmount));

            cmds.Add(new CmdParameter("@ChangeQty", Detail.ChangeQty));
            cmds.Add(new CmdParameter("@ChangePrice", Detail.ChangePrice));
            cmds.Add(new CmdParameter("@ChangeAmount", Detail.ChangeAmount));
            cmds.Add(new CmdParameter("@Description", Detail.Description));
            cmds.Add(new CmdParameter("@Remark", Detail.Remark));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ChangeDetailNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_ChangeDetail_Add", CommandType.StoredProcedure, pResult.Parameters,Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            Detail.ChangeDetailNo = pResult["@ChangeDetailNo"].ToString();
            return Detail;
        }
        /// <summary>
        /// 更新清单项
        /// </summary>
        /// <param name="Boi"></param>
        /// <param name="OperationBy">操作人</param>
        private void UpdateDetail(ContractBoqChangeDetail Boi, String OperationBy, SqlTransaction Tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ChangeDetailNo", Boi.ChangeDetailNo));
            cmds.Add(new CmdParameter("@ChangeNo", Boi.ChangeNo));
            cmds.Add(new CmdParameter("@Type", Boi.Type));
            cmds.Add(new CmdParameter("@IsUpInfo", Boi.IsUpInfo));
            cmds.Add(new CmdParameter("@IsUpQty", Boi.IsUpQty));
            cmds.Add(new CmdParameter("@IsUpPrice", Boi.IsUpPrice));
                
            cmds.Add(new CmdParameter("@ItemNo", Boi.ItemNo));
            cmds.Add(new CmdParameter("@ItemCode", Boi.ItemCode));
            cmds.Add(new CmdParameter("@IItemCoe", Boi.IItemCoe));
            cmds.Add(new CmdParameter("@ItemName", Boi.ItemName));
            cmds.Add(new CmdParameter("@ParentCode", Boi.ParentCode));
            cmds.Add(new CmdParameter("@Currency", Boi.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", Boi.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", Boi.ExchangeRate));
            cmds.Add(new CmdParameter("@Uom", Boi.Uom));
            cmds.Add(new CmdParameter("@BefQty", Boi.BefQty));
            cmds.Add(new CmdParameter("@BefPrjPrice", Boi.BefPrjPrice));
            cmds.Add(new CmdParameter("@BefAmount", Boi.BefAmount));
            cmds.Add(new CmdParameter("@AfQty", Boi.AfQty));
            cmds.Add(new CmdParameter("@AfPrice", Boi.AfPrice));
            cmds.Add(new CmdParameter("@AfAmount", Boi.AfAmount));
            cmds.Add(new CmdParameter("@ChangeQty", Boi.ChangeQty));
            cmds.Add(new CmdParameter("@ChangePrice", Boi.ChangePrice));
            cmds.Add(new CmdParameter("@ChangeAmount", Boi.ChangeAmount));
            cmds.Add(new CmdParameter("@Description", Boi.Description));
            cmds.Add(new CmdParameter("@Remark", Boi.Remark));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_ChangeDetail_Update", CommandType.StoredProcedure, pResult.Parameters,Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        /// <summary>
        /// 删除清单项
        /// </summary>
        /// <param name="ChangeDetailNo">清单项No</param>
        /// <param name="OperationBy">操作人</param>
        private void DeleteDetail(String ChangeDetailNo, String OperationBy, SqlTransaction Tran = null)
        {

            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ChangeDetailNo", ChangeDetailNo));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_ChangeDetail_Delete", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        /// <summary>
        /// 获取清单项变更记录
        /// </summary>
        /// <param name="ItemNo"></param>
        /// <returns></returns>
        public List<ContractBoqChangeLog> GetItemChangeLog(string ItemNo)
        {
            return HdDbCmdManager.GetInstance().QueryForList<ContractBoqChangeLog>(CHANGE_LOG_BYITEMNO, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@ItemNo",ItemNo)});
        }



        #endregion

    }
}
