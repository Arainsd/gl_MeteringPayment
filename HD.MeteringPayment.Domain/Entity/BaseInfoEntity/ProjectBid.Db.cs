using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.BaseInfoEntity
{
    public partial class ProjectBid
    {
        public int Id
        {
            get;
            set;
        }
        public string BidNo
        {
            get;
            set;
        }
        public string BidName
        {
            get;
            set;
        }
        public string ParentNo
        {
            get;
            set;
        }
        public Nullable<int> Category
        {
            get;
            set;
        }
        public string SupervisorNo
        {
            get;
            set;
        }
        public string SupervisorName
        {
            get;
            set;
        }
        public string AdminOfficeNo
        {
            get;
            set;
        }
        public string AdminOfficeName
        {
            get;
            set;
        }
        public string ContractorNo
        {
            get;
            set;
        }
        public string ContractorName
        {
            get;
            set;
        }
        public string ProjectNo
        {
            get;
            set;
        }
        public string ProjectName
        {
            get;
            set;
        }
        public string RelOrgNo
        {
            get;
            set;
        }
        public string RelOrgName
        {
            get;
            set;
        }
        public string CompanyNo
        {
            get;
            set;
        }
        public string CompanyName
        {
            get;
            set;
        }
        public string Description
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
        public string BidCode
        {
            get;
            set;
        }
        public string BidVisibleCode
        {
            get;
            set;
        }
        /// <summary>
        /// 承包单位
        /// </summary>
        public string EngineerProjectNo
        {
            get;
            set;
        }
        /// <summary>
        /// 承包单位
        /// </summary>
        public string EngineerProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 设计单位
        /// </summary>
        public string ConstructDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 设计单位
        /// </summary>
        public string ConstructDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 合同段
        /// </summary>
        public string ContractSectionNo
        {
            get;
            set;
        }
        /// <summary>
        /// 合同段
        /// </summary>
        public string ContractSectionName
        {
            get;
            set;
        }
        /// <summary>
        /// 监理单位
        /// </summary>
        public string SupervisorDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 监理单位
        /// </summary>
        public string SupervisorDeptName
        {
            get;
            set;
        }
    }
}
