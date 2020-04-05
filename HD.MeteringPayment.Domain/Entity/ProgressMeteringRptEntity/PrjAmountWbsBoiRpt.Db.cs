using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    public partial class PrjAmountWbsBoiRpt
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

        public string WbsLineName
        {
            get;
            set;
        }
        public string WbsRptCode
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
        public string IItemCode
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
        public Nullable<decimal> Qty
        {
            get;
            set;
        }
        public Nullable<decimal> Price
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

