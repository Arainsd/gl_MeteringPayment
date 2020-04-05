using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    public partial class PrjAmountRpt
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
        public string ContractCode
        {
            get;
            set;
        }
        public string ContractName
        {
            get;
            set;
        }
        public Nullable<int> ReportPeriods
        {
            get;
            set;
        }
        public string CtrcStarEndNo
        {
            get;
            set;
        }
        public string CtrcPrjName
        {
            get;
            set;
        }
        public string OwnerDeptNo
        {
            get;
            set;
        }
        public string OwnerDeptCode
        {
            get;
            set;
        }
        public string OwnerDeptName
        {
            get;
            set;
        }
        public string SupDeptNo
        {
            get;
            set;
        }
        public string SupDeptCode
        {
            get;
            set;
        }
        public string SupDeptName
        {
            get;
            set;
        }
        public string CtrcDeptNo
        {
            get;
            set;
        }
        public string CtrcDeptCode
        {
            get;
            set;
        }
        public string CtrcDeptName
        {
            get;
            set;
        }
        public string PrepareBy
        {
            get;
            set;
        }
        public Nullable<DateTime> PrepareDate
        {
            get;
            set;
        }
        public string PrepareDeptNo
        {
            get;
            set;
        }
        public string PrepareDeptName
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

