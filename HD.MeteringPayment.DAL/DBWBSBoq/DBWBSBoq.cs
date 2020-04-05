using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using Hondee.Common.Attributes;
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

namespace HD.MeteringPayment.DAL.DBWBSBoq
{
    public class DBWBSBoq : IWBSBoq
    {
        #region 查找BOQ的SQL,传入参数@ProjectNo    WBS_BY_PROJECTNO
        const String WBS_BY_PROJECTNO = @"SELECT  ProjectNo
		                                            ,BidNo
		                                            ,WbsNo
		                                            ,WbsName
		                                            ,TotalAmount
		                                            ,TotalAmountEx
		                                            ,ApprovalDate
		                                            ,ApprovalStat
		                                            ,ExecuteStat
		                                            ,Fixed
                                                    ,ReleaseBy
                                                    ,ReleaseDate
                                                FROM [ERP_BoQ].[dbo].[v_gl_cntrct_wbs] WHERE ProjectNo=@ProjectNo AND EDIT=1";
        #endregion 
        #region 查找BOQ的SQL    WBSLINE_BY_BOQNO
        const String WBSLINE_BY_BOQNO = @"SELECT  ProjectNo
		                                            ,BidNo
		                                            ,WbsNo
		                                            ,WbsLineNo
		                                            ,WbsLineCode
		                                            ,WbsSysCode
		                                            ,WbsLineName
		                                            ,ParentCode
		                                            ,Amount
		                                            ,AmountEx
		                                            ,Currency
		                                            ,CurrencyCode
		                                            ,ExchangeRate
		                                            ,Edit
		                                            ,Locked
		                                            ,StatId
                                                FROM [ERP_BoQ].[dbo].[gl_cntrct_wbsline] WHERE WbsNo=@WbsNo AND EDIT=1";
        #endregion
        #region 查找BOQ的SQL    RELATION_BY_BOQNO
        const String RELATION_BY_BOQNO = @"SELECT Id
                                                    ,ProjectNo
                                                    ,BidNo
                                                    ,WbsNo
                                                    ,BoQNo
                                                    ,ItemNo
                                                    ,ItemName
                                                    ,WBSLineNo
                                                    ,WbsSysCode
                                                    ,Currency
                                                    ,CurrencyCode
                                                    ,ExchangeRate
                                                    ,Uom
                                                    ,Qty
                                                    ,Amount
                                                    ,CtrctQty
                                                    ,CtrctPrjPrice
                                                    ,CtrctAmount
                                                    ,LatestQty
                                                    ,LatestPrice
                                                    ,LatestAmount
                                                    ,ChangeQty
                                                    ,ChangePrice
                                                    ,ChangeAmount
                                                    ,Description
                                                    ,CategoryNo
                                                    ,isImportant
                                                    ,IsTax
                                                    ,IItemCoe
                                                    ,EndingComputeQty
                                                    ,EndingComputeAmount
                                                    ,StatId
                                                FROM [ERP_BoQ].[dbo].[v_gl_cntrct_wbsline_boi] WHERE WbsNo=@WbsNo AND StatId=1";
        #endregion
        /// <summary>
        /// 获取Boq
        /// </summary>
        /// <param name="ProjectNo">项目No</param>
        /// <returns></returns>
        public WBSBoq GetByProjectNo(string ProjectNo)
        {
            WBSBoq WBSResult= HdDbCmdManager.GetInstance().QueryForFirstRow<WBSBoq>(WBS_BY_PROJECTNO, System.Data.CommandType.Text, new CmdParameter[] {
                                                new CmdParameter("@ProjectNo",ProjectNo)});
            if (WBSResult != null)
            {
                string strOrderString = " ORDER BY WbsSysCode ASC";
                WBSResult.WBSlineList = HdDbCmdManager.GetInstance().QueryForList<WBSline>(WBSLINE_BY_BOQNO + strOrderString, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@WbsNo",WBSResult.WbsNo)});

                WBSResult.AllRelationList = HdDbCmdManager.GetInstance().QueryForList<WBSline_boi>(RELATION_BY_BOQNO, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@WbsNo",WBSResult.WbsNo)});
            }
            return WBSResult;
        }

        /// <summary>
        /// 获取Boq并将其压缩成字节流
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        public Byte[] GetBytesByProjectNo(string ProjectNo)
        {

            DataSet ds = HdDbCmdManager.GetInstance().QueryForDataSet(WBS_BY_PROJECTNO, System.Data.CommandType.Text, new CmdParameter[] {
                                                new CmdParameter("@ProjectNo",ProjectNo)});
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string WbsNo = ds.Tables[0].Rows[0]["WbsNo"].ToString();
                    string strOrderString = " ORDER BY WbsSysCode ASC";
                    string strOrderString01 = " ORDER BY WbsSysCode,ERP_BoQ.dbo.formatstr(IItemCoe,'-') ASC";

                    DataTable dtWbsline = HdDbCmdManager.GetInstance().QueryForDataSet(WBSLINE_BY_BOQNO + strOrderString, System.Data.CommandType.Text, new CmdParameter[] {
                                                                     new CmdParameter("@WbsNo", WbsNo)}).Tables[0].Copy();
                    dtWbsline.TableName = "change_applywbs";

                    DataTable dtAllRelation = HdDbCmdManager.GetInstance().QueryForDataSet(RELATION_BY_BOQNO + strOrderString01, System.Data.CommandType.Text, new CmdParameter[] {
                                                                 new CmdParameter("@WbsNo",WbsNo)}).Tables[0].Copy();
                    dtAllRelation.TableName = "change_applydetail";

                    ds.Tables.Add(dtWbsline);
                    ds.Tables.Add(dtAllRelation);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = ex.ToString()
                });
            }

