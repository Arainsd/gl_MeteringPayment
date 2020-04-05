using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Erp.CommonData;
using Erp.CommonData.Entity.Business;
using Erp.GpServiceClient;
using Erp.SharedLib.Presentation.FormBases;
using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DataConvertor;
using HD.MeteringPayment.Module.BootLoader.Config;

namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    public partial class frmGSMng : GpFormBase
    {
        #region 变量定义
        /// <summary>
        /// 
        /// </summary>
        private Manager _refGSManager;

        public Manager RefGSManager
        {
            get { return _refGSManager; }
            set
            {
                _refGSManager = value;
                GSMng = new Manager();
                GSMng.Copy(value);
                bindingSource1.DataSource = value;

            }
        }

        private Manager GSMng;
        public frmGSManager MainHandler = null;
        #endregion

        public frmGSMng()
        {
            InitializeComponent();
            LoadData();
        }
       
        private void LoadData()
        {
        GpUserSelector.PopMaxWidth = 0;
            GpUserSelector.KeyField = "LoginName";
            GpUserSelector.DisplayField = "UserName";
            GpUserSelector.AddColumn("人员姓名", "UserName");
            GpUserSelector.AddColumn("OA名", "LoginName");
            GpUserSelector.InnerControl.MainView.OptionsView.ShowDetailButtons = false;
            GpUserSelector.GetDataSource = () => { return new MeteringPaymentClient().GetIGpuserService().GetList(AppConfig.GlOrgCode); };
            GpUserSelector.RefDataSource = () => { return new MeteringPaymentClient().GetIGpuserService().GetList(AppConfig.GlOrgCode); };
            GpUserSelector.Closed += (sender, e) =>
            {
                bindingSource1.EndEdit();
            };
            GpUserSelector.ClearEvent += () =>
            {
                bindingSource1.EndEdit();
            };
        }

        private void GSMngSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bindingSource1.EndEdit();
            DoWork("保存数据", "保存数据", () =>
            {
                if (String.IsNullOrEmpty(RefGSManager.ProjectName))
                    new MeteringPaymentClient().GetIManagerService().Add(RefGSManager, LoginInfor.LoginName);
                else
                    new MeteringPaymentClient().GetIManagerService().Update(RefGSManager, LoginInfor.LoginName);
            }, (ex) =>
            {
                if (ex == null)
                {
                    if (MainHandler != null)
                        MainHandler.LoadGrid();
                    this.Close();
                }
            });

        }
        private HDNorm _norm;
        public HDNorm norm
        {
            get
            {
                return _norm;
            }
            set
            {
                _norm = value;
                _norm.Ctrls.Clear();
                if (RefGSManager != null)
                {
                    _norm.AddCtrl(GpUserSelector, NormCtrlEditParameter.REQUIRED);
                    _norm.RefreshEditChanged();
                }
            }
        }

        private void frmGSMng_Load(object sender, EventArgs e)
        {

        }

        private void GSMngCancle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void GpUserSelector_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
