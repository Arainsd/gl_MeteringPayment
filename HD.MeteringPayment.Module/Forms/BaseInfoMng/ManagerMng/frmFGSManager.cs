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
    public partial class frmFGSManager : GpControlBase
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
        public frmFGSManager()
        {
            InitializeComponent();
        }

        private void frmFGSManager_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        public void LoadGrid()
        {
            DoWork("加载管理员数据", "加载管理员数据", () =>
            {
                LstManager = client.GetIManagerService().GetList("where UserType=2 ORDER BY LoginName ASC, OrgNo ASC");
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcManager.DataSource = LstManager;
                    gvManager.ExpandAllGroups();
                }
            });
        }


        private void gcManager_Click(object sender, EventArgs e)
        {

        }


        private void FGSMngAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Manager NewFGSMng = new Manager();
            NewFGSMng.UserType = 2;
            //frmCompanyMng form = new frmCompanyMng();
            frmAddBidMng form = new frmAddBidMng();
            form.LstExitManager = LstManager;
            form.mainHandle = this;
            form.ShowDialog();
        }

        private void FGSMngDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Manager DeleteManager = gvManager.GetFocusedRow() as Manager;
            if (DeleteManager != null)
            {
                if (XtraMessageBox.Show(String.Format("是否确定删除用户：[{0}]", DeleteManager.UserName), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    List<Manager> deleteLst = new List<Manager>();
                    deleteLst = LstManager.FindAll(m => m.LoginName == DeleteManager.LoginName);
                    DoWork("删除数据", "删除数据", () =>
                    {
                        deleteLst.ForEach(m =>
                        {
                            client.GetIManagerService().Delete(m, LoginInfor.LoginName);
                        });
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

        private void FGSMngRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadGrid();
        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarExpand_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvManager.ExpandAllGroups();
        }
        /// <summary>
        /// 折叠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarShrink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvManager.CollapseAllGroups();
        }

        private void gvManager_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            Manager manager1 = gvManager.GetRow(e.RowHandle1) as Manager;
            Manager manager2 = gvManager.GetRow(e.RowHandle2) as Manager;
            if(manager1 != null && manager2 != null && manager1.LoginName == manager2.LoginName)
            {
                e.Merge = true;
            }
        }
    }
}
