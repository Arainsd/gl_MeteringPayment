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
    public class DBContractBoq : IContractBoq
    {
        /// <summary>
        /// 查找BOQ的SQL,传入参数@ProjectNo
        /// </summary>
        const String BOQ_BY_PROJECTNO = "SELECT * FROM [ERP_BoQ].[dbo].[v_gl_cntrct_boq] WHERE ProjectNo=@ProjectNo AND EDIT=1";
        const String BOI_BY_BOQNO = "SELECT * FROM [ERP_BoQ].[dbo].[v_gl_cntrct_boi] WHERE BoQNo=@BoQNo AND EDIT=1";
        /// <summary>
        /// 获取Boq
        /// </summary>
        /// <param name="ProjectNo">项目No</param>
        /// <returns></returns>
        public ContractBoq GetByProjectNo(string ProjectNo)
        {
            ContractBoq boqResult= HdDbCmdManager.GetInstance().QueryForFirstRow<ContractBoq>(BOQ_BY_PROJECTNO, System.Data.CommandType.Text, new CmdParameter[] {
                                                new CmdParameter("@ProjectNo",ProjectNo)});
            if (boqResult != null)
            {
                string strOrderString = " ORDER BY ItemCode ASC";
                boqResult.BoiList = HdDbCmdManager.GetInstance().QueryForList<ContractBoi>(BOI_BY_BOQNO + strOrderString, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@BoqNo",boqResult.BoQNo)});
            }
            return boqResult;
        }

        public List<ContractBoiNoInfo> Save(string ProjectNo, string BidNo, String BoqName, decimal TotalAmount, List<ContractBoi> AddList, List<ContractBoi> UpdateList, List<string> DeleteItemNoList)
        {
            List<ContractBoiNoInfo> lstBoiNoInfo = new List<ContractBoiNoInfo>();
            String strBoqNo = null;
            HdDbCmdManager.GetInstance().ExecuteTran(Tran=> {
                strBoqNo = GetBoqNo(ProjectNo, Tran);
                if (String.IsNullOrEmpty(strBoqNo))
                {
                    strBoqNo = InsertBoq(ProjectNo, BidNo, BoqName, TotalAmount, ServiceContext.LoginName, Tran); 
                }
                else{
                    UpdateBoq(strBoqNo, BoqName, TotalAmount, ServiceContext.LoginName, Tran);
                }
                //删除明细
                if (DeleteItemNoList != null)
                {
                    DeleteItemNoList.ForEach(m=> {
                        DeleteBoi(m, ServiceContext.LoginName, Tran);
                    });
                }
                //修改
                if (UpdateList != null)
                {
                    UpdateList.ForEach(m=> {
                        UpdateBoi(m, ServiceContext.LoginName, Tran);
                    });
                }
                //新增
                if (AddList != null)
                {
                    AddList.ForEach(m=> {
                        lstBoiNoInfo.Add(InsertBoi(strBoqNo, m, ServiceContext.LoginName, Tran));
                    });
                }
            });
            return lstBoiNoInfo;
        }
        public void ChangeStat(String BoqNo, int ExecuteStat)
        {
            Commit(BoqNo, ServiceContext.LoginName);
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@BoqNo", BoqNo));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ApprovalStat", 1));
            cmds.Add(new CmdParameter("@ExecuteStat", ExecuteStat)); 
            cmds.Add(new CmdParameter("@OperationBy", ServiceContext.LoginName));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Boq_ChangeBusinessStat", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
          
        }
        #region 清单的数据库操作
        /// <summary>
        /// 提交清单
        /// </summary>
        /// <param name="BoqNo"></param>
        /// <param name="OperationBy"></param>
        /// <param name="Tran"></param>
        /// <returns></returns>
        private int Commit(String BoqNo,String OperationBy, SqlTransaction Tran=null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@BoqNo", BoqNo));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Version", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Boq_Commit", CommandType.StoredProcedure, pResult.Parameters,Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return Convert.ToInt32(pResult["@Version"]);
        }
        /// <summary>
        /// 获取清单No数据
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        private String GetBoqNo(String ProjectNo, SqlTransaction tran = null)
        {
            String strBoqNo = null;
            ContractBoq boqHeader= HdDbCmdManager.GetInstance().QueryForFirstRow<ContractBoq>(BOQ_BY_PROJECTNO, CommandType.Text, new CmdParameter[] {
                new CmdParameter("@ProjectNo",ProjectNo)
            }, tran);
            if (boqHeader != null)
                strBoqNo = boqHeader.BoQNo;
            return strBoqNo;
        }
        /// <summary>
        /// 插入清单
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="BoqName"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="OperationBy">操作人</param>
        private String InsertBoq(String ProjectNo, String BidNo, String BoqName, decimal TotalAmount, String OperationBy, SqlTransaction tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", ProjectNo));
            cmds.Add(new CmdParameter("@BidNo", BidNo));
            cmds.Add(new CmdParameter("@BoQName", BoqName));
            cmds.Add(new CmdParameter("@Description", null));
            cmds.Add(new CmdParameter("@TotalAmount", TotalAmount));
            cmds.Add(new CmdParameter("@TotalAmountEx", null));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@BoqNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Boq_Add", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return pResult["@BoqNo"].ToString();
        }
        /// <summary>
        /// 更新清单头
        /// </summary>
        /// <param name="BoqNo"></param>
        /// <param name="BoqName"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="Tran"></param>
        private void UpdateBoq(String BoqNo, String BoqName, Decimal TotalAmount, String OperationBy, SqlTransaction Tran)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@BoqNo", BoqNo)); 
            cmds.Add(new CmdParameter("@BoQName", BoqName));
            cmds.Add(new CmdParameter("@Description", null));
            cmds.Add(new CmdParameter("@TotalAmount", TotalAmount));
            cmds.Add(new CmdParameter("@TotalAmountEx", null));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Boq_Update", CommandType.StoredProcedure, pResult.Parameters);
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
        /// <param name="Boi"></param>
        /// <param name="OperationBy">操作人</param>
        /// <returns>No信息</returns>
        private ContractBoiNoInfo InsertBoi(String BoqNo,ContractBoi Boi, String OperationBy,SqlTransaction Tran=null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", Boi.ProjectNo));
            cmds.Add(new CmdParameter("@BidNo", Boi.BidNo));
            cmds.Add(new CmdParameter("@BoQNo", BoqNo));
            cmds.Add(new CmdParameter("@IItemCoe", Boi.IItemCoe));
            cmds.Add(new CmdParameter("@ItemName", Boi.ItemName));
            cmds.Add(new CmdParameter("@ParentCode", Boi.ParentCode));
            cmds.Add(new CmdParameter("@Currency", Boi.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", Boi.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", Boi.ExchangeRate));
            cmds.Add(new CmdParameter("@Uom", Boi.Uom));
            cmds.Add(new CmdParameter("@CtrctQty", Boi.CtrctQty));
            cmds.Add(new CmdParameter("@CtrctPrjPrice", Boi.CtrctPrjPrice));
            cmds.Add(new CmdParameter("@CtrctAmount", Boi.CtrctAmount));   
            cmds.Add(new CmdParameter("@Description", Boi.Description));
            cmds.Add(new CmdParameter("@CategoryNo", Boi.CategoryNo));
            cmds.Add(new CmdParameter("@isCntrctItem", Boi.isCntrctItem));
            cmds.Add(new CmdParameter("@isImportant", Boi.isImportant));
            cmds.Add(new CmdParameter("@isTax", Boi.isTax));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ItemNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ItemCode", Boi.ItemCode, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Boi_Add", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            ContractBoiNoInfo BoiInfo = new ContractBoiNoInfo();
            BoiInfo.ItemCode = Boi.ItemCode;
            BoiInfo.ItemNo = pResult["@ItemNo"].ToString();
            return BoiInfo;
        }
        /// <summary>
        /// 更新清单项
        /// </summary>
        /// <param name="Boi"></param>
        /// <param name="OperationBy">操作人</param>
        private void UpdateBoi(ContractBoi Boi, String OperationBy, SqlTransaction Tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ItemNo", Boi.ItemNo));
            cmds.Add(new CmdParameter("@ProjectNo", Boi.ProjectNo));
            cmds.Add(new CmdParameter("@BoQNo", Boi.BoQNo));
            cmds.Add(new CmdParameter("@ItemCode", Boi.ItemCode));
            cmds.Add(new CmdParameter("@IItemCoe", Boi.IItemCoe));
            cmds.Add(new CmdParameter("@ItemName", Boi.ItemName));
            cmds.Add(new CmdParameter("@ParentCode", Boi.ParentCode));
            cmds.Add(new CmdParameter("@Currency", Boi.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", Boi.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", Boi.ExchangeRate));
            cmds.Add(new CmdParameter("@Uom", Boi.Uom));
            cmds.Add(new CmdParameter("@CtrctQty", Boi.CtrctQty));
            cmds.Add(new CmdParameter("@CtrctPrjPrice", Boi.CtrctPrjPrice));
            cmds.Add(new CmdParameter("@CtrctAmount", Boi.CtrctAmount)); 
            cmds.Add(new CmdParameter("@Description", Boi.Description));
            cmds.Add(new CmdParameter("@CategoryNo", Boi.CategoryNo));
            cmds.Add(new CmdParameter("@isCntrctItem", Boi.isCntrctItem));
            cmds.Add(new CmdParameter("@isImportant", Boi.isImportant));
            cmds.Add(new CmdParameter("@isTax", Boi.isTax));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Boi_Update", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        /// <summary>
        /// 删除清单项
        /// </summary>
        /// <param name="BoiNo">清单项No</param>
        /// <param name="OperationBy">操作人</param>
        private void DeleteBoi(String ItemNo, String OperationBy, SqlTransaction Tran = null)
        {

            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ItemNo", ItemNo));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Boi_Delete", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        #endregion

    }
}
