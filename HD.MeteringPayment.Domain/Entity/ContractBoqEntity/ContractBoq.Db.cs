using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
    public partial class ContractBoq
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
        public string BoQNo
        {
            get;
            set;
        }
        public string BoQName
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public Nullable<decimal> TotalAmount
        {
            get;
            set;
        }
        public Nullable<decimal> TotalAmountEx
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
    }
}
