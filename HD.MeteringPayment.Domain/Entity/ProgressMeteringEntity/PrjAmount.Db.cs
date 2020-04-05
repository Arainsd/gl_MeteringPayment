using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity
{
    public partial class PrjAmount
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
        public string PrjamountNo
        {
            get;
            set;
        }
        public string PrjamountCode
        {
            get;
            set;
        }
        public string PrjamountName
        {
            get;
            set;
        }
        public int Periods
        {
            get;
            set;
        }
        public string PeriodsName
        {
            get;
            set;
        }
        public bool IsChange
        {
            get;
            set;
        }
        public string SubProject
        {
            get;
            set;
        }
        public string Position
        {
            get;
            set;
        }
        public string StaStopNum
        {
            get;
            set;
        }
        public string DrawingCode
        {
            get;
            set;
        }
        /// <summary>
        /// 中间交工证书号
        /// </summary>
        public string MidCertifiNum
        {
            get;
            set;
        }
        public string Remark
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
        /// <summary>
        /// 新审批状态 0退回/新建、1待审、2审批通过
        /// </summary>
        public Nullable<int> ApprovalStat
        {
            get;
            set;
        }
        /// <summary>
        /// 数据提交状态：0在项目标段、1在承包单位 、2监理、3在业主合约部（项目公司合约部）、4在业主总经理（项目公司总经理）、5在项目公司合约部经理
        /// </summary>
        public Nullable<int> ExecuteStat
        {
            get;
            set;
        }
        /// <summary>
        /// 标识某层级是否第一次审批，例如，2代表监理已经保存默认值，3代表业主已保存默认值，如果监理审批操作后该数值小于2，则将标段的值赋值给监理，业主操作同监理
        /// </summary>
        public int EditFlag
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
        public string ShowStat { get; set; }
    }
}
