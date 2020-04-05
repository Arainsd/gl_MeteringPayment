using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;
using Hondee.Common.Attributes;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    /// <summary>
    /// 计量报表头
    /// </summary>
    public partial class PrjAmountRpt : BaseEntity<PrjAmountRpt>
    {
        public PrjAmountRpt() 
        {
            lstPayRpt = new List<PrjAmountPayRpt>();
            lstBoiPayRpt = new List<PrjAmountBoiRpt>();
            lstWbsBoiRpt = new List<PrjAmountWbsBoiRpt>();
            lstWbsRpt = new List<PrjAmountWbsRpt>();
        }
        /// <summary>
        /// 是否为201911之后数据
        /// </summary>
        [DBField(false)]
        public bool IsNewData
        {
            get
            {
                int value = 1;
                return Int32.TryParse(this.PeriodsName, out value) && value > 201910 || lstPayRpt!=null&& lstPayRpt.Count>0 && lstPayRpt.Exists(x=>x.ItemName== "质量保证金");
            }
        }
        /// <summary>
        /// 截止日期
        /// </summary>
        [DBField(false)]
        public string PEndDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.PeriodsName))
                    return this.PrepareDate.HasValue ? string.Format("{0}年{1}月20日", this.PrepareDate.Value.Year, this.PrepareDate.Value.Month) : null;
                else return string.Format("{0}年{1:d2}月20日", int.Parse(this.PeriodsName) / 100, int.Parse(this.PeriodsName) % 100);
            }
        }
        /// <summary>
        /// 广州从化至清远连州高速公路
        /// </summary>
        [DBField(false)]
        public string PrtTile
        {
            get
            {
                return "广州从化至清远连州高速公路";
            }
        }
        /// <summary>
        /// 本期应支付金额
        /// </summary>
        [DBField(false)]
        public decimal ThisPeriodShouldPay
        {
            get;
            set;
        }

        [DBField(false)]
        public String ThisPeriodPay
        {
            get;
            set;
        }
        #region 视图字段
        /// <summary>
        /// 承办人
        /// </summary> 
        public String ContractorSign { get; set; }
        public byte[] ContractorSignImg { get; set; }
        /// <summary>
        /// 专业监理工程师
        /// </summary> 
        public String SupervisionSign_zy { get; set; }
        public byte[] SupervisionSign_zyImg { get; set; }
        /// <summary>
        /// 合同监理工程师
        /// </summary> 
        public String SupervisionSign_ht { get; set; }
        public byte[] SupervisionSign_htImg { get; set; }
        /// <summary>
        /// 总监
        /// </summary> 
        public String SupervisionSign { get; set; }
        public byte[] SupervisionSignImg { get; set; }
        /// <summary>
        /// 管理处专业工程师
        /// </summary> 
        public String SupervisionSign_glzy { get; set; }
        public byte[] SupervisionSign_glzyImg { get; set; }
        /// <summary>
        /// 管理处主任
        /// </summary> 
        public String SupervisionSign_glzr { get; set; }
        public byte[] SupervisionSign_glzrImg { get; set; }
        /// <summary>
        /// 业主计量工程师
        /// </summary> 
        public String BusDeptSign { get; set; }
        public byte[] BusDeptSignImg { get; set; }
        /// <summary>
        /// 业主合约部经理
        /// </summary> 
        public String BusMngSign { get; set; }
        public byte[] BusMngSignImg { get; set; }
        /// <summary>
        /// 业主生产副总经理
        /// </summary> 
        public String BusLeaderSign_fu { get; set; }

        public byte[] BusLeaderSign_fuImg { get; set; }
        /// <summary>
        /// 业主总经理
        /// </summary> 
        public String BusLeaderSign { get; set; }

        public byte[] BusLeaderSignImg { get; set; }
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
        /// <summary>
        /// 业主确认时间
        /// </summary>
        public Nullable<DateTime> BusLeaderSignDate { get; set; }
        /// <summary>
        /// 是否发布（完成）
        /// </summary>
        public Boolean Fixed { get; set; }

        /// <summary>
        /// 新审批状态 0退回/新建、1待审、2审批通过
        /// </summary>
        public Nullable<int> ApprovalStat
        {
            get;
            set;
        }
        /// <summary>
        /// 数据提交状态：0在项目标段、1在承包单位 、2监理、3在业主合约部、4在业主总经理
        /// </summary>
        public Nullable<int> ExecuteStat
        {
            get;
            set;
        }
        #endregion
        #region 各报表明细
        /// <summary>
        /// 中间支付证书
        /// </summary>
        public List<PrjAmountPayRpt> lstPayRpt
        {
            get;
            set;
        }
        /// <summary>
        /// 清单支付报表
        /// </summary>
        public List<PrjAmountBoiRpt> lstBoiPayRpt
        {
            get;
            set;
        }
        /// <summary>
        /// 中间计量支付汇总表
        /// </summary>
        public List<PrjAmountWbsBoiRpt> lstWbsBoiRpt
        {
            get;
            set;
        }
        /// <summary>
        /// 中间计量表列表
        /// </summary>
        public List<PrjAmountWbsRpt> lstWbsRpt
        {
            get;
            set;
        }
        #endregion 
        [DBField(false)]

        public string periodsStr
        {
            get
            {
                return string.Format("第{0}期", Periods);
            }
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
                                return "业主合约部-退回";
                            }
                            else if (ApprovalStat == 1)
                            {
                                return "业主合约部-待审";
                            }
                        }
                        break;
                    case 4:
                        {
                            return "通过";
                        }
                    default:
                        {
                            return "";
                        }
                }
                return "未知状态";
            }
        }
    }
}
