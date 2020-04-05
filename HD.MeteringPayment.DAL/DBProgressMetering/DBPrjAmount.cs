using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using Erp.CommonData.Interface;
using System.Transactions;
using HD.MeteringPayment.Domain.EnumEntity;

namespace HD.MeteringPayment.DAL.DBProgressMetering
{
    public class DBPrjAmount : IPrjAmount, ICompleteApproval
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        #region 查询语句 WBSLISTQUERY
        private const String WBSLISTQUERY = @"SELECT Id
                                                        ,ProjectNo
                                                        ,BidNo
                                                        ,WbsNo
                                                        ,WbsLineNo
                                                        ,WbsLineCode
                                                        ,WbsLineName
                                                        ,WbsSysCode
                                                        ,PrjamountNo
                                                        ,ParentCode
                                                        ,MidCertifiNum
                                                        ,Currency
                                                        ,CurrencyCode
                                                        ,ExchangeRate
                                                        ,StartingApplyAmount
                                                        ,ApplyAmount
                                                        ,PrjApplyAmount
                                                        ,EndingApplyAmount
                                                        ,StartingSupervisionQty
                                                        ,SupervisionQty
                                                        ,EndingSupervisionQty
                                                        ,StartingSupervisionAmount
                                                        ,SupervisionAmount
                                                        ,EndingSupervisionAmount
                                                        ,StartingOwnerQty
                                                        ,OwnerQty
                                                        ,EndingOwnerQty
                                                        ,StartingOwnerAmount
                                                        ,OwnerAmount
                                                        ,EndingOwnerAmount
                                                        ,StartingApplyAmountEx
                                                        ,ApplyAmountEx
                                                        ,EndingApplyAmountEx
                                                        ,StartingSupervisionAmountEx
                                                        ,SupervisionAmountEx
                                                        ,EndingSupervisionAmountEx
                                                        ,StartingOwnerAmountEx
                                                        ,OwnerAmountEx
                                                        ,EndingOwnerAmountEx 
                                                    FROM [ERP_Subpay].[dbo].[subp_prjamountwbs]
                                                WHERE PrjamountNo = @PrjamountNo AND Edit = 1";
        #endregion
        #region 查询语句 AllDetailQuery
        private const String ALLDETAILQUERY = @"SELECT Id
                                                        ,ProjectNo
                                                        ,BidNo
                                                        ,WbsNo
                                                        ,WbsLineNo
                                                        ,WbsLineCode
                                                        ,WbsLineName
                                                        ,WbsSysCode
                                                        ,WbsParentCode
                                                        ,PrjamountNo
                                                        ,PrjamountdetailNo
                                                        ,PrjamountdetailCode
                                                        ,PrjamountdetailName
                                                        ,ItemNo
                                                        ,ItemCode
                                                        ,IItemCoe
                                                        ,ItemName
                                                        ,ParentCode
                                                        ,WbsItemCode
                                                        ,Uom
                                                        ,CtrctQty
                                                        ,CtrctPrjPrice
                                                        ,CtrctAmount
                                                        ,LatestQty
                                                        ,LatestPrice
                                                        ,LatestAmount
                                                        ,ChangeQty
                                                        ,ChangePrice
                                                        ,ChangeAmount
                                                        ,Currency
                                                        ,CurrencyCode
                                                        ,ExchangeRate
                                                        ,StartingApplyQty
                                                        ,ApplyQty
                                                        ,EndingApplyQty
                                                        ,StartingApplyAmount
                                                        ,ApplyAmount
                                                        ,EndingApplyAmount
   					                                    ,PrjApplyQty
					                                    ,EndingPrjApplyQty 
					                                    ,PrjApplyAmount
					                                    ,EndingPrjApplyAmount
                                                        ,StartingSupervisionQty
                                                        ,SupervisionQty
                                                        ,EndingSupervisionQty
                                                        ,StartingSupervisionAmount
                                                        ,SupervisionAmount
                                                        ,EndingSupervisionAmount
                                                        ,StartingOwnerQty
                                                        ,OwnerQty
                                                        ,EndingOwnerQty
                                                        ,StartingOwnerAmount
                                                        ,OwnerAmount
                                                        ,EndingOwnerAmount
                                                        ,StartingApplyAmountEx
                                                        ,ApplyAmountEx
                                                        ,EndingApplyAmountEx
                                                        ,StartingSupervisionAmountEx
                                                        ,SupervisionAmountEx
                                                        ,EndingSupervisionAmountEx
                                                        ,StartingOwnerAmountEx
                                                        ,OwnerAmountEx
                                                        ,EndingOwnerAmountEx 
                                                    FROM [ERP_Subpay].[dbo].[subp_prjamountdetail] 
                                                    WHERE PrjamountNo = @PrjamountNo AND Edit = 1 order by ERP_BoQ.dbo.formatstr(IItemCoe,'-') asc";
        #endregion 查询语句
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<PrjAmount> GetList(string whereQuery)
        {
            return hdDbCmdManager.QueryForList<PrjAmount>(@"SELECT * FROM [ERP_Subpay].[dbo].[v_subp_prjamount]  " + whereQuery, System.Data.CommandType.Text, null);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="PrjamountNo"></param>
        /// <returns></returns>
        public PrjAmount Get(string PrjamountNo)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", PrjamountNo));
            PrjAmount result = hdDbCmdManager.QueryForFirstRow<PrjAmount>(@"SELECT * 
                                                                            FROM [ERP_Subpay].[dbo].[v_subp_prjamount] 
                                                                            WHERE PrjamountNo = @PrjamountNo AND Edit = 1", System.Data.CommandType.Text, cmds.ToArray());
            if (result != null)
            {
                List<PrjAmountDetail> wbsList = hdDbCmdManager.QueryForList<PrjAmountDetail>(WBSLISTQUERY, System.Data.CommandType.Text, cmds.ToArray());

                List<PrjAmountDetail> AllDetail = hdDbCmdManager.QueryForList<PrjAmountDetail>(ALLDETAILQUERY, System.Data.CommandType.Text, cmds.ToArray());

                //建立树形结构
                if (wbsList != null && wbsList.Count > 0)
                {
                    Dictionary<string, PrjAmountDetail> dicAllDetail = new Dictionary<string, PrjAmountDetail>();
                    if (AllDetail != null)
                    {
                        AllDetail.ForEach(m =>
                        {
                            m.WbsSysCode = null;
                            if (!dicAllDetail.ContainsKey(m.PrjamountdetailNo))
                            {
                                dicAllDetail.Add(m.PrjamountdetailNo, m);
                            }
                        });
                    }
                    wbsList.ForEach(m =>
                    {
                        m.WbsParentCode = m.ParentCode;

                        //List<PrjAmountDetail> patchResult = AllDetail.FindAll(n => n.WbsLineNo == m.WbsLineNo);
                        List<PrjAmountDetail> patchResult = new List<PrjAmountDetail>();
                        int index = 0;
                        foreach (PrjAmountDetail detail in dicAllDetail.Values)
                        {
                            if (detail.WbsLineNo == m.WbsLineNo)
                            {
                                detail.WbsSysCode = m.WbsSysCode + (index + 1).ToString("d3");

                                detail.WbsParentCode = m.WbsSysCode;
                                patchResult.Add(detail);
                                index++;
                            }
                        }
                    });

                    result.LstAmountDetail = wbsList;
                    result.LstAmountDetail.AddRange(AllDetail);
                }
                else
                {
                    result.LstAmountDetail = AllDetail;
                } 
                result.LstAmountDetail = result.LstAmountDetail.OrderBy(x => x.WbsSysCode).ToList();
                result.LstAmountOther = hdDbCmdManager.QueryForList<PrjAmountOther>(@"SELECT * 
                                                                                      FROM [ERP_Subpay].[dbo].[subp_prjamountother] 
                                                                                      WHERE PrjAmountNo = @PrjamountNo AND Edit = 1", System.Data.CommandType.Text, cmds.ToArray());
            }
            return result;
        }


        /// <summary>
        /// 获得最大序号
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        public int GetMaxPeriod(string ProjectNo)
        {
            PrjAmount result = hdDbCmdManager.QueryForFirstRow<PrjAmount>(String.Format(@"SELECT * 
                                                                                            FROM [ERP_Subpay].[dbo].[v_subp_prjamount]
                                                                                           WHERE ProjectNo = '{0}' AND Edit = 1
                                                                                           ORDER BY Periods DESC", ProjectNo), System.Data.CommandType.Text, null);
            if (result != null)
            {
                return result.Periods;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获得最大序号
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        public int GetMaxPeriod01(string ProjectNo)
        {
            PrjAmount result = hdDbCmdManager.QueryForFirstRow<PrjAmount>(String.Format(@"SELECT * 
                                                                                            FROM [ERP_Subpay].[dbo].[v_subp_prjamount]
                                                                                           WHERE ProjectNo = '{0}' AND Edit = 1 and ISNULL(PeriodsName,'')<>''
                                                                                           ORDER BY PeriodsName DESC", ProjectNo), System.Data.CommandType.Text, null);
            if (result != null && !string.IsNullOrEmpty(result.PeriodsName))
                return int.Parse(result.PeriodsName);
            else return 0;
        }

        /// <summary>
        /// 新增
        /// </summary>
        public PrjAmount Add(PrjAmount entity, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                //WBSBoq wbsInfo = GetWbsInfo(entity.ProjectNo, tran);
                //if (wbsInfo != null)
                //{
                //    entity.WbsNo = wbsInfo.WbsNo;
                //}
                //else
                //{
                //    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                //    {
                //        ErrorMessage = "找不到该项目的Wbs清单，请编辑Wbs清单后编写施工计量"
                //    });
                //}
                if (String.IsNullOrEmpty(entity.WbsNo))
                {
                    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                    {
                        ErrorMessage = "找不到该项目的Wbs清单"
                    });
                }

                InnerAdd(tran, entity, operationBy);
            });
            return entity;
        }

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="operationBy"></param>
        ///// <returns></returns>
        //public void Update(PrjAmount changedAmount, List<PrjAmountDetail> changedDetail, List<PrjAmountDetail> changedWBS, List<PrjAmountOther> changedOther, string operationBy)
        //{
        //    using (TransactionScope transaction = new TransactionScope())
        //    {
        //        InnerUpdate(changedAmount, operationBy);

        //        if (changedDetail != null)
        //        {
        //            changedDetail.ForEach(m =>
        //            {
        //                InnerUpdateDetail(m, operationBy);
        //            });
        //        }

        //        if (changedOther != null)
        //        {
        //            changedOther.ForEach(m =>
        //            {
        //                InnerUpdateOtherDetail(m, operationBy);
        //            });
        //        }
        //        if (changedWBS != null)
        //        {
        //            changedWBS.ForEach(m =>
        //            {
        //                InnerUpdateWBS(changedAmount.PrjamountNo, m, operationBy);
        //            });

        //        }
        //        InnerInitRptAmount(changedAmount.PrjamountNo, operationBy);

        //        transaction.Complete();
        //    }

        //}

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        public void UpdateAll(PrjAmount changedAmount, List<PrjAmountDetail> changedDetail, List<PrjAmountDetail> changedWBS, List<PrjAmountOther> changedOther, string operationBy)
        {
            InnerUpdate(changedAmount, operationBy);
            hdDbCmdManager.ExecuteTran(tran => {
                if (changedDetail != null)
                {
                    //changedDetail.ForEach(m =>
                    //{
                    //    InnerUpdateDetail(m, operationBy);
                    //});
                    BulkUpdateDetails(changedDetail, operationBy, tran);
                }
                if (changedOther != null)
                {
                    changedOther.ForEach(m =>
                    {
                        InnerUpdateOtherDetail(m, operationBy, tran);
                    });
                }
                if (changedWBS != null)
                {
                    BulkUpdateWbs(changedAmount.PrjamountNo, changedWBS, operationBy, tran);
                }
            });
            InnerInitRptAmount(changedAmount.PrjamountNo, operationBy);
        }
        private void BulkUpdateDetails(List<PrjAmountDetail> lstDetails, string operationBy, SqlTransaction tran)
        {
            if (lstDetails == null || lstDetails.Count == 0)
                return;

            List<string> lstColumnNames = new List<string>()  //数据表列名
            {
                "PrjamountNo",
                "LatestAmount",
                "ChangeQty",
                "ChangePrice",
                "ChangeAmount",
                "Currency",
                "CurrencyCode",
                "ExchangeRate",
                "StartingApplyQty",
                "ApplyQty",
                "EndingApplyQty",
                "PrjamountdetailNo",
                "StartingApplyAmount",
                "ApplyAmount",
                "EndingApplyAmount",
                "PrjApplyQty",
                "EndingPrjApplyQty",
                "PrjApplyAmount",
                "EndingPrjApplyAmount",
                "StartingSupervisionQty",
                "SupervisionQty",
                "EndingSupervisionQty",
                "PrjamountdetailName",
                "StartingSupervisionAmount",
                "SupervisionAmount",
                "EndingSupervisionAmount",
                "StartingOwnerQty",
                "OwnerQty",
                "EndingOwnerQty",
                "StartingOwnerAmount",
                "OwnerAmount",
                "EndingOwnerAmount",
                "Uom",
                "CtrctQty",
                "StartingApplyAmountEx",
                "ApplyAmountEx",
                "EndingApplyAmountEx",
                "StartingSupervisionAmountEx",
                "SupervisionAmountEx",
                "EndingSupervisionAmountEx",
                "StartingOwnerAmountEx",
                "OwnerAmountEx",
                "CtrctPrjPrice",
                "EndingOwnerAmountEx",
                "operationBy",
                "CtrctAmount",
                "LatestQty",
                "LatestPrice",
                "ThisYearApplyQty",
                "ThisYearApplyAmount",
                "ThisYearProp"
            };
            //创建数据表
            DataTable dtStakes = Erp.CommonData.Tool.CollectionHelper.ConvertToEx<PrjAmountDetail>(lstDetails);
            //去除多余列
            for (int index = dtStakes.Columns.Count - 1; index >= 0; index--)
            {
                if (!lstColumnNames.Exists(m => m == dtStakes.Columns[index].ColumnName))
                {
                    dtStakes.Columns.RemoveAt(index);
                }
            }

            //创建SqlCommand对象
            SqlCommand cmd = tran.Connection.CreateCommand();
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;

            //获取Create临时表的SQL
            string sql = GetCreateTableSql(dtStakes, "#tmp", "ERP_Subpay.dbo.subp_prjamountdetail");
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            //

            //批量将数据插入临时表
            SqlBulkCopy bulkCopy = new SqlBulkCopy(tran.Connection, SqlBulkCopyOptions.Default, tran);
            using (bulkCopy)
            {
                bulkCopy.BatchSize = dtStakes.Rows.Count;
                #region 数据表和数据库中临时表的字段映射
                bulkCopy.ColumnMappings.Add("PrjamountNo", "PrjamountNo");
                bulkCopy.ColumnMappings.Add("LatestAmount", "LatestAmount");
                bulkCopy.ColumnMappings.Add("ChangeQty", "ChangeQty");
                bulkCopy.ColumnMappings.Add("ChangePrice", "ChangePrice");
                bulkCopy.ColumnMappings.Add("ChangeAmount", "ChangeAmount");
                bulkCopy.ColumnMappings.Add("Currency", "Currency");
                bulkCopy.ColumnMappings.Add("CurrencyCode", "CurrencyCode");
                bulkCopy.ColumnMappings.Add("ExchangeRate", "ExchangeRate");
                bulkCopy.ColumnMappings.Add("StartingApplyQty", "StartingApplyQty");
                bulkCopy.ColumnMappings.Add("ApplyQty", "ApplyQty");
                bulkCopy.ColumnMappings.Add("EndingApplyQty", "EndingApplyQty");
                bulkCopy.ColumnMappings.Add("PrjamountdetailNo", "PrjamountdetailNo");
                bulkCopy.ColumnMappings.Add("StartingApplyAmount", "StartingApplyAmount");
                bulkCopy.ColumnMappings.Add("ApplyAmount", "ApplyAmount");
                bulkCopy.ColumnMappings.Add("EndingApplyAmount", "EndingApplyAmount");
                bulkCopy.ColumnMappings.Add("PrjApplyQty", "PrjApplyQty");
                bulkCopy.ColumnMappings.Add("EndingPrjApplyQty", "EndingPrjApplyQty");
                bulkCopy.ColumnMappings.Add("PrjApplyAmount", "PrjApplyAmount");
                bulkCopy.ColumnMappings.Add("EndingPrjApplyAmount", "EndingPrjApplyAmount");
                bulkCopy.ColumnMappings.Add("StartingSupervisionQty", "StartingSupervisionQty");
                bulkCopy.ColumnMappings.Add("SupervisionQty", "SupervisionQty");
                bulkCopy.ColumnMappings.Add("EndingSupervisionQty", "EndingSupervisionQty");
                bulkCopy.ColumnMappings.Add("PrjamountdetailName", "PrjamountdetailName");
                bulkCopy.ColumnMappings.Add("StartingSupervisionAmount", "StartingSupervisionAmount");
                bulkCopy.ColumnMappings.Add("SupervisionAmount", "SupervisionAmount");
                bulkCopy.ColumnMappings.Add("EndingSupervisionAmount", "EndingSupervisionAmount");
                bulkCopy.ColumnMappings.Add("StartingOwnerQty", "StartingOwnerQty");
                bulkCopy.ColumnMappings.Add("OwnerQty", "OwnerQty");
                bulkCopy.ColumnMappings.Add("EndingOwnerQty", "EndingOwnerQty");
                bulkCopy.ColumnMappings.Add("StartingOwnerAmount", "StartingOwnerAmount");
                bulkCopy.ColumnMappings.Add("OwnerAmount", "OwnerAmount");
                bulkCopy.ColumnMappings.Add("EndingOwnerAmount", "EndingOwnerAmount");
                bulkCopy.ColumnMappings.Add("Uom", "Uom");
                bulkCopy.ColumnMappings.Add("CtrctQty", "CtrctQty");
                bulkCopy.ColumnMappings.Add("StartingApplyAmountEx", "StartingApplyAmountEx");
                bulkCopy.ColumnMappings.Add("ApplyAmountEx", "ApplyAmountEx");
                bulkCopy.ColumnMappings.Add("EndingApplyAmountEx", "EndingApplyAmountEx");
                bulkCopy.ColumnMappings.Add("StartingSupervisionAmountEx", "StartingSupervisionAmountEx");
                bulkCopy.ColumnMappings.Add("SupervisionAmountEx", "SupervisionAmountEx");
                bulkCopy.ColumnMappings.Add("EndingSupervisionAmountEx", "EndingSupervisionAmountEx");
                bulkCopy.ColumnMappings.Add("StartingOwnerAmountEx", "StartingOwnerAmountEx");
                bulkCopy.ColumnMappings.Add("OwnerAmountEx", "OwnerAmountEx");
                bulkCopy.ColumnMappings.Add("CtrctPrjPrice", "CtrctPrjPrice");
                bulkCopy.ColumnMappings.Add("EndingOwnerAmountEx", "EndingOwnerAmountEx");
                bulkCopy.ColumnMappings.Add("CtrctAmount", "CtrctAmount");
                bulkCopy.ColumnMappings.Add("LatestQty", "LatestQty");
                bulkCopy.ColumnMappings.Add("LatestPrice", "LatestPrice");
                #endregion

                bulkCopy.DestinationTableName = "#tmp";
                bulkCopy.WriteToServer(dtStakes);

            }

            //批量更新数据
            cmd.CommandText = String.Format(@"
            USE ERP_Subpay
            UPDATE A
			   SET A.PrjamountNo=b.PrjamountNo
				  ,A.PrjamountdetailNo=b.PrjamountdetailNo
				  ,A.PrjamountdetailName=b.PrjamountdetailName
				  ,A.Uom=b.Uom
				  ,A.CtrctQty=b.CtrctQty
				  ,A.CtrctPrjPrice=b.CtrctPrjPrice
				  ,A.CtrctAmount=b.CtrctAmount
				  ,A.LatestQty=b.LatestQty
				  ,A.LatestPrice=b.LatestPrice
				  ,A.LatestAmount=b.LatestAmount
				  ,A.ChangeQty=b.ChangeQty
				  ,A.ChangePrice=b.ChangePrice
				  ,A.ChangeAmount=b.ChangeAmount
				  ,A.Currency=b.Currency
				  ,A.CurrencyCode=b.CurrencyCode
				  ,A.ExchangeRate=b.ExchangeRate
				  ,A.StartingApplyQty=b.StartingApplyQty
				  ,A.ApplyQty=b.ApplyQty
				  ,A.EndingApplyQty=b.EndingApplyQty
				  ,A.StartingApplyAmount=b.StartingApplyAmount
				  ,A.ApplyAmount=b.ApplyAmount
				  ,A.EndingApplyAmount=b.EndingApplyAmount
				  ,A.PrjApplyQty=b.PrjApplyQty
				  ,A.EndingPrjApplyQty=b.EndingPrjApplyQty
				  ,A.PrjApplyAmount=b.PrjApplyAmount
				  ,A.EndingPrjApplyAmount=b.EndingPrjApplyAmount
				  ,A.StartingSupervisionQty=b.StartingSupervisionQty
				  ,A.SupervisionQty=b.SupervisionQty
				  ,A.EndingSupervisionQty=b.EndingSupervisionQty
				  ,A.StartingSupervisionAmount=b.StartingSupervisionAmount
				  ,A.SupervisionAmount=b.SupervisionAmount
				  ,A.EndingSupervisionAmount=b.EndingSupervisionAmount
				  ,A.StartingOwnerQty=b.StartingOwnerQty
				  ,A.OwnerQty=b.OwnerQty
				  ,A.EndingOwnerQty=b.EndingOwnerQty
				  ,A.StartingOwnerAmount=b.StartingOwnerAmount
				  ,A.OwnerAmount=b.OwnerAmount
				  ,A.EndingOwnerAmount=b.EndingOwnerAmount 
				 
				  ,A.StartingApplyAmountEx=b.StartingApplyAmountEx
				  ,A.ApplyAmountEx=b.ApplyAmountEx
				  ,A.EndingApplyAmountEx=b.EndingApplyAmountEx
				  ,A.StartingSupervisionAmountEx=b.StartingSupervisionAmountEx
				  ,A.SupervisionAmountEx=b.SupervisionAmountEx
				  ,A.EndingSupervisionAmountEx=b.EndingSupervisionAmountEx
				  ,A.StartingOwnerAmountEx=b.StartingOwnerAmountEx
				  ,A.OwnerAmountEx=b.OwnerAmountEx
				  ,A.EndingOwnerAmountEx=b.EndingOwnerAmountEx
			 
				  ,A.UpdatedBy = '{0}'
				  ,A.RecordDate = getdate()
				  FROM dbo.subp_prjamountdetail A
				  INNER JOIN #tmp b on a.PrjamountdetailNo=b.PrjamountdetailNo 
			 WHERE a.Locked=0

	        INSERT INTO dbo.subp_prjamountdetail
					   (ProjectNo
			    	   ,BidNo
			    	   ,WbsNo
			    	   ,WbsLineNo
			    	   ,WbsLineCode
			    	   ,WbsLineName
			    	   ,WbsSysCode
			    	   ,WbsParentCode
			    	   ,PrjamountNo
			    	   ,PrjamountdetailNo
			    	   ,PrjamountdetailCode
			    	   ,PrjamountdetailName
			    	   ,ItemNo
			    	   ,ItemCode
			    	   ,IItemCoe
			    	   ,ItemName
			    	   ,ParentCode
			    	   ,WbsItemCode
			    	   ,Uom
			    	   ,CtrctQty
			    	   ,CtrctPrjPrice
			    	   ,CtrctAmount
			    	   ,LatestQty
			    	   ,LatestPrice
			    	   ,LatestAmount
			    	   ,ChangeQty
			    	   ,ChangePrice
			    	   ,ChangeAmount
			    	   ,Currency
			    	   ,CurrencyCode
			    	   ,ExchangeRate
			    	   ,StartingApplyQty
			    	   ,ApplyQty
			    	   ,EndingApplyQty
			    	   ,StartingApplyAmount
			    	   ,ApplyAmount
			    	   ,EndingApplyAmount
			    	   ,StartingSupervisionQty
			    	   ,SupervisionQty
			    	   ,EndingSupervisionQty
					   ,PrjApplyQty
					   ,EndingPrjApplyQty 
					   ,PrjApplyAmount
					   ,EndingPrjApplyAmount
			    	   ,StartingSupervisionAmount
			    	   ,SupervisionAmount
			    	   ,EndingSupervisionAmount
			    	   ,StartingOwnerQty
			    	   ,OwnerQty
			    	   ,EndingOwnerQty
			    	   ,StartingOwnerAmount
			    	   ,OwnerAmount
			    	   ,EndingOwnerAmount
			    	   ,StartingApplyAmountEx
			    	   ,ApplyAmountEx
			    	   ,EndingApplyAmountEx
			    	   ,StartingSupervisionAmountEx
			    	   ,SupervisionAmountEx
			    	   ,EndingSupervisionAmountEx
			    	   ,StartingOwnerAmountEx
			    	   ,OwnerAmountEx
			    	   ,EndingOwnerAmountEx 
					   ,New
					   ,CreatedBy
					   ,CreateDate)
				SELECT A.ProjectNo
			    	   ,A.BidNo
			    	   ,A.WbsNo
			    	   ,A.WbsLineNo
			    	   ,A.WbsLineCode
			    	   ,A.WbsLineName
			    	   ,A.WbsSysCode
			    	   ,A.WbsParentCode
			    	   ,A.PrjamountNo
			    	   ,A.PrjamountdetailNo
			    	   ,A.PrjamountdetailCode
			    	   ,A.PrjamountdetailName
			    	   ,A.ItemNo
			    	   ,A.ItemCode
			    	   ,A.IItemCoe
			    	   ,A.ItemName
			    	   ,A.ParentCode
			    	   ,A.WbsItemCode
			    	   ,A.Uom
			    	   ,A.CtrctQty
			    	   ,A.CtrctPrjPrice
			    	   ,A.CtrctAmount
			    	   ,A.LatestQty
			    	   ,A.LatestPrice
			    	   ,A.LatestAmount
			    	   ,A.ChangeQty
			    	   ,A.ChangePrice
			    	   ,A.ChangeAmount
			    	   ,A.Currency
			    	   ,A.CurrencyCode
			    	   ,A.ExchangeRate
			    	   ,A.StartingApplyQty
			    	   ,A.ApplyQty
			    	   ,A.EndingApplyQty
			    	   ,A.StartingApplyAmount
			    	   ,A.ApplyAmount
			    	   ,A.EndingApplyAmount
					   ,A.PrjApplyQty
					   ,A.EndingPrjApplyQty 
					   ,A.PrjApplyAmount
					   ,A.EndingPrjApplyAmount
			    	   ,A.StartingSupervisionQty
			    	   ,A.SupervisionQty
			    	   ,A.EndingSupervisionQty
			    	   ,A.StartingSupervisionAmount
			    	   ,A.SupervisionAmount
			    	   ,A.EndingSupervisionAmount
			    	   ,A.StartingOwnerQty
			    	   ,A.OwnerQty
			    	   ,A.EndingOwnerQty
			    	   ,A.StartingOwnerAmount
			    	   ,A.OwnerAmount
			    	   ,A.EndingOwnerAmount
			    	   ,A.StartingApplyAmountEx
			    	   ,A.ApplyAmountEx
			    	   ,A.EndingApplyAmountEx
			    	   ,A.StartingSupervisionAmountEx
			    	   ,A.SupervisionAmountEx
			    	   ,A.EndingSupervisionAmountEx
			    	   ,A.StartingOwnerAmountEx
			    	   ,A.OwnerAmountEx
			    	   ,A.EndingOwnerAmountEx 
					  
					   ,0
					   ,A.CreatedBy
					   ,A.CreateDate
				  FROM dbo.subp_prjamountdetail A
				  INNER JOIN #tmp B on A.PrjamountdetailNo=B.PrjamountdetailNo
				 WHERE A.VersionValidity=1 and A.Locked=1 and A.Edit=1 and NOT EXISTS(SELECT 1 FROM dbo.subp_prjamountdetail S WHERE S.PrjamountdetailNo=A.PrjamountdetailNo AND S.PrjAmountNo=A.PrjAmountNo AND S.Locked=0)
			UPDATE A
			   SET A.Edit = 0
				  ,A.UpdatedBy = '{0}'
				  ,A.RecordDate = GETDATE()
				  FROM dbo.subp_prjamountdetail A
				 INNER JOIN #tmp b on a.PrjamountdetailNo=b.PrjamountdetailNo
			 WHERE A.VersionValidity=1
			UPDATE A
			   SET A.StartingApplyQty=b.StartingApplyQty
				  ,A.ApplyQty=b.ApplyQty
				  ,A.EndingApplyQty=b.EndingApplyQty
				  ,A.StartingApplyAmount=b.StartingApplyAmount
				  ,A.ApplyAmount=b.ApplyAmount
				  ,A.EndingApplyAmount=b.EndingApplyAmount
				  ,A.PrjApplyQty=b.PrjApplyQty
				  ,A.EndingPrjApplyQty=b.EndingPrjApplyQty
				  ,A.PrjApplyAmount=b.PrjApplyAmount
				  ,A.EndingPrjApplyAmount=b.EndingPrjApplyAmount
				  ,A.StartingSupervisionQty=b.StartingSupervisionQty
				  ,A.SupervisionQty=b.SupervisionQty
				  ,A.EndingSupervisionQty=b.EndingSupervisionQty
				  ,A.StartingSupervisionAmount=b.StartingSupervisionAmount
				  ,A.SupervisionAmount=b.SupervisionAmount
				  ,A.EndingSupervisionAmount=b.EndingSupervisionAmount
				  ,A.StartingOwnerQty=b.StartingOwnerQty
				  ,A.OwnerQty=b.OwnerQty
				  ,A.EndingOwnerQty=b.EndingOwnerQty
				  ,A.StartingOwnerAmount=b.StartingOwnerAmount
				  ,A.OwnerAmount=b.OwnerAmount
				  ,A.EndingOwnerAmount=b.EndingOwnerAmount  
				 
				  ,A.StartingApplyAmountEx=b.StartingApplyAmountEx
				  ,A.ApplyAmountEx=b.ApplyAmountEx
				  ,A.EndingApplyAmountEx=b.EndingApplyAmountEx
				  ,A.StartingSupervisionAmountEx=b.StartingSupervisionAmountEx
				  ,A.SupervisionAmountEx=b.SupervisionAmountEx
				  ,A.EndingSupervisionAmountEx=b.EndingSupervisionAmountEx
				  ,A.StartingOwnerAmountEx=b.StartingOwnerAmountEx
				  ,A.OwnerAmountEx=b.OwnerAmountEx
				  ,A.EndingOwnerAmountEx=b.EndingOwnerAmountEx
				 
				  ,A.UpdatedBy = '{0}'
				  ,A.RecordDate = getdate()
				  FROM dbo.subp_prjamountdetail A
				   INNER JOIN #tmp b on a.PrjamountdetailNo=b.PrjamountdetailNo
			     WHERE A.Locked=0
                DROP TABLE #tmp", operationBy);
            cmd.CommandTimeout = 660;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 批量更新Detail(WbsBoi)
        /// </summary>
        /// <param name="lstDetails"></param>
        /// <param name="operationBy"></param>
        /// <param name="tran"></param>
        private void BulkUpdateWbs(String prjAmountNo, List<PrjAmountDetail> lstDetails, string operationBy, SqlTransaction tran)
        {
            if (lstDetails == null || lstDetails.Count == 0)
                return;

            List<string> lstColumnNames = new List<string>()  //数据表列名
            {
                "PrjamountNo",
                "WbsLineNo",
                "StartingApplyAmount",
                "ApplyAmount",
                "EndingApplyAmount",
                "PrjApplyAmount",
                "StartingSupervisionAmount",
                "SupervisionAmount",
                "EndingSupervisionAmount",
                "StartingOwnerAmount",
                "OwnerAmount",
                "EndingOwnerAmount"
            };
            //创建数据表
            DataTable dtStakes = Erp.CommonData.Tool.CollectionHelper.ConvertToEx<PrjAmountDetail>(lstDetails);
            //去除多余列
            for (int index = dtStakes.Columns.Count - 1; index >= 0; index--)
            {
                if (!lstColumnNames.Exists(m => m == dtStakes.Columns[index].ColumnName))
                {
                    dtStakes.Columns.RemoveAt(index);
                }
            }

            //创建SqlCommand对象
            SqlCommand cmd = tran.Connection.CreateCommand();
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;

            //获取Create临时表的SQL
            string sql = GetCreateTableSql(dtStakes, "#tmp", "ERP_Subpay.dbo.subp_prjamountwbs");
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            //

            //批量将数据插入临时表
            SqlBulkCopy bulkCopy = new SqlBulkCopy(tran.Connection, SqlBulkCopyOptions.Default, tran);
            using (bulkCopy)
            {
                bulkCopy.BatchSize = dtStakes.Rows.Count;
                #region 数据表和数据库中临时表的字段映射
                bulkCopy.ColumnMappings.Add("PrjamountNo", "PrjamountNo");
                bulkCopy.ColumnMappings.Add("WbsLineNo", "WbsLineNo");
                bulkCopy.ColumnMappings.Add("StartingApplyAmount", "StartingApplyAmount");
                bulkCopy.ColumnMappings.Add("ApplyAmount", "ApplyAmount");
                bulkCopy.ColumnMappings.Add("EndingApplyAmount", "EndingApplyAmount");
                bulkCopy.ColumnMappings.Add("PrjApplyAmount", "PrjApplyAmount");
                bulkCopy.ColumnMappings.Add("StartingSupervisionAmount", "StartingSupervisionAmount");
                bulkCopy.ColumnMappings.Add("SupervisionAmount", "SupervisionAmount");
                bulkCopy.ColumnMappings.Add("EndingSupervisionAmount", "EndingSupervisionAmount");
                bulkCopy.ColumnMappings.Add("StartingOwnerAmount", "StartingOwnerAmount");
                bulkCopy.ColumnMappings.Add("OwnerAmount", "OwnerAmount");
                bulkCopy.ColumnMappings.Add("EndingOwnerAmount", "EndingOwnerAmount");
                #endregion

                bulkCopy.DestinationTableName = "#tmp";
                bulkCopy.WriteToServer(dtStakes);

            }

            //批量更新数据
            cmd.CommandText = String.Format(@"
            USE ERP_Subpay
            UPDATE A
			  SET  A.StartingApplyAmount=b.StartingApplyAmount
				  ,A.ApplyAmount=b.ApplyAmount
				  ,A.EndingApplyAmount=b.EndingApplyAmount 
				  ,A.PrjApplyAmount=b.PrjApplyAmount
				  ,A.StartingSupervisionAmount=b.StartingSupervisionAmount
				  ,A.SupervisionAmount=b.SupervisionAmount
				  ,A.EndingSupervisionAmount=b.EndingSupervisionAmount
				  ,A.StartingOwnerAmount=b.StartingOwnerAmount
				  ,A.OwnerAmount=b.OwnerAmount
				  ,A.EndingOwnerAmount=b.EndingOwnerAmount   
				  ,A.UpdatedBy = '{0}'
				  ,A.RecordDate = getdate()
				  FROM dbo.subp_prjamountwbs A
				  INNER JOIN #tmp b on a.WbsLineNo=b.WbsLineNo 
			 WHERE a.Locked=0 and A.PrjAmountNo='{1}'

	       		INSERT INTO dbo.subp_prjamountwbs
					   (PrjamountNo
			    	   ,PrjApplyAmount
			    	   ,ProjectNo
			    	   ,BidNo
			    	   ,WbsNo
			    	   ,WbsLineNo
			    	   ,WbsLineCode
			    	   ,WbsLineName
			    	   ,WbsSysCode
			    	   ,ParentCode
			    	   ,IsChange
			    	   ,SubProject
			    	   ,Position
			    	   ,StaStopNum
			    	   ,DrawingCode
			    	   ,MidCertifiNum
			    	   ,Amount
			    	   ,AmountEx
			    	   ,Currency
			    	   ,CurrencyCode
			    	   ,ExchangeRate
					   ,StartingCmptAmount
					   ,CmptAmount
					   ,EndingCmptAmount
					   ,StartingCmptAmountEx
					   ,CmptAmountEx
					   ,EndingCmptAmountEx
			    	   ,StartingApplyAmount
			    	   ,ApplyAmount
			    	   ,EndingApplyAmount
			    	   ,StartingSupervisionQty
			    	   ,SupervisionQty
			    	   ,EndingSupervisionQty
			    	   ,StartingSupervisionAmount
			    	   ,SupervisionAmount
			    	   ,EndingSupervisionAmount
			    	   ,StartingOwnerQty
			    	   ,OwnerQty
			    	   ,EndingOwnerQty
			    	   ,StartingOwnerAmount
			    	   ,OwnerAmount
			    	   ,EndingOwnerAmount
			    	   ,StartingApplyAmountEx
			    	   ,ApplyAmountEx
			    	   ,EndingApplyAmountEx
			    	   ,StartingSupervisionAmountEx
			    	   ,SupervisionAmountEx
			    	   ,EndingSupervisionAmountEx
			    	   ,StartingOwnerAmountEx
			    	   ,OwnerAmountEx
			    	   ,EndingOwnerAmountEx 
					   ,New
					   ,CreatedBy
					   ,CreateDate)
				SELECT A.PrjamountNo
			    	   ,A.PrjApplyAmount
			    	   ,A.ProjectNo
			    	   ,A.BidNo
			    	   ,A.WbsNo
			    	   ,A.WbsLineNo
			    	   ,A.WbsLineCode
			    	   ,A.WbsLineName
			    	   ,A.WbsSysCode
			    	   ,A.ParentCode
			    	   ,A.IsChange
			    	   ,A.SubProject
			    	   ,A.Position
			    	   ,A.StaStopNum
			    	   ,A.DrawingCode
			    	   ,A.MidCertifiNum
			    	   ,A.Amount
			    	   ,A.AmountEx
			    	   ,A.Currency
			    	   ,A.CurrencyCode
			    	   ,A.ExchangeRate
					   ,A.StartingCmptAmount
					   ,A.CmptAmount
					   ,A.EndingCmptAmount
					   ,A.StartingCmptAmountEx
					   ,A.CmptAmountEx
					   ,A.EndingCmptAmountEx
			    	   ,A.StartingApplyAmount
			    	   ,A.ApplyAmount
			    	   ,A.EndingApplyAmount
			    	   ,A.StartingSupervisionQty
			    	   ,A.SupervisionQty
			    	   ,A.EndingSupervisionQty
			    	   ,A.StartingSupervisionAmount
			    	   ,A.SupervisionAmount
			    	   ,A.EndingSupervisionAmount
			    	   ,A.StartingOwnerQty
			    	   ,A.OwnerQty
			    	   ,A.EndingOwnerQty
			    	   ,A.StartingOwnerAmount
			    	   ,A.OwnerAmount
			    	   ,A.EndingOwnerAmount
			    	   ,A.StartingApplyAmountEx
			    	   ,A.ApplyAmountEx
			    	   ,A.EndingApplyAmountEx
			    	   ,A.StartingSupervisionAmountEx
			    	   ,A.SupervisionAmountEx
			    	   ,A.EndingSupervisionAmountEx
			    	   ,A.StartingOwnerAmountEx
			    	   ,A.OwnerAmountEx
			    	   ,A.EndingOwnerAmountEx 
					   ,0
					   ,A.CreatedBy
					   ,A.CreateDate
				  FROM dbo.subp_prjamountwbs A 
				 WHERE A.VersionValidity=1 and A.Locked=1  AND EXISTS(SELECT 1 FROM #tmp B WHERE B.WbsLineNo=A.WbsLineNo)
                        and A.Edit=1  and A.PrjAmountNo='{1}'  and NOT EXISTS(SELECT 1 FROM dbo.subp_prjamountwbs S WHERE S.WbsLineNo=A.WbsLineNo  and S.PrjAmountNo='{1}' AND S.Locked=0)
			UPDATE A
			   SET A.Edit = 0
				  ,A.UpdatedBy = '{0}'
				  ,A.RecordDate = GETDATE()
				  FROM dbo.subp_prjamountwbs A 
			 WHERE A.VersionValidity=1  and A.PrjAmountNo='{1}'   AND EXISTS(SELECT 1 FROM #tmp B WHERE B.WbsLineNo=A.WbsLineNo)
			UPDATE A
			   SET  A.StartingApplyAmount=b.StartingApplyAmount
				  ,A.ApplyAmount=b.ApplyAmount
				  ,A.EndingApplyAmount=b.EndingApplyAmount 
				  ,A.PrjApplyAmount=b.PrjApplyAmount
				  ,A.StartingSupervisionAmount=b.StartingSupervisionAmount
				  ,A.SupervisionAmount=b.SupervisionAmount
				  ,A.EndingSupervisionAmount=b.EndingSupervisionAmount
				  ,A.StartingOwnerAmount=b.StartingOwnerAmount
				  ,A.OwnerAmount=b.OwnerAmount
				  ,A.EndingOwnerAmount=b.EndingOwnerAmount   
				  ,A.UpdatedBy = '{0}'
				  ,A.RecordDate = getdate()
				  FROM dbo.subp_prjamountwbs A
				   INNER JOIN #tmp b on a.WbsLineNo=b.WbsLineNo
			     WHERE A.Locked=0  and A.PrjAmountNo='{1}' 
                DROP TABLE #tmp", operationBy, prjAmountNo);
            cmd.CommandTimeout = 660;
            cmd.ExecuteNonQuery();
        }

        private string GetCreateTableSql(DataTable table, string tmptableName, string tableName, Dictionary<string, string> dicSpecialColumn = null)
        {
            var colList = new List<string>();
            var fieldNames = new List<string>();
            foreach (DataColumn col in table.Columns)
            {
                if (dicSpecialColumn != null && dicSpecialColumn.ContainsKey(col.ColumnName))
                {
                    fieldNames.Add(dicSpecialColumn[col.ColumnName]);
                }
                else
                {
                    fieldNames.Add(col.ColumnName);
                }
            }

            var sql = string.Format(@"USE ERP_Subpay
                                      -- if object_id('{0}') is not null 
                                      --   begin truncate table {0} drop table {0} end 
                                       select top 0 {1} into {0} from {2};",
                tmptableName, string.Join(",", fieldNames), tableName);

            return sql;
        }
        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="operationBy"></param>
        ///// <returns></returns>
        //public void UpdateAll(PrjAmount changedAmount, List<PrjAmountDetail> changedDetail, List<PrjAmountDetail> changedWBS, List<PrjAmountOther> changedOther, string operationBy)
        //{
        //    InnerUpdate(changedAmount, operationBy);

        //    if (changedDetail != null)
        //    {
        //        changedDetail.ForEach(m =>
        //        {
        //            InnerUpdateDetail(m, operationBy);
        //        });
        //    }

        //    if (changedOther != null)
        //    {
        //        changedOther.ForEach(m =>
        //        {
        //            InnerUpdateOtherDetail(m, operationBy);
        //        });
        //    }
        //    if (changedWBS != null)
        //    {
        //        changedWBS.ForEach(m =>
        //        {
        //            InnerUpdateWBS(changedAmount.PrjamountNo, m, operationBy);
        //        });

        //    }
        //    InnerInitRptAmount(changedAmount.PrjamountNo, operationBy);
        //}

        public void updateAmountAndQty(List<PrjAmountDetail> entity, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                entity.ForEach(m =>
                {
                    if (m.isChanged)
                    {
                        InnerUpdateDetailEx(tran, m, operationBy);
                    }
                });
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        public void Delete(String prjAmountNo, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerDelete(tran, prjAmountNo, operationBy);
            });
        }

        /// <summary>
        /// 提交版本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        public int Commit(String prjAmountNo, string operationBy)
        {
            int version = -1;
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                version = InnerCommit(tran, prjAmountNo, operationBy);
            });
            return version;
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        public void Release(String prjAmountNo, string releaseBy, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerCommit(tran, prjAmountNo, operationBy);
                InnerRelease(tran, prjAmountNo, releaseBy, operationBy);
            });
        }

        /// <summary>
        /// 撤销发布
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        public void UnFix(String prjAmountNo, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerUnfix(tran, prjAmountNo, operationBy);
            });
        }

        /// <summary>
        /// 获取该项目的WBS清单信息和关联项信息
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        public WBSBoq GetWbsInfo(string ProjectNo)
        {
            return _innerGetWbsInfo(ProjectNo, null);
        }

        /// <summary>
        /// 是否有上期未发布数据_发布时判断
        /// </summary>
        /// <returns></returns
        public bool hasUnFixed(String ProjectNo, int currentPeriod)
        {
            PrjAmount result = hdDbCmdManager.QueryForFirstRow<PrjAmount>(String.Format(@"SELECT * 
                                                                                            FROM [ERP_Subpay].[dbo].[v_subp_prjamount]
                                                                                           WHERE ProjectNo = '{0}' AND Fixed = 0
                                                                                           AND Periods <{1}", ProjectNo, currentPeriod), System.Data.CommandType.Text, null);
            if (result != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否有下期未撤销发布数据-撤销发布时判断
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="currentPeriod"></param>
        /// <returns></returns>
        public bool hasFixed(String ProjectNo, int currentPeriod)
        {
            PrjAmount result = hdDbCmdManager.QueryForFirstRow<PrjAmount>(String.Format(@"SELECT * 
                                                                                            FROM [ERP_Subpay].[dbo].[v_subp_prjamount]
                                                                                           WHERE ProjectNo = '{0}' AND Fixed = 1
                                                                                           AND Periods >{1}", ProjectNo, currentPeriod), System.Data.CommandType.Text, null);
            if (result != null)
            {
                return true;
            }
            return false;
        }
        #region 内部方法
        private WBSBoq _innerGetWbsInfo(string ProjectNo, SqlTransaction tran)
        {
            WBSBoq WBSResult = new WBSBoq();
            if (tran != null)
            {
                WBSResult = HdDbCmdManager.GetInstance().QueryForFirstRow<WBSBoq>(@"SELECT * 
                                                                                      FROM [ERP_BoQ].[dbo].[gl_cntrct_wbs] 
                                                                                     WHERE ProjectNo=@ProjectNo AND EDIT=1", System.Data.CommandType.Text
                                                                                                                                  , new CmdParameter[] { new CmdParameter("@ProjectNo", ProjectNo) }, tran);

            }
            else
            {
                WBSResult = HdDbCmdManager.GetInstance().QueryForFirstRow<WBSBoq>(@"SELECT * 
                                                                                      FROM [ERP_BoQ].[dbo].[gl_cntrct_wbs] 
                                                                                     WHERE ProjectNo=@ProjectNo AND EDIT=1", System.Data.CommandType.Text
                                                                                                                                , new CmdParameter[] { new CmdParameter("@ProjectNo", ProjectNo) }, null);
            }
            return WBSResult;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="projectinfo"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private PrjAmount InnerAdd(SqlTransaction tran, PrjAmount m, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", m.ProjectNo));
            cmds.Add(new CmdParameter("@SubProject", m.SubProject));
            cmds.Add(new CmdParameter("@Position", m.Position));
            cmds.Add(new CmdParameter("@StaStopNum", m.StaStopNum));
            cmds.Add(new CmdParameter("@DrawingCode", m.DrawingCode));
            cmds.Add(new CmdParameter("@MidCertifiNum", m.MidCertifiNum));
            cmds.Add(new CmdParameter("@Remark", m.Remark));
            cmds.Add(new CmdParameter("@PrepareBy", m.PrepareBy));
            cmds.Add(new CmdParameter("@PrepareDate", m.PrepareDate));
            cmds.Add(new CmdParameter("@PrepareDeptNo", m.PrepareDeptNo));
            cmds.Add(new CmdParameter("@PrepareDeptName", m.PrepareDeptName));
            cmds.Add(new CmdParameter("@BidNo", m.BidNo));
            cmds.Add(new CmdParameter("@WbsNo", m.WbsNo));
            cmds.Add(new CmdParameter("@WbsLineNo", m.WbsLineNo));
            cmds.Add(new CmdParameter("@PrjamountCode", m.PrjamountCode));
            cmds.Add(new CmdParameter("@PrjamountName", m.PrjamountName));
            cmds.Add(new CmdParameter("@PeriodsName", m.PeriodsName));
            cmds.Add(new CmdParameter("@Periods", m.Periods));
            cmds.Add(new CmdParameter("@IsChange", m.IsChange));
            cmds.Add(new CmdParameter("@EditFlag", m.EditFlag));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@PrjamountNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmount_Add]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            m.Id = Convert.ToInt32(pResult["@Id"]);
            m.PrjamountNo = pResult["@PrjamountNo"].ToString();
            return m;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private void InnerUpdate(PrjAmount changedAmount, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", changedAmount.ProjectNo));
            cmds.Add(new CmdParameter("@IsChange", changedAmount.IsChange));
            cmds.Add(new CmdParameter("@SubProject", changedAmount.SubProject));
            cmds.Add(new CmdParameter("@Position", changedAmount.Position));
            cmds.Add(new CmdParameter("@StaStopNum", changedAmount.StaStopNum));
            cmds.Add(new CmdParameter("@DrawingCode", changedAmount.DrawingCode));
            cmds.Add(new CmdParameter("@MidCertifiNum", changedAmount.MidCertifiNum));
            cmds.Add(new CmdParameter("@BidNo", changedAmount.BidNo));
            cmds.Add(new CmdParameter("@Remark", changedAmount.Remark));
            cmds.Add(new CmdParameter("@PrepareBy", changedAmount.PrepareBy));
            cmds.Add(new CmdParameter("@WbsNo", changedAmount.WbsNo));
            cmds.Add(new CmdParameter("@EditFlag", changedAmount.EditFlag));
            cmds.Add(new CmdParameter("@PrepareDate", changedAmount.PrepareDate));
            cmds.Add(new CmdParameter("@PrepareDeptNo", changedAmount.PrepareDeptNo));
            cmds.Add(new CmdParameter("@PrepareDeptName", changedAmount.PrepareDeptName));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@WbsLineNo", changedAmount.WbsLineNo));
            cmds.Add(new CmdParameter("@PrjamountNo", changedAmount.PrjamountNo));
            cmds.Add(new CmdParameter("@PrjamountCode", changedAmount.PrjamountCode));
            cmds.Add(new CmdParameter("@PrjamountName", changedAmount.PrjamountName));
            cmds.Add(new CmdParameter("@PeriodsName", changedAmount.PeriodsName));
            cmds.Add(new CmdParameter("@Periods", changedAmount.Periods));

            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmount_Update]", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private void InnerDelete(SqlTransaction tran, String prjAmountNo, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", prjAmountNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmount_Delete]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 提交版本
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="m"></param>
        /// <param name="operationBy"></param>
        private int InnerCommit(SqlTransaction tran, String prjAmountNo, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", prjAmountNo));
            cmds.Add(new CmdParameter("@Version", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Result", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmount_Commit]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return Convert.ToInt32(pResult["@Version"]);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="m"></param>
        /// <param name="operationBy"></param>
        private void InnerRelease(SqlTransaction tran, String prjAmountNo, String releaseBy, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", prjAmountNo));
            cmds.Add(new CmdParameter("@ReleaseBy", releaseBy));
            cmds.Add(new CmdParameter("@ReleaseDate", DateTime.Now));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmount_Release]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 撤销发布
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="m"></param>
        /// <param name="operationBy"></param>
        private void InnerUnfix(SqlTransaction tran, String prjAmountNo, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", prjAmountNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmount_UnFix]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 新增施工计量明细
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="projectinfo"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private PrjAmountDetail InnerAddDetail(SqlTransaction tran, PrjAmountDetail m, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();

            cmds.Add(new CmdParameter("@ProjectNo", m.ProjectNo));
            cmds.Add(new CmdParameter("@PrjamountdetailCode", m.PrjamountdetailCode));
            cmds.Add(new CmdParameter("@PrjamountdetailName", m.PrjamountdetailName));
            cmds.Add(new CmdParameter("@ItemNo", m.ItemNo));
            cmds.Add(new CmdParameter("@ItemCode", m.ItemCode));
            cmds.Add(new CmdParameter("@IItemCoe", m.IItemCoe));
            cmds.Add(new CmdParameter("@ItemName", m.ItemName));
            cmds.Add(new CmdParameter("@ParentCode", m.ParentCode));
            cmds.Add(new CmdParameter("@WbsItemCode", m.WbsItemCode));
            cmds.Add(new CmdParameter("@Uom", m.Uom));
            cmds.Add(new CmdParameter("@CtrctQty", m.CtrctQty));
            cmds.Add(new CmdParameter("@BidNo", m.BidNo));
            cmds.Add(new CmdParameter("@CtrctPrjPrice", m.CtrctPrjPrice));
            cmds.Add(new CmdParameter("@CtrctAmount", m.CtrctAmount));
            cmds.Add(new CmdParameter("@LatestQty", m.LatestQty));
            cmds.Add(new CmdParameter("@LatestPrice", m.LatestPrice));
            cmds.Add(new CmdParameter("@LatestAmount", m.LatestAmount));
            cmds.Add(new CmdParameter("@ChangeQty", m.ChangeQty));
            cmds.Add(new CmdParameter("@ChangePrice", m.ChangePrice));
            cmds.Add(new CmdParameter("@ChangeAmount", m.ChangeAmount));
            cmds.Add(new CmdParameter("@Currency", m.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", m.CurrencyCode));
            cmds.Add(new CmdParameter("@WbsNo", m.WbsNo));
            cmds.Add(new CmdParameter("@ExchangeRate", m.ExchangeRate));
            cmds.Add(new CmdParameter("@StartingApplyQty", m.StartingApplyQty));
            cmds.Add(new CmdParameter("@ApplyQty", m.ApplyQty));
            cmds.Add(new CmdParameter("@EndingApplyQty", m.EndingApplyQty));
            cmds.Add(new CmdParameter("@StartingApplyAmount", m.StartingApplyAmount));
            cmds.Add(new CmdParameter("@ApplyAmount", m.ApplyAmount));
            cmds.Add(new CmdParameter("@EndingApplyAmount", m.EndingApplyAmount));
            cmds.Add(new CmdParameter("@StartingSupervisionQty", m.StartingSupervisionQty));
            cmds.Add(new CmdParameter("@SupervisionQty", m.SupervisionQty));
            cmds.Add(new CmdParameter("@EndingSupervisionQty", m.EndingSupervisionQty));
            cmds.Add(new CmdParameter("@WbsLineNo", m.WbsLineNo));
            cmds.Add(new CmdParameter("@StartingSupervisionAmount", m.StartingSupervisionAmount));
            cmds.Add(new CmdParameter("@SupervisionAmount", m.SupervisionAmount));
            cmds.Add(new CmdParameter("@EndingSupervisionAmount", m.EndingSupervisionAmount));
            cmds.Add(new CmdParameter("@StartingOwnerQty", m.StartingOwnerQty));
            cmds.Add(new CmdParameter("@OwnerQty", m.OwnerQty));
            cmds.Add(new CmdParameter("@EndingOwnerQty", m.EndingOwnerQty));
            cmds.Add(new CmdParameter("@StartingOwnerAmount", m.StartingOwnerAmount));
            cmds.Add(new CmdParameter("@OwnerAmount", m.OwnerAmount));
            cmds.Add(new CmdParameter("@EndingOwnerAmount", m.EndingOwnerAmount));
            cmds.Add(new CmdParameter("@StartingApplyAmountEx", m.StartingApplyAmountEx));
            cmds.Add(new CmdParameter("@WbsLineCode", m.WbsLineCode));
            cmds.Add(new CmdParameter("@ApplyAmountEx", m.ApplyAmountEx));
            cmds.Add(new CmdParameter("@EndingApplyAmountEx", m.EndingApplyAmountEx));
            cmds.Add(new CmdParameter("@StartingSupervisionAmountEx", m.StartingSupervisionAmountEx));
            cmds.Add(new CmdParameter("@SupervisionAmountEx", m.SupervisionAmountEx));
            cmds.Add(new CmdParameter("@EndingSupervisionAmountEx", m.EndingSupervisionAmountEx));
            cmds.Add(new CmdParameter("@StartingOwnerAmountEx", m.StartingOwnerAmountEx));
            cmds.Add(new CmdParameter("@OwnerAmountEx", m.OwnerAmountEx));
            cmds.Add(new CmdParameter("@EndingOwnerAmountEx", m.EndingOwnerAmountEx));
            cmds.Add(new CmdParameter("@WbsSysCode", m.WbsSysCode));
            cmds.Add(new CmdParameter("@WbsParentCode", m.WbsParentCode));
            cmds.Add(new CmdParameter("@PrjamountNo", m.PrjamountNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@WbsLineName", m.WbsLineName));
            cmds.Add(new CmdParameter("@PrjamountdetailNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmountDetail_Add]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            m.Id = Convert.ToInt32(pResult["@Id"]);
            m.PrjamountdetailNo = pResult["@PrjamountdetailNo"].ToString();
            return m;
        }
        /// <summary>
        /// 更新施工计量明细
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private PrjAmountDetail InnerUpdateDetail(PrjAmountDetail m, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", m.PrjamountNo));
            cmds.Add(new CmdParameter("@LatestPrice", m.LatestPrice));
            cmds.Add(new CmdParameter("@LatestAmount", m.LatestAmount));
            cmds.Add(new CmdParameter("@ChangeQty", m.ChangeQty));
            cmds.Add(new CmdParameter("@ChangePrice", m.ChangePrice));
            cmds.Add(new CmdParameter("@ChangeAmount", m.ChangeAmount));
            cmds.Add(new CmdParameter("@Currency", m.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", m.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", m.ExchangeRate));
            cmds.Add(new CmdParameter("@StartingApplyQty", m.StartingApplyQty));
            cmds.Add(new CmdParameter("@ApplyQty", m.ApplyQty));
            cmds.Add(new CmdParameter("@PrjamountdetailNo", m.PrjamountdetailNo));
            cmds.Add(new CmdParameter("@EndingApplyQty", m.EndingApplyQty));
            cmds.Add(new CmdParameter("@StartingApplyAmount", m.StartingApplyAmount));
            cmds.Add(new CmdParameter("@ApplyAmount", m.ApplyAmount));
            cmds.Add(new CmdParameter("@EndingApplyAmount", m.EndingApplyAmount));
            cmds.Add(new CmdParameter("@PrjApplyQty", m.PrjApplyQty));
            cmds.Add(new CmdParameter("@EndingPrjApplyQty", m.EndingPrjApplyQty));
            cmds.Add(new CmdParameter("@PrjApplyAmount", m.PrjApplyAmount));
            cmds.Add(new CmdParameter("@EndingPrjApplyAmount", m.EndingPrjApplyAmount));
            cmds.Add(new CmdParameter("@StartingSupervisionQty", m.StartingSupervisionQty));
            cmds.Add(new CmdParameter("@SupervisionQty", m.SupervisionQty));
            cmds.Add(new CmdParameter("@EndingSupervisionQty", m.EndingSupervisionQty));
            cmds.Add(new CmdParameter("@StartingSupervisionAmount", m.StartingSupervisionAmount));
            cmds.Add(new CmdParameter("@SupervisionAmount", m.SupervisionAmount));
            cmds.Add(new CmdParameter("@EndingSupervisionAmount", m.EndingSupervisionAmount));
            cmds.Add(new CmdParameter("@PrjamountdetailCode", m.PrjamountdetailCode));
            cmds.Add(new CmdParameter("@StartingOwnerQty", m.StartingOwnerQty));
            cmds.Add(new CmdParameter("@OwnerQty", m.OwnerQty));
            cmds.Add(new CmdParameter("@EndingOwnerQty", m.EndingOwnerQty));
            cmds.Add(new CmdParameter("@StartingOwnerAmount", m.StartingOwnerAmount));
            cmds.Add(new CmdParameter("@OwnerAmount", m.OwnerAmount));
            cmds.Add(new CmdParameter("@EndingOwnerAmount", m.EndingOwnerAmount));
            cmds.Add(new CmdParameter("@StartingApplyAmountEx", m.StartingApplyAmountEx));
            cmds.Add(new CmdParameter("@ApplyAmountEx", m.ApplyAmountEx));
            cmds.Add(new CmdParameter("@EndingApplyAmountEx", m.EndingApplyAmountEx));
            cmds.Add(new CmdParameter("@StartingSupervisionAmountEx", m.StartingSupervisionAmountEx));
            cmds.Add(new CmdParameter("@PrjamountdetailName", m.PrjamountdetailName));
            cmds.Add(new CmdParameter("@SupervisionAmountEx", m.SupervisionAmountEx));
            cmds.Add(new CmdParameter("@EndingSupervisionAmountEx", m.EndingSupervisionAmountEx));
            cmds.Add(new CmdParameter("@StartingOwnerAmountEx", m.StartingOwnerAmountEx));
            cmds.Add(new CmdParameter("@OwnerAmountEx", m.OwnerAmountEx));
            cmds.Add(new CmdParameter("@EndingOwnerAmountEx", m.EndingOwnerAmountEx));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Uom", m.Uom));
            cmds.Add(new CmdParameter("@CtrctQty", m.CtrctQty));
            cmds.Add(new CmdParameter("@CtrctPrjPrice", m.CtrctPrjPrice));
            cmds.Add(new CmdParameter("@CtrctAmount", m.CtrctAmount));
            cmds.Add(new CmdParameter("@LatestQty", m.LatestQty));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };

            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmountDetail_Update]", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return m;
        }

        /// <summary>
        /// 更新监理或者业主的本期完成数量和金额
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="m"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private void InnerUpdateDetailEx(SqlTransaction tran, PrjAmountDetail m, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", m.PrjamountNo));
            cmds.Add(new CmdParameter("@LatestPrice", m.LatestPrice));
            cmds.Add(new CmdParameter("@LatestAmount", m.LatestAmount));
            cmds.Add(new CmdParameter("@ChangeQty", m.ChangeQty));
            cmds.Add(new CmdParameter("@ChangePrice", m.ChangePrice));
            cmds.Add(new CmdParameter("@ChangeAmount", m.ChangeAmount));
            cmds.Add(new CmdParameter("@Currency", m.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", m.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", m.ExchangeRate));
            cmds.Add(new CmdParameter("@StartingApplyQty", m.StartingApplyQty));
            cmds.Add(new CmdParameter("@ApplyQty", m.ApplyQty));
            cmds.Add(new CmdParameter("@PrjamountdetailNo", m.PrjamountdetailNo));
            cmds.Add(new CmdParameter("@EndingApplyQty", m.EndingApplyQty));
            cmds.Add(new CmdParameter("@StartingApplyAmount", m.StartingApplyAmount));
            cmds.Add(new CmdParameter("@ApplyAmount", m.ApplyAmount));
            cmds.Add(new CmdParameter("@EndingApplyAmount", m.EndingApplyAmount));
            cmds.Add(new CmdParameter("@PrjApplyQty", m.PrjApplyQty));
            cmds.Add(new CmdParameter("@EndingPrjApplyQty", m.EndingPrjApplyQty));
            cmds.Add(new CmdParameter("@PrjApplyAmount", m.PrjApplyAmount));
            cmds.Add(new CmdParameter("@EndingPrjApplyAmount", m.EndingPrjApplyAmount));
            cmds.Add(new CmdParameter("@StartingSupervisionQty", m.StartingSupervisionQty));
            cmds.Add(new CmdParameter("@SupervisionQty", m.SupervisionQty));
            cmds.Add(new CmdParameter("@EndingSupervisionQty", m.EndingSupervisionQty));
            cmds.Add(new CmdParameter("@StartingSupervisionAmount", m.StartingSupervisionAmount));
            cmds.Add(new CmdParameter("@SupervisionAmount", m.SupervisionAmount));
            cmds.Add(new CmdParameter("@EndingSupervisionAmount", m.EndingSupervisionAmount));
            cmds.Add(new CmdParameter("@PrjamountdetailCode", m.PrjamountdetailCode));
            cmds.Add(new CmdParameter("@StartingOwnerQty", m.StartingOwnerQty));
            cmds.Add(new CmdParameter("@OwnerQty", m.OwnerQty));
            cmds.Add(new CmdParameter("@EndingOwnerQty", m.EndingOwnerQty));
            cmds.Add(new CmdParameter("@StartingOwnerAmount", m.StartingOwnerAmount));
            cmds.Add(new CmdParameter("@OwnerAmount", m.OwnerAmount));
            cmds.Add(new CmdParameter("@EndingOwnerAmount", m.EndingOwnerAmount));
            cmds.Add(new CmdParameter("@StartingApplyAmountEx", m.StartingApplyAmountEx));
            cmds.Add(new CmdParameter("@ApplyAmountEx", m.ApplyAmountEx));
            cmds.Add(new CmdParameter("@EndingApplyAmountEx", m.EndingApplyAmountEx));
            cmds.Add(new CmdParameter("@StartingSupervisionAmountEx", m.StartingSupervisionAmountEx));
            cmds.Add(new CmdParameter("@PrjamountdetailName", m.PrjamountdetailName));
            cmds.Add(new CmdParameter("@SupervisionAmountEx", m.SupervisionAmountEx));
            cmds.Add(new CmdParameter("@EndingSupervisionAmountEx", m.EndingSupervisionAmountEx));
            cmds.Add(new CmdParameter("@StartingOwnerAmountEx", m.StartingOwnerAmountEx));
            cmds.Add(new CmdParameter("@OwnerAmountEx", m.OwnerAmountEx));
            cmds.Add(new CmdParameter("@EndingOwnerAmountEx", m.EndingOwnerAmountEx));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Uom", m.Uom));
            cmds.Add(new CmdParameter("@CtrctQty", m.CtrctQty));
            cmds.Add(new CmdParameter("@CtrctPrjPrice", m.CtrctPrjPrice));
            cmds.Add(new CmdParameter("@CtrctAmount", m.CtrctAmount));
            cmds.Add(new CmdParameter("@LatestQty", m.LatestQty));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };

            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmountDetail_Update]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        /// <summary>
        /// 删除施工计量明细
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private void InnerDelete(SqlTransaction tran, PrjAmountDetail m, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountdetailNo", m.PrjamountdetailNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmountDetail_Delete]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 新增其他计量明细
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="projectinfo"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private PrjAmountOther InnerAddOtherDetail(SqlTransaction tran, PrjAmountOther m, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();

            cmds.Add(new CmdParameter("@PrjAmountOtherCode", m.PrjAmountOtherCode));
            cmds.Add(new CmdParameter("@PrjAmountOtherName", m.PrjAmountOtherName));
            cmds.Add(new CmdParameter("@ParentCode", m.ParentCode));
            cmds.Add(new CmdParameter("@Type", m.Type));
            cmds.Add(new CmdParameter("@Catalog", m.Catalog));
            cmds.Add(new CmdParameter("@Currency", m.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", m.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", m.ExchangeRate));
            cmds.Add(new CmdParameter("@PrjAmountNo", m.PrjAmountNo));
            cmds.Add(new CmdParameter("@StartingAmount", m.StartingAmount));
            cmds.Add(new CmdParameter("@Amount", m.Amount));
            cmds.Add(new CmdParameter("@EndingAmount", m.EndingAmount));
            cmds.Add(new CmdParameter("@StartingAmountEx", m.StartingAmountEx));
            cmds.Add(new CmdParameter("@AmountEx", m.AmountEx));
            cmds.Add(new CmdParameter("@EndingAmountEx", m.EndingAmountEx));
            cmds.Add(new CmdParameter("@Remark", m.Remark));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@PrjAmountOtherNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmountOther_Add]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            m.Id = Convert.ToInt32(pResult["@Id"]);
            m.PrjAmountOtherNo = pResult["PrjAmountOtherNo"].ToString();
            return m;
        }
        /// <summary>
        /// 更新其他计量明细
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns> 
        private PrjAmountOther InnerUpdateOtherDetail(PrjAmountOther m, string operationBy, SqlTransaction tran = null)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();

            cmds.Add(new CmdParameter("@PrjAmountNo", m.PrjAmountNo));
            cmds.Add(new CmdParameter("@PrjAmountOtherCode", m.PrjAmountOtherCode));
            cmds.Add(new CmdParameter("@PrjAmountOtherName", m.PrjAmountOtherName));
            cmds.Add(new CmdParameter("@ParentCode", m.ParentCode));
            cmds.Add(new CmdParameter("@Type", m.Type));
            cmds.Add(new CmdParameter("@Catalog", m.Catalog));
            cmds.Add(new CmdParameter("@Currency", m.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", m.CurrencyCode));
            cmds.Add(new CmdParameter("@ExchangeRate", m.ExchangeRate));
            cmds.Add(new CmdParameter("@StartingAmount", m.StartingAmount));
            cmds.Add(new CmdParameter("@Amount", m.Amount));
            cmds.Add(new CmdParameter("@EndingAmount", m.EndingAmount));
            cmds.Add(new CmdParameter("@StartingAmountEx", m.StartingAmountEx));
            cmds.Add(new CmdParameter("@AmountEx", m.AmountEx));
            cmds.Add(new CmdParameter("@EndingAmountEx", m.EndingAmountEx));
            cmds.Add(new CmdParameter("@Remark", m.Remark));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@PrjAmountOtherNo", m.PrjAmountOtherNo));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() }; 
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmountOther_Update]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return m;
        }

        private void InnerUpdateWBS(String PrjAmountNo, PrjAmountDetail project, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjApplyAmount", project.PrjApplyAmount));
            cmds.Add(new CmdParameter("@PrjamountNo", project.PrjamountNo));
            cmds.Add(new CmdParameter("@MidCertifiNum", project.MidCertifiNum));
            cmds.Add(new CmdParameter("@Currency", project.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", project.CurrencyCode));
            cmds.Add(new CmdParameter("@ProjectNo", project.ProjectNo));
            cmds.Add(new CmdParameter("@ExchangeRate", project.ExchangeRate));
            cmds.Add(new CmdParameter("@StartingApplyAmount", project.StartingApplyAmount));
            cmds.Add(new CmdParameter("@ApplyAmount", project.ApplyAmount));
            cmds.Add(new CmdParameter("@EndingApplyAmount", project.EndingApplyAmount));
            cmds.Add(new CmdParameter("@BidNo", project.BidNo));
            cmds.Add(new CmdParameter("@StartingSupervisionQty", project.StartingSupervisionQty));
            cmds.Add(new CmdParameter("@SupervisionQty", project.SupervisionQty));
            cmds.Add(new CmdParameter("@EndingSupervisionQty", project.EndingSupervisionQty));
            cmds.Add(new CmdParameter("@StartingSupervisionAmount", project.StartingSupervisionAmount));
            cmds.Add(new CmdParameter("@SupervisionAmount", project.SupervisionAmount));
            cmds.Add(new CmdParameter("@EndingSupervisionAmount", project.EndingSupervisionAmount));
            cmds.Add(new CmdParameter("@StartingOwnerQty", project.StartingOwnerQty));
            cmds.Add(new CmdParameter("@OwnerQty", project.OwnerQty));
            cmds.Add(new CmdParameter("@EndingOwnerQty", project.EndingOwnerQty));
            cmds.Add(new CmdParameter("@StartingOwnerAmount", project.StartingOwnerAmount));
            cmds.Add(new CmdParameter("@WbsNo", project.WbsNo));
            cmds.Add(new CmdParameter("@OwnerAmount", project.OwnerAmount));
            cmds.Add(new CmdParameter("@EndingOwnerAmount", project.EndingOwnerAmount));
            cmds.Add(new CmdParameter("@StartingApplyAmountEx", project.StartingApplyAmountEx));
            cmds.Add(new CmdParameter("@ApplyAmountEx", project.ApplyAmountEx));
            cmds.Add(new CmdParameter("@EndingApplyAmountEx", project.EndingApplyAmountEx));
            cmds.Add(new CmdParameter("@StartingSupervisionAmountEx", project.StartingSupervisionAmountEx));
            cmds.Add(new CmdParameter("@SupervisionAmountEx", project.SupervisionAmountEx));
            cmds.Add(new CmdParameter("@EndingSupervisionAmountEx", project.EndingSupervisionAmountEx));
            cmds.Add(new CmdParameter("@StartingOwnerAmountEx", project.StartingOwnerAmountEx));
            cmds.Add(new CmdParameter("@OwnerAmountEx", project.OwnerAmountEx));
            cmds.Add(new CmdParameter("@WbsLineNo", project.WbsLineNo));
            cmds.Add(new CmdParameter("@EndingOwnerAmountEx", project.EndingOwnerAmountEx));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@WbsLineCode", project.WbsLineCode));
            cmds.Add(new CmdParameter("@WbsLineName", project.WbsLineName));
            cmds.Add(new CmdParameter("@WbsSysCode", project.WbsSysCode));
            cmds.Add(new CmdParameter("@ParentCode", project.ParentCode));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmountWbs_Update]", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        /// <summary>
        /// 删除其他计量明细
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private void InnerDeleteOtherDetail(SqlTransaction tran, PrjAmountOther m, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjAmountOtherNo", m.PrjAmountOtherNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmountOther_Delete]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 修改各个报表数据
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="PrjamountNo"></param>
        /// <param name="operationBy"></param>
        private void InnerInitRptAmount(String PrjamountNo, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", PrjamountNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Rpt_PrjAmountRpt_Init]", CommandType.StoredProcedure, pResult.Parameters);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        #endregion


        public void ComplateNotice(int taskId, string appMenuNo, string menuItemNo, string refNo, string refName, string approvalText)
        {

            taskEntity wfTaskResult = hdDbCmdManager.QueryForFirstRow<taskEntity>("SELECT * FROM ERP_BPM.dbo.vWfTask WHERE Id = " + taskId, CommandType.Text);

            if (wfTaskResult != null)
            {
                ApprovalOpinion opinion = new DBProgressMetering.ApprovalOpinion();
                PrjAmount DataSource = hdDbCmdManager.QueryForFirstRow<PrjAmount>(String.Format(@"SELECT * 
                                                                            FROM [ERP_Subpay].[dbo].[v_subp_prjamount] 
                                                                            WHERE PrjamountNo = '{0}' AND Edit = 1", wfTaskResult.RefNo), System.Data.CommandType.Text);

                //当前审批流程-ExcuteStat : 1(承包人)-2(监理)-3(业主合约部（项目公司合约部）)-5(项目公司合约部经理)-4(业主总经理)
                if (menuItemNo == "2208000000000000418")  //承包人 
                {
                    if (wfTaskResult.WfTaskResult == 1)
                    {
                        hdDbCmdManager.Execute(String.Format(@"Update Erp_SubPay.dbo.subp_prjamount 
                                                    Set ContractorSign = @ContractorSign
                                                        ,ContractorSignDate = @ContractorSignDate
                                                        , ContractorOpinion =@ContractorOpinion
                                                    WHERE PrjamountNo='{0}' AND Edit=1", wfTaskResult.RefNo)
                                                   , CommandType.Text, new CmdParameter[] {
                                                   new CmdParameter("@ContractorSign",wfTaskResult.Holder)
                                                   ,new CmdParameter("@ContractorSignDate",wfTaskResult.RecordDate)
                                                   ,new CmdParameter("@ContractorOpinion",wfTaskResult.WfTaskComment)
                                                   });
                        DataSource.ExecuteStat = 2;
                        DataSource.ApprovalStat = 1; 
                    }
                }
                else if (menuItemNo == "2208000000000000560" || menuItemNo == "2208000000000000562"
                    || menuItemNo == "2208000000000000410" || menuItemNo == "2208000000000000563" || menuItemNo == "2208000000000000564") //监理
                {
                    PrjAmount result = Get(DataSource.PrjamountNo);
                    if (result == null)
                    {
                        Hondee.Common.Log.LoggerFactory.CreateLog().LogError(String.Format("获取进度计量数据失败,并导致监理设置默认值失败\n错误单据编号为：{0}", DataSource.PrjamountNo));
                        return;
                    }
                    else
                    {
                        DataSource = result;
                        SaveDefaultValue(DataSource, 2, wfTaskResult.Holder); 
                    }
                    if (wfTaskResult.WfTaskResult == 1)
                    {
                        if (menuItemNo == "2208000000000000410")
                            hdDbCmdManager.Execute(String.Format(@"Update Erp_SubPay.dbo.subp_prjamount Set SupervisionSign = @SupervisionSign
                                                                        ,SupervisionSignDate = @SupervisionSignDate, SupervisionOpinion = @SupervisionOpinion 
                                                                            WHERE PrjamountNo='{0}' AND Edit=1", wfTaskResult.RefNo)
                                                                                , CommandType.Text, new CmdParameter[] {
                                                new CmdParameter("@SupervisionSign",wfTaskResult.Holder)
                                                ,new CmdParameter("@SupervisionSignDate",wfTaskResult.RecordDate)
                                                ,new CmdParameter("@SupervisionOpinion",wfTaskResult.WfTaskComment)
                                                    }); 
                        if (menuItemNo == "2208000000000000564")
                            DataSource.ExecuteStat = 3;
                        DataSource.ApprovalStat = 1;
                    }
                }
                else if (menuItemNo == "2208000000000000420")  //业主计量工程师
                {
                    PrjAmount result = Get(DataSource.PrjamountNo);
                    if (result == null)
                    {
                        Hondee.Common.Log.LoggerFactory.CreateLog().LogError(String.Format("获取进度计量数据失败,并导致业主合约部设置默认值失败\n错误单据编号为：{0}", DataSource.PrjamountNo));
                    }
                    else
                    {
                        DataSource = result;
                        SaveDefaultValue(DataSource, 3, wfTaskResult.Holder);
                    }

                    if (wfTaskResult.WfTaskResult == 1)
                    {
                        hdDbCmdManager.Execute(String.Format(@"Update Erp_SubPay.dbo.subp_prjamount Set BusDeptSign = @BusDeptSign
                                                                            ,BusDeptSignDate = @BusDeptSignDate, BusDeptOpinion = @BusDeptOpinion
                                                                             WHERE PrjamountNo='{0}' AND Edit=1", wfTaskResult.RefNo)
                                                                        , CommandType.Text, new CmdParameter[] {
                                                   new CmdParameter("@BusDeptSign",wfTaskResult.Holder)
                                                   ,new CmdParameter("@BusDeptSignDate",wfTaskResult.RecordDate)
                                                   ,new CmdParameter("@BusDeptOpinion",wfTaskResult.WfTaskComment)
                                                   });
                        DataSource.ExecuteStat = 5;
                        DataSource.ApprovalStat = 1;
                    }
                }
                else if (menuItemNo == "2208000000000000414")  //业主合约部经理
                {
                    if (wfTaskResult.WfTaskResult == 1)
                    {

                        hdDbCmdManager.Execute(String.Format(@"Update Erp_SubPay.dbo.subp_prjamount Set BusMngSign = @BusMngSign
                                                                            ,BusMngSignDate = @BusMngSignDate, BusMngOpinion = @BusMngOpinion
                                                                             WHERE PrjamountNo='{0}' AND Edit=1", wfTaskResult.RefNo)
                                                                        , CommandType.Text, new CmdParameter[] {
                                                   new CmdParameter("@BusMngSign",wfTaskResult.Holder)
                                                   ,new CmdParameter("@BusMngSignDate",wfTaskResult.RecordDate)
                                                   ,new CmdParameter("@BusMngOpinion",wfTaskResult.WfTaskComment)
                                                   });
                        DataSource.ExecuteStat = 4;
                        DataSource.ApprovalStat = 1;
                    }
                } 
                else if (menuItemNo == "2208000000000000421" || menuItemNo == "2208000000000000565")  //业主总经理   业主生产副总经理
                {
                    if (wfTaskResult.WfTaskResult == 1)
                    {
                        if (menuItemNo == "2208000000000000421")
                        {
                            hdDbCmdManager.Execute(String.Format(@"Update Erp_SubPay.dbo.subp_prjamount Set BusLeaderSign = @BusLeaderSign
                                                                            ,BusLeaderSignDate = @BusLeaderSignDate, BusLeaderOpinion = @BusLeaderOpinion
                                                                              WHERE PrjamountNo = '{0}' AND Edit = 1", wfTaskResult.RefNo)
                                                                            , CommandType.Text, new CmdParameter[] {
                                                   new CmdParameter("@BusLeaderSign",wfTaskResult.Holder)
                                                   ,new CmdParameter("@BusLeaderSignDate",wfTaskResult.RecordDate)
                                                   ,new CmdParameter("@BusLeaderOpinion",wfTaskResult.WfTaskComment)
                                                       });
                            hdDbCmdManager.ExecuteTran((tran) =>
                            {
                                InnerReleaseEx(tran, refNo, wfTaskResult.Holder);
                            });
                        }
                        DataSource.ApprovalStat = menuItemNo == "2208000000000000421" ? 2 : 1;
                    }
                }
                switch (menuItemNo)
                {
                    case "2208000000000000418"://承包人
                        opinion.RoleId = 4;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.专业监理工程师1 : PrjamountShowStat.承包人2; break;
                    case "2208000000000000560"://专业监理工程师
                        opinion.RoleId = 24;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.合同监理工程师1 : PrjamountShowStat.专业监理工程师2; break;
                    case "2208000000000000562"://合同监理工程师
                        opinion.RoleId = 25;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.总监1 : PrjamountShowStat.合同监理工程师2; break;
                    case "2208000000000000410"://总监
                        opinion.RoleId = 1;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.管理处专业工程师1 : PrjamountShowStat.总监2; break;
                    case "2208000000000000563"://管理处专业工程师
                        opinion.RoleId = 26;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.管理处主任1 : PrjamountShowStat.管理处专业工程师2; break;
                    case "2208000000000000564"://管理处主任
                        opinion.RoleId = 27;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.业主计量工程师1 : PrjamountShowStat.管理处主任2; break;
                    case "2208000000000000420"://业主计量工程师
                        opinion.RoleId = 5;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.业主合约部经理1 : PrjamountShowStat.业主计量工程师2; break;
                    case "2208000000000000414"://业主合约部经理
                        opinion.RoleId = 13;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.业主生产副总经理1 : PrjamountShowStat.业主合约部经理2; break;
                    case "2208000000000000565"://业主生产副总经理
                        opinion.RoleId = 28;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? PrjamountShowStat.业主总经理1 : PrjamountShowStat.业主生产副总经理2; break;
                    case "2208000000000000421"://业主总经理
                        opinion.RoleId = 6;
                        DataSource.ShowStat = wfTaskResult.WfTaskResult == 1 ? "同意" : PrjamountShowStat.业主总经理2; break;
                }
                opinion.BusinessNo = wfTaskResult.RefNo;
                opinion.AppMenuNo = appMenuNo;
                opinion.MenuItemNo = menuItemNo;
                opinion.LoginName = wfTaskResult.Holder;
                opinion.UserName = wfTaskResult.HolderName;
                opinion.WfTaskResult = wfTaskResult.WfTaskResult;
                opinion.WfTaskComment = wfTaskResult.WfTaskComment;
                opinion.CreatedDate = wfTaskResult.RecordDate;
                opinion.RType = 1;
                InserApprovalOpinion(opinion);
                if (wfTaskResult.WfTaskResult == 0)
                {
                    DataSource.ApprovalStat = 0;
                }
                ChangeBusState(DataSource, wfTaskResult.Holder);
            }
        }
        /// <summary>
        /// 写入流程记录相关信息到cmn_approvalopinion
        /// </summary>
        private void InserApprovalOpinion(ApprovalOpinion opinion)
        {
            hdDbCmdManager.Execute(String.Format(@"insert into Erp_SubPay.dbo.cmn_approvalopinion(BusinessNo 
                                                                            ,CreatedDate  
                                                                            ,AppMenuNo    
                                                                            ,MenuItemNo   
                                                                            ,RoleId       
                                                                            ,LoginName    
                                                                            ,UserName     
                                                                            ,WfTaskResult 
                                                                            ,WfTaskComment
                                                                            ,RType)               
                                                                        values(@BusinessNo
                                                                            ,@CreatedDate 
                                                                            ,@AppMenuNo 
                                                                            ,@MenuItemNo 
                                                                            ,@RoleId 
                                                                            ,@LoginName 
                                                                            ,@UserName 
                                                                            ,@WfTaskResult 
                                                                            ,@WfTaskComment  
                                                                            ,@RType ) ")
                                                , CommandType.Text, new CmdParameter[] {
                                                   new CmdParameter("@BusinessNo",opinion.BusinessNo)
                                                   ,new CmdParameter("@CreatedDate",opinion.CreatedDate)
                                                   ,new CmdParameter("@AppMenuNo",opinion.AppMenuNo)
                                                   ,new CmdParameter("@MenuItemNo",opinion.MenuItemNo)
                                                   ,new CmdParameter("@RoleId",opinion.RoleId)
                                                   ,new CmdParameter("@LoginName",opinion.LoginName)
                                                   ,new CmdParameter("@UserName",opinion.UserName)
                                                   ,new CmdParameter("@WfTaskResult",opinion.WfTaskResult)
                                                   ,new CmdParameter("@WfTaskComment",opinion.WfTaskComment)
                                                   ,new CmdParameter("@RType",opinion.RType)
                           });

        }
        private void SaveDefaultValue(PrjAmount DataSource, int LoginRoleType, string OperationBy)
        {
            DataSource.SetDefaultValue(LoginRoleType);

            List<PrjAmountDetail> changedPrjAmountWBS = new List<PrjAmountDetail>();
            List<PrjAmountDetail> changedPrjAmountDetail = new List<PrjAmountDetail>();
            //取出所有被修改的detail清单+WBS清单
            if (DataSource.LstAmountDetail != null)
            {
                DataSource.LstAmountDetail.ForEach(m =>
                {
                    if (m.isChanged)
                    {
                        if (!String.IsNullOrEmpty(m.PrjamountdetailNo))
                        {
                            changedPrjAmountDetail.Add(m);
                        }
                        else
                        {
                            changedPrjAmountWBS.Add(m);
                        }
                    }
                });
            }

            if (changedPrjAmountWBS.Count + changedPrjAmountDetail.Count > 0)
            {
                PrjAmount prjAmount = DataSource.Clone();
                prjAmount.LstAmountDetail = new List<PrjAmountDetail>();
                prjAmount.LstAmountOther = new List<PrjAmountOther>();
                UpdateAll(prjAmount, changedPrjAmountDetail, changedPrjAmountWBS, prjAmount.LstAmountOther, OperationBy);
                Commit(prjAmount.PrjamountNo, OperationBy);
            }
        }

        private void InnerReleaseEx(SqlTransaction tran, String prjAmountNo, string releaseBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", prjAmountNo));
            cmds.Add(new CmdParameter("@ReleaseBy", releaseBy));
            cmds.Add(new CmdParameter("@ReleaseDate", DateTime.Now));
            cmds.Add(new CmdParameter("@OperationBy", releaseBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmount_Release]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }

        /// <summary>
        /// 更新数据提交状态
        /// </summary>
        /// <param name="prjAmount"></param>
        /// <param name="operationBy"></param>
        public void ChangeBusState(PrjAmount prjAmount, String operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerChangeBusState(tran, prjAmount, operationBy);
            });
        }
        private void InnerChangeBusState(SqlTransaction tran, PrjAmount prjAmount, String operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", prjAmount.PrjamountNo));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ApprovalBy", prjAmount.ApprovalBy));
            cmds.Add(new CmdParameter("@ApprovalDate", prjAmount.ApprovalDate));
            cmds.Add(new CmdParameter("@ApprovalStat", prjAmount.ApprovalStat));
            cmds.Add(new CmdParameter("@ExecuteStat", prjAmount.ExecuteStat));
            cmds.Add(new CmdParameter("@ShowStat", prjAmount.ShowStat));
            cmds.Add(new CmdParameter("@WfdefId", prjAmount.WfdefId));
            cmds.Add(new CmdParameter("@RefCategory", prjAmount.RefCategory));
            cmds.Add(new CmdParameter("@inWorkflow", prjAmount.inWorkflow));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Subp_PrjAmount_ChangeBusinessStat]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
    }

    public class taskEntity
    {
        public int WfInstId { get; set; }

        public string RefNo { get; set; }
        public string Applicant { get; set; }

        public string ApplicantName { get; set; }

        public string Holder { get; set; }

        public string HolderName { get; set; }

        public int WfTaskResult { get; set; }

        public string WfTaskComment { get; set; }

        public string AppMenuNo { get; set; }
        public string MenuItemNo { get; set; }

        public string WfTaskStatNo { get; set; }
        public DateTime RecordDate { get; set; }
    }

    public class ApprovalOpinion
    {
        public int Id { get; set; }
        public string BusinessNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AppMenuNo { get; set; }
        public string MenuItemNo { get; set; }
        public int RoleId { get; set; }
        public string LoginName { get; set; } 
        public string UserName { get; set; }
        public int WfTaskResult { get; set; }
        public string WfTaskComment { get; set; }
        public int RType { get; set; }
    }
}
