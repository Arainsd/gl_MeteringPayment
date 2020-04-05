using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractEntity
{
    public partial class Contract
    {
        public int Id
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
        /// 合同分类
        /// </summary>
        public string Catalog
        {
            get;
            set;
        }
        /// <summary>
        /// 合同子类
        /// </summary>
        public string SubCatalog
        {
            get;
            set;
        }
        /// <summary>
        /// 类别（普通，海外，特例，小额）
        /// </summary>
        public string SubClass
        {
            get;
            set;
        }
        public Nullable<int> Year
        {
            get;
            set;
        }
        public Nullable<int> Quarter
        {
            get;
            set;
        }
        public Nullable<int> Month
        {
            get;
            set;
        }
        /// <summary>
        /// 系统编号
        /// </summary>
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
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName
        {
            get;
            set;
        }
        /// <summary>
        /// 需求部门
        /// </summary>
        public string DemandDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 需求部门(用作“第三方”字段)
        /// </summary>
        public string DemandDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 甲方名称
        /// </summary>
        public string PartAName
        {
            get;
            set;
        }
        /// <summary>
        /// 甲方负责人
        /// </summary>
        public string PartAResponser
        {
            get;
            set;
        }
        /// <summary>
        /// 甲方代理人
        /// </summary>
        public string PartAEntruster
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方办公地点
        /// </summary>
        public string PartAOfficeAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 资质专业及等级
        /// </summary>
        public string PartAQualification
        {
            get;
            set;
        }
        /// <summary>
        /// 复审时间及有效期
        /// </summary>
        public string PartAValidityTerm
        {
            get;
            set;
        }
        /// <summary>
        /// 资质号码
        /// </summary>
        public string PartACertificateNo
        {
            get;
            set;
        }
        /// <summary>
        /// 资质发证机关
        /// </summary>
        public string PartAIssuingAuthority
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方
        /// </summary>
        public string PartB
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方负责人
        /// </summary>
        public string PartBResponser
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方代理人
        /// </summary>
        public string PartBEntruster
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方办公地点
        /// </summary>
        public string PartBOfficeAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方注册地址
        /// </summary>
        public string PartBRegistAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方银行
        /// </summary>
        public string PartBBank
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方银行账号
        /// </summary>
        public string PartBBankAccount
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方描述
        /// </summary>
        public string PartBDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 资质专业及等级
        /// </summary>
        public string PartBQualification
        {
            get;
            set;
        }
        /// <summary>
        /// 复审时间及有效期
        /// </summary>
        public string PartBValidityTerm
        {
            get;
            set;
        }
        /// <summary>
        /// 资质号码
        /// </summary>
        public string PartBCertificateNo
        {
            get;
            set;
        }
        /// <summary>
        /// 资质发证机关
        /// </summary>
        public string PartBIssuingAuthority
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方联系人
        /// </summary>
        public string PartBContact
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方联系人邮编
        /// </summary>
        public string PartBContactZipCode
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方联系人电话
        /// </summary>
        public string PartBContactPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方联系人传真
        /// </summary>
        public string PartBContactFax
        {
            get;
            set;
        }
        /// <summary>
        /// 总承包人
        /// </summary>
        public string GeneralContractor
        {
            get;
            set;
        }
        /// <summary>
        /// 发包人
        /// </summary>
        public string PartyIssuingContract
        {
            get;
            set;
        }
        /// <summary>
        /// 质量等级
        /// </summary>
        public string QualityGrade
        {
            get;
            set;
        }
        /// <summary>
        /// 工程名称
        /// </summary>
        public string EngrName
        {
            get;
            set;
        }
        /// <summary>
        /// 工程地点
        /// </summary>
        public string EngrPlace
        {
            get;
            set;
        }
        /// <summary>
        /// 服务内容
        /// </summary>
        public string ContractDetail
        {
            get;
            set;
        }
        /// <summary>
        /// 收付款方向
        /// </summary>
        public Nullable<int> RecipientDirection
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
        /// <summary>
        /// 合同总价
        /// </summary>
        public Nullable<decimal> TotalAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 币别
        /// </summary>
        public string Currency
        {
            get;
            set;
        }
        /// <summary>
        /// 合同金额中文
        /// </summary>
        public string AmountChn
        {
            get;
            set;
        }
        /// <summary>
        /// 安全生产费
        /// </summary>
        public Nullable<decimal> SafetyExpense
        {
            get;
            set;
        }
        /// <summary>
        /// 币别
        /// </summary>
        public string SECurrency
        {
            get;
            set;
        }
        /// <summary>
        /// 中文-安全生产费
        /// </summary>
        public string SafetyExpenseChn
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
        /// 完工日期
        /// </summary>
        public Nullable<DateTime> EndingDate
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
        /// 签约时间
        /// </summary>
        public Nullable<DateTime> SignDate
        {
            get;
            set;
        }
        /// <summary>
        /// 签约地点
        /// </summary>
        public string SignPlace
        {
            get;
            set;
        }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string Settlement
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
        /// 区
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
        /// (小)区:如:海淀区\朝阳区等
        /// </summary>
        public string District
        {
            get;
            set;
        }
        /// <summary>
        /// 国内海外
        /// </summary>
        public string DomOvsea
        {
            get;
            set;
        }
        /// <summary>
        /// 保留金比例%
        /// </summary>
        public Nullable<decimal> RetentionRate
        {
            get;
            set;
        }
        /// <summary>
        /// 保留金
        /// </summary>
        public Nullable<decimal> RetentionAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 保修金比例%
        /// </summary>
        public Nullable<decimal> MaintenanceRate
        {
            get;
            set;
        }
        /// <summary>
        /// 保修金
        /// </summary>
        public Nullable<decimal> MaintenanceAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 合同支付比例%
        /// </summary>
        public Nullable<decimal> PaymentRate
        {
            get;
            set;
        }
        /// <summary>
        /// 合同存放地点
        /// </summary>
        public string FileLocation
        {
            get;
            set;
        }
        /// <summary>
        /// 登记人
        /// </summary>
        public string RegBy
        {
            get;
            set;
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public Nullable<DateTime> RegDate
        {
            get;
            set;
        }
        /// <summary>
        /// 拟稿人用户名
        /// </summary>
        public string PrepareBy
        {
            get;
            set;
        }
        /// <summary>
        /// 生成日期
        /// </summary>
        public Nullable<DateTime> PrepareDate
        {
            get;
            set;
        }
        /// <summary>
        /// 拟稿人部门名称
        /// </summary>
        public string PrepareDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 拟稿人部门名称
        /// </summary>
        public string PrepareDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 拟稿人部门负责人
        /// </summary>
        public string PrepareResponser
        {
            get;
            set;
        }
        /// <summary>
        /// 密级
        /// </summary>
        public string SecurityClassification
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
        /// <summary>
        /// 审批人
        /// </summary>
        public string ApprovalBy
        {
            get;
            set;
        }
        /// <summary>
        /// 审批时间
        /// </summary>
        public Nullable<DateTime> ApprovalDate
        {
            get;
            set;
        }
        /// <summary>
        /// 审批状态 1 未审核 2 已审核
        /// </summary>
        public Nullable<int> ApprovalStat
        {
            get;
            set;
        }
        /// <summary>
        /// 审批状态 1 未审核 2 已审核
        /// </summary>
        public Nullable<int> ExecuteStat
        {
            get;
            set;
        }
        /// <summary>
        /// 版本
        /// </summary>
        public Nullable<int> Version
        {
            get;
            set;
        }
        /// <summary>
        /// 版本有效？
        /// </summary>
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
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sequence
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int StatId
        {
            get;
            set;
        }
        /// <summary>
        /// 数据有效？
        /// </summary>
        public bool RecordValidity
        {
            get;
            set;
        }
        /// <summary>
        /// 拟稿人登录名
        /// </summary>
        public string CreatedBy
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdatedBy
        {
            get;
            set;
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime RecordDate
        {
            get;
            set;
        }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime RowPointer
        {
            get;
            set;
        }
        /// <summary>
        /// 是否工作流锁定？
        /// </summary>
        public bool inWorkflow
        {
            get;
            set;
        }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool isFinished
        {
            get;
            set;
        }
        /// <summary>
        /// 正式合同
        /// </summary>
        public bool isFormal
        {
            get;
            set;
        }
        /// <summary>
        /// 实际开工日期
        /// </summary>
        public Nullable<DateTime> ActualStartingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 实际完工日期
        /// </summary>
        public Nullable<DateTime> ActualEndingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 营销合同名称
        /// </summary>
        public string PrjContractNo
        {
            get;
            set;
        }
        /// <summary>
        /// 营销合同名称
        /// </summary>
        public string PrjContractName
        {
            get;
            set;
        }
        public bool isAvoid
        {
            get;
            set;
        }

    }

}

