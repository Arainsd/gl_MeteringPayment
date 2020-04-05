using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Erp.SharedLib.Presentation.ControlBases;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Domain.Client;
using DevExpress.XtraEditors;
using Erp.CommonData;

namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    public partial class frmGSManager : GpControlBase
    {
         static Guid SignGuid = Guid.NewGuid();
        private List<Manager> LstManager;
        private MeteringPaymentClient client = new MeteringPaymentClient();
        public override Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }
        public frmGSManager()
        {
            InitializeComponent();
        }

        private void frmGSManager_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        public void LoadGrid()
        {
            DoWork("加载管理员数据", "加载管理员数据", () =>
            {
                LstManager = client.GetIManagerService().GetList("where UserType=3");
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcGSManager.DataSource = LstManager;
                }
            });
        }


        private void gcGSManager_Click(object sender, EventArgs e)
        {

        }



        private void GSMngAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Manager NewGSMng = new Manager();
            NewGSMng.UserType = 3;
            frmGrpMng form = new frmGrpMng();
            form.LstExitManager = LstManager;
            form.MainHandler = this;
            form.ShowDialog();
        }

        private void GSMngDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("是否确定删除", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Manager DelGSManager = gvGSManager.GetFocusedRow() as Manager;
                if (DelGSManager != null)
                {
                    DoWork("删除数据", "删除数据", () =>
                    {
                        new MeteringPaymentClient().GetIManagerService().Delete(DelGSManager, LoginInfor.LoginName);
                    }, (ex) =>
                    {
                        if (ex == null)
                        {
                            LoadGrid();
                        }
                    });
                }
            }
        }

        private void GSMngCancle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoWork("加载管理员数据", "加载管理员数据", () =>
            {
                LstManager = client.GetIManagerService().GetList("where UserType=3");
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcGSManager.DataSource = LstManager;
                }
            });
        }

      
    }
}
