using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Data.Filtering.Helpers;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Module.BootLoader.Config;
using Erp.SharedLib.Presentation.ControlBases;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Module.Forms.ProgressMeteringMng;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng
{

    public partial class BidTreeSelectControl : GpControlBase
    {

        #region 
        /// <summary>
        /// Type 加载表单类型：1合同清单，2WBS清单，3进度计量，4计量报表
        /// </summary>
        private int Type = 1;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">Type 加载表单类型：1合同清单，2WBS清单，3进度计量，4计量报表</param>
        public BidTreeSelectControl(int Type)
        {
            InitializeComponent();
            tlBid.FilterNode += OnFilterNode;

            tlBid.ActiveFilterString = GenerateFliterString();
            this.Type = Type;
            this.Load += BidTreeSelectControl_Load;
        }

        private void BidTreeSelectControl_Load(object sender, EventArgs e)
        {
            this.ParentForm.Shown += ParentForm_Shown;
        }

        private void ParentForm_Shown(object sender, EventArgs e)
        {
            LoadBidData();
        }

        private void LoadBidData()
        {
            if(AppConfig.LstBid == null)
            {
                //加载标段
                DoWorkRun("加载标段数据中，请稍候...", "加载数据中",
                    () =>
                    {
                        List<ProjectBid> result = new List<ProjectBid>();
                        result = new MeteringPaymentClient().GetIProjectBidService().GetList("WHERE StatId = 1");
                        return result;
                    },
                    (result, ex) =>
                    {
                        if(ex == null)
                        {
                            AppConfig.LstBid = result as List<ProjectBid>;
                            tlBid.DataSource = AppConfig.LstBid;
                            tlBid.ExpandAll();
                        }
                    });
            }
            else
            {
                tlBid.FocusedNodeChanged -= tlBid_FocusedNodeChanged;
                tlBid.DataSource = AppConfig.LstBid;
                tlBid.FocusedNodeChanged += tlBid_FocusedNodeChanged;

                tlBid.RefreshDataSource();
                tlBid.ExpandAll();
                tlBid.FocusedNode = null;
            }
        }

        #region 行过滤查找  ，避免treelist只筛选第一层的BUG
        private void OnFilterNode(object sender, FilterNodeEventArgs e)
        {
            if (!Object.ReferenceEquals(tlBid.ActiveFilterCriteria, null))
            {
                e.Handled = true;
                e.Node.Visible = IsNodeMatchFilter(e.Node);
            }
        }
        static bool IsNodeMatchFilter(TreeListNode node)
        {
            bool isVisible = true;
            if (!Object.ReferenceEquals(node.TreeList.ActiveFilterCriteria, null))
            {
                ExpressionEvaluator ee = new ExpressionEvaluator(TypeDescriptor.GetProperties(typeof(ProjectBid)), node.TreeList.ActiveFilterCriteria, false);
                isVisible = ee.Fit(node.TreeList.GetDataRecordByNode(node));
                if (!isVisible)
                {
                    foreach (TreeListNode n in node.Nodes)
                    {
                        if (IsNodeMatchFilter(n))
                        {
                            isVisible = true;
                            break;
                        }
                    }
                }
            }
            return isVisible;

        }
        #endregion

        private void tlBid_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            ProjectBid focusedNode = tlBid.GetDataRecordByNode(tlBid.FocusedNode) as ProjectBid;
            if(focusedNode != null)
            {
                switch(Type)
                {
                    case 1:
                        {
                            InitContractBoqConttrol(focusedNode.ProjectNo, focusedNode.BidName);
                            break;
                        }
                    case 2:
                        {
                            InitWBSConttrol(focusedNode.ProjectNo, focusedNode.BidName);
                            break;
                        }
                    case 3:
                        {
                            InitPMForm(focusedNode.ProjectNo, focusedNode.BidName);
                            break;
                        }
                    case 4:
                        {
                            InitMeteringRptMng(focusedNode.ProjectNo, focusedNode.BidName);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 加载合同清单表单
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="ProjectName"></param>
        private void InitContractBoqConttrol(string ProjectNo, string ProjectName)
        {
            panelControl1.Controls.Clear();
            if (!String.IsNullOrEmpty(ProjectNo))
            {
                HD.MeteringPayment.Module.Forms.ContractBoqMng.frmPrjBoq form = new HD.MeteringPayment.Module.Forms.ContractBoqMng.frmPrjBoq(ProjectNo, ProjectName);
                form.Dock = DockStyle.Fill;
                panelControl1.Controls.Add(form);
            }
        }

        /// <summary>
        /// 加载WBS清单
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="ProjectName"></param>
        private void InitWBSConttrol(string ProjectNo, string ProjectName)
        {
            panelControl1.Controls.Clear();
            HD.MeteringPayment.Module.Forms.WBSBoqMng.frmPrjBoq WBSForm = new HD.MeteringPayment.Module.Forms.WBSBoqMng.frmPrjBoq(ProjectNo, ProjectName);
            WBSForm.Dock = DockStyle.Fill;
            panelControl1.Controls.Add(WBSForm);
            WBSForm.LoadParameter();

        }
        /// <summary>
        /// 加载进度计量
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="ProjectName"></param>
        private void InitPMForm(string ProjectNo, string ProjectName)
        {
            panelControl1.Controls.Clear();
            PMListControl PMListControlForm = new PMListControl(ProjectNo, ProjectName, AppConfig.GetRoleLevel());
            PMListControlForm.Dock = DockStyle.Fill;
            PMListControlForm.LoadParameter();
            panelControl1.Controls.Add(PMListControlForm);
            PMListControlForm.LoadParameter();
        }


        /// <summary>
        /// 加载计量报表
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="ProjectName"></param>
        private void InitMeteringRptMng(string ProjectNo, string ProjectName)
        {
            panelControl1.Controls.Clear();
            if (!String.IsNullOrEmpty(ProjectNo))
            {
                HD.MeteringPayment.Module.Forms.MeteringRptMng.MeteringRptLstControl form = new HD.MeteringPayment.Module.Forms.MeteringRptMng.MeteringRptLstControl(ProjectNo, ProjectName);
                form.Dock = DockStyle.Fill;
                panelControl1.Controls.Add(form);
                form.LoadParameter(null);
            }
        }

        private String GenerateFliterString()
        {
            string strFliter = "";
            switch(AppConfig.CurrentLoginUser.LoginRole)
            {
                case AppConfig.ManagerRole.FGSMngr:
                    {
                        AppConfig.CurrentLoginUser.LstFGSOrgs.ForEach(m=>
                        {
                            if(String.IsNullOrEmpty(strFliter))
                            {
                                strFliter = String.Format("BidNo = '{0}' ", m.BidNo);
                            }
                            else
                            {
                                strFliter += String.Format(" OR BidNo = '{0}'", m.BidNo);
                            }
                        });
                        break;
                    }
            }
            return strFliter;
        }
    }
}
