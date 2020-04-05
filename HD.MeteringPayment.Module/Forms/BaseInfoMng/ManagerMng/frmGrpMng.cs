using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Erp.CommonData;
using Erp.CommonData.Entity.Business;
using Erp.GpServiceClient;
using Erp.SharedLib.Presentation.FormBases;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DataConvertor;
using HD.MeteringPayment.Module.BootLoader.Config;

namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    public partial class frmGrpMng : GpFormBase
    {
        #region 变量定义
        /// <summary>
        /// 用户列表
        /// </summary>
        private List<CacheGpuser> LstUser;
        /// <summary>
        /// 管理员列表
        /// </summary>
        private List<Manager> LstManager = new List<Manager>();
        public List<Manager> LstExitManager;
        /// <summary>
        /// 父窗体
        /// </summary>
        public frmGSManager MainHandler = null;
        #endregion

        public frmGrpMng()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            DoWork("获取用户数据", "获取用户数据", () =>
            {
                LstUser = new MeteringPaymentClient().GetIGpuserService().GetList(AppConfig.GlOrgCode);
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcPrjDeptMngLeft.DataSource = LstUser;
                }
            });
        }
        private void PrjDeptMngSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DoWork("保存数据", "保存数据", () =>
            {
                new MeteringPaymentClient().GetIManagerService().AddList(LstManager, LoginInfor.LoginName);
            }, (ex) =>
            {
                if (ex == null)
                {
                    if (MainHandler != null)
                        MainHandler.LoadGrid();
                    this.Close();
                }
            });
        }

        private void gvPrjDeptMngLeft_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                if (gvPrjDeptMngLeft.GetRow(e.RowHandle) != null)
                {
                    CacheGpuser clickUser = gvPrjDeptMngLeft.GetRow(e.RowHandle) as CacheGpuser;
                    if (clickUser != null)
                    {
                        if (LstExitManager.Exists(m => m.LoginName == clickUser.LoginName))
                        {
                            XtraMessageBox.Show("局管理员中已存在该用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            if (!LstManager.Exists(m => m.LoginName == clickUser.LoginName))
                            {
                                Manager NewManager = new Manager();
                                NewManager.LoginName = clickUser.LoginName;
                                NewManager.UserName = clickUser.UserName;
                                NewManager.UserType = 3;

                                ProjectBid bidInfo = AppConfig.LstBid.Find(m => m.BidNo == AppConfig.GlBidRootNo);
                                NewManager.OrgNo = bidInfo.BidNo;
                                NewManager.OrgName = bidInfo.BidName;
      
                                LstManager.Add(NewManager);
                                gcPrjDeptMngRight.DataSource = LstManager;
                                gcPrjDeptMngRight.RefreshDataSource();
                            }
                        }
                    }
                }
            }
        }

        private void gvPrjDeptMngRight_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                if (gvPrjDeptMngRight.GetRow(e.RowHandle) != null)
                {
                    Manager clickManager = gvPrjDeptMngRight.GetRow(e.RowHandle) as Manager;
                    if (clickManager != null)
                    {
                        LstManager.Remove(clickManager);
                        gcPrjDeptMngRight.DataSource = LstManager;
                        gcPrjDeptMngRight.RefreshDataSource();
                    }
                }
            }
        }




    }
}
