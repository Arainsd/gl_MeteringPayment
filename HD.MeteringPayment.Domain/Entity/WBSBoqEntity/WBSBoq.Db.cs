using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.WBSBoqEntity
{
    public partial class WBSBoq
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

        /// <summary>
        /// 标段编号
        /// </summary>
        public string BidNo
        {
            get;
            set;
        }

        /// <summary>
        /// wbs清单头编号
        /// </summary>
        public string WbsNo
        {
            get;
            set;
        }
        /// <summary>
        /// wbs清单名称
        /// </summary>
        public string WbsName
        {
            get;
            set;
        }
        /// <summary>
        /// 总金额
        /// </summary>
        public Nullable<decimal> TotalAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 总金额（人民币）
        /// </summary>
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
        /// <summary>
        /// 清单状态 1未锁定，2已锁定，3变更中
        /// </summary>
        public Nullable<int> ExecuteStat
        {
            get;
            set;
        }
        //public bool Edit
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
        public bool inWorkflow
        {
            get;
            set;
        }
        public int WfdefId
        {
            get;
            set;
        }
        public int RefCategory
        {
            get;
            set;
        }

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
    }
}
