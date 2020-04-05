using Erp.CommonData;
using Erp.CommonData.Entity;
using Erp.CommonData.Interface;
using Erp.GpServiceClient;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HD.MeteringPayment.Module.BootLoader.Config
{
    public class AppConfig
    {
        private static IMainModuleForm _mmForm;
        public const String SYSTEM_NO = "A0118";
        public static bool IsTest = false;
        /// <summary>
        /// 主模块引用
        /// </summary>
        public static IMainModuleForm mmForm
        {
            get
            {
                return _mmForm;
            }
            set
            {
                _mmForm = value;
            }
        }
        private static String sEndPoint;
        private static String sLoginName;
        private static String sPsw;
        public static String SEndPoint
        {
            get
            {
                if (IsTest) return "net.tcp://localhost:9094";
                InitEndPoint();
                return sEndPoint;
            }
        }
        private static void InitEndPoint()
        {
            if (String.IsNullOrEmpty(sEndPoint))
            {
                if (String.IsNullOrEmpty(ServerInfo.ServiceEndPoint))
                {
                    ServerInfo.ServiceEndPoint = System.Configuration.ConfigurationManager.AppSettings["DbServiceEndPoint"].ToString();
                    ServerInfo.ServiceLoginName = Encrypt.DecryptDES(System.Configuration.ConfigurationManager.AppSettings["ServiceLoginName"], "shj20140");
                    ServerInfo.ServicePsw = Encrypt.DecryptDES(System.Configuration.ConfigurationManager.AppSettings["ServicePsw"], "shj20140");
                }
                ModuleEndPointInfor pointInfor = GpClient.CreateInstance().GetModuleEndPoint("HD.MeteringPayment.Service");
                sEndPoint = pointInfor.EndPoint;
                sLoginName = pointInfor.LoginName;
                sPsw = pointInfor.Psw;
            }
        }
        public static String SLoginName
        {
            get
            {
                if (IsTest) return "test";
                InitEndPoint();
                return sLoginName;
            }
        }
        public static String SPsw
        {
            get
            {
                if (IsTest) return "linger_000";
                InitEndPoint();
                return sPsw;
            }
        }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static LoginUser CurrentLoginUser
        {
            get;
            set;
        }
        public static ProjectInfo SelectProject
        {
            get;
            set;
        }
        public enum ManagerRole
        {
            /// <summary>
            /// 项目公司合约部
            /// </summary>
            GrpMngr = 1,
            /// <summary>
            /// 监理
            /// </summary>
            FGSMngr = 2,
            /// <summary>
            /// 标段管理员
            /// </summary>
            XMBMngr = 3,
        };
        #region 工作流菜单
        /// <summary>
        /// 进度计量-提交审批(工作流菜单号)
        /// </summary>
        public static string WfMenuNo_MeteringSubmit = "Menu0219";
        #endregion

        /// <summary>
        /// 标段树缓存
        /// </summary>
        public static List<ProjectBid> LstBid
        {
            get;
            set;
        }
        public static int GetRoleLevel()
        {
            switch (AppConfig.CurrentLoginUser.LoginRole)
            {
                case AppConfig.ManagerRole.XMBMngr:
                    return 0;
                case AppConfig.ManagerRole.FGSMngr:
                    return 2;
                case AppConfig.ManagerRole.GrpMngr:
                    return 3;
            }
            return 0;
        }

        private static string _prjAmountQRCodeAddress = null;
        /// <summary>
        /// 计量支付报表二维码地址
        /// </summary>
        public static string PrjAmountQRCodeAddress
        {
            get
            {
                if(String.IsNullOrEmpty(_prjAmountQRCodeAddress))
                {
                    System.Data.DataTable dt = Erp.GpServiceClient.GpClient.CreateInstance().Execute(@"SELECT ConfigValue
                                                                                                         FROM ERP_Basedata.dbo.cmn_sysconfig 
                                                                                                        WHERE ConfigName = 'GlPrjAmountQRCodeAddress'", System.Data.CommandType.Text).Data.Tables[0];
                    if(dt.Rows.Count > 0)
                    {
                        _prjAmountQRCodeAddress = dt.Rows[0][0].ToString();
                    }
                }
                return _prjAmountQRCodeAddress;
            }
        }

        private static string _glBidRootNo = null;
        /// <summary>
        /// 获取广联项目标段树根节点编号
        /// </summary>
        /// <returns></returns>
        public static string GlBidRootNo
        {
            get
            {
                if(String.IsNullOrEmpty(_glBidRootNo))
                {
                    string result = "element";
                    String sql = @"SELECT ConfigValue
                                     FROM ERP_Basedata.dbo.cmn_sysconfig 
                                    WHERE ConfigName = 'GlBidRootNo'";

                    System.Data.DataTable dt = Erp.GpServiceClient.GpClient.CreateInstance().Execute(sql, System.Data.CommandType.Text).Data.Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
                    _glBidRootNo = result;
                }
                return _glBidRootNo;
            }

        }

        private static string _glCreateByOrgNo = null;
        /// <summary>
        /// 获取配置中的广连公司人员的创建单位编号
        /// </summary>
        public static string GlCreateByOrgNo
        {
            get
            {
                if (String.IsNullOrEmpty(_glCreateByOrgNo))
                {
                    string result = "element";
                    String sql = @"SELECT ConfigValue
                                     FROM ERP_Basedata.dbo.cmn_sysconfig 
                                    WHERE ConfigName = 'GlCompanyNo'";

                    System.Data.DataTable dt = Erp.GpServiceClient.GpClient.CreateInstance().Execute(sql, System.Data.CommandType.Text).Data.Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
                    _glCreateByOrgNo = result;
                }
                return _glCreateByOrgNo;
            }
        }


        private static string _glOrgCode = null;
        /// <summary>
        /// 获取配置中的广连公司人员的创建单位编号
        /// </summary>
        public static string GlOrgCode
        {
            get
            {
                if (String.IsNullOrEmpty(_glOrgCode))
                {
                    string result = "";
                    String sql = String.Format(@"Select OrgCode 
		                            From ERP_Identity.Org.Org 
		                            Where OrgNo = '{0}'", AppConfig.GlCreateByOrgNo);

                    System.Data.DataTable dt = Erp.GpServiceClient.GpClient.CreateInstance().Execute(sql, System.Data.CommandType.Text).Data.Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
                    _glOrgCode = result;
                }
                return _glOrgCode;
            }
        }

    }
}
