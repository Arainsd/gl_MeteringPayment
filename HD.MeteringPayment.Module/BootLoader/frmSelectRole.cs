using DevExpress.XtraEditors;
using Erp.CommonData.Entity.Business;
using Erp.SharedLib.Presentation.FormBases;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Module.BootLoader.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HD.MeteringPayment.Module.BootLoader
{
    public partial class frmSelectRole : GpFormBase
    {
        private AppForm MainForm = null;
        public frmSelectRole(AppForm mainHandler)
        {
            MainForm = mainHandler;
            InitializeComponent();
        }

        private void frmSelectRole_Load(object sender, EventArgs e)
        {
            if (AppConfig.CurrentLoginUser.HasGrpMngrRole)
            {
                cbLoginRoles.Properties.Items.Add("业主");
            }
            if (AppConfig.CurrentLoginUser.HasBaseMngrRole)
            {
                cbLoginRoles.Properties.Items.Add("监理");
            }
            if (AppConfig.CurrentLoginUser.HasXMBMngrRole)
            {
                cbLoginRoles.Properties.Items.Add("标段管理员");
            }
            cbLoginRoles.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbLoginRoles.Text))
            {
                XtraMessageBox.Show("请选择登录角色");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                MainForm.ForceClose();
                return;

            }
            else
            {
                AppConfig.CurrentLoginUser.SetRoles(cbLoginRoles.Text);
                AppConfig.CurrentLoginUser.LoginOrg = cbLoginOrg.SelectedItem as ProjectBid;
                if (AppConfig.CurrentLoginUser.LoginRole == AppConfig.ManagerRole.XMBMngr)
                    AppConfig.SelectProject = new MeteringPaymentClient().GetIProjectInfoService().GetCurrencyAndExchangeRate(cbProject.SelectedItem as ProjectInfo);
                if (MainForm != null)
                {
                    if (AppConfig.CurrentLoginUser.LoginRole == AppConfig.ManagerRole.XMBMngr)
                        MainForm.RoleInfo = "当前登录角色:" + AppConfig.SelectProject.ProjectName + "-" + cbLoginRoles.Text;
                    else
                        MainForm.RoleInfo = "当前登录角色:" + /*AppConfig.CurrentLoginUser.LoginOrg.BidName + "-"  +*/ " " + cbLoginRoles.Text;
                }
            }
        }

        private void cbLoginRoles_SelectedValueChanged(object sender, EventArgs e)
        {
            cbLoginOrg.Properties.Items.Clear();
            cbProject.Properties.Items.Clear();
            cbLoginOrg.Text = "";
            switch (cbLoginRoles.Text)
            {
                case "业主":
                    lciOrg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciProject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    cbLoginOrg.Properties.ReadOnly = AppConfig.CurrentLoginUser.LstGrpOrgs.Count == 1;
                    for (int i = 0; i < AppConfig.CurrentLoginUser.LstGrpOrgs.Count; i++)
                        cbLoginOrg.Properties.Items.Add(AppConfig.CurrentLoginUser.LstGrpOrgs[i]);
                    cbLoginOrg.SelectedIndex = 0;
                    break;
                case "监理":
                    lciOrg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciProject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    cbLoginOrg.Properties.ReadOnly = AppConfig.CurrentLoginUser.LstFGSOrgs.Count == 1;
                    for (int i = 0; i < AppConfig.CurrentLoginUser.LstFGSOrgs.Count; i++)
                        cbLoginOrg.Properties.Items.Add(AppConfig.CurrentLoginUser.LstFGSOrgs[i]);
                    cbLoginOrg.SelectedIndex = 0;
                    break;
                case "标段管理员":
                    lciOrg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciProject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    cbProject.Properties.ReadOnly = AppConfig.CurrentLoginUser.LstProject.Count == 1;
                    for (int i = 0; i < AppConfig.CurrentLoginUser.LstProject.Count; i++)
                        cbProject.Properties.Items.Add(AppConfig.CurrentLoginUser.LstProject[i]);
                    cbProject.SelectedIndex = 0;
                    break;
            }
        }
    }
}