            return Erp.CommonData.DataZip.CompressConvertToBytes(ds);
        }

        public WBSBoq Save(string ProjectNo, string BidNo, String WbsName, decimal TotalAmount
                                       , List<WBSline> AddList, List<WBSline> UpdateList, List<string> DeleteItemNoList
                                       , List<WBSline_boi> AddRelation, List<WBSline_boi> UpdateRelation, List<WBSline_boi> DeleteRelation)
        {
            WBSBoq result = new WBSBoq();
            String strWbsNo = null;
            HdDbCmdManager.GetInstance().ExecuteTran(Tran =>
            {
                strWbsNo = GetWbsNo(ProjectNo, Tran);
                if (String.IsNullOrEmpty(strWbsNo))
                {
                    strWbsNo = InsertBoq(ProjectNo, BidNo, WbsName, TotalAmount, ServiceContext.LoginName, Tran);
                }
                else {
                    UpdateBoq(BidNo, strWbsNo, WbsName, TotalAmount, ServiceContext.LoginName, Tran);
                }

                //删除关联关系
                if (DeleteRelation != null)
                {
                    DeleteRelation.ForEach(m =>
                        {
                            DeleteWBSlineBoi(m.ItemNo, m.WBSLineNo, ServiceContext.LoginName, Tran);
                        });
                }

                //删除明细
                if (DeleteItemNoList != null)
                {
                    DeleteItemNoList.ForEach(m =>
                    {
                        DeleteWBSline(m, ServiceContext.LoginName, Tran);
                    });
                }

                //修改关联关系
                if (UpdateRelation != null)
                {
                    UpdateRelation.ForEach(m =>
                    {
                        if (DeleteRelation.Find(n => n.WBSLineNo == m.WBSLineNo && n.ItemNo == m.ItemNo) == null)
                            UpdateWBSlineBoi(m, ServiceContext.LoginName, Tran);
                    });
                }

                //修改
                if (UpdateList != null)
                {
                    UpdateList.ForEach(m =>
                    {
                        UpdateWBSline(m, ServiceContext.LoginName, Tran);
                    });
                }

                //新增WBS清单项
                if (AddList != null)
                {
                    AddList.ForEach(m =>
                    {
                        result.WBSlineList.Add(InsertWBSline(strWbsNo, m, ServiceContext.LoginName, Tran));
                    });
                }

                //新增关联关系
                if (AddRelation != null)
                {
                    AddRelation.ForEach(m =>
                    {
                        m.WbsNo = strWbsNo;
                        if (String.IsNullOrEmpty(m.WBSLineNo))
                        {
                            WBSline line = result.WBSlineList.Find(n => n.WbsSysCode == m.WbsSysCode);
                            m.WBSLineNo = line != null ? line.WbsLineNo : null;
                        }
                        result.AllRelationList.Add(AddWBSlineBoi(strWbsNo, m, ServiceContext.LoginName, Tran));
                    });
                }
            });
            return result;
        }


