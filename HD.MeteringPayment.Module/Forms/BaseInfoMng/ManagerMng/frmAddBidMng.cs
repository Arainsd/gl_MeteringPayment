using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraTreeList;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Module.BootLoader.Config;
using Erp.SharedLib.Presentation.FormBases;
using HD.MeteringPayment.Domain.Client;
using Erp.CommonData;

namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    public partial class frmAddBidMng : GpFormBase
    {
        #region 变量声明
        public frmFGSManager mainHandle;
        public List<Manager> LstExitManager;
        private IManager client;
        #endregion
        public frmAddBidMng()
        {
            InitializeComponent();
            client = new MeteringPaymentClient().GetIManagerService();
            tlBid.FilterNode += OnFilterNode;
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

        private void frmAddBidMng_Load(object sender, EventArgs e)
        {
            UserSelector.Properties.DataSource = new MeteringPaymentClient().GetIGpuserService().GetList(AppConfig.GlOrgCode);
            LoadBidData();
        }

        private void LoadBidData()
        {
            if (AppConfig.LstBid == null)
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
                        if (ex == null)
                        {
                            AppConfig.LstBid = result as List<ProjectBid>;
                            tlBid.DataSource = AppConfig.LstBid;
                            tlBid.ExpandAll();
                        }
                    });
            }
            else
            {
                tlBid.DataSource = AppConfig.LstBid;
                tlBid.RefreshDataSource();
                tlBid.ExpandAll();
                tlBid.FocusedNode = null;
            }
        }

        /// <summary>
        /// 选中该用户已经存在的标段
        /// </summary>
        private void SetCheckedNode()
        {
            if (UserSelector.EditValue == null || String.IsNullOrEmpty(UserSelector.EditValue.ToString()))
                return;
            string loginName = UserSelector.EditValue.ToString();
            List<Manager> lstMng = LstExitManager.FindAll(m => m.LoginName == loginName && m.UserType == 2);
            if (lstMng == null || lstMng.Count == 0)
                return;
        }

        private void tlBid_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            //if (e.PrevState == CheckState.Unchecked)
            //{
            //    if (e.Node.HasChildren)//如果存在子节点，则不可以选中
            //    {
            //        e.Node.CheckState = CheckState.Unchecked;
            //        e.CanCheck = false;
            //        return;
            //    }
            //}
        }
        /// <summary>
        /// 选中根节点后选中子节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlBid_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if(e.Node.HasChildren)
            {
                foreach (TreeListNode child in e.Node.Nodes)
                {
                    CheckChildNode(child);
                }
            }
        }

        private void CheckChildNode(TreeListNode node)
        {
            node.CheckState = CheckState.Checked;
            if(node.HasChildren)
            {
                foreach(TreeListNode child in node.Nodes)
                {
                    CheckChildNode(child);
                }
            }
        }

        private void BarSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (UserSelector.EditValue == null || String.IsNullOrEmpty(UserSelector.EditValue.ToString()))
            {
                XtraMessageBox.Show("请选择一个用户");
                return;
            }
            DoWork("保存中，请稍候...", "保存数据",
                () =>
                {
                    string loginName = UserSelector.EditValue.ToString();
                    List<TreeListNode> lstCheckedNodes = tlBid.GetAllCheckedNodes();  //所有选中的节点
                    List<ProjectBid> lstCheckedBids = new List<ProjectBid>(); //所有选中的节点的标段信息
                    List<Manager> lstAddedMng = new List<Manager>();  //需要新增的用户
                    List<Manager> lstDelMng = new List<Manager>();  //需要删除的用户
                    lstCheckedNodes.ForEach(m =>  //遍历所选中的标段找需要新增的信息
                    {
                        if (!m.HasChildren)
                        {
                            ProjectBid bidInfo = tlBid.GetDataRecordByNode(m) as ProjectBid;
                            if (bidInfo != null)
                            {
                                lstCheckedBids.Add(bidInfo);
                                if (LstExitManager.FindIndex(n => n.LoginName == loginName && n.OrgNo == bidInfo.BidNo && n.UserType == 2) == -1) //选中的标段监理管理员不存在该用户
                                {
                                    Manager newManager = new Manager();
                                    newManager.LoginName = loginName;
                                    newManager.UserName = UserSelector.Text;
                                    newManager.UserType = 2;
                                    newManager.OrgNo = bidInfo.BidNo;
                                    newManager.OrgName = bidInfo.BidName;

                                    lstAddedMng.Add(newManager);
                                }
                            }
                        }
                    });

                    LstExitManager.ForEach(m =>   //遍历该用户正在管理的标段找需要删除的信息
                    {
                        if (m.LoginName == loginName && m.UserType == 2)
                        {
                            if (lstCheckedBids.FindIndex(n => n.BidNo == m.OrgNo) == -1)  //如果该用户该标段未被选中，则删除该标段信息
                            {
                                lstDelMng.Add(m);
                            }
                        }
                    });

                    client.Save(lstAddedMng, null, lstDelMng, LoginInfor.LoginName);
                },
                (ex) =>
                {
                    if(ex == null)
                    {
                        if (mainHandle != null)
                        {
                            mainHandle.LoadGrid();
                        }
                        XtraMessageBox.Show("保存成功");
                        this.Close();
                    }
                });
        }

        /// <summary>
        /// 勾选全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarCheckAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlBid.CheckAll();
        }

        /// <summary>
        /// 取消勾选全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarUnCheckAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlBid.UncheckAll();
        }
    }
}