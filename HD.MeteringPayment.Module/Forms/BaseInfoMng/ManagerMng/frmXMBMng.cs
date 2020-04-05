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
using Erp.CommonData.Entity.Business;
using Erp.GpServiceClient;
using Hondee.Common.DataConvertor;
using HD.MeteringPayment.Module.BootLoader.Config;

namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    public partial class frmXMBMng : GpFormBase
    {
        #region 变量定义
        /// <summary>
        /// 
        /// </summary>
        private Manager _refXMBManager;
        public List<Manager> ExitsUserList { get; set; }

        public Manager RefXMBManager
        {
            get { return _refXMBManager; }
            set
            {
                _refXMBManager = value;
                XMBMng = new Manager();
                XMBMng.Copy(value);
                bindingSource1.DataSource = value;
                //norm = new HDNorm();

                if (norm == null)
                {
                    norm = new HDNorm();
                    norm.Edit = true;
                }
            }
        }
        private Manager XMBMng;
        public frmXMBManager MainHandler = null;
        private MeteringPaymentClient client = new MeteringPaymentClient();

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
                _norm.AddCtrl(ProjectSelector, NormCtrlEditParameter.REQUIRED);
                _norm.AddCtrl(GpUserSelector, NormCtrlEditParameter.REQUIRED);
                _norm.RefreshEditChanged();
            }
        }
        #endregion

        public frmXMBMng()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            ProjectSelector.PopMaxWidth = 0;
            ProjectSelector.KeyField = "ProjectNo";
            ProjectSelector.DisplayField = "BidName";
            ProjectSelector.AddColumn("标段名称", "BidName");
            ProjectSelector.InnerControl.MainView.OptionsView.ShowDetailButtons = false;
            ProjectSelector.GetDataSource = () => {
                List<ProjectInfo> infoList = client.GetIProjectInfoService().GetList(AppConfig.GlBidRootNo);
            
              infoList= infoList.FindAll(m=>m.Category==4);
                return infoList;

            };
            ProjectSelector.RefDataSource = () => { return client.GetIProjectInfoService().GetList(AppConfig.GlBidRootNo).FindAll(m => m.Category == 4); };
            ProjectSelector.Closed += (sender, e) =>
            {
                bindingSource1.EndEdit();
            };
            ProjectSelector.ClearEvent += () =>
            {
                bindingSource1.EndEdit();
            };

            GpUserSelector.PopMaxWidth = 0;
            GpUserSelector.KeyField = "LoginName";
            GpUserSelector.DisplayField = "UserName";
            GpUserSelector.AddColumn("人员姓名", "UserName");
            GpUserSelector.AddColumn("OA名", "LoginName");
            GpUserSelector.InnerControl.MainView.OptionsView.ShowDetailButtons = false;
            GpUserSelector.GetDataSource = () => { return client.GetIGpuserService().GetList(AppConfig.GlOrgCode); };
            GpUserSelector.RefDataSource = () => { return client.GetIGpuserService().GetList(AppConfig.GlOrgCode); };
            GpUserSelector.Closed += (sender, e) =>
            {
                bindingSource1.EndEdit();
            };
            GpUserSelector.ClearEvent += () =>
            {
                bindingSource1.EndEdit();
            };
        }
     
        private void XMBMngSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bindingSource1.EndEdit();
            if (!norm.Validate())
                return;
            //判断项目和用户是否重复
            if (ExitsUserList.Exists(m => m.LoginName == String.Format("{0}", GpUserSelector.Value) && m.ProjectNo == String.Format("{0}", ProjectSelector.Value)))
            {
                XtraMessageBox.Show("该标段管理员中已存在该用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
                DoWork("保存数据", "保存数据", () =>
                {
                    if (String.IsNullOrEmpty(RefXMBManager.ManagerNo))
                        client.GetIManagerService().Add(RefXMBManager, LoginInfor.LoginName);
                    else
                        client.GetIManagerService().Update(RefXMBManager, LoginInfor.LoginName);

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

        private void frmXMBMng_Load(object sender, EventArgs e)
        {
        
        }
        

        //private void frmXMBMng_Load(object sender, EventArgs e)
        //{

        //}

        /// <summary>
        /// 编辑状态
        /// </summary>
        //private bool _isEdit;

        //public bool IsEdit
        //{
        //    get { return _isEdit; }
        //    set
        //    {
        //        _isEdit = value;
        //        RefreshButton();
        //        if (norm != null)
        //        {
        //            norm.Edit = value;
        //        }
        //        else
        //        {
        //            norm.Edit = value;

        //        }
        //    }
        //}
        /// <summary>
        /// 验证器
        /// </summary>
        //private HDNorm _norm;
        //public HDNorm norm
        //{
        //    get
        //    {
        //        return _norm;
        //    }
        //    set
        //    {
        //        _norm = value;
        //        _norm.Ctrls.Clear();
        //        if (RefXMBManager != null)
        //        {
        //            _norm.AddCtrl(ProjectSelector, NormCtrlEditParameter.REQUIRED);
        //            _norm.AddCtrl(GpUserSelector, NormCtrlEditParameter.REQUIRED);
        //            _norm.RefreshEditChanged();
        //        }
        //    }
        //}
        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        //private void RefreshButton()
        //{
        //    //btnEdit.Visibility = !IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
        //    XMBMngCancle.Visibility = IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
        //    XMBMngSave.Visibility = IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
        //}

        //private void XMBMngCancle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    RefXMBManager = XMBMng;
        //    bindingSource1.ResetBindings(true);
        //    IsEdit = false;
        //}


    }
}
