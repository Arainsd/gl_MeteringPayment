using DevExpress.XtraEditors;
using Erp.SharedLib.Presentation.ControlBases;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using HD.MeteringPayment.Module.BootLoader;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.Forms.ContractBoqMng;
using HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HD.MeteringPayment.Module.Forms.PrjBoqInfo
{
    public partial class frmPrjBoqChange : GpControlBase
    {
        static Guid SignGuid = Guid.NewGuid();
        public override Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }
        public ChangeListViewModel viewModel;
        public string projectNo;
        public string projectName;
        public frmPrjBoqChange(string projectNo, string projectName)
        {
            InitializeComponent();
            this.projectNo = projectNo;
            this.projectName = projectName;
            viewModel = new ChangeListViewModel(projectNo, projectName);
            viewModel.Load();
            gcChange.DataSource = viewModel.ChangedList;
            RefreshBottomBar();
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (ContractBoqChangeInfo temp in viewModel.ChangedList)
            {
                if ( temp.Fixed == null || !temp.Fixed.Value)
                {
                    XtraMessageBox.Show("存在未发布的变更清单，请先发布上期的变更");
                    return ;
                }
            }
            ChangeDetailViewModel detailModel = viewModel.Add();
            frmPrjBoqChangeForm form = new frmPrjBoqChangeForm(detailModel);
            form.MainHandler = this;
            form.Edit = true;
            AppForm.CurrentForm.ChangeForm("新增", form);
        }

        private void bbiOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ContractBoqChangeInfo changeInfo = gvChange.GetFocusedRow() as ContractBoqChangeInfo;
            if (changeInfo == null)
            {
                XtraMessageBox.Show("请选中项");
                return;
            }
            ChangeDetailViewModel detailModel = new ChangeDetailViewModel(viewModel.ChangedList,projectNo);
            detailModel.Load(changeInfo.ChangeNo);
            frmPrjBoqChangeForm form = new frmPrjBoqChangeForm(detailModel);
            form.MainHandler = this;
            AppForm.CurrentForm.ChangeForm("修改", form, changeInfo.ChangeNo);
            RefreshBottomBar();
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ContractBoqChangeInfo changeInfo = gvChange.GetFocusedRow() as ContractBoqChangeInfo;
            if (changeInfo == null)
            {
                XtraMessageBox.Show("请选中项");
                return;
            }
            if (changeInfo.Fixed.HasValue && changeInfo.Fixed.Value)
            {
                XtraMessageBox.Show("发布后不可删除");
                return;
            }
            if (XtraMessageBox.Show("确认删除该项？", "确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                viewModel.Delete(changeInfo);
                RefreshBottomBar();
            }
        } 
        public void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewModel.Load();
            RefreshBottomBar();
        }

        private void gvChange_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks >= 2)
            {
                ContractBoqChangeInfo changeInfo = gvChange.GetFocusedRow() as ContractBoqChangeInfo;
                if (changeInfo == null)
                { 
                    return;
                }
                ChangeDetailViewModel detailModel = new ChangeDetailViewModel(viewModel.ChangedList, projectNo);
                detailModel.Load(changeInfo.ChangeNo);
                frmPrjBoqChangeForm form = new frmPrjBoqChangeForm(detailModel);
                form.MainHandler = this;
                AppForm.CurrentForm.ChangeForm("修改", form, changeInfo.ChangeNo);
                RefreshBottomBar();
            }
        }

        /// <summary>
        /// 计算累计量并刷新底部状态栏
        /// </summary>
        private void RefreshBottomBar()
        {
            //计算合同币合计---------------------------------------------------------------
            decimal totalAmount = 0;//金额合计
            foreach (ContractBoqChangeInfo line in viewModel.ChangedList)
            {
                //金额
                totalAmount = totalAmount + line.ChangeAmount;
            }

            barAmount.Caption = string.Format("变更合计金额:{0}", totalAmount.ToString("g0"));
           
        }

        /// <summary>
        /// 点击打开附件查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvChange_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (gvChange.FocusedRowHandle < 0)
                return;
            if (e.Column != colHasAttachmnt)
                return;
            ContractBoqChangeInfo row = gvChange.GetFocusedRow() as ContractBoqChangeInfo;
            if (row != null && row.HasAttachmnt)
            {
                frmPrjBoqChangeAttachmnt attaForm = new frmPrjBoqChangeAttachmnt();
                attaForm.RefBoqChangeInfo = row;
                attaForm.ShowDialog();
            }
        }
    }
}
