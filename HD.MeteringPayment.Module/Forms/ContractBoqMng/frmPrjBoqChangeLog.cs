using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng
{
    public partial class frmPrjBoqChangeLog : Form
    {
       public  String ItemNo { get; set; }
        IContractBoqChange boqChangeService;
        public frmPrjBoqChangeLog(String ItemNo)
        {
            InitializeComponent();
            this.ItemNo = ItemNo;
            boqChangeService = new MeteringPaymentClient().GetIContractBoqChangeService();
            LoadData();
        }
        public void LoadData()
        {
            gridControl1.DataSource= boqChangeService.GetItemChangeLog(ItemNo);
        }

        private void bbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridExportHelper.ExportExcel(bandedGridView1, "清单项变更");
        }
    }
}
