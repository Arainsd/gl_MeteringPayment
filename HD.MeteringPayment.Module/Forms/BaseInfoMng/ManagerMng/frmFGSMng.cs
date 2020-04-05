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

namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    public partial class frmFGSMng : GpFormBase
    {
        #region 变量定义
        /// <summary>
        /// 
        /// </summary>
        private Manager _refFGSManager;

        public Manager RefFGSManager
        {
            get { return _refFGSManager; }
            set
            {
                _refFGSManager = value;
                FGSMng = new Manager();
                FGSMng.Copy(value);
                bindingSource1.DataSource = value;
                norm = new HDNorm();
            }
        }
         private Manager FGSMng;
        public frmFGSManager MainHandler = null;
        private MeteringPaymentClient client = new MeteringPaymentClient();
        #endregion

        public frmFGSMng()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            List<ProjectInfo> LstProject = client.GetIProjectInfoService().GetList("");
            gridLookUpEdit1.Properties.DataSource = LstProject;
        }
        private void FGSMngSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bindingSource1.EndEdit();
            DoWork("保存数据", "保存数据", () =>
            {
                if (String.IsNullOrEmpty(RefFGSManager.ProjectName))
                    new MeteringPaymentClient().GetIManagerService().Add(RefFGSManager, LoginInfor.LoginName);
                else
                    new MeteringPaymentClient().GetIManagerService().Update(RefFGSManager, LoginInfor.LoginName);
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

        private void frmFGSMng_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 编辑状态
        /// </summary>
        private bool _isEdit;

        public bool IsEdit
        {
            get { return _isEdit; }
            set
            {
                _isEdit = value;
                RefreshButton();
                if (norm != null)
                {
                    norm.Edit = value;
                }
                else
                {
                    norm.Edit = value;

                }
            }
        }
        /// <summary>
        /// 验证器
        /// </summary>
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
                if (RefFGSManager != null)
                {
                    _norm.AddCtrl(gridLookUpEdit1, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(gridLookUpEdit2, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(textEdit1, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(gridLookUpEdit3, NormCtrlEditParameter.NORMAL);
                    _norm.RefreshEditChanged();
                }
            }
        }
        /// <summary>
        /// 用于取消操作恢复原值
        /// </summary>




        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        private void RefreshButton()
        {
            //btnEdit.Visibility = !IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            FCSMngCancle.Visibility = IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            FGSMngSave.Visibility = IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void FCSMngCancle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefFGSManager = FGSMng;
            bindingSource1.ResetBindings(true);
            IsEdit = false;
        }
    }
}
