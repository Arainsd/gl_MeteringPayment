using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity
{
    public partial class PrjAmountDetail
    {
        /// <summary>
        /// 申报数量
        /// </summary>
        public Nullable<decimal> PrjApplyQty
        {
            get;
            set;
        }
        /// <summary>
        /// 申报金额
        /// </summary>
        public Nullable<decimal> PrjApplyAmount
        {
            get;
            set;
        }
        public Nullable<decimal> EndingPrjApplyQty
        {
            get;
            set;
        }
        public Nullable<decimal> EndingPrjApplyAmount
        {
            set;
            get;
        }
        public int Id
        {
            get;
            set;
        }
        public string ProjectNo
        {
            get;
            set;
        }
        public string BidNo
        {
            get;
            set;
        }
        public string WbsNo
        {
            get;
            set;
        }
        public string WbsLineNo
        {
            get;
            set;
        }
        public string WbsLineCode
        {
            get;
            set;
        }
        /// <summary>
        /// 中间交工证书号
        /// </summary>
        public String MidCertifiNum
        {
            get;
            set;
        }
        public string WbsLineName
        {
            get;
            set;
        }
        public string WbsSysCode
        {
            get;
            set;
        }
        public string WbsParentCode
        {
            get;
            set;
        }
        public string PrjamountNo
        {
            get;
            set;
        }
        public string PrjamountdetailNo
        {
            get;
            set;
        }
        public string PrjamountdetailCode
        {
            get;
            set;
        }
        public string PrjamountdetailName
        {
            get;
            set;
        }
        public string ItemNo
        {
            get;
            set;
        }
        public string ItemCode
        {
            get;
            set;
        }
        public string IItemCoe
        {
            get;
            set;
        }
        public string ItemName
        {
            get;
            set;
        }
        public string ParentCode
        {
            get;
            set;
        }
        public string WbsItemCode
        {
            get;
            set;
        }
        public string Uom
        {
            get;
            set;
        }
        public Nullable<decimal> CtrctQty
        {
            get;
            set;
        }
        public Nullable<decimal> CtrctPrjPrice
        {
            get;
            set;
        }
        public Nullable<decimal> CtrctAmount
        {
            get;
            set;
        }
        public Nullable<decimal> LatestQty
        {
            get;
            set;
        }
        public Nullable<decimal> LatestPrice
        {
            get;
            set;
        }
        public Nullable<decimal> LatestAmount
        {
            get;
            set;
        }
        public Nullable<decimal> ChangeQty
        {
            get;
            set;
        }
        public Nullable<decimal> ChangePrice
        {
            get;
            set;
        }
        public Nullable<decimal> ChangeAmount
        {
            get;
            set;
        }
        public string Currency
        {
            get;
            set;
        }
        public string CurrencyCode
        {
            get;
            set;
        }
        public Nullable<decimal> ExchangeRate
        {
            get;
            set;
        }
        public Nullable<decimal> StartingApplyQty
        {
            get;
            set;
        }
        /// <summary>
        /// 实际量
        /// </summary>
        public Nullable<decimal> ApplyQty
        {
            get;
            set;
        }
        public Nullable<decimal> EndingApplyQty
        {
            get;
            set;
        }
        public Nullable<decimal> StartingApplyAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 实际金额
        /// </summary>
        public Nullable<decimal> ApplyAmount
        {
            get;
            set;
        }
        public Nullable<decimal> EndingApplyAmount
        {
            get;
            set;
        }
        public Nullable<decimal> StartingSupervisionQty
        {
            get;
            set;
        }
        /// <summary>
        /// 监理完成量
        /// </summary>
        public Nullable<decimal> SupervisionQty
        {
            get;
            set;
        }
        public Nullable<decimal> EndingSupervisionQty
        {
            get;
            set;
        }
        public Nullable<decimal> StartingSupervisionAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 监理完成价
        /// </summary>
        public Nullable<decimal> SupervisionAmount
        {
            get;
            set;
        }
        public Nullable<decimal> EndingSupervisionAmount
        {
            get;
            set;
        }
        public Nullable<decimal> StartingOwnerQty
        {
            get;
            set;
        }
        /// <summary>
        /// 业主完成量
        /// </summary>
        public Nullable<decimal> OwnerQty
        {
            get;
            set;
        }
        public Nullable<decimal> EndingOwnerQty
        {
            get;
            set;
        }
        public Nullable<decimal> StartingOwnerAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 业主完成价
        /// </summary>
        public Nullable<decimal> OwnerAmount
        {
            get;
            set;
        }
        public Nullable<decimal> EndingOwnerAmount
        {
            get;
            set;
        }
        public Nullable<decimal> StartingApplyAmountEx
        {
            get;
            set;
        }
        /// <summary>
        /// 申报价
        /// </summary>
        public Nullable<decimal> ApplyAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> EndingApplyAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> StartingSupervisionAmountEx
        {
            get;
            set;
        }
        /// <summary>
        /// 监理完成价
        /// </summary>
        public Nullable<decimal> SupervisionAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> EndingSupervisionAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> StartingOwnerAmountEx
        {
            get;
            set;
        }
        /// <summary>
        /// 业主完成价
        /// </summary>
        public Nullable<decimal> OwnerAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> EndingOwnerAmountEx
        {
            get;
            set;
        }
        //public string ApprovalBy
        //{
        //    get;
        //    set;
        //}
        //public Nullable<DateTime> ApprovalDate
        //{
        //    get;
        //    set;
        //}
        //public Nullable<int> ApprovalStat
        //{
        //    get;
        //    set;
        //}
        //public Nullable<int> ExecuteStat
        //{
        //    get;
        //    set;
        //}
        //public Nullable<int> Version
        //{
        //    get;
        //    set;
        //}
        //public bool VersionValidity
        //{
        //    get;
        //    set;
        //}
        //public bool Edit
        //{
        //    get;
        //    set;
        //}
        //public bool Locked
        //{
        //    get;
        //    set;
        //}
        //public bool New
        //{
        //    get;
        //    set;
        //}
        //public Nullable<int> WfdefId
        //{
        //    get;
        //    set;
        //}
        //public int RefCategory
        //{
        //    get;
        //    set;
        //}
        //public int Sequence
        //{
        //    get;
        //    set;
        //}
        //public int StatId
        //{
        //    get;
        //    set;
        //}
        //public bool RecordValidity
        //{
        //    get;
        //    set;
        //}
        //public string CreatedBy
        //{
        //    get;
        //    set;
        //}
        //public DateTime CreateDate
        //{
        //    get;
        //    set;
        //}
        //public string UpdatedBy
        //{
        //    get;
        //    set;
        //}
        //public DateTime RecordDate
        //{
        //    get;
        //    set;
        //}
        //public DateTime RowPointer
        //{
        //    get;
        //    set;
        //}
        //public bool inWorkflow
        //{
        //    get;
        //    set;
        //}        
    }

}

