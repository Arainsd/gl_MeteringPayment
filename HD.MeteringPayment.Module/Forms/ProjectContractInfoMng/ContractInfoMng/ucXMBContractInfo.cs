using Erp.SharedLib.Presentation.ControlBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Domain.Entity.ContractEntity;
using HD.MeteringPayment.Module.Forms.ProjectContractInfoMng.ContractInfoMng;
using HD.MeteringPayment.Module.BootLoader;
using HD.MeteringPayment.Module.Forms.ProjectContractInfoMng.XMBProjectInfoMng;

namespace HD.MeteringPayment.Module.Forms.ProjectInfoMng
{
    public partial class ucXMBContractInfo : GpControlBase
    {
        static Guid SignGuid = Guid.NewGuid();
        private List<XMBContractInfo> LstXMBContractInfo;
        private MeteringPaymentClient client = new MeteringPaymentClient();
        public override Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }
        public ucXMBContractInfo()
        {
            InitializeComponent();
            LoadGrid();
            gvXMBContractInfo.RowClick += gvXMBContractInfo_RowClick;
        }
        public void LoadGrid()
        {
            DoWork("加载合同数据", "加载合同数据", () =>
            {
                LstXMBContractInfo = client.GetIXMBContractInfoService().GetList("");
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcXMBContractInfo.DataSource = LstXMBContractInfo;
                }
            });
        }
        private void gvXMBContractInfo_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                if (gvXMBContractInfo.GetRow(e.RowHandle) != null)
                {
                    XMBContractInfo SelectXMBContractInfo = gvXMBContractInfo.GetRow(e.RowHandle) as XMBContractInfo;
                    if (SelectXMBContractInfo != null)
                    {
                        frmXMBContractInfo form = new frmXMBContractInfo();
                        form.RefXMBContractInfo = SelectXMBContractInfo;
                        //form.IsEdit = false;
                        AppForm.CurrentForm.ChangeForm("合同信息查看", form);
                    }
                }
            }
        }

        private void OpenDetail()
        {
            XMBContractInfo SelectXMBContractInfo = gvXMBContractInfo.GetFocusedRow() as XMBContractInfo;
            if (SelectXMBContractInfo != null)
            {
                frmXMBContractInfo form = new frmXMBContractInfo();
                form.RefXMBContractInfo = SelectXMBContractInfo;
                //form.IsEdit = false;
                AppForm.CurrentForm.ChangeForm("合同信息查看", form);
            }
        }

        private void XMBContractInfoOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenDetail();
        }

        private void XMBContractInfoRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoWork("加载合同数据", "加载合同数据", () =>
            {
                LstXMBContractInfo = client.GetIXMBContractInfoService().GetList("");
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcXMBContractInfo.DataSource = LstXMBContractInfo;
                }
            });
        }


    }
}
