using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    public partial class PrjAmountBoiRpt
    {
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
        public string PrjamountNo
        {
            get;
            set;
        }
        public Nullable<int> Periods
        {
            get;
            set;
        }
        public string PeriodsName
        {
            get;
            set;
        }
        public string ContractNo
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
        public string ItemName
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
        public Nullable<decimal> CtrctPrice
        {
            get;
            set;
        }
        public Nullable<decimal> CtrctAmount
        {
            get;
            set;
        }
        public Nullable<decimal> CtrctAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> ChangeQty
        {
            get;
            set;
        }
        public Nullable<decimal> RevisionQty
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
        public Nullable<decimal> LatestAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> EndingQty
        {
            get;
            set;
        }
        public Nullable<decimal> EndingAmount
        {
            get;
            set;
        }
        public Nullable<decimal> EndingAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> StartingQty
        {
            get;
            set;
        }
        public Nullable<decimal> StartingAmount
        {
            get;
            set;
        }
        public Nullable<decimal> StartingAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> AmountQty
        {
            get;
            set;
        }
        public Nullable<decimal> Amount
        {
            get;
            set;
        }
        public Nullable<decimal> AmountEx
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        /// <summary>
        /// 序号， 1：普通数据， 9998：筛选前合计，9999：筛选后合计
        /// </summary>
        public int Sequence
        {
            get;
            set;
        }
        public int StatId
        {
            get;
            set;
        }
        public bool RecordValidity
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public DateTime CreateDate
        {
            get;
            set;
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public DateTime RecordDate
        {
            get;
            set;
        }
        public DateTime RowPointer
        {
            get;
            set;
        }
        public bool inWorkflow
        {
            get;
            set;
        }        
    }

}

