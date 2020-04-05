using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;
using Erp.CommonData;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity
{
    public partial class PrjAmount:BaseEntity<PrjAmount>
    {
        public PrjAmount() 
        {
            LstAmountDetail = new List<PrjAmountDetail>();
            LstAmountOther = new List<PrjAmountOther>();
            ExecuteStat = 0;
            ApprovalStat = 0;
            ShowStat = "新建";
        }
        public String StrPeriods 
        { 
            get 
            {
                return "第" + Periods + "期";
            }
        }
        #region 视图字段
        public bool Released { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ReleaseBy { get; set; }
        public bool Fixed { get; set; }
        /// <summary>
        /// 当期申报金额
        /// </summary>
        public Nullable<decimal> SumPrjAmount { get; set; }
        /// <summary>
        /// 当期监理金额
        /// </summary>
        public Nullable<decimal> SumSupervisionAmount { get; set; }
        /// <summary>
        /// 当期业主金额
        /// </summary>
        public Nullable<decimal> SumOwnerAmount { get; set; }
        /// <summary>
        /// 累计业主确认金额
        /// </summary>
        public Nullable<decimal> SumEndingOwnerAmount { get; set; }
        public Nullable<DateTime> BusLeaderSignDate { get; set; }
        #endregion

        public List<PrjAmountDetail> LstAmountDetail
        {
            get;
            set;
        }

        public List<PrjAmountOther> LstAmountOther
        {
            get;
            set;
        } 
        public string statString
        {
            get
            {
                switch (this.ExecuteStat)
                {
                    case 0:
                        {
                            if (ApprovalStat == 0)
                            {
                                return "新建";
                            }
                        }
                        break;
                    case 1:
                        {
                            if (ApprovalStat == 0)
                            {
                                return "承包单位-退回";
                            }
                            else if (ApprovalStat == 1)
                            {
                                return "承包单位-待审";
                            }
                        }
                        break;
                    case 2:
                        {
                            if (ApprovalStat == 0)
                            {
                                return "监理-退回";
                            }
                            else if (ApprovalStat == 1)
                            {
                                return "监理-待审";
                            }
                        }
                        break;
                    case 3:
                        {
                            if (ApprovalStat == 0)
                            {
                                return "项目公司合约部-退回";
                            }
                            else if (ApprovalStat == 1)
                            {
                                return "项目公司合约部-待审";
                            }
                        }
                        break;
                    case 5:
                        {
                            if (ApprovalStat == 0)
                            {
                                return "项目公司合约部经理-退回";
                            }
                            else if (ApprovalStat == 1)
                            {
                                return "项目公司合约部经理-待审";
                            }
                        }
                        break;
                    case 4:
                        {
                            if (ApprovalStat == 2 || Fixed)
                            {
                                return "通过";
                            }
                            else if (ApprovalStat == 0)
                            {
                                return "项目公司总经理-退回";
                            }
                            else if (ApprovalStat == 1)
                            {
                                return "项目公司总经理-待审";
                            }
                        }
                        break;
                    default:
                        {
                            return "";
                        }
                }
                return "未知状态";
            }
        }


        /// <summary>
        /// 设置默认值
        /// 监理界面时-判断如果是第一次开始审批，则将标段的值赋给监理，业主界面则将监理的值赋给业主
        /// <param name="LoginRoleType">1标段管理员、2监理、3业主总经理</param>
        /// </summary>
        public void SetDefaultValue(int LoginRoleType)
        {
            if (LoginRoleType == 2)  //监理
            {
                foreach (PrjAmountDetail temp in this.LstAmountDetail)
                {
                    if (this.ExecuteStat == 2 && this.EditFlag < 2)
                    {
                        if (temp.SupervisionQty != temp.PrjApplyQty || temp.SupervisionAmount != temp.PrjApplyAmount)
                        {
                            temp.SupervisionQty = temp.PrjApplyQty;
                            temp.SupervisionAmount = temp.PrjApplyAmount;
                            temp.StartingSupervisionAmount = temp.StartingApplyAmount;
                            temp.EndingSupervisionAmount = temp.EndingPrjApplyAmount;
                            temp.EndingSupervisionQty = temp.EndingPrjApplyQty;
                            temp.isChanged = true;
                        }                      
                    }

                    if (temp.ApplyQty != temp.SupervisionQty || temp.ApplyAmount != temp.SupervisionAmount)  //设置实际值
                    {
                        temp.ApplyQty = temp.SupervisionQty;
                        temp.ApplyAmount = temp.SupervisionAmount;
                        temp.EndingApplyQty = temp.EndingSupervisionQty;
                        temp.EndingApplyAmount = temp.EndingSupervisionAmount;
                        temp.isChanged = true;
                    }
                }

                //设置父节点实际值
                List<PrjAmountDetail> lstPrjAmountDetail = this.LstAmountDetail.FindAll(m => !String.IsNullOrEmpty(m.PrjamountdetailNo));
                Dictionary<String, Decimal> dicAmount = new Dictionary<string, decimal>();
                lstPrjAmountDetail.ForEach(m =>
                {
                    Decimal amount = 0;
                    if (!dicAmount.TryGetValue(m.WbsParentCode, out amount))
                    {
                        dicAmount.Add(m.WbsParentCode, ObjectHelper.GetDefaultDecimal(m.ApplyAmount));
                    }
                    else
                    {
                        dicAmount[m.WbsParentCode] = dicAmount[m.WbsParentCode] + ObjectHelper.GetDefaultDecimal(m.ApplyAmount);
                    }
                });
                foreach (String key in dicAmount.Keys)
                {
                    PrjAmountDetail prjAmount = this.LstAmountDetail.Find(m => m.WbsSysCode == key);
                    if (ObjectHelper.GetDefaultDecimal(prjAmount.ApplyAmount) != ObjectHelper.GetDefaultDecimal(dicAmount[key]))
                    {
                        prjAmount.ApplyAmount = dicAmount[key];
                        prjAmount.isChanged = true;
                    }
                }
                this.EditFlag = 2;
            }
            if (LoginRoleType == 3 && this != null && this.EditFlag < 3)
            {
                foreach (PrjAmountDetail temp in this.LstAmountDetail)
                {
                    if (this.ExecuteStat == 3)
                    {
                        if (temp.OwnerQty != temp.SupervisionQty || temp.OwnerAmount != temp.SupervisionAmount)  //设置实际值
                        {
                            temp.OwnerQty = temp.SupervisionQty;
                            temp.OwnerAmount = temp.SupervisionAmount;
                            temp.EndingOwnerAmount = temp.EndingSupervisionAmount;
                            temp.EndingOwnerQty = temp.EndingSupervisionQty;
                            temp.isChanged = true;
                        }

                        if (temp.ApplyQty != temp.OwnerQty || temp.ApplyAmount != temp.OwnerAmount)
                        {
                            temp.ApplyQty = temp.SupervisionQty;
                            temp.ApplyAmount = temp.SupervisionAmount;
                            temp.EndingApplyQty = temp.EndingOwnerQty;
                            temp.EndingApplyAmount = temp.EndingOwnerAmount;
                            temp.isChanged = true;
                        }
                    }
                }

                //设置父节点实际值
                List<PrjAmountDetail> lstPrjAmountDetail = this.LstAmountDetail.FindAll(m => !String.IsNullOrEmpty(m.PrjamountdetailNo));
                Dictionary<String, Decimal> dicAmount = new Dictionary<string, decimal>();
                lstPrjAmountDetail.ForEach(m =>
                {
                    Decimal amount = 0;
                    if (!dicAmount.TryGetValue(m.WbsParentCode, out amount))
                    {
                        dicAmount.Add(m.WbsParentCode, ObjectHelper.GetDefaultDecimal(m.ApplyAmount));
                    }
                    else
                    {
                        dicAmount[m.WbsParentCode] = dicAmount[m.WbsParentCode] + ObjectHelper.GetDefaultDecimal(m.ApplyAmount);
                    }
                });
                foreach (String key in dicAmount.Keys)
                {
                    PrjAmountDetail prjAmount = this.LstAmountDetail.Find(m => m.WbsSysCode == key);
                    if (ObjectHelper.GetDefaultDecimal(prjAmount.ApplyAmount) != ObjectHelper.GetDefaultDecimal(dicAmount[key]))
                    {
                        prjAmount.ApplyAmount = dicAmount[key];
                        prjAmount.isChanged = true;
                    }
                }
                this.EditFlag = 3;
            }
        }
    }
}
