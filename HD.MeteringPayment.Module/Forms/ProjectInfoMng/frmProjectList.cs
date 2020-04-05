using Erp.SharedLib.Presentation.ControlBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HD.MeteringPayment.Module.Forms.ProjectInfoMng
{
    public partial class frmProjectList : GpControlBase
    {
        static Guid SignGuid = Guid.NewGuid();
        public override Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }
        public frmProjectList()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
