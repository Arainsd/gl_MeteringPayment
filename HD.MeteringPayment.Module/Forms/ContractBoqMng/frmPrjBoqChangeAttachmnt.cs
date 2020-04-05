using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Erp.SharedLib.Presentation.FormBases;
using Erp.SharedLib.Presentation.AttactmentForm;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng
{
    public partial class frmPrjBoqChangeAttachmnt : GpFormBase
    {
        #region 变量声明
        #region GpForm标识
        private static Guid _guid = Guid.NewGuid();
        public override Guid FormId
        {
	        get 
	        { 
		         return _guid;
	        }
        }
        #endregion
        GeneralUploadForm myAttachmentControl = new GeneralUploadForm();

        private ContractBoqChangeInfo _refBoqChangeInfo = new ContractBoqChangeInfo();
        public ContractBoqChangeInfo RefBoqChangeInfo
        {
            get
            {
                return _refBoqChangeInfo;
            }
            set
            {
                _refBoqChangeInfo = value;
                if (!String.IsNullOrEmpty(value.ChangeNo))
                {
                    myAttachmentControl.BusinessNo = value.ChangeNo;
                    myAttachmentControl.LoadParameter(value.ChangeNo);
                }
            }
        }
        #endregion
        public frmPrjBoqChangeAttachmnt()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            myAttachmentControl.Folder = AppConfig.SYSTEM_NO;
            myAttachmentControl.SetEditPanelVisable(false);
            myAttachmentControl.SetEditEnable(false);
            myAttachmentControl.Dock = DockStyle.Fill;
            this.Controls.Add(myAttachmentControl);
        }
    }
}