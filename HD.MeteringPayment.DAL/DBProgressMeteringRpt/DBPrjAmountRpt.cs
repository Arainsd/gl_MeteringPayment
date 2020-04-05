using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using Erp.CommonData.Tool;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;

namespace HD.MeteringPayment.DAL.DBProgressMeteringRpt
{
    public class DBPrjAmountRpt : IPrjAmountRpt
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<PrjAmountRpt> GetList(string whereQuery)
        {
            return hdDbCmdManager.QueryForList<PrjAmountRpt>(@"SELECT * FROM [ERP_Subpay].[dbo].[v_rpt_prjamountrpt]  " + whereQuery, System.Data.CommandType.Text, null);
        }

        /// <summary>
        /// 获取报表实例
        /// </summary>
        /// <param name="PrjamountNo"></param>
        /// <returns></returns>
        public PrjAmountRpt Get(string PrjamountNo)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@PrjamountNo", PrjamountNo));
            PrjAmountRpt result = hdDbCmdManager.QueryForFirstRow<PrjAmountRpt>(@"SELECT * 
                                                                                  FROM [ERP_Subpay].[dbo].[v_rpt_prjamountrpt] 
                                                                                  WHERE PrjamountNo = @PrjamountNo AND StatId = 1", System.Data.CommandType.Text, cmds.ToArray());
            if (result != null)
            {  
                #region 获取签名字段
                if (!String.IsNullOrEmpty(result.ContractorSign))  //承办人
                {
                    try
                    {
                        result.ContractorSignImg = GetSignImg(result.ContractorSign);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取承办人签名图片失败，承办人{0}", result.ContractorSign));
                    }
                }
                if (!String.IsNullOrEmpty(result.SupervisionSign_zy))  //专业监理工程师
                {
                    try
                    {
                        result.SupervisionSign_zyImg = GetSignImg(result.SupervisionSign_zy);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取专业监理工程师签名图片失败，专业监理工程师{0}", result.SupervisionSign_zy));
                    }
                }
                if (!String.IsNullOrEmpty(result.SupervisionSign_ht))  //合同监理工程师
                {
                    try
                    {
                        result.SupervisionSign_htImg = GetSignImg(result.SupervisionSign_ht);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取合同监理工程师签名图片失败，合同监理工程师{0}", result.SupervisionSign_ht));
                    }
                }
                if (!String.IsNullOrEmpty(result.SupervisionSign))  //总监
                {
                    try
                    {
                        result.SupervisionSignImg = GetSignImg(result.SupervisionSign);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取总监签名图片失败，总监{0}", result.SupervisionSign));
                    }
                }
                if (!String.IsNullOrEmpty(result.SupervisionSign_glzy))  //管理处专业工程师
                {
                    try
                    {
                        result.SupervisionSign_glzyImg = GetSignImg(result.SupervisionSign_glzy);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取管理处专业工程师签名图片失败，管理处专业工程师{0}", result.SupervisionSign_glzy));
                    }
                }
                if (!String.IsNullOrEmpty(result.SupervisionSign_glzr))  //管理处主任
                {
                    try
                    {
                        result.SupervisionSign_glzrImg = GetSignImg(result.SupervisionSign_glzr);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取总监签名图片失败，总监{0}", result.SupervisionSign_glzr));
                    }
                }
                if (!String.IsNullOrEmpty(result.BusDeptSign))  //业主计量工程师
                {
                    try
                    {
                        result.BusDeptSignImg = GetSignImg(result.BusDeptSign);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取业主计量工程师签名图片失败，业主计量工程师{0}", result.BusDeptSign));
                    }
                }
                if (!String.IsNullOrEmpty(result.BusMngSign))  //业主合约部经理
                {
                    try
                    {
                        result.BusMngSignImg = GetSignImg(result.BusMngSign);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取业主合约部经理签名图片失败，业主合约部经理{0}", result.BusDeptSign));
                    }
                }
                if (!String.IsNullOrEmpty(result.BusLeaderSign_fu))  //业主生产副总经理
                {
                    try
                    {
                        result.BusLeaderSign_fuImg = GetSignImg(result.BusLeaderSign_fu);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取业主生产副总经理签名图片失败，业主生产副总经理{0}", result.BusLeaderSign_fu));
                    }
                }
                if (!String.IsNullOrEmpty(result.BusLeaderSign))  //业主总经理
                {
                    try
                    {
                        result.BusLeaderSignImg = GetSignImg(result.BusLeaderSign);
                    }
                    catch
                    {
                        Console.WriteLine(String.Format("获取业主总经理签名图片失败，业主总经理{0}", result.BusLeaderSign));
                    }
                }
                #endregion

                result.lstPayRpt = hdDbCmdManager.QueryForList<PrjAmountPayRpt>(@"SELECT a.*
                                                                                        ,b.IItemCoe
                                                                                    FROM ERP_Subpay.dbo.rpt_prjamountpayrpt a
                                                                                    LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_boi b ON a.ItemNo = b.ItemNo AND b.Edit = 1
                                                                                   WHERE a.PrjamountNo = @PrjamountNo AND a.StatId = 1
                                                                                   ORDER BY ItemCode ASC", System.Data.CommandType.Text, cmds.ToArray());

                List<PrjAmountPayRpt> titles = new List<PrjAmountPayRpt>();
                if (result.lstPayRpt != null && result.lstPayRpt.Count > 0)
                {
                    int type = 0, i = 0;
                    for (int index = 0; index < result.lstPayRpt.Count; index++)
                    {
                        if (result.lstPayRpt[index].Type != type)  //如果Type改变，则清空i并重新开始计数
                        {
                            type = result.lstPayRpt[index].Type;
                            i = 0;
                        }
                        result.lstPayRpt[index].Sequence = result.lstPayRpt[index].Type * 100 + i;
                        i++;

                        if (result.lstPayRpt[index].Type == 3)
                        {
                            PrjAmountPayRpt title1 = result.lstPayRpt.Find(n => n.Type == 2);
                            result.lstPayRpt[index].IItemCoe = title1 != null ? title1.ItemName : "";
                        }
                        else if(result.lstPayRpt[index].Type == 5)
                        {
                            PrjAmountPayRpt title2 = result.lstPayRpt.Find(n => n.Type == 4);
                            result.lstPayRpt[index].IItemCoe = title2 != null ? title2.ItemName : "";
                        }
                        else if (result.lstPayRpt[index].Type == 7)
                        {
                            PrjAmountPayRpt title3 = result.lstPayRpt.Find(n => n.Type == 6);
                            result.lstPayRpt[index].IItemCoe = title3 != null ? title3.ItemName : "";
                        }
                    }
                    result.lstPayRpt.RemoveAll(m => m.Type == 2 || m.Type == 4 || m.Type == 6);  //移除其他计量标题

                    #region 添加合计行并排序
                    int forIndex = result.IsNewData ? 9 : 7;
                    string hj = result.IsNewData ? "合计" : "小计";
                    for (int index = 1; index <= forIndex; index = index + 2)
                    {
                        PrjAmountPayRpt sum = new PrjAmountPayRpt();
                        sum.Type = index;
                        sum.Sequence = index * 100 + 99;
                        if(index != forIndex)
                        { 
                            //合计-四舍五入为整数
                            sum.CtrctAmount = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.CtrctAmount ?? 0), 0, MidpointRounding.AwayFromZero) : 0) ;
                            sum.CtrctAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.CtrctAmountEx ?? 0), 0, MidpointRounding.AwayFromZero) : 0) ;
                            sum.ChangeAmount = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.ChangeAmount ?? 0), 0, MidpointRounding.AwayFromZero) : 0) ;
                            sum.ChangeAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.ChangeAmountEx ?? 0), 0, MidpointRounding.AwayFromZero) : 0) ;
                            sum.LatestAmount = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.LatestAmount ?? 0), 0, MidpointRounding.AwayFromZero) : 0) ;
                            sum.LatestAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.LatestAmountEx ?? 0), 0, MidpointRounding.AwayFromZero) : 0) ;
                            sum.StartingAmount = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.StartingAmount ?? 0), 0, MidpointRounding.AwayFromZero) : 0) 
                                + (result.IsNewData && index == 3 ? result.lstPayRpt.Find(m => m.ItemName == "100章-700章小计").StartingAmount : 0);
                            sum.StartingAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.StartingAmountEx ?? 0), 0, MidpointRounding.AwayFromZero) : 0)
                                + (result.IsNewData && index == 3 ? result.lstPayRpt.Find(m => m.ItemName == "100章-700章小计").StartingAmountEx : 0);
                            sum.Amount = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.Amount ?? 0), 0, MidpointRounding.AwayFromZero) : 0)
                                + (result.IsNewData && index == 3 ? result.lstPayRpt.Find(m => m.ItemName == "100章-700章小计").Amount : 0);
                            sum.AmountEx = result.lstPayRpt.Sum(m => m.Type == index ? Math.Round((m.AmountEx ?? 0), 0, MidpointRounding.AwayFromZero) : 0)
                                + (result.IsNewData && index == 3 ? result.lstPayRpt.Find(m => m.ItemName == "100章-700章小计").AmountEx : 0);
                            sum.EndingAmount = sum.StartingAmount+ sum.Amount;
                            sum.EndingAmountEx = sum.StartingAmountEx + sum.AmountEx;
                            //合计2019-10-21
                            //sum.CtrctAmount = result.lstPayRpt.Sum(m => m.Type == index ? m.CtrctAmount ?? 0 : 0);
                            //sum.CtrctAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? m.CtrctAmountEx ?? 0 : 0);
                            //sum.ChangeAmount = result.lstPayRpt.Sum(m => m.Type == index ? m.ChangeAmount ?? 0 : 0);
                            //sum.ChangeAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? m.ChangeAmountEx ?? 0 : 0);
                            //sum.LatestAmount = result.lstPayRpt.Sum(m => m.Type == index ? m.LatestAmount ?? 0 : 0);
                            //sum.LatestAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? m.LatestAmountEx : 0);
                            //sum.EndingAmount = result.lstPayRpt.Sum(m => m.Type == index ? m.EndingAmount : 0);
                            //sum.EndingAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? m.EndingAmountEx : 0);
                            //sum.StartingAmount = result.lstPayRpt.Sum(m => m.Type == index ? m.StartingAmount ?? 0 : 0);
                            //sum.StartingAmountEx = result.lstPayRpt.Sum(m => m.Type == index ? m.StartingAmountEx ?? 0 : 0);
                            //sum.Amount = result.lstPayRpt.Sum(m => m.Type == index ? m.Amount ?? 0 : 0);
                            //sum.AmountEx = result.lstPayRpt.Sum(m => m.Type == index ? m.AmountEx ?? 0 : 0);

                            sum.EndingProp = sum.LatestAmount.HasValue && sum.LatestAmount != 0 ? sum.EndingAmount / sum.LatestAmount : 0;
                            sum.LastProp = sum.LatestAmount.HasValue && sum.LatestAmount != 0 ? sum.StartingAmount / sum.LatestAmount : 0;
                            sum.Prop = sum.LatestAmount.HasValue && sum.LatestAmount != 0 ? sum.Amount / sum.LatestAmount : 0;
                        }
                        else
                        {
                            sum.CtrctAmount = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").CtrctAmount - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").CtrctAmount + result.lstPayRpt.Find(m => m.ItemName == hj).CtrctAmount;
                            sum.CtrctAmountEx = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").CtrctAmountEx - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").CtrctAmountEx + result.lstPayRpt.Find(m => m.ItemName == hj).CtrctAmountEx;
                            sum.ChangeAmount = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").ChangeAmount - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").ChangeAmount + result.lstPayRpt.Find(m => m.ItemName == hj).ChangeAmount;
                            sum.ChangeAmountEx = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").ChangeAmountEx - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").ChangeAmountEx + result.lstPayRpt.Find(m => m.ItemName == hj).ChangeAmountEx;
                            sum.LatestAmount = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").LatestAmount - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").LatestAmount + result.lstPayRpt.Find(m => m.ItemName == hj).LatestAmount;
                            sum.LatestAmountEx = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").LatestAmountEx - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").LatestAmountEx + result.lstPayRpt.Find(m => m.ItemName == hj).LatestAmountEx;
                            sum.StartingAmount = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").StartingAmount - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").StartingAmount + result.lstPayRpt.Find(m => m.ItemName == hj).StartingAmount;
                            sum.StartingAmountEx = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").StartingAmountEx - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").StartingAmountEx + result.lstPayRpt.Find(m => m.ItemName == hj).StartingAmountEx;
                            sum.Amount = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").Amount - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").Amount + result.lstPayRpt.Find(m => m.ItemName == hj).Amount;
                            sum.AmountEx = result.lstPayRpt.Find(m => m.ItemName == "应计量合计").AmountEx - result.lstPayRpt.Find(m => m.ItemName == "应扣款合计").AmountEx + result.lstPayRpt.Find(m => m.ItemName == hj).AmountEx;
                            sum.EndingAmount = sum.StartingAmount + sum.Amount;
                            sum.EndingAmountEx = sum.StartingAmountEx + sum.AmountEx;

                            sum.EndingProp = sum.LatestAmount.HasValue && sum.LatestAmount != 0 ? sum.EndingAmount / sum.LatestAmount : 0;
                            sum.LastProp = sum.LatestAmount.HasValue && sum.LatestAmount != 0 ? sum.StartingAmount / sum.LatestAmount : 0;
                            sum.Prop = sum.LatestAmount.HasValue && sum.LatestAmount != 0 ? sum.Amount / sum.LatestAmount : 0;

                            sum.Type = forIndex;
                            result.ThisPeriodShouldPay = sum.Amount ?? 0;
                        }
                        if (result.IsNewData)
                        {
                            switch (index)
                            {
                                case 1:
                                    sum.ItemName = "100章-700章小计";
                                    sum.ItemCode = "";
                                    break;
                                case 3:
                                    sum.ItemName = "合计";
                                    sum.IItemCoe = result.lstPayRpt.Find(n => n.Type == 3) != null ? result.lstPayRpt.Find(n => n.Type == 3).IItemCoe : "";
                                    break;
                                case 5:
                                    sum.ItemName = "应计量合计";
                                    sum.IItemCoe = result.lstPayRpt.Find(n => n.Type == 5) != null ? result.lstPayRpt.Find(n => n.Type == 5).IItemCoe : "";
                                    break;
                                case 7:
                                    sum.ItemName = "应扣款合计";
                                    sum.IItemCoe = result.lstPayRpt.Find(n => n.Type == 7) != null ? result.lstPayRpt.Find(n => n.Type == 7).IItemCoe : "";
                                    break;
                                case 9:
                                    sum.ItemName = "合计 + 应计量合计 - 应扣款合计";
                                    sum.IItemCoe = "应支付费用";
                                    break;
                            }
                        }
                        else
                        {
                            switch (index)
                            {
                                case 1:
                                    sum.ItemName = "小计";
                                    sum.ItemCode = "";
                                    break;
                                case 3:
                                    sum.ItemName = "应计量合计";
                                    sum.IItemCoe = result.lstPayRpt.Find(n => n.Type == 3) != null ? result.lstPayRpt.Find(n => n.Type == 3).IItemCoe : "";
                                    break;
                                case 5:
                                    sum.ItemName = "应扣款合计";
                                    sum.IItemCoe = result.lstPayRpt.Find(n => n.Type == 5) != null ? result.lstPayRpt.Find(n => n.Type == 5).IItemCoe : "";
                                    break;
                                case 7:
                                    sum.ItemName = "小计 + 应计量合计 - 应扣款合计";
                                    sum.IItemCoe = "应支付费用";
                                    break;
                            }
                        }
                        result.lstPayRpt.Add(sum);
                    }

                    result.lstPayRpt = result.lstPayRpt.OrderBy(m => m.Sequence).ToList(); //按照序号顺排
                    #endregion
                }

                //清单支付报表
                result.lstBoiPayRpt = hdDbCmdManager.QueryForList<PrjAmountBoiRpt>(@"SELECT a.*
                                                                                           ,b.IItemCoe 
                                                                                      FROM ERP_Subpay.dbo.rpt_prjamountboirpt a
                                                                                      LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_boi b ON a.ItemNo = b.ItemNo AND b.Edit = 1
                                                                                     WHERE a.PrjamountNo = @PrjamountNo AND a.StatId = 1  order by a.ItemCode", System.Data.CommandType.Text, cmds.ToArray());

                //读取清单父节点
                //List<ContractBoi> parentNodes = hdDbCmdManager.QueryForList<ContractBoi>(String.Format(@"SELECT *
                //                                                                                          FROM ERP_BoQ.dbo.gl_cntrct_boi i
                //                                                                                         WHERE i.ProjectNo = '{0}' AND ISNULL(i.ParentCode, '') = '' 
                //                                                                                           AND Edit = 1 AND StatId = 1
                //                                                                                         ORDER BY ItemCode", result.ProjectNo), CommandType.Text);
                List<ContractBoi> parentNodes = hdDbCmdManager.QueryForList<ContractBoi>(String.Format(@"SELECT DISTINCT tb.ProjectNo ,tb.BoQNo ,tb.ItemNo ,tb.ItemCode ,tb.IItemCoe ,tb.ItemName,tb.CtrctAmount,tb.LatestAmount FROM(
                                                SELECT '{0}' AS ProjectNo
                                                 ,CASE WHEN ISNULL(i.BoQNo,'')='' THEN cast(newid() as varchar(36)) ELSE i.BoQNo END AS BoQNo
                                                 ,CASE WHEN ISNULL(i.ItemNo,'')='' THEN cast(newid() as varchar(36)) ELSE i.ItemNo END AS ItemNo
                                                 ,CASE WHEN ISNULL(i.ItemCode,'')='' THEN tmp.ItemCode ELSE i.ItemCode END AS ItemCode
                                                 ,CASE WHEN ISNULL(i.IItemCoe,'')='' THEN tmp.IItemCoe ELSE i.IItemCoe END AS IItemCoe
                                                 ,CASE WHEN ISNULL(i.ItemName,'')='' THEN tmp.ItemName ELSE i.ItemName END AS ItemName
                                                 ,i.CtrctAmount
                                                 ,i.LatestAmount
                                                FROM (
                                                SELECT '01' AS ItemCode,'100' AS IItemCoe,'总则' AS ItemName
                                                UNION ALL
                                                SELECT '02' AS ItemCode,'200' AS IItemCoe,'路基' AS ItemName
                                                UNION ALL
                                                SELECT '03' AS ItemCode,'300' AS IItemCoe,'路面' AS ItemName
                                                UNION ALL
                                                SELECT '04' AS ItemCode,'400' AS IItemCoe,'桥梁、涵洞工程' AS ItemName
                                                UNION ALL
                                                SELECT '05' AS ItemCode,'500' AS IItemCoe,'隧道' AS ItemName
                                                UNION ALL
                                                SELECT '06' AS ItemCode,'600' AS IItemCoe,'交通安全设施' AS ItemName
                                                UNION ALL
                                                SELECT '07' AS ItemCode,'700' AS IItemCoe,'绿化及环境保护设施' AS ItemName
                                                )tmp LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_boi i ON i.IItemCoe=tmp.IItemCoe and i.ProjectNo = '{0}' AND ISNULL(i.ParentCode, '') = '' AND Edit = 1 AND StatId = 1 
                                                )tb ORDER BY tb.ItemCode", result.ProjectNo), CommandType.Text);
                //添加各个清单父节点合计行
                List<PrjAmountBoiRpt> lstSummary1 = new List<PrjAmountBoiRpt>();
                if(parentNodes != null)
                {
                    foreach(ContractBoi boi in parentNodes)
                    {
                        if (String.IsNullOrEmpty(boi.ItemCode))
                            continue;
                        List<PrjAmountBoiRpt> filterByCode = result.lstBoiPayRpt.FindAll(m => m.ItemCode.StartsWith(boi.ItemCode));
                        //if (filterByCode == null || filterByCode.Count == 0) //如果该父节点没有清单明细，则跳过
                        //    continue;
                        PrjAmountBoiRpt sum = new PrjAmountBoiRpt();
                        sum.ItemCode = boi.ItemCode;
                        sum.IItemCoe = boi.IItemCoe;
                        //sum.ItemName = String.Format("{0}章 - {1}", boi.IItemCoe, boi.ItemName);
                        sum.ItemName = boi.ItemName;
                        sum.CtrctAmount = Math.Round(boi.CtrctAmount, 0, MidpointRounding.AwayFromZero);
                        sum.LatestAmount = Math.Round(boi.LatestAmount, 0, MidpointRounding.AwayFromZero);
                        sum.Sequence = 9998;
                        if (filterByCode != null && filterByCode.Count > 0)
                        {
                            //sum.CtrctAmount = filterByCode.Sum(m => Math.Round((m.CtrctAmount ?? 0), 0, MidpointRounding.AwayFromZero)); 
                            //sum.LatestAmount = filterByCode.Sum(m => Math.Round((m.LatestAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                            sum.StartingAmount = filterByCode.Sum(m => Math.Round((m.StartingAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                            sum.Amount = filterByCode.Sum(m => Math.Round((m.Amount ?? 0), 0, MidpointRounding.AwayFromZero));
                            sum.EndingAmount = sum.StartingAmount + sum.Amount;
                            //2019-10-21
                            //sum.CtrctAmount = filterByCode.Sum(m => m.CtrctAmount ?? 0);
                            //sum.LatestAmount = filterByCode.Sum(m => m.LatestAmount ?? 0);
                            //sum.EndingAmount = filterByCode.Sum(m => m.EndingAmount ?? 0);
                            //sum.StartingAmount = filterByCode.Sum(m => m.StartingAmount ?? 0);
                            //sum.Amount = filterByCode.Sum(m => m.Amount ?? 0);
                        }
                        lstSummary1.Add(sum);
                    }
                }
                //添加总体未筛选合计行
                PrjAmountBoiRpt summary1 = new PrjAmountBoiRpt();
                summary1.ItemCode = "999";
                summary1.IItemCoe = "999";
                summary1.ItemName = "合计：";
                summary1.Sequence = 9998;

                //summary1.CtrctAmount = result.lstBoiPayRpt.Sum(m => Math.Round((m.CtrctAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                //summary1.LatestAmount = result.lstBoiPayRpt.Sum(m => Math.Round((m.LatestAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                summary1.CtrctAmount = parentNodes.Sum(m => Math.Round(m.CtrctAmount, 0, MidpointRounding.AwayFromZero));
                summary1.LatestAmount = parentNodes.Sum(m => Math.Round(m.LatestAmount, 0, MidpointRounding.AwayFromZero));
                summary1.StartingAmount = result.lstBoiPayRpt.Sum(m => Math.Round((m.StartingAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                summary1.Amount = result.lstBoiPayRpt.Sum(m => Math.Round((m.Amount ?? 0), 0, MidpointRounding.AwayFromZero));
                summary1.EndingAmount = summary1.StartingAmount+ summary1.Amount;
                //2019-10-21
                //summary1.CtrctAmount = result.lstBoiPayRpt.Sum(m => m.CtrctAmount ?? 0);
                //summary1.LatestAmount = result.lstBoiPayRpt.Sum(m => m.LatestAmount ?? 0);
                //summary1.EndingAmount = result.lstBoiPayRpt.Sum(m => m.EndingAmount ?? 0);
                //summary1.StartingAmount = result.lstBoiPayRpt.Sum(m => m.StartingAmount ?? 0);
                //summary1.Amount = result.lstBoiPayRpt.Sum(m => m.Amount ?? 0);

                //添加各个清单父节点筛选后合计行，筛选所有至本期末完成有值且不等于0的项
                List<PrjAmountBoiRpt> lstSummary2 = new List<PrjAmountBoiRpt>();
                if (parentNodes != null)
                {
                    foreach (ContractBoi boi in parentNodes)
                    {
                        if (String.IsNullOrEmpty(boi.ItemCode))
                            continue;
                        List<PrjAmountBoiRpt> filterByCode = result.lstBoiPayRpt.FindAll(m => m.ItemCode.StartsWith(boi.ItemCode) && m.EndingAmount.HasValue && m.EndingAmount != 0);
                        //if (filterByCode == null || filterByCode.Count == 0) //如果该父节点没有清单明细，则跳过
                        //    continue;
                        PrjAmountBoiRpt sum = new PrjAmountBoiRpt();
                        sum.ItemCode = boi.ItemCode;
                        sum.IItemCoe = boi.IItemCoe;
                        sum.ItemName = boi.ItemName;
                        sum.CtrctAmount = Math.Round(boi.CtrctAmount, 0, MidpointRounding.AwayFromZero);
                        sum.LatestAmount = Math.Round(boi.LatestAmount, 0, MidpointRounding.AwayFromZero);
                        sum.Sequence = 9999;
                        if (filterByCode != null && filterByCode.Count > 0)
                        {
                            //sum.CtrctAmount = filterByCode.Sum(m => Math.Round((m.CtrctAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                            //sum.LatestAmount = filterByCode.Sum(m => Math.Round((m.LatestAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                            sum.StartingAmount = filterByCode.Sum(m => Math.Round((m.StartingAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                            sum.Amount = filterByCode.Sum(m => Math.Round((m.Amount ?? 0), 0, MidpointRounding.AwayFromZero));
                            sum.EndingAmount = sum.StartingAmount + sum.Amount;
                            //2019-10-21
                            //sum.CtrctAmount = filterByCode.Sum(m => m.CtrctAmount ?? 0);
                            //sum.LatestAmount = filterByCode.Sum(m => m.LatestAmount ?? 0);
                            //sum.EndingAmount = filterByCode.Sum(m => m.EndingAmount ?? 0);
                            //sum.StartingAmount = filterByCode.Sum(m => m.StartingAmount ?? 0);
                        }
                        lstSummary2.Add(sum);
                    }
                }
                //添加总体筛选后合计行，筛选所有至本期末完成有值且不等于0的项
                PrjAmountBoiRpt summary2 = new PrjAmountBoiRpt();
                summary2.ItemCode = "999";
                summary2.IItemCoe = "999";
                summary2.ItemName = "合计：";
                summary2.Sequence = 9999;
                List<PrjAmountBoiRpt> filterLst = result.lstBoiPayRpt.FindAll(m => m.EndingAmount.HasValue && m.EndingAmount != 0);
                //summary2.CtrctAmount = result.lstBoiPayRpt.Sum(m => Math.Round((m.CtrctAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                //summary2.LatestAmount = result.lstBoiPayRpt.Sum(m => Math.Round((m.LatestAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                summary2.CtrctAmount = parentNodes.Sum(m => Math.Round(m.CtrctAmount, 0, MidpointRounding.AwayFromZero));
                summary2.LatestAmount = parentNodes.Sum(m => Math.Round(m.LatestAmount, 0, MidpointRounding.AwayFromZero));
                summary2.StartingAmount = result.lstBoiPayRpt.Sum(m => Math.Round((m.StartingAmount ?? 0), 0, MidpointRounding.AwayFromZero));
                summary2.Amount = result.lstBoiPayRpt.Sum(m => Math.Round((m.Amount ?? 0), 0, MidpointRounding.AwayFromZero));
                summary2.EndingAmount = summary2.StartingAmount+ summary2.Amount;
                //2019-10-21
                //summary2.CtrctAmount = result.lstBoiPayRpt.Sum(m => m.CtrctAmount ?? 0);
                //summary2.LatestAmount = result.lstBoiPayRpt.Sum(m => m.LatestAmount ?? 0);
                //summary2.EndingAmount = result.lstBoiPayRpt.Sum(m => m.EndingAmount ?? 0);
                //summary2.StartingAmount = result.lstBoiPayRpt.Sum(m => m.StartingAmount ?? 0);
                //summary2.Amount = result.lstBoiPayRpt.Sum(m => m.Amount ?? 0);

                result.lstBoiPayRpt.AddRange(lstSummary1);
                result.lstBoiPayRpt.Add(summary1);
                result.lstBoiPayRpt.AddRange(lstSummary2);
                result.lstBoiPayRpt.Add(summary2);
                result.lstBoiPayRpt = result.lstBoiPayRpt.OrderBy(x => x.ItemCode).ToList();
                result.lstWbsBoiRpt = hdDbCmdManager.QueryForList<PrjAmountWbsBoiRpt>(@"SELECT a.Id
                                                                                              ,a.ProjectNo
                                                                                              ,a.PrjamountNo
                                                                                              ,a.Periods
                                                                                              ,a.PeriodsName
                                                                                              ,a.WbsLineNo
                                                                                              ,a.WbsLineCode
                                                                                              ,a.WbsLineName
                                                                                              ,a.ItemNo
                                                                                              ,a.ItemCode
                                                                                              ,a.ItemName
                                                                                              ,a.Uom
                                                                                              ,a.Qty
                                                                                              ,a.Price
                                                                                              ,a.Amount
                                                                                              ,a.AmountEx
                                                                                              ,a.Sequence
                                                                                              ,a.StatId
                                                                                              ,a.WbsRptCode
                                                                                              ,b.IItemCoe
                                                                                         FROM [ERP_Subpay].[dbo].[rpt_prjamountwbsboirptdetail] a
                                                                                         LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_boi b ON a.ItemNo = b.ItemNo AND b.Edit = 1
                                                                                        WHERE a.PrjamountNo = @PrjamountNo AND a.StatId = 1 order by a.ItemCode", System.Data.CommandType.Text, cmds.ToArray());
                if (result.lstWbsBoiRpt != null && result.lstWbsBoiRpt.Count > 0)
                    result.lstWbsBoiRpt.ForEach(m=> {
                        if (m.ItemName == "本项合计")
                        {
                            m.IItemCoe = null;
                            m.WbsRptCode = null;
                            m.Uom = null; 
                        }
                    });
                result.lstWbsRpt = hdDbCmdManager.QueryForList<PrjAmountWbsRpt>(@"SELECT a.Id
                                                                                        ,a.ProjectNo
                                                                                        ,a.PrjamountNo
                                                                                        ,a.Periods
                                                                                        ,a.PeriodsName
                                                                                        ,a.WbsLineNo
                                                                                        ,a.WbsLineCode
                                                                                        ,b.WbsLineName as WbsLineName
                                                                                        ,a.IsChange
                                                                                        ,a.CtrcStarEndNo
                                                                                        ,b.WbsLineName as Part
                                                                                        ,a.FigureNo
                                                                                        ,a.Certificate
                                                                                        ,a.Description
                                                                                        ,a.StatId
                                                                                        ,a.RecordValidity
                                                                                        ,a.CreatedBy
                                                                                        ,a.CreateDate
                                                                                        ,a.UpdatedBy
                                                                                        ,a.RecordDate
                                                                                        ,a.inWorkflow
                                                                                        ,a.WbsRptCode
                                                                                   FROM [ERP_Subpay].[dbo].[rpt_prjamountwbsrpt] a
                                                                                   LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_wbsline b ON a.WbsLineNo = b.WbsLineNo AND b.Edit = 1
                                                                                   LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_wbsline c ON c.WbsSysCode = b.ParentCode AND c.ProjectNo = b.ProjectNo AND c.Edit = 1 
                                                                                  WHERE a.PrjamountNo = @PrjamountNo AND a.StatId = 1 order by a.WbsRptCode", System.Data.CommandType.Text, cmds.ToArray());
            }
            return result;
        }

        /// <summary>
        /// 获取中间计量表实例
        /// </summary>
        /// <param name="WbsLineNo"></param>
        /// <returns></returns>
        public PrjAmountWbsRpt GetWbsRpt(string WbsLineNo, String PrjAmountNo)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@WbsLineNo", WbsLineNo));
            cmds.Add(new CmdParameter("@PrjAmountNo", PrjAmountNo));
            PrjAmountWbsRpt result = hdDbCmdManager.QueryForFirstRow<PrjAmountWbsRpt>(@"SELECT a.Id
                                                                                            ,a.ProjectNo
                                                                                            ,a.PrjamountNo
                                                                                            ,a.Periods
                                                                                            ,a.PeriodsName
                                                                                            ,a.WbsLineNo
                                                                                            ,a.WbsLineCode
                                                                                            ,b.WbsLineName as WbsLineName
                                                                                            ,a.IsChange
                                                                                            ,a.CtrcStarEndNo
                                                                                            ,b.WbsLineName as Part
                                                                                            ,a.FigureNo
                                                                                            ,a.Certificate
                                                                                            ,a.Description
                                                                                            ,a.StatId
                                                                                            ,a.RecordValidity
                                                                                            ,a.CreatedBy
                                                                                            ,a.CreateDate
                                                                                            ,a.UpdatedBy
                                                                                            ,a.RecordDate
                                                                                            ,a.inWorkflow
                                                                                            ,a.WbsRptCode
                                                                                       FROM [ERP_Subpay].[dbo].[rpt_prjamountwbsrpt] a
                                                                                       LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_wbsline b ON a.WbsLineNo = b.WbsLineNo AND b.Edit = 1
                                                                                       LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_wbsline c ON c.WbsSysCode = b.ParentCode AND c.ProjectNo = b.ProjectNo AND c.Edit = 1 
                                                                                      WHERE a.PrjamountNo = @PrjamountNo AND a.WbsLineNo = @WbsLineNo AND a.StatId = 1", System.Data.CommandType.Text, cmds.ToArray());
            if (result != null)
            {
                result.detail = hdDbCmdManager.QueryForList<PrjAmountWbsRptDetail>(@"SELECT DISTINCT 
                                                                            c.StartStakesNo,
		                                                                    c.EndStakesNo,
		                                                                    d.PrjApplyQty AS PrjApplyQtyDetail,
		                                                                    d.PrjApplyAmount AS PrjApplyAmountDetail,
		                                                                    d.SupervisionQty AS SupervisionQtyDetail,
		                                                                    d.SupervisionAmount AS SupervisionAmountDetail,
		                                                                    d.OwnerQty AS OwnerQtyDetail,
		                                                                    d.OwnerAmount AS OwnerAmountDetail,
		                                                                    a.Id,
		                                                                    a.ProjectNo,
		                                                                    a.PrjamountNo,
		                                                                    a.WbsLineNo,
		                                                                    a.ItemNo,
		                                                                    a.ItemCode,
		                                                                    b.IItemCoe,
		                                                                    a.ItemName,
		                                                                    a.Uom,
		                                                                    a.PrjPart,
		                                                                    a.Qty,
		                                                                    a.Price,
		                                                                    a.Amount,
		                                                                    a.AmountEx,
		                                                                    a.Sequence,
		                                                                    a.StatId,
		                                                                    a.ApplyQty,
		                                                                    a.EndingApplyQty,
		                                                                    a.StartingApplyAmount,
		                                                                    a.ApplyAmount,
		                                                                    a.EndingApplyAmount,
		                                                                    a.StartingSupervisionQty,
		                                                                    a.SupervisionQty,
		                                                                    a.EndingSupervisionQty,
		                                                                    a.StartingSupervisionAmount,
		                                                                    a.SupervisionAmount,
		                                                                    a.EndingSupervisionAmount,
		                                                                    a.StartingOwnerQty,
		                                                                    a.OwnerQty,
		                                                                    a.EndingOwnerQty,
		                                                                    a.StartingOwnerAmount,
		                                                                    a.OwnerAmount,
		                                                                    a.EndingOwnerAmount,
		                                                                    a.StartingApplyAmountEx,
		                                                                    a.ApplyAmountEx,
		                                                                    a.EndingApplyAmountEx,
		                                                                    a.StartingSupervisionAmountEx,
		                                                                    a.SupervisionAmountEx,
		                                                                    a.EndingSupervisionAmountEx,
		                                                                    a.StartingOwnerAmountEx,
		                                                                    a.OwnerAmountEx,
		                                                                    a.EndingOwnerAmountEx,
		                                                                    a.StartingApplyQty
	                                                                    FROM [ERP_Subpay].[dbo].[rpt_prjamountwbsrptdetail] a
	                                                                    LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_boi b ON a.ItemNo = b.ItemNo AND b.Edit = 1
	                                                                    LEFT OUTER JOIN ERP_BoQ.dbo.gl_cntrct_wbsline c ON a.WbsLineNo = c.WbsLineNo AND c.Edit = 1
	                                                                    LEFT OUTER JOIN ERP_Subpay.dbo.subp_prjamountDetail d ON a.PrjamountNo = d.PrjamountNo AND a.WbsLineNo = d.WbsLineNo AND a.ItemNo = d.ItemNo AND d.Edit = 1
                                                                    WHERE a.WbsLineNo = @WbsLineNo AND a.PrjAmountNo = @PrjamountNo AND a.StatId = 1 order by a.ItemCode", System.Data.CommandType.Text, cmds.ToArray());

            }
            return result;
        }

        /// <summary>
        /// 获取签名图片
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public byte[] GetSignImg(string LoginName)
        {
            byte[] result = { };
           
            DataSet ds = hdDbCmdManager.QueryForDataSet("SELECT SignPhoto FROM ERP_Identity.Auth.Gpuser WHERE LoginName = @LoginName AND StatId = 1", CommandType.Text, new CmdParameter[]
                { new CmdParameter("@LoginName", LoginName) });

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               result = (byte[])(ds.Tables[0].Rows[0][0]);

            }
            return result;
        }
    }
}
