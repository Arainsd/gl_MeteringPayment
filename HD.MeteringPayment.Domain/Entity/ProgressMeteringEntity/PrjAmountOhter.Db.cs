using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity
{
    public partial class PrjAmountOther
    {
        public int Id
        {
            get;
            set;
        }
        public string PrjAmountNo
        {
            get;
            set;
        }
        public string PrjAmountOtherNo
        {
            get;
            set;
        }
        public string PrjAmountOtherCode
        {
            get;
            set;
        }
        public string PrjAmountOtherName
        {
            get;
            set;
        }
        public string ParentCode
        {
            get;
            set;
        }
        public Nullable<int> Type
        {
            get;
            set;
        }
        public Nullable<int> Catalog
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
        public Nullable<decimal> StartingAmount
        {
            get;
            set;
        }
        public Nullable<decimal> Amount
        {
            get;
            set;
        }
        public Nullable<decimal> EndingAmount
        {
            get;
            set;
        }
        public Nullable<decimal> StartingAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> AmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> EndingAmountEx
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public string ApprovalBy
        {
            get;
            set;
        }
        public Nullable<DateTime> ApprovalDate
        {
            get;
            set;
        }
        public Nullable<int> ApprovalStat
        {
            get;
            set;
        }
        public Nullable<int> ExecuteStat
        {
            get;
            set;
        }
        public Nullable<int> Version
        {
            get;
            set;
        }
        public bool VersionValidity
        {
            get;
            set;
        }
        public bool Edit
        {
            get;
            set;
        }
        public bool Locked
        {
            get;
            set;
        }
        public bool New
        {
            get;
            set;
        }
        public Nullable<int> WfdefId
        {
            get;
            set;
        }
        public int RefCategory
        {
            get;
            set;
        }
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

