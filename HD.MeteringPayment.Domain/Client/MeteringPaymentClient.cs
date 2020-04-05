using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using Hondee.Common.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HD.MeteringPayment.Domain.Entity.ContractEntity;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;

namespace HD.MeteringPayment.Domain.Client
{
    public class MeteringPaymentClient : HdClientBase
    {
        /// <summary>
        /// 终结点名称
        /// </summary>
        protected override string SystemEndpointName
        {
            get
            {
                return "HD.MeteringPayment.Service";
            }
        }
        public  string SystemEndpointName1
        {
            get
            {
                return SystemEndpointName;
            }
        }
        public MeteringPaymentClient(String EndPoint = null, String LoginName = null, String Psw = null)
            : base((IsTest ? TestEndPoint : EndPoint)
            , (IsTest ? TestLoginName : LoginName)
            , (IsTest ? TestPsw : Psw))
        {
        }
        public static bool IsTest = false;
        public static string TestEndPoint = "";
        public static string TestLoginName = "";
        public static string TestPsw = "";
        #region 管理员服务
        public IManager GetIManagerService()
        {
            return GetI<IManager>();
        }
        #endregion
        #region GpUser服务
        public IGlUsers GetIGpuserService()
        {
            return GetI<IGlUsers>();
        }
        #endregion
        #region 标段服务
        public IProjectBid GetIProjectBidService()
        {
            return GetI<IProjectBid>();
        }
        #endregion
        #region 项目服务
        public IProjectInfo GetIProjectInfoService()
        {
            return GetI<IProjectInfo>();
        }
        #endregion
        #region 合同清单服务
        public IContractBoq GetIContractBoqService()
        {
            return GetI<IContractBoq>();
        }
        #endregion
        #region WBS清单服务
        public IWBSBoq GetIWBSBoqService()
        {
            return GetI<IWBSBoq>();
        }
        #endregion
        #region 合同服务
        public IContract GetIContractService()
        {
            return GetI<IContract>();
        }
        #endregion
        #region 合同清单变更服务
        public IContractBoqChange GetIContractBoqChangeService()
        {
            return GetI<IContractBoqChange>();
        }
        #endregion
        #region 项目合同信息服务
        public IXMBContractInfo GetIXMBContractInfoService()
        {
            return GetI<IXMBContractInfo>();
        }
        #endregion
        #region 进度计量服务
        public IPrjAmount GetIPrjAmountService()
        {
            return GetI<IPrjAmount>();
        }
        #endregion
        #region 进度计量报表服务
        public IPrjAmountRpt GetIPrjAmountRptService()
        {
            return GetI<IPrjAmountRpt>();
        }
        #endregion
        #region 项目部信息
        public IXMBProjectInfo GetIXMBProjectInfoService()
        {
            return GetI<IXMBProjectInfo>();
        }
        #endregion
        #region 补充协议
        public ICtrctAgreement GetICtrctAgreementService()
        {
            return GetI<ICtrctAgreement>();
        }
        #endregion
    }
}
