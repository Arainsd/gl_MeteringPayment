using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    public partial class PrjAmountWbsRptDetail
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
        public string WbsLineNo
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
        public string PrjPart
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
        public Nullable<decimal> StartingApplyQty
        {
            get;
            set;
        }
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

