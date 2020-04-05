using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using HD.MeteringPayment.Module.BootLoader.Config;

namespace HD.MeteringPayment.Module.Forms.MeteringRptMng
{
    public partial class PrintTitleForm : DevExpress.XtraReports.UI.XtraReport
    {
        public PrintTitleForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public PrjAmountRpt Datasource
        {
            get
            {
                return bindingSource1.DataSource as PrjAmountRpt;
            }
            set
            {
                bindingSource1.DataSource = value;
                bindingSource1.ResetBindings(true);
                //xrBarCode1.Text = String.Format("{0}{1}",AppConfig.PrjAmountQRCodeAddress, value.PrjamountNo).Trim();
                //xrBarCode1.Text = String.Format("http://192.168.2.200:8081/wfPlatform/meteringPayment/noFilter/{0}", value.PrjamountNo);


            }
        }

    }
}
