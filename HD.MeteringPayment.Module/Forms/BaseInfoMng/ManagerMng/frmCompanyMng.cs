using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Erp.SharedLib.Presentation.ControlBases;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using DevExpress.XtraEditors;
using Erp.CommonData;
using Erp.SharedLib.Presentation.FormBases;
using Erp.SharedLib.Presentation.Lib;
using Hondee.Common.DataConvertor;
using Erp.CommonData.Entity.Business;
using Erp.GpServiceClient;
using HD.MeteringPayment.Module.BootLoader.Config;

namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    public partial class frmCompanyMng : GpFormBase
    {
        #region 变量定义
        /// <summary>
        /// 用户列表
        /// </summary>
        private List<CacheGpuser> LstUser;
        /// <summary>
        /// 管理员列表
        /// </summary>
        private List<Manager> LstManager = new List<Manager>();
        public List<Manager> LstExitManager;
        /// <summary>
        /// 父窗体
        /// </summary>
        public frmFGSManager MainHandler = null;
        #endregion


        private Manager FGSMng;
        private MeteringPaymentClient client = new MeteringPaymentClient();
        //#endregion

        public frmCompanyMng()
        {
            InitializeComponent();
        }
        private void frmFGSMng_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            //OrgSelector.PopMaxWidth = 0;
            //OrgSelector.KeyField = "OrgNo";
            //OrgSelector.DisplayField = "OrgName";
            //OrgSelector.AddColumn("机构", "OrgName");
            //OrgSelector.InnerControl.MainView.OptionsView.ShowDetailButtons = false;
            //OrgSelector.GetDataSource = () => { return CollectionHelper.ConvertTo<CacheOrg>(GpClient.OrgTb).ToList(); };
            //OrgSelector.RefDataSource = () => { return CollectionHelper.ConvertTo<CacheOrg>(GpClient.OrgTb).ToList(); };
            //OrgSelector.Closed += (sender, e) =>
            //{
            //    bindingSource1.EndEdit();
            //};
            //OrgSelector.ClearEvent += () =>
            //{
            //    bindingSource1.EndEdit();
            //};

            DoWork("获取用户数据", "获取用户数据", () =>
            {
                LstUser = new MeteringPaymentClient().GetIGpuserService().GetList(AppConfig.GlOrgCode);
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcCompanyMngLeft.DataSource = LstUser;
                }
            });

            

        }
        private void FGSMngSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bindingSource1.EndEdit();
            DoWork("保存数据", "保存数据", () =>
            {
                LstManager.ForEach(m =>
                {
                    m.OrgNo = BidSelector.EditValue.ToString();
                    m.OrgName = BidSelector.Text;
                });
                new MeteringPaymentClient().GetIManagerService().AddList(LstManager, LoginInfor.LoginName);
            }, (ex) =>
            {
                if (ex == null)
                {
                    if (MainHandler != null)
                        MainHandler.LoadGrid();
                    XtraMessageBox.Show("保存成功");
                    this.Close();
                }
            });
        }




        private void OrgSelector_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gvfCompanyMngLeft_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                if (gvfCompanyMngLeft.GetRow(e.RowHandle) != null)
                {
                    CacheGpuser clickUser = gvfCompanyMngLeft.GetRow(e.RowHandle) as CacheGpuser;
                    if (clickUser != null)
                    {
                        if (BidSelector.EditValue == null)
                        {
                            XtraMessageBox.Show("请先选择标段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                            if (LstExitManager.Exists(m => m.LoginName == clickUser.LoginName && String.Format("{0}", BidSelector.EditValue).Equals(m.OrgNo)))
                        {
                            XtraMessageBox.Show("该标段监理管理员中已存在该用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            if (!LstManager.Exists(m => m.LoginName == clickUser.LoginName))
                            {
                                Manager NewManager = new Manager();
                                NewManager.LoginName = clickUser.LoginName;
                                NewManager.UserName = clickUser.UserName;
                                NewManager.UserType = 2;
                                NewManager.OrgName = BidSelector.Text;
                                NewManager.OrgNo = BidSelector.EditValue.ToString();
                                LstManager.Add(NewManager);
                                gcCompanyMngRight.DataSource = LstManager;
                                gcCompanyMngRight.RefreshDataSource();
                            }
                        }
                    }
                }
            }
        }

        private void gvCompanyMngRight_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                if (gvCompanyMngRight.GetRow(e.RowHandle) != null)
                {
                    Manager clickManager = gvCompanyMngRight.GetRow(e.RowHandle) as Manager;
                    if (clickManager != null)
                    {
                        LstManager.Remove(clickManager);
                        gcCompanyMngRight.DataSource = LstManager;
                        gcCompanyMngRight.RefreshDataSource();
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
