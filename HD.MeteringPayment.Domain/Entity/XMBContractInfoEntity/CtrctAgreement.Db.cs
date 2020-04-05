using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.BaseInfoEntity
{
    public partial class CtrctAgreement
    {
        /// <summary>
        /// 补充ID
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 合同NO
        /// </summary>
        public string ContractNo
        {
            get;
            set;
        }
        /// <summary>
        /// 合同CODE
        /// </summary>
        public string ContractCode
        {
            get;
            set;
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName
        {
            get;
            set;
        }
        /// <summary>
        /// 变更no
        /// </summary>
        public string AgreementNo
        {
            get;
            set;
        }
        /// <summary>
        /// 补充code
        /// </summary>
        public string AgreementCode
        {
            get;
            set;
        }
        /// <summary>
        /// 补充名称
        /// </summary>
        public string AgreementName
        {
            get;
            set;
        }
        /// <summary>
        /// 补充金额
        /// </summary>
        public Nullable<decimal> Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 币别名称
        /// </summary>
        public string Currency
        {
            get;
            set;
        }
        /// <summary>
        /// 合同总价
        /// </summary>
        public Nullable<decimal> TotalAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 变更时间
        /// </summary>
        public Nullable<DateTime> SignDate
        {
            get;
            set;
        }
        /// <summary>
        /// 变更内容
        /// </summary>
        public string AgreementContent
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
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
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime RecordDate
        {
            get;
            set;
        }
        //public datetime2 RowPointer
        //{
        //    get;
        //    set;
        //}
        public bool inWorkflow
        {
            get;
            set;
        }
        /// <summary>
        /// 合同ID
        /// </summary>
        public Nullable<int> OldContractId
        {
            get;
            set;
        }          
    }
}
