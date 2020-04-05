using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using System.Collections.Generic;
using Erp.CommonData;

namespace HD.MeteringPayment.Module.Forms.MeteringRptMng.PrintListPayForms
{
    public partial class PrintListPayForm : DevExpress.XtraReports.UI.XtraReport
    {
        private MeteringPayRptRoot _root = new MeteringPayRptRoot();

        public MeteringPayRptRoot Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
                if (value != null)
                {
                    bindingSource1.DataSource = value;
                }
            }
        }

        public PrintListPayForm()
        {
            InitializeComponent();
            xp1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            xp2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            xp3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            xp4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage; 
        }

        /// <summary>
        /// 如果绑定的时候值为0，则不绑定任何值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCell_EvaluateBinding(object sender, BindingEventArgs e)
        {
            decimal value = ObjectHelper.GetDefaultDecimal(e.Value);
            if (value == 0)
            {
                e.Value = null;
            }
        }
    }
}
