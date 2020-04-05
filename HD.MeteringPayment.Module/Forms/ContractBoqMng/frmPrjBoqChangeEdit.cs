using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using Erp.SharedLib.Presentation.ControlBases;
using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng
{
    public partial class frmPrjBoqChangeEdit : XtraForm
    {

        /// <summary>
        /// 项目编号
        /// </summary>
        public String ProjectNo { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public String ProjectName { get; set; }
        /// <summary>
        /// 管理清单的视图模型
        /// </summary>
        private ContractBoqChanegEditViewModel viewModel;
        private IContractBoq projectBoq;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ProjectNo">项目编号</param>
        public frmPrjBoqChangeEdit(ContractBoqChanegEditViewModel viewModel, String ProjectNo, String ProjectName)
        {
            InitializeComponent();

            this.ProjectNo = ProjectNo;
            this.ProjectName = ProjectName;
            this.viewModel = viewModel;
            projectBoq = new MeteringPaymentClient().GetIContractBoqService();
            tlBoi.ActiveFilterString = "StatId=1";
            this.Shown += ParentForm_Shown;
            viewModel.Editing = true;
        }
        /// <summary>
        /// 窗体第一次显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentForm_Shown(object sender, EventArgs e)
        {
            viewModel.Load();

            tlBoi.DataSource = viewModel.NodeList;
            RefreshStat();
        }

        /// <summary>
        /// 更新窗体各控件的可编辑和显示性
        /// </summary>
        public void RefreshStat()
        {
            //刷新更菜单的可见性 
            bbiSave.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiSave.Enabled = viewModel.Editing;
            bbiNew.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNew.Enabled = viewModel.Editing;
            bbiNewChild.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNewChild.Enabled = viewModel.Editing;
            bbiDelete.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDelete.Enabled = viewModel.Editing;
            bbiImport.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImport.Enabled = viewModel.Editing;
            bbiRefresh.Visibility = !viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRefresh.Enabled = !viewModel.Editing;
            tlBoi.OptionsBehavior.Editable = viewModel.Editing;
            bbiChangeStat.Visibility = false && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiChangeStat.Enabled = false && viewModel.Editing;
            bbiShowStat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }
        /// <summary>
        /// 进入编辑状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewModel.Editing = true;
            RefreshStat();
        }
        /// <summary>
        /// 折叠显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCollapse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlBoi.CollapseAll();
        }
        /// <summary>
        /// 展示显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExpand_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlBoi.ExpandToLevel(1);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            viewModel.Load();
            tlBoi.DataSource = viewModel.NodeList;
            RefreshStat();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridExportHelper.ExportExcel(tlBoi, ProjectName + "-清单");
        }
        /// <summary>
        /// 新增项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ContractBoiChangeNode nodeNew;
            ContractBoiChangeNode nodeSelected = tlBoi.GetDataRecordByNode(tlBoi.FocusedNode) as ContractBoiChangeNode;
            nodeNew = viewModel.InsertNode(nodeSelected != null ? nodeSelected.ParentBoiNode : null);
            TreeListNode tlnNode = tlBoi.FindNodeByKeyID(nodeNew.ItemCode);
            tlBoi.MakeNodeVisible(tlnNode);
            tlBoi.SetFocusedNode(tlnNode);
        }
        /// <summary>
        /// 新增子项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiNewChild_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ContractBoiChangeNode nodeNew;
            ContractBoiChangeNode nodeSelected = tlBoi.GetDataRecordByNode(tlBoi.FocusedNode) as ContractBoiChangeNode;
            if (nodeSelected == null)
            {
                XtraMessageBox.Show("请选中项!");
                return;
            }
            nodeNew = viewModel.InsertNode(nodeSelected);
            TreeListNode tlnNode = tlBoi.FindNodeByKeyID(nodeNew.ItemCode);
            tlBoi.MakeNodeVisible(tlnNode);
            tlBoi.SetFocusedNode(tlnNode);
        }
        /// <summary>
        /// 删除或禁用项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ContractBoiChangeNode> lstDeleteNode;
            List<TreeListNode> lstNode = tlBoi.GetAllCheckedNodes();
            List<ContractBoiChangeNode> lstBoiNode = lstNode.ConvertAll<ContractBoiChangeNode>(m => tlBoi.GetDataRecordByNode(m) as ContractBoiChangeNode);
            if (lstBoiNode.Count == 0)
            {
                XtraMessageBox.Show("请选中项!");
                return;
            }
            if (lstBoiNode.Exists(m => m.IsUse))
            {
                XtraMessageBox.Show("存在已关联项，请先删除关联关系后再删除关联项！", "警告", MessageBoxButtons.OK);
                return;
            }
            if (XtraMessageBox.Show("是否删除项？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            { 
                return;
            }
            lstDeleteNode = lstBoiNode.FindAll(m => !lstBoiNode.Exists(n => n.Children.Contains(m)));
            lstDeleteNode.ForEach(m =>
            {
                //if (m.OriginalBoi == null)
                //{
                    viewModel.DeleteNode(m);
                //}
                //else
                //{
                //    //viewModel.ChangeNodeStat(m, false);
                //}
            });
        }
        /// <summary>
        /// 启用项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiChangeStat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ContractBoiChangeNode> lstDeleteNode;
            List<TreeListNode> lstNode = tlBoi.GetAllCheckedNodes();
            List<ContractBoiChangeNode> lstBoiNode = lstNode.ConvertAll<ContractBoiChangeNode>(m => tlBoi.GetDataRecordByNode(m) as ContractBoiChangeNode);
            if (lstBoiNode.Count == 0)
            {
                XtraMessageBox.Show("请选中项!");
                return;
            }
            if (!lstBoiNode.Exists(m => m.StatId == 0))
            {
                XtraMessageBox.Show("没有禁用项！", "提示");
                return;
            }
            else
            {
                if (XtraMessageBox.Show("是否启用项？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }
            lstDeleteNode = lstBoiNode.FindAll(m => !lstBoiNode.Exists(n => n.Children.Contains(m)));
            lstDeleteNode.ForEach(m =>
            {
                if (m.StatId == 0)
                {
                    //viewModel.DeleteNode(m);
                    viewModel.ChangeNodeStat(m, true);
                }
            });
        }
        /// <summary>
        /// TreeList的样式处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlBoi_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            ContractBoiChangeNode boiNode = tlBoi.GetDataRecordByNode(e.Node) as ContractBoiChangeNode;
            if (boiNode != null && boiNode.StatId == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                e.Appearance.ForeColor = Color.Gray;
            }
            if (viewModel.Editing)
            {
                // if(boiNode.StatId==1)
            }
        }

        private void bbiImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        /// <summary>
        /// 锁定清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewModel.Lock();
            RefreshStat();
        }
        /// <summary>
        /// 保存清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlBoi.PostEditor();
            viewModel.SaveChanged();
            XtraMessageBox.Show("生成变更清单成功！");
            this.Close();
        }

        private void bbiShowStat_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bbiShowStat.Checked)
            {
                tlBoi.ActiveFilterString = "";
            }
            else
            {
                tlBoi.ActiveFilterString = "StatId=1";
            }
        }
    }
}
