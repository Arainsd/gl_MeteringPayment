using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.BaseInfoEntity
{
    public partial class XMBProjectInfo
    {
        /// <summary>
        /// 工程项目ID
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectNo
        {
            get;
            set;
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectNoNew
        {
            get;
            set;
        }
        /// <summary>
        /// 项目手动编号
        /// </summary>
        public string ProjectCode
        {
            get;
            set;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerNo
        {
            get;
            set;
        }
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerCode
        {
            get;
            set;
        }
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目分类：局签项目、分包项目、公司签项目、总承包公司签项目
        /// </summary>
        public string Catalog
        {
            get;
            set;
        }
        /// <summary>
        /// 项目级别：一类、二类、三类、四类
        /// </summary>
        public string ProjectClass
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
        public Nullable<int> ProjectDuration
        {
            get;
            set;
        }
        public string Currency
        {
            get;
            set;
        }
        /// <summary>
        /// 币别
        /// </summary>
        public string CurrencyCode
        {
            get;
            set;
        }
        /// <summary>
        /// 币别
        /// </summary>
        public string CtrctCurrency
        {
            get;
            set;
        }
        /// <summary>
        /// 币别
        /// </summary>
        public string CtrctCurrencyCode
        {
            get;
            set;
        }
        public Nullable<decimal> CtrctExChangeRate
        {
            get;
            set;
        }
        /// <summary>
        /// 国内/境外
        /// </summary>
        public string DomOvsea
        {
            get;
            set;
        }
        public string AddressName
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
        /// 大区
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
        /// 市辖区
        /// </summary>
        public string District
        {
            get;
            set;
        }
        /// <summary>
        /// 父项目
        /// </summary>
        public string ParentProjectNo
        {
            get;
            set;
        }
        /// <summary>
        /// 父项目
        /// </summary>
        public string ParentProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 工程概况
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public Nullable<decimal> ContractAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string ProjectManager
        {
            get;
            set;
        }
        /// <summary>
        /// 投标单位No
        /// </summary>
        public string BidDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 招标单位
        /// </summary>
        public string BidDeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 招标时间
        /// </summary>
        public Nullable<DateTime> BidDate
        {
            get;
            set;
        }
        /// <summary>
        /// 项目开始日期
        /// </summary>
        public Nullable<DateTime> BeginningDate
        {
            get;
            set;
        }
        /// <summary>
        /// 项目结束日期
        /// </summary>
        public Nullable<DateTime> Endingdate
        {
            get;
            set;
        }
        public Nullable<DateTime> PlanDate
        {
            get;
            set;
        }
        public Nullable<DateTime> ActualBeginningDate
        {
            get;
            set;
        }
        public Nullable<DateTime> ActualEndingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 目前阶段
        /// </summary>
        public string StageNo
        {
            get;
            set;
        }
        /// <summary>
        /// 目前阶段
        /// </summary>
        public string Stage
        {
            get;
            set;
        }
        /// <summary>
        /// 项目阶段
        /// </summary>
        public string ProjectPhaseNo
        {
            get;
            set;
        }
        /// <summary>
        /// 项目阶段
        /// </summary>
        public string ProjectPhaseName
        {
            get;
            set;
        }
        public string ProjectBalanceNo
        {
            get;
            set;
        }
        public string ProjectBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 签订合同时间
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
        public string SignDept
        {
            get;
            set;
        }
        /// <summary>
        /// 建设单位
        /// </summary>
        public string EngnrDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 建设单位
        /// </summary>
        public string EngnrDept
        {
            get;
            set;
        }
        /// <summary>
        /// 建设单位
        /// </summary>
        public string EngnrDeptRecord
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
        public string DesignDept
        {
            get;
            set;
        }
        /// <summary>
        /// 责任单位
        /// </summary>
        public string ResponseDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 责任单位
        /// </summary>
        public string ResponseDept
        {
            get;
            set;
        }
        /// <summary>
        /// 主办单位
        /// </summary>
        public string PrepDeptNo
        {
            get;
            set;
        }
        /// <summary>
        /// 主办单位
        /// </summary>
        public string PrepDept
        {
            get;
            set;
        }
        /// <summary>
        /// 预计规模
        /// </summary>
        public string Scale
        {
            get;
            set;
        }
        /// <summary>
        /// 经营预算
        /// </summary>
        public Nullable<decimal> PlanScale
        {
            get;
            set;
        }
        /// <summary>
        /// 中标概率
        /// </summary>
        public Nullable<decimal> Probability
        {
            get;
            set;
        }
        /// <summary>
        /// 项目来源
        /// </summary>
        public string SourceProject
        {
            get;
            set;
        }
        /// <summary>
        /// 项目状态
        /// </summary>
        public string ProjectStat
        {
            get;
            set;
        }
        /// <summary>
        /// 项目级别
        /// </summary>
        public string ProjectLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 重要程度
        /// </summary>
        public string Important
        {
            get;
            set;
        }
        /// <summary>
        /// 重要程度
        /// </summary>
        public bool IsImportant
        {
            get;
            set;
        }
        public string PrjType
        {
            get;
            set;
        }
        public bool DiffPrjType
        {
            get;
            set;
        }
        /// <summary>
        /// 产值统计类型
        /// </summary>
        public string OvType
        {
            get;
            set;
        }
        public string OvTypeNo
        {
            get;
            set;
        }
        /// <summary>
        /// 项目类别
        /// </summary>
        public string ProjectTypeNo
        {
            get;
            set;
        }
        /// <summary>
        /// 项目类别
        /// </summary>
        public string ProjectType
        {
            get;
            set;
        }
        public string ProjectType1No
        {
            get;
            set;
        }
        public string ProjectType1
        {
            get;
            set;
        }
        public bool DiffProjectType
        {
            get;
            set;
        }
        /// <summary>
        /// 已关闭
        /// </summary>
        public bool isClosed
        {
            get;
            set;
        }
        /// <summary>
        /// 原因备注
        /// </summary>
        public string CloseReason
        {
            get;
            set;
        }
        /// <summary>
        /// 建设单位
        /// </summary>
        public string DeptNo
        {
            get;
            set;
        }
        public string DeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 关联项目
        /// </summary>
        public string ConnectProject
        {
            get;
            set;
        }
        /// <summary>
        /// 预计招标时间
        /// </summary>
        public Nullable<DateTime> TenderOpenDate
        {
            get;
            set;
        }
        /// <summary>
        /// 预计招标时间年
        /// </summary>
        public string TenderOpenYear
        {
            get;
            set;
        }
        /// <summary>
        /// 预计招标时间季
        /// </summary>
        public string TenderOpenQuarter
        {
            get;
            set;
        }
        /// <summary>
        /// 预计招标时间月
        /// </summary>
        public string TenderOpenMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭原因
        /// </summary>
        public string CloseType
        {
            get;
            set;
        }
        /// <summary>
        /// 来源项目
        /// </summary>
        public string FromProjects
        {
            get;
            set;
        }
        /// <summary>
        /// 进展情况
        /// </summary>
        public string ProjectSchedule
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
        /// 审核状态
        /// </summary>
        public Nullable<int> ApprovalStat
        {
            get;
            set;
        }
        public bool isOutputValue
        {
            get;
            set;
        }
        public bool isGroupAdmin
        {
            get;
            set;
        }
        /// <summary>
        /// 项目部
        /// </summary>
        public string OrgNo
        {
            get;
            set;
        }
        /// <summary>
        /// 项目部
        /// </summary>
        public string OrgName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否需要进度汇报
        /// </summary>
        public bool ndReport
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

        public bool inWorkflow
        {
            get;
            set;
        }
        public bool EditFlag
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
        public Nullable<int> Version
        {
            get;
            set;
        }
        public bool isMerged
        {
            get;
            set;
        }
        public bool VersionValidity
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
        public string uProjectNo
        {
            get;
            set;
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public Nullable<int> OldCustomerId
        {
            get;
            set;
        }
        public Nullable<int> OldAddressId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目地点
        /// </summary>
        public string OldAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 投标单位Id
        /// </summary>
        public Nullable<int> OldBidDeptId
        {
            get;
            set;
        }
        public Nullable<int> OldDeptId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否需要进展报告
        /// </summary>
        public string OldneedReport
        {
            get;
            set;
        }
        /// <summary>
        /// 已关闭
        /// </summary>
        public string OldisClosedC
        {
            get;
            set;
        }
        public Nullable<int> OldCreatedByUserId
        {
            get;
            set;
        }
        public Nullable<int> OldId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目负责人登录名
        /// </summary>
        public string ProjectManagerLoginName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目总工登录名
        /// </summary>
        public string PrjGeneralEngineerLoginName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目总工名
        /// </summary>
        public string PrjGeneralEngineerUserName
        {
            get;
            set;
        }
        public string VirtualProjectNo
        {
            get;
            set;
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public Nullable<decimal> BBContractAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 管理类型 0-普通项目，1-大包项目
        /// </summary>
        public Nullable<int> ManageType
        {
            get;
            set;
        }
        public bool IsBadDebts
        {
            get;
            set;
        }
        public bool IsThree
        {
            get;
            set;
        }
        /// <summary>
        /// 业主属性
        /// </summary>
        public string OwnerAttribute
        {
            get;
            set;
        }
        /// <summary>
        /// 支付比例
        /// </summary>
        public Nullable<decimal> PaymentRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 保修金比例
        /// </summary>
        public Nullable<decimal> WarrantyRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 增值税率
        /// </summary>
        public Nullable<Decimal> VATRate
        {
            get;
            set;
        }
        /// <summary>
        /// 保修金起算日
        /// </summary>
        public Nullable<int> WarrantyStartingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 保修期限(月)
        /// </summary>
        public Nullable<int> WarrantyPeriod
        {
            get;
            set;
        }
        /// <summary>
        /// 交工验收日期
        /// </summary>
        public Nullable<DateTime> AcceptanceDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结算签署日期
        /// </summary>
        public Nullable<DateTime> SettlementDate
        {
            get;
            set;
        }
        /// <summary>
        /// 竣工验收日期
        /// </summary>
        public Nullable<DateTime> CompletedAccDate
        {
            get;
            set;
        }
        /// <summary>
        /// 项目经理
        /// </summary>
        public string PrjManager
        {
            get;
            set;
        }   
    }
}
