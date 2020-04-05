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
    public partial class frmXMBManager : GpControlBase
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
        public frmXMBManager()
        {
            InitializeComponent();
        }

        private void frmXMBManager_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        public void LoadGrid()
        {
            DoWork("加载管理员数据", "加载管理员数据", () =>
            {
                LstManager = client.GetIManagerService().GetList("where UserType=1 ORDER BY ProjectName ASC");
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcXMBMng.DataSource = LstManager;
                    
                    gvXMBMng.RefreshData();
                    gvXMBMng.ExpandAllGroups();
                }
            });
        }


        private void gcXMBMng_Click(object sender, EventArgs e)
        {

        }
        private void gvXMBMng_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                Manager SelectXMBManager = gvXMBMng.GetRow(e.RowHandle) as Manager;
                if (SelectXMBManager != null)
                {
                    frmXMBMng form = new frmXMBMng();
                    form.RefXMBManager = SelectXMBManager;
                    //form.IsEdit = false;
                    form.MainHandler = this;
                    form.ShowDialog();
                }
            }
        }

        private void XMBMngAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Manager NewXMBMng = new Manager();
            NewXMBMng.UserType = 1;
            frmXMBMng form = new frmXMBMng();
            form.ExitsUserList = LstManager;
            form.RefXMBManager = NewXMBMng;
            //form.IsEdit = true;
            form.MainHandler = this;
            form.ShowDialog();            
        }

        private void XMBMngDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("是否确定删除", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Manager DeleteManager = gvXMBMng.GetFocusedRow() as Manager;
                if (DeleteManager != null)
                {
                    DoWork("删除数据", "删除数据", () =>
                    {
                        client.GetIManagerService().Delete(DeleteManager, LoginInfor.LoginName);
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

        private void XMBMngRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadGrid();
        }

    }
}
