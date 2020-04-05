using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractEntity
{
    public partial class XMBContractInfo
    {
        /// <summary>
        /// 合同Id
        /// </summary>
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
        /// 合同编号
        /// </summary>
        public string ProjectCode
        {
            get;
            set;
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ProjectName
        {
            get;
            set;
        }
        public string ContractNo
        {
            get;
            set;
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractCode
        {
            get;
            set;
        }
        public string CategoryNo
        {
            get;
            set;
        }
        public string Category
        {
            get;
            set;
        }
        /// <summary>
        /// 内部合同编号
        /// </summary>
        public string InnerContractCode
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
        /// 业主名称
        /// </summary>
        public string CustomerNo
        {
            get;
            set;
        }
        /// <summary>
        /// 业主名称
        /// </summary>
        public string CustomerCode
        {
            get;
            set;
        }
        /// <summary>
        /// 业主名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public Nullable<decimal> Amount
        {
            get;
            set;
        }
        public Nullable<decimal> AftAmount
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
        /// 签约时间
        /// </summary>
        public Nullable<DateTime> SignDate
        {
            get;
            set;
        }
        /// <summary>
        /// 签约单位
        /// </summary>
        public string SignDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 签约单位
        /// </summary>
        public string SignDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 实施单位
        /// </summary>
        public string EngnrDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 实施单位
        /// </summary>
        public string EngnrDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string ProjectType
        {
            get;
            set;
        }
        /// <summary>
        /// 工期
        /// </summary>
        public string Duration
        {
            get;
            set;
        }
        /// <summary>
        /// 开工日期
        /// </summary>
        public Nullable<DateTime> StartingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 预计峻工日期
        /// </summary>
        public Nullable<DateTime> PlanEndingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 合同负责人
        /// </summary>
        public string Leader
        {
            get;
            set;
        }
        /// <summary>
        /// 合同保存地点
        /// </summary>
        public string FileLocation
        {
            get;
            set;
        }
        /// <summary>
        /// 项目分类
        /// </summary>
        public string ProjectClass
        {
            get;
            set;
        }
        /// <summary>
        /// 合同收到时间
        /// </summary>
        public Nullable<DateTime> ReceiveCntrctFileDate
        {
            get;
            set;
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string DeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string DeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 投标申请
        /// </summary>
        public string BidApplicationNo
        {
            get;
            set;
        }
        /// <summary>
        /// 投标申请编号
        /// </summary>
        public string BidApplicationCode
        {
            get;
            set;
        }
        /// <summary>
        /// 国内境外
        /// </summary>
        public string DomOvsea
        {
            get;
            set;
        }
        /// <summary>
        /// 项目类别
        /// </summary>
        public bool isFocus
        {
            get;
            set;
        }
        /// <summary>
        /// 项目性质
        /// </summary>
        public string ProjectType1
        {
            get;
            set;
        }
        /// <summary>
        /// 施工阶段
        /// </summary>
        public string EngnrPhase
        {
            get;
            set;
        }
        /// <summary>
        /// 所属公司编号
        /// </summary>
        public string CompanyNo
        {
            get;
            set;
        }
        /// <summary>
        /// 所属公司名称
        /// </summary>
        public string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 工程地点
        /// </summary>
        public string ProjectAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 工程结构特征
        /// </summary>
        public string EngnrStructure
        {
            get;
            set;
        }
        /// <summary>
        /// 设计单位
        /// </summary>
        public string DesignDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 设计单位
        /// </summary>
        public string DesignDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 监理单位
        /// </summary>
        public string SupervisionDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 监理单位
        /// </summary>
        public string SupervisionDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目副经理
        /// </summary>
        public string PrjDeputyManager
        {
            get;
            set;
        }
        /// <summary>
        /// 项目书记
        /// </summary>
        public string PrjAudit
        {
            get;
            set;
        }
        /// <summary>
        /// 交工日期
        /// </summary>
        public Nullable<DateTime> AcceptanceDate
        {
            get;
            set;
        }
        /// <summary>
        /// 竣工日期
        /// </summary>
        public Nullable<DateTime> CompleteDate
        {
            get;
            set;
        }
        /// <summary>
        /// 洲
        /// </summary>
        public string Continent
        {
            get;
            set;
        }
        /// <summary>
        /// 地区
        /// </summary>
        public string Area
        {
            get;
            set;
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country
        {
            get;
            set;
        }
        /// <summary>
        /// 片区
        /// </summary>
        public string Region
        {
            get;
            set;
        }
        /// <summary>
        /// 省
        /// </summary>
        public string Province
        {
            get;
            set;
        }
        /// <summary>
        /// 市
        /// </summary>
        public string City
        {
            get;
            set;
        }
        /// <summary>
        /// 区
        /// </summary>
        public string District
        {
            get;
            set;
        }
        /// <summary>
        /// 二级类别
        /// </summary>
        public string PrjClassType
        {
            get;
            set;
        }
        /// <summary>
        /// 项目概述
        /// </summary>
        public string Overview
        {
            get;
            set;
        }
        /// <summary>
        /// 合同约定支付比例
        /// </summary>
        public string PayoutRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 合同约定保修期
        /// </summary>
        public string WarrantyPeriod
        {
            get;
            set;
        }
        /// <summary>
        /// 合同约定保留期
        /// </summary>
        public string RetentionPeriod
        {
            get;
            set;
        }
        /// <summary>
        /// 合同约定付款期
        /// </summary>
        public string PaymentDate
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
        /// <summary>
        /// EDITFLAG
        /// </summary>
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
        public bool StatId
        {
            get;
            set;
        }
        public bool RecordValidity
        {
            get;
            set;
        }
        /// <summary>
        /// 录入时间
        /// </summary>
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
        /// 是否显示
        /// </summary>
        public bool isShow
        {
            get;
            set;
        }
        /// <summary>
        /// BIZ_CONTRACT_ID
        /// </summary>
        public Nullable<int> OldId
        {
            get;
            set;
        }
        /// <summary>
        /// 工程项目ID
        /// </summary>
        public Nullable<int> OldProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 业主ID
        /// </summary>
        public Nullable<int> OldCustomerId
        {
            get;
            set;
        }
        /// <summary>
        /// 币别
        /// </summary>
        public Nullable<int> OldCurrencyId
        {
            get;
            set;
        }
        /// <summary>
        /// 投标申请ID
        /// </summary>
        public Nullable<int> OldBidApplicationId
        {
            get;
            set;
        }
        /// <summary>
        /// 机构id
        /// </summary>
        public Nullable<int> OldDeptId
        {
            get;
            set;
        }
        /// <summary>
        /// 所属公司
        /// </summary>
        public Nullable<int> OldCompanyId
        {
            get;
            set;
        }

    }
}
