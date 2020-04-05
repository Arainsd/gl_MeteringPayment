using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using Erp.SharedLib.Presentation.ControlBases;

namespace HD.MeteringPayment.Module.Forms.MeteringRptMng
{
    public partial class PrjAmountWbsRptDetailForm : GpControlBase
    {

        #region 
        public PrjAmountWbsRpt DataSource
        {
            get
            {
                return bindingSource1.DataSource as PrjAmountWbsRpt;
            }
            set
            {
                bindingSource1.DataSource = value;
                bindingSource1.ResetBindings(true);
            }
        }

        #endregion

        public PrjAmountWbsRptDetailForm()
        {
            InitializeComponent();
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.bindingSource1.EndEdit();
            Erp.GpServiceClient.GpClient.CreateInstance().Execute(string.Format(@"update ERP_Subpay.dbo.rpt_prjamountwbsrpt set Description='{0}'
                                                             WHERE PrjamountNo = '{1}' and WbsLineNo='{2}'", memoEdit1.Text.Trim(), DataSource.PrjamountNo, DataSource.WbsLineNo), System.Data.CommandType.Text);
            XtraMessageBox.Show("保存成功");
        }
    }
}