        ///// <summary>
        ///// 快速插入关联关系-用于关联关系初始化导入
        ///// </summary>
        ///// <param name="ProjectNo"></param>
        ///// <param name="BidNo"></param>
        ///// <param name="WbsName"></param>
        ///// <param name="TotalAmount"></param>
        ///// <param name="AddList"></param>
        ///// <param name="UpdateList"></param>
        ///// <param name="DeleteItemNoList"></param>
        ///// <param name="AddRelation"></param>
        ///// <param name="UpdateRelation"></param>
        ///// <param name="DeleteRelation"></param>
        ///// <returns></returns>
        //public WBSBoq Save(string ProjectNo, string BidNo, String WbsName, decimal TotalAmount
        //                              , List<WBSline> AddList, List<WBSline> UpdateList, List<string> DeleteItemNoList
        //                              , List<WBSline_boi> AddRelation, List<WBSline_boi> UpdateRelation, List<WBSline_boi> DeleteRelation)
        //{
        //    WBSBoq result = new WBSBoq();
        //    String strWbsNo = null;
        //    strWbsNo = GetWbsNo(ProjectNo);

        //    //修改
        //    if (UpdateList != null)
        //    {
        //        UpdateList.ForEach(m =>
        //        {
        //            UpdateWBSline(m, ServiceContext.LoginName);
        //        });
        //    }

        //    //新增关联关系
        //    if (AddRelation != null)
        //    {
        //        AddRelation.ForEach(m =>
        //        {
        //            m.WbsNo = strWbsNo;
        //            if (String.IsNullOrEmpty(m.WBSLineNo))
        //            {
        //                WBSline line = result.WBSlineList.Find(n => n.WbsSysCode == m.WbsSysCode);
        //                m.WBSLineNo = line != null ? line.WbsLineNo : null;
        //            }
        //            //result.AllRelationList.Add(AddWBSlineBoi(strWbsNo, m, ServiceContext.LoginName));
        //        });

        //        //剔除类中DBField = false的属性名
        //        List<string> ingorePropetiesName = new List<string>();
        //        System.Reflection.PropertyInfo[] properties = typeof(WBSline_boi).GetProperties();

        //        foreach (System.Reflection.PropertyInfo item in properties)
        //        {
        //            var classAttribute = (DBFieldAttribute)Attribute.GetCustomAttribute(item, typeof(DBFieldAttribute));

        //            if (classAttribute != null && !classAttribute.IsField)  //剔除Dbfield = False的属性
        //            {
        //                ingorePropetiesName.Add(item.Name);
        //            }

        //            if (item.Name == "id" || item.Name == "Id" || item.Name == "ID")  //如果存在ID属性，同样剔除
        //            {
        //                ingorePropetiesName.Add(item.Name);
        //            }
        //            if (item.Name == "TypeOfDataModified")  //如果存在TypeOfDataModified属性，同样剔除
        //            {
        //                ingorePropetiesName.Add("TypeOfDataModified");
        //            }
        //        }
        //        //批量插入
        //        HdSqlCommand.GetInstance().BulkInsert<WBSline_boi>("ERP_BoQ.dbo.gl_cntrct_wbsline_boi", AddRelation, ingorePropetiesName.ToArray());


        //    }
        //    return result;
        //}

