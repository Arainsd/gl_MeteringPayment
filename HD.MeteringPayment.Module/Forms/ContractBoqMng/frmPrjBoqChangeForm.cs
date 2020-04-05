using DevExpress.XtraEditors;
using Erp.SharedLib.Presentation.ControlBases;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.Forms.ContractBoqMng;
using HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel;
using Hondee.ControlLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Erp.SharedLib.Presentation.AttactmentForm;

namespace HD.MeteringPayment.Module.Forms.PrjBoqInfo
{
    public partial class frmPrjBoqChangeForm : GpControlBase
    {
        public frmPrjBoqChange MainHandler;
        static Guid SignGuid = Guid.NewGuid();
        public override Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }
        private Boolean edit;
        public Boolean Edit
        {
            get
            {
                return edit;
            }
            set
            {
                bool temp = edit;
                edit = value;
                if (temp != value)
                {
                    valiation.Edit = value;
                }
                if (attaOfPrjBoqControl != null)
                {
                    attaOfPrjBoqControl.SetEditEnable(value);
                }
                RefreshButton();
            }
        }
        private ChangeDetailViewModel viewModel;
        private ContractBoqChanegEditViewModel boiChangeModel;
        HDFormValiation valiation = new HDFormValiation();
        private GeneralUploadForm attaOfPrjBoqControl = new GeneralUploadForm();  //附件控件
        public frmPrjBoqChangeForm(ChangeDetailViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            bsDetail.DataSource = viewModel.BoqChangeEx.DetailExList;
            bsForm.DataSource = viewModel.BoqChangeEx;
            valiation.LayoutContainer = dataLayoutControl1;
            valiation.AddCtrl(textEdit3, ValidateType.READONLY);
            valiation.AddCtrl(textEdit2, ValidateType.READONLY);
            valiation.AddCtrl(gcChangeDetail, ValidateType.READONLY);
            valiation.AddCtrl(textEdit1, ValidateType.REQUIRED);
            valiation.AddCtrl(imageComboBoxEdit1, ValidateType.REQUIRED);
            valiation.AddCtrl(gcChangeDetail, ValidateType.READONLY);
            valiation.AddCtrl(txtPrepareBy, ValidateType.READONLY);
            valiation.AddCtrl(textEdit5, ValidateType.READONLY);
            //初始化附件控件
            if (attaOfPrjBoqControl == null)
                attaOfPrjBoqControl = new GeneralUploadForm();
            attaOfPrjBoqControl.Dock = DockStyle.Fill;
            attaOfPrjBoqControl.Folder = AppConfig.SYSTEM_NO;

            attaOfPrjBoqControl.CatalogNo = null;
            attaOfPrjBoqControl.SetEditEnable(false);
            attaOfPrjBoqControl.SetEditPanelVisable(false);
            if (viewModel != null && viewModel.BoqChangeEx != null && !String.IsNullOrEmpty(viewModel.BoqChangeEx.ChangeNo))
            {
                attaOfPrjBoqControl.BusinessNo = viewModel.BoqChangeEx.ChangeNo;
                attaOfPrjBoqControl.LoadParameter(viewModel.BoqChangeEx.ChangeNo);
            }
            xtraTabPage2.Controls.Add(attaOfPrjBoqControl);
            //刷新按钮状态
            RefreshButton();
        }
        /// <summary>
        /// 刷新按钮状态
        /// </summary>
        void RefreshButton()
        {
            bbiEdit.Enabled = !Edit && viewModel.BoqChangeEx.Fixed != true;
            bbiEidtChange.Enabled = Edit;
            bbiDelete.Enabled= Edit;
            bbiFixed.Enabled=!Edit && viewModel.BoqChangeEx.Fixed != true;
            bbiUnFixed.Enabled= !Edit && viewModel.BoqChangeEx.Fixed == true;
            bbiSave.Enabled = Edit;
            pictureBox1.Visible = viewModel.BoqChangeEx.Fixed == true;
            valiation.Edit = Edit;
            valiation.RefreshEditChanged();
        }

        private void bbiEidtChange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            boiChangeModel = new ContractBoqChanegEditViewModel(viewModel.BoqChangeEx.DetailExList, viewModel.projectNo, viewModel.projectName);
            frmPrjBoqChangeEdit from = new frmPrjBoqChangeEdit(boiChangeModel, viewModel.projectNo, viewModel.projectName);
            from.ShowDialog();
            viewModel.BoqChangeEx.BoiNum = viewModel.BoqChangeEx.DetailExList.Count;
            viewModel.BoqChangeEx.CalcChangeAmount();
        }

        private void bbiFixed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewModel.Fixed();
            RefreshButton();
        }

        private void bbiUnFixed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewModel.UnFixed();
            RefreshButton();

        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bsForm.EndEdit();
            if (!valiation.Validate())
            {
                XtraMessageBox.Show(valiation.GetErrorText());
                return;
            }
            if (imageComboBoxEdit1.Text.ToString() == "0")
            {
                XtraMessageBox.Show("变更类型不能为空");
                return;
            }
            viewModel.Save();

            if (viewModel != null && viewModel.BoqChangeEx != null && !String.IsNullOrEmpty(viewModel.BoqChangeEx.ChangeNo))
            {
                attaOfPrjBoqControl.BusinessNo = viewModel.BoqChangeEx.ChangeNo;
                attaOfPrjBoqControl.Save(); //保存附件
                attaOfPrjBoqControl.LoadParameter(viewModel.BoqChangeEx.ChangeNo); //重新读取数据
            }
            Edit = false;
            XtraMessageBox.Show("保存成功！");
            RefreshButton();
            if (MainHandler != null)
            {
                MainHandler.bbiRefresh_ItemClick(null, null);
            }
        }

        private void bcOnlyShow_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Filter(bcOnlyShow.Checked); 
        }
        private void Filter(Boolean onlyShowQty)
        { 
               String filter = "";
            if (onlyShowQty)
                filter = "(Type=2 AND (IsUpQty=true OR IsUpPrice=true)) OR Type!=2";
            bandedGridView1.ActiveFilterString = filter;
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit = true;
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ContractBoqChangeDetailEx exDetail = bandedGridView1.GetFocusedRow() as ContractBoqChangeDetailEx;
            if (exDetail == null)
            {
                XtraMessageBox.Show("请选中项");
                return;
            }
            List<ContractBoqChangeDetailEx> lstDetail = viewModel.BoqChangeEx.DetailExList.ToList();
            List<ContractBoqChangeDetailEx> lstDel= lstDetail.FindAll(m => m.ItemCode.StartsWith(exDetail.ItemCode));
            lstDel.ForEach(m => {
                viewModel.BoqChangeEx.DetailExList.Remove(m);
            });
            viewModel.BoqChangeEx.BoiNum = viewModel.BoqChangeEx.DetailExList.Count;
            viewModel.BoqChangeEx.CalcChangeAmount();
        }
    }
}
