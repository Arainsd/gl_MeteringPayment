using Erp.SharedLib.Presentation.FormBases;
using System;
using System.Windows.Forms;
using DevExpress.XtraTabbedMdi;
using Erp.CommonData;
using Erp.CommonData.Interface;
using Erp.CommonData.Entity;
using HD.MeteringPayment.Module.BootLoader.Config;
using Erp.GpServiceClient;
using HD.MeteringPayment.Domain.Client;
using Hondee.Common.Client;

namespace HD.MeteringPayment.UITest
{
    public partial class AppForm : GpFormBase, IMainModuleForm
    {
        public AppForm()
            : base(null)
        {
            InitializeComponent();
            this.Load += AppForm_Load;
            this.FormClosing += AppForm_FormClosing;
            //LoginInfor.LoginName = "zqingqing1";
            //LoginInfor.UserName = "庄卿卿";
            //LoginInfor.Psw = "000000"; 

            //LoginInfor.LoginName = "stang";
            //LoginInfor.UserName = "唐松";
            //LoginInfor.Psw = "000000"; 

            //LoginInfor.LoginName = "zwei5";
            //LoginInfor.UserName = "曾伟";
            //LoginInfor.Psw = "000000"; 

            LoginInfor.LoginName = "ljunbiao";
            LoginInfor.UserName = "刘俊彪";
            LoginInfor.Psw = "000000";

            ////初始化系统环境
            LoginInfo.LoginName = LoginInfor.LoginName;
            LoginInfo.UserName = LoginInfor.UserName;
            LoginInfo.Psw = LoginInfor.Psw;

            AppConfig.IsTest = false; 
            #region 63
            //ServerInfo.ServiceEndPoint = "net.tcp://172.16.0.63:9019/GpService.svc/tcp";
            //ServerInfo.ServiceLoginName = "hd";
            //ServerInfo.ServicePsw = "HD!874";

            //MeteringPaymentClient.IsTest = false;
            //MeteringPaymentClient.TestEndPoint = "net.tcp://localhost:9094";

            //MeteringPaymentClient.TestLoginName = "test";
            //MeteringPaymentClient.TestPsw = "linger_000";
            #endregion

            #region 测试环境
            ServerInfo.ServiceEndPoint = "net.tcp://192.168.2.202:9019/GpService.svc/tcp";
            ServerInfo.ServiceLoginName = "test";
            ServerInfo.ServicePsw = "qq_123qwe";

            //ServerInfo.ServiceEndPoint = "net.tcp://172.16.0.27:9019/GpService.svc/tcp";
            //ServerInfo.ServerIp = "172.16.0.27";
            //ServerInfo.ServiceLoginName = "hd";
            //ServerInfo.ServicePsw = "HD!874";

            //ServerInfo.ServiceEndPoint = "net.tcp://s1.gzpcc.com:9019/GpService.svc/tcp";
            //ServerInfo.ServiceLoginName = "HD";
            //ServerInfo.ServicePsw = "Hd!874";


            #endregion

            #region 41
            //ServerInfo.ServiceEndPoint = "net.tcp://s1.gzpcc.com:9019/GpService.svc/tcp";
            //ServerInfo.ServiceLoginName = "HD";
            //ServerInfo.ServicePsw = "Hd!874";

            //MeteringPaymentClient.IsTest = true;
            //MeteringPaymentClient.TestEndPoint = "net.tcp://localhost:9094";

            //MeteringPaymentClient.TestLoginName = "test";
            //MeteringPaymentClient.TestPsw = "linger_000";
            #endregion
            CurrentForm = this;
        }
        public static AppForm CurrentForm;
        void AppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("确认退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = false;
                this.Hide();
            }
            else
            {
                e.Cancel = true;
            }
        }

        void AppForm_Load(object sender, EventArgs e)
        {
            HD.MeteringPayment.Module.BootLoader.AppForm appForm = new HD.MeteringPayment.Module.BootLoader.AppForm(this);
            appForm.Show();
        }

        private bool isEntry = false;
        public object GetChildForm(Guid FormId)
        {
            foreach (XtraMdiTabPage xt in frmMdiManager.Pages)
            {
                IChildGuidForm instance = xt.MdiChild as IChildGuidForm;
                if (instance == null) continue;
                if (instance.FormId == FormId)
                {
                    return xt.MdiChild;
                }
            }
            return null;
        }


        public ModuleEndPointInfor GetServiceEndPoint()
        {
            ModuleEndPointInfor mInfor = new ModuleEndPointInfor();
            mInfor.Name = "Erp.GpPlatform";
            mInfor.EndPoint = ServerInfo.ServiceEndPoint;
            mInfor.LoginName = ServerInfo.ServiceLoginName;
            mInfor.Psw = ServerInfo.ServicePsw;
            return mInfor;

        }

        public ModuleEndPointInfor GetServiceEndPoint(string moduleName)
        {
            return GpClient.CreateInstance().GetModuleEndPoint(moduleName);
        }

        public IGpSvcClient GpSvcClient
        {
            get { return GpClient.CreateInstance(); }
        }

        private void AppForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