        /// <summary>
        /// 修改清单头的状态字段
        /// </summary>
        /// <param name="boq"></param>
        public void ChangeStat(WBSBoq boq)
        {
            Commit(boq.WbsNo, ServiceContext.LoginName);
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@WbsNo", boq.WbsNo));
            cmds.Add(new CmdParameter("@ApprovalBy", boq.ApprovalBy));
            cmds.Add(new CmdParameter("@ApprovalDate", boq.ApprovalDate));
            cmds.Add(new CmdParameter("@ApprovalStat", boq.ApprovalStat));
            cmds.Add(new CmdParameter("@ExecuteStat", boq.ExecuteStat));
            cmds.Add(new CmdParameter("@WfdefId", boq.WfdefId));
            cmds.Add(new CmdParameter("@RefCategory", boq.RefCategory));
            cmds.Add(new CmdParameter("@inWorkflow", boq.inWorkflow));
            cmds.Add(new CmdParameter("@OperationBy", ServiceContext.LoginName));

            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbs_ChangeBusinessStat", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
          
        }


        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="m"></param>
        public void Release(WBSBoq m)
        {
            Commit(m.WbsNo, ServiceContext.LoginName);
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@WbsNo", m.WbsNo));
            cmds.Add(new CmdParameter("@ReleaseBy", m.ReleaseBy));
            cmds.Add(new CmdParameter("@ReleaseDate", m.ReleaseDate));
            cmds.Add(new CmdParameter("@OperationBy", ServiceContext.LoginName));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbs_Release", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 撤销发布
        /// </summary>
        /// <param name="m"></param>
        public void Unfix(WBSBoq m)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@WbsNo", m.WbsNo));
            cmds.Add(new CmdParameter("@OperationBy", ServiceContext.LoginName));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbs_UnFix", CommandType.StoredProcedure, pResult.Parameters);
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
        private int Commit(String WbsNo, String OperationBy, SqlTransaction Tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@WbsNo", WbsNo));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Version", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbs_Commit", CommandType.StoredProcedure, pResult.Parameters, Tran);
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
        private String GetWbsNo(String ProjectNo, SqlTransaction tran = null)
        {
            String strWbsNo = null;
            WBSBoq boqHeader= HdDbCmdManager.GetInstance().QueryForFirstRow<WBSBoq>(WBS_BY_PROJECTNO, CommandType.Text, new CmdParameter[] {
                new CmdParameter("@ProjectNo",ProjectNo)
            }, tran);
            if (boqHeader != null)
                strWbsNo = boqHeader.WbsNo;
            return strWbsNo;
        }
        /// <summary>
        /// 插入清单
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="BoqName"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="OperationBy">操作人</param>
        private String InsertBoq(String ProjectNo, String BidNo, String WbsName, decimal TotalAmount, String OperationBy, SqlTransaction tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", ProjectNo));
            cmds.Add(new CmdParameter("@BidNo", BidNo));
            cmds.Add(new CmdParameter("@WbsName", WbsName));
            cmds.Add(new CmdParameter("@TotalAmount", TotalAmount));
            cmds.Add(new CmdParameter("@TotalAmountEx", null));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@WbsNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbs_Add", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return pResult["@WbsNo"].ToString();
        }
        /// <summary>
        /// 更新清单头
        /// </summary>
        /// <param name="BoqNo"></param>
        /// <param name="BoqName"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="Tran"></param>
        private void UpdateBoq(String BidNo, String WbsNo, String WbsName, Decimal TotalAmount, String OperationBy, SqlTransaction Tran)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@BidNo", BidNo));
            cmds.Add(new CmdParameter("@WbsNo", WbsNo));
            cmds.Add(new CmdParameter("@WbsName", WbsName));
            cmds.Add(new CmdParameter("@TotalAmount", TotalAmount));
            cmds.Add(new CmdParameter("@TotalAmountEx", null));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbs_Update", CommandType.StoredProcedure, pResult.Parameters, Tran);
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
        private WBSline InsertWBSline(String WbsNo, WBSline Boi, String OperationBy, SqlTransaction Tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", Boi.ProjectNo));
            cmds.Add(new CmdParameter("@BidNo", Boi.BidNo));
            cmds.Add(new CmdParameter("@WbsNo", WbsNo));
            cmds.Add(new CmdParameter("@WbsLineCode", Boi.WbsLineCode));
            cmds.Add(new CmdParameter("@WbsLineName", Boi.WbsLineName));
            cmds.Add(new CmdParameter("@WbsSysCode", Boi.WbsSysCode));
            cmds.Add(new CmdParameter("@ParentCode", Boi.ParentCode));
            cmds.Add(new CmdParameter("@Amount", Boi.Amount));
            cmds.Add(new CmdParameter("@AmountEx", Boi.AmountEx));
            cmds.Add(new CmdParameter("@Currency", Boi.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", Boi.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", Boi.ExchangeRate));
            cmds.Add(new CmdParameter("@DrawNo", Boi.DrawNo));
            cmds.Add(new CmdParameter("@StartStakesNo", Boi.StartStakesNo));
            cmds.Add(new CmdParameter("@EndStakesNo", Boi.EndStakesNo));
            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@WbsLineNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbsline_Add", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            Boi.WbsNo = WbsNo;
            Boi.WbsLineNo = pResult["@WbsLineNo"].ToString();
            return Boi;
        }
        /// <summary>
        /// 更新清单项
        /// </summary>
        /// <param name="Boi"></param>
        /// <param name="OperationBy">操作人</param>
        private void UpdateWBSline(WBSline Boi, String OperationBy, SqlTransaction Tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", Boi.ProjectNo));
            cmds.Add(new CmdParameter("@BidNo", Boi.BidNo));
            cmds.Add(new CmdParameter("@WbsNo", Boi.WbsNo));
            cmds.Add(new CmdParameter("@WbsLineNo", Boi.WbsLineNo));
            cmds.Add(new CmdParameter("@WbsLineCode", Boi.WbsLineCode));
            cmds.Add(new CmdParameter("@WbsLineName", Boi.WbsLineName));
            cmds.Add(new CmdParameter("@WbsSysCode", Boi.WbsSysCode));
            cmds.Add(new CmdParameter("@ParentCode", Boi.ParentCode));
            cmds.Add(new CmdParameter("@Amount", Boi.Amount));
            cmds.Add(new CmdParameter("@AmountEx", Boi.AmountEx));
            cmds.Add(new CmdParameter("@Currency", Boi.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", Boi.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", Boi.ExchangeRate));
            cmds.Add(new CmdParameter("@DrawNo", Boi.DrawNo));
            cmds.Add(new CmdParameter("@StartStakesNo", Boi.StartStakesNo));
            cmds.Add(new CmdParameter("@EndStakesNo", Boi.EndStakesNo));

            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbsline_Update", CommandType.StoredProcedure, pResult.Parameters, Tran);
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
        private void DeleteWBSline(String WbsLineNo, String OperationBy, SqlTransaction Tran = null)
        {

            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@WbsLineNo", WbsLineNo));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_Wbsline_Delete", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 插入关联关系项
        /// </summary>
        /// <param name="BoqNo"></param>
        /// <param name="Boi"></param>
        /// <param name="OperationBy">操作人</param>
        /// <returns>No信息</returns>
        private WBSline_boi AddWBSlineBoi(string WbsNo, WBSline_boi Boi, String OperationBy, SqlTransaction Tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", Boi.ProjectNo));
            cmds.Add(new CmdParameter("@BidNo", Boi.BidNo));
            cmds.Add(new CmdParameter("@WbsNo", WbsNo));
            cmds.Add(new CmdParameter("@BoQNo", Boi.BoQNo));
            cmds.Add(new CmdParameter("@ItemNo", Boi.ItemNo));
            cmds.Add(new CmdParameter("@ItemName", Boi.ItemName));
            cmds.Add(new CmdParameter("@WBSLineNo", Boi.WBSLineNo));
            cmds.Add(new CmdParameter("@WbsSysCode", Boi.WbsSysCode));
            cmds.Add(new CmdParameter("@Currency", Boi.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", Boi.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", Boi.ExchangeRate));

            cmds.Add(new CmdParameter("@Uom", Boi.Uom));
            cmds.Add(new CmdParameter("@Qty", Boi.Qty));
            cmds.Add(new CmdParameter("@Amount", Boi.Amount));

            cmds.Add(new CmdParameter("@CtrctQty", Boi.CtrctQty));
            cmds.Add(new CmdParameter("@CtrctPrjPrice", Boi.CtrctPrjPrice));
            cmds.Add(new CmdParameter("@CtrctAmount", Boi.CtrctAmount));
            cmds.Add(new CmdParameter("@LatestQty", Boi.LatestQty));
            cmds.Add(new CmdParameter("@LatestPrice", Boi.LatestPrice));
            cmds.Add(new CmdParameter("@LatestAmount", Boi.LatestAmount));
            cmds.Add(new CmdParameter("@ChangeQty", Boi.ChangeQty));
            cmds.Add(new CmdParameter("@ChangePrice", Boi.ChangePrice));
            cmds.Add(new CmdParameter("@ChangeAmount", Boi.ChangeAmount));
            cmds.Add(new CmdParameter("@Description", Boi.Description));
            cmds.Add(new CmdParameter("@CategoryNo", Boi.CategoryNo));
            cmds.Add(new CmdParameter("@isCntrcItem", Boi.isCntrcItem));
            cmds.Add(new CmdParameter("@isImportant", Boi.isImportant));
            cmds.Add(new CmdParameter("@IsTax", Boi.IsTax));
            cmds.Add(new CmdParameter("@EndingComputeQty", Boi.EndingComputeQty));
            cmds.Add(new CmdParameter("@EndingComputeAmount", Boi.EndingComputeAmount));

            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_WbsLine_Boi_Add", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            Boi.WbsNo = WbsNo;
            return Boi;
        }
        /// <summary>
        /// 更新关联关系项
        /// </summary>
        /// <param name="Boi"></param>
        /// <param name="OperationBy">操作人</param>
        private void UpdateWBSlineBoi(WBSline_boi Boi, String OperationBy, SqlTransaction Tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", Boi.ProjectNo));
            cmds.Add(new CmdParameter("@BidNo", Boi.BidNo));
            cmds.Add(new CmdParameter("@WbsNo", Boi.WbsNo));
            cmds.Add(new CmdParameter("@BoQNo", Boi.BoQNo));
            cmds.Add(new CmdParameter("@ItemNo", Boi.ItemNo));
            cmds.Add(new CmdParameter("@WBSLineNo", Boi.WBSLineNo));
            cmds.Add(new CmdParameter("@WbsSysCode", Boi.WbsSysCode));
            cmds.Add(new CmdParameter("@Currency", Boi.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", Boi.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", Boi.ExchangeRate));

            cmds.Add(new CmdParameter("@Uom", Boi.Uom));
            cmds.Add(new CmdParameter("@Qty", Boi.Qty));
            cmds.Add(new CmdParameter("@Amount", Boi.Amount));

            cmds.Add(new CmdParameter("@CtrctQty", Boi.CtrctQty));
            cmds.Add(new CmdParameter("@CtrctPrjPrice", Boi.CtrctPrjPrice));
            cmds.Add(new CmdParameter("@CtrctAmount", Boi.CtrctAmount));
            cmds.Add(new CmdParameter("@LatestQty", Boi.LatestQty));
            cmds.Add(new CmdParameter("@LatestPrice", Boi.LatestPrice));
            cmds.Add(new CmdParameter("@LatestAmount", Boi.LatestAmount));
            cmds.Add(new CmdParameter("@ChangeQty", Boi.ChangeQty));
            cmds.Add(new CmdParameter("@ChangePrice", Boi.ChangePrice));
            cmds.Add(new CmdParameter("@ChangeAmount", Boi.ChangeAmount));
            cmds.Add(new CmdParameter("@Description", Boi.Description));
            cmds.Add(new CmdParameter("@CategoryNo", Boi.CategoryNo));
            cmds.Add(new CmdParameter("@isCntrcItem", Boi.isCntrcItem));
            cmds.Add(new CmdParameter("@isImportant", Boi.isImportant));
            cmds.Add(new CmdParameter("@IsTax", Boi.IsTax));
            cmds.Add(new CmdParameter("@EndingComputeQty", Boi.EndingComputeQty));
            cmds.Add(new CmdParameter("@EndingComputeAmount", Boi.EndingComputeAmount));

            cmds.Add(new CmdParameter("@OperationBy", OperationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_WbsLine_Boi_Update", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        /// <summary>
        /// 删除关联关系项
        /// </summary>
        /// <param name="BoiNo">关联关系项No</param>
        /// <param name="OperationBy">操作人</param>
        private void DeleteWBSlineBoi(string ItemNo, String WbsLineNo, String OperationBy, SqlTransaction Tran = null)
        {

            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ItemNo", ItemNo));
            cmds.Add(new CmdParameter("@WbsLineNo", WbsLineNo));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            HdDbCmdManager.GetInstance().Execute("ERP_BoQ.dbo.Gl_Cntrct_WbsLine_Boi_Delete", CommandType.StoredProcedure, pResult.Parameters, Tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        public void ImportWBS(String WbsNo,List<WBSline> ImportList)
        {
            HdDbCmdManager.GetInstance().ExecuteTran(Tran => {
                ImportList.ForEach(m => { InsertWBSline(WbsNo, m, ServiceContext.LoginName, Tran); }); });
            
        }
        #endregion

    }
}
