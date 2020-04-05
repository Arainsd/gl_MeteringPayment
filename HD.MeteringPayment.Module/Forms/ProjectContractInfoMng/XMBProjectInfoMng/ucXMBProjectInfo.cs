using Erp.SharedLib.Presentation.ControlBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Module.Forms.ProjectContractInfoMng.XMBProjectInfoMng;
using HD.MeteringPayment.Module.BootLoader;

namespace HD.MeteringPayment.Module.Forms.ProjectInfoMng
{
    public partial class ucXMBProjectInfo : GpControlBase
    {
        static Guid SignGuid = Guid.NewGuid();
        private List<XMBProjectInfo> LstXMBProjectInfo;
        private MeteringPaymentClient client = new MeteringPaymentClient();
        public override Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }
        public ucXMBProjectInfo()
        {
            InitializeComponent();
            Init();
            LoadGrid();
            gvXMBProjectInfo.RowClick += gvXMBProjectInfo_RowClick;
        }

        private void Init()
        {
            //加载项目结算表
            DataTable tbProjectBalance = Erp.GpServiceClient.GpClient.CreateInstance().Execute(String.Format(@"SELECT *
                                                                                                                 FROM ERP_Project.dbo.project_balance"), CommandType.Text).Data.Tables[0];

            //加载项目阶段信息
            DataTable tbStage = Erp.GpServiceClient.GpClient.CreateInstance().Execute(String.Format(@"SELECT * 
                                                                                                    FROM ERP_Project.dbo.prj_stage
                                                                                                    WHERE ParentNo = '03' AND StageName <> '完工'
                                                                                                    ORDER BY StageNo ASC"), CommandType.Text).Data.Tables[0];

            stageEdit.DataSource = tbStage;
            balanceEdit.DataSource = tbProjectBalance;
        }

        public void LoadGrid()
        {
            DoWork("加载项目数据", "加载项目数据", () =>
            {
                LstXMBProjectInfo = client.GetIXMBProjectInfoService().GetList("");
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcXMBProjectInfo.DataSource = LstXMBProjectInfo;
                }
            });
        }


        
        private void XMBProjectInfoOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenDetail();
        }

        private void XMBProjectInfoRef_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoWork("加载项目数据", "加载项目数据", () =>
            {
                LstXMBProjectInfo = client.GetIXMBProjectInfoService().GetList("");
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcXMBProjectInfo.DataSource = LstXMBProjectInfo;
                }
            });
        }

        private void gvXMBProjectInfo_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                if (gvXMBProjectInfo.GetRow(e.RowHandle) != null)
                {
                    XMBProjectInfo SelectXMBProjectInfo = gvXMBProjectInfo.GetRow(e.RowHandle) as XMBProjectInfo;
                    if (SelectXMBProjectInfo != null)
                    {
                        frmXMBProjectInfo form = new frmXMBProjectInfo();
                        form.RefXMBProjectInfo = SelectXMBProjectInfo;
                        form.IsEdit = false;
                        AppForm.CurrentForm.ChangeForm("项目信息修改",form);
                    } 
                }
            }
        }
        private void OpenDetail()
        {
            XMBProjectInfo SelectXMBProjectInfo = gvXMBProjectInfo.GetFocusedRow() as XMBProjectInfo;
            if (SelectXMBProjectInfo != null)
            {
                frmXMBProjectInfo form = new frmXMBProjectInfo();
                form.RefXMBProjectInfo = SelectXMBProjectInfo;
                form.IsEdit = false;
                AppForm.CurrentForm.ChangeForm("项目信息修改", form);
            } 
        }

    }
}

