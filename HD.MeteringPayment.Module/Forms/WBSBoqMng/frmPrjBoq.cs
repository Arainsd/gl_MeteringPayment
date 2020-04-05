using DevExpress.Data.Filtering.Helpers;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Erp.CommonData;
using Erp.SharedLib.Presentation.ControlBases;
using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel;
using HD.MeteringPayment.Module.Forms.WBSBoqMng.ViewModel;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace HD.MeteringPayment.Module.Forms.WBSBoqMng
{
    //public partial class frmPrjBoq : XtraForm//GpControlBase
    public partial class frmPrjBoq :GpControlBase
    {
        static Guid SignGuid = Guid.NewGuid();
        public override Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }

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
        private WBSBoqViewModel viewModel;
        private IWBSBoq projectBoq;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ProjectNo">项目编号</param>
        public frmPrjBoq(String ProjectNo,String ProjectName)
        {
            InitializeComponent();
            pictureEdit1.Visible = false;
            this.ProjectNo = ProjectNo;
            this.ProjectName = ProjectName;
            projectBoq = new MeteringPaymentClient().GetIWBSBoqService();
            tlWBS.ActiveFilterString = "StatId=1";
            tlWBS.FilterNode += OnFilterNode;
        }
        /// <summary>
        /// 窗体装载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrjBoq_Load(object sender, EventArgs e)
        {
            tlWBS.DataSource = null;
            gcRelationDetail.DataSource = null;
            DoWork("加载清单中...", "加载清单中...", () =>
            {
                viewModel = new WBSBoqViewModel(ProjectNo, ProjectName);
                viewModel.RelationListChanged += ResetGridBingdings;
                viewModel.Load();
            }, (myException) =>
            {
                if (myException == null)
                {
                    tlWBS.DataSource = viewModel.NodeBindingSource;
                    tlWBS.ExpandAll();
                    gcRelationDetail.DataSource = viewModel.RelationBindingSource;
                    RefreshStat();
                }
            });
        }
        public override void LoadParameter(params object[] args)
        {
            
        }
        /// <summary>
        /// 窗体第一次显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentForm_Shown(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 重新绑定数据源
        /// </summary>
        private void ResetGridBingdings()
        {
            gcRelationDetail.DataSource = viewModel.RelationBindingSource;
            gvRelationDetail.RefreshData();
        }
        /// <summary>
        /// 更新窗体各控件的可编辑和显示性
        /// </summary>
        public void RefreshStat()
        {
            //刷新更菜单的可见性
            bbiEdit.Visibility = AppConfig.CurrentLoginUser.LoginRole == AppConfig.ManagerRole.XMBMngr && !viewModel.Editing && viewModel.BoqEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEdit.Enabled = !viewModel.BoqFixedStat && !viewModel.Editing && viewModel.BoqEdit;
            bbiSave.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiSave.Enabled = !viewModel.BoqFixedStat && viewModel.Editing;
            bbiCancel.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancel.Enabled = !viewModel.BoqFixedStat && viewModel.Editing;

            BarFix.Visibility = !viewModel.Editing && viewModel.Boq != null && !String.IsNullOrEmpty(viewModel.Boq.WbsNo) && !viewModel.BoqFixedStat? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarFix.Enabled = !viewModel.Editing && !viewModel.BoqFixedStat;
            BarUnFix.Visibility =/* !viewModel.Editing && viewModel.BoqFixedStat? DevExpress.XtraBars.BarItemVisibility.Always :*/ DevExpress.XtraBars.BarItemVisibility.Never;
            BarUnFix.Enabled = !viewModel.Editing && viewModel.BoqFixedStat;

            standaloneBarDockControlLeft.Visible = viewModel.Editing;  //左侧操作栏编辑时才显示
            BarWBSNew.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarWBSNew.Enabled = viewModel.Editing;
            BarWBSNewChild.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarWBSNewChild.Enabled = viewModel.Editing;
            BarWBSDelete.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarWBSDelete.Enabled = viewModel.Editing;
            BarWBSImport.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarWBSImport.Enabled = viewModel.Editing;

            standaloneBarDockControlRight.Visible = viewModel.Editing;  //右侧操作栏编辑时才显示

            bbiNew.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNew.Enabled = viewModel.Editing && tlWBS.FocusedNode != null && !tlWBS.FocusedNode.HasChildren;
            bbiDelete.Visibility = viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDelete.Enabled = viewModel.Editing && tlWBS.FocusedNode != null && !tlWBS.FocusedNode.HasChildren;

            BarExportRelationTemplate.Enabled = viewModel.Editing && viewModel.BoqEdit;

            //bbi_import.Enabled = !viewModel.Editing && viewModel.BoqEdit;
            bbiCollapse1.Enabled = !viewModel.Editing && viewModel.BoqEdit;
            bbiExpande.Enabled = !viewModel.Editing && viewModel.BoqEdit;

            tlWBS.OptionsBehavior.Editable = viewModel.Editing;
            gvRelationDetail.OptionsBehavior.Editable = viewModel.Editing;

            pictureEdit1.Visible = viewModel.BoqFixedStat;
        }

        /// <summary>
        /// 所选择的WBS清单项改变时，加载相应的WBS清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlWBS_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            WBSLineNode focusedNode = tlWBS.GetDataRecordByNode(tlWBS.FocusedNode) as WBSLineNode;
            if (focusedNode != null)
            {
                viewModel.CurrentSelectedNode = focusedNode;
                RefreshStat();
            }
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
        /// 取消编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("是否取消编辑？", "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(viewModel.HasChanged())
                      viewModel.Load();
                viewModel.Editing = false;
                RefreshStat();
            }
        }

        /// <summary>
        /// 保存清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.Validate())
            {
                XtraMessageBox.Show("输入不符合规则");
                return;
            }
            gvRelationDetail.PostEditor();

            if (viewModel.NodeBindingSource.Count == 0)
            {
                XtraMessageBox.Show("至少有一条WBS清单明细");
                return;
            }
            if (viewModel.NodeBindingSource.ToList().Exists(m => String.IsNullOrEmpty(m.WbsLineName) || String.IsNullOrEmpty(m.WbsLineName.Trim())))
            {
                XtraMessageBox.Show("WBS清单项名称不能为空");
                return;
            }

            DoWork("保存中...", "提示", () =>
            {
                tlWBS.DataSource = null;
                gcRelationDetail.DataSource = null;
                viewModel.Save();
            }, (myException) =>
            {
                if (myException == null)
                {
                    tlWBS.DataSource = viewModel.NodeBindingSource;
                    tlWBS.ExpandAll();
                    gcRelationDetail.DataSource = viewModel.RelationBindingSource;
                    gvRelationDetail.RefreshData();
                    RefreshStat();
                    XtraMessageBox.Show("保存成功！");
                }
            });
        }
       
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoWork("加载清单中...", "加载清单中...", () =>
            {
                tlWBS.DataSource = null;
                gcRelationDetail.DataSource = null;

                viewModel = new WBSBoqViewModel(ProjectNo,ProjectName);
                //viewModel.ListChanged += RefreshBottomBar;
                viewModel.Load();
            }, (myException) =>
            {
                if (myException == null)
                {
                    tlWBS.DataSource = viewModel.NodeBindingSource;
                    tlWBS.ExpandAll();

                    gcRelationDetail.DataSource = viewModel.RelationBindingSource;
                    gvRelationDetail.RefreshData();
                    RefreshStat();
                }
            });
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridExportHelper.ExportExcel(tlWBS, AppConfig.SelectProject.ProjectName + "-WBS清单");
        }
        /// <summary>
        /// 新增关联关系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvRelationDetail.PostEditor();
            if (!this.Validate())
            {
                XtraMessageBox.Show("输入不符合规则");
                return;
            }

            frmWBSRelation addForm = new frmWBSRelation();
            addForm.LstContractBoi = viewModel.remainContractBoiList;
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                List<ContractBoi> selectBoi = addForm.selectBoi;
                if (selectBoi != null && selectBoi.Count > 0)
                {
                    selectBoi.ForEach(m =>
                        {
                            viewModel.InsertCurrentSelectNodeRelation(m.ItemNo, 1);
                        });
                    gcRelationDetail.RefreshDataSource();
                }
            }
        }
        /// <summary>
        /// 删除关联关系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvRelationDetail.PostEditor();
            if (!this.Validate())
            {
                XtraMessageBox.Show("输入不符合规则");
                return;
            }

            WBSline_boi deleteRow = gvRelationDetail.GetFocusedRow() as WBSline_boi;
            if (deleteRow != null)
            {
                viewModel.DeleteRelation(deleteRow.ItemNo);
                gcRelationDetail.RefreshDataSource();
            }
        }

        /// <summary>
        /// TreeList的样式处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlBoi_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            WBSLineNode boiNode = tlWBS.GetDataRecordByNode(e.Node) as WBSLineNode;
            if (boiNode != null && !String.IsNullOrEmpty(boiNode.WbsLineNo) && boiNode.StatId == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                e.Appearance.ForeColor = Color.Gray;
            }
        }

        private void bbiImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataSet ds= Hondee.Common.Export.ExeclOperation.ToDataTable(openFileDialog1.FileName);
                try
                {
                    viewModel.ImportBoq(ds.Tables[0]);
                    XtraMessageBox.Show("数据成功导入到程序中，保存后生效！");
                }
                catch (BusinessException be)
                {
                    XtraMessageBox.Show(be.BusMessage);
                }
            }
        }

        /// <summary>
        /// 导出Wbs清单模板（还需修改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExportTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Title = "保存合同清单导入模板文件";
            //saveFileDialog.FileName = "合同清单导入模板.xls";
            //saveFileDialog.Filter = "Excel文件|*.xls";
            //if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
            //    Stream sm = asm.GetManifestResourceStream("HD.MeteringPayment.Module.TemplateXls.xls");
            //    using (FileStream sr = new FileStream(saveFileDialog.FileName, FileMode.Create))
            //    {
            //        int b = 0;
            //        while ((b = sm.ReadByte()) != -1)
            //        {
            //            sr.WriteByte((byte)b);
            //        }
            //    }
            //    OpenFile(saveFileDialog.FileName);
            //}
        }

        /// <summary>
        /// 导出关系模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarExportRelationTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {                 
                frmPrjBoqExport exportForm = new frmPrjBoqExport(viewModel.ProjectName + "WBS导入模板", viewModel.Boq.AllRelationList, new List<WBSLineNode>(viewModel.NodeBindingSource));
                exportForm.Export(); 
        }
        //private void BarExportRelationTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Title = "保存WBS关联项导入模板文件";
        //    saveFileDialog.FileName = "WBS关联项导入模板.xls";
        //    saveFileDialog.Filter = "Excel文件|*.xls";
        //    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
        //        Stream sm = asm.GetManifestResourceStream("HD.MeteringPayment.Module.WbsRelationTemplateXls.xls");
        //        using (FileStream sr = new FileStream(saveFileDialog.FileName, FileMode.Create))
        //        {
        //            int b = 0;
        //            while ((b = sm.ReadByte()) != -1)
        //            {
        //                sr.WriteByte((byte)b);
        //            }
        //        }
        //        OpenFile(saveFileDialog.FileName);
        //    }
        //}
        private static void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("打开导出文件?", "导出...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(UserLookAndFeel.Default, "找不到合适的应用来打开导出的文件.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region 行过滤查找  ，避免treelist只筛选第一层的BUG
        private void OnFilterNode(object sender, FilterNodeEventArgs e)
        {
            if (!Object.ReferenceEquals(tlWBS.ActiveFilterCriteria, null))
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
                ExpressionEvaluator ee = new ExpressionEvaluator(TypeDescriptor.GetProperties(typeof(WBSLineNode)), node.TreeList.ActiveFilterCriteria, false);
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

        /// <summary>
        /// 新增WBS项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarWBSNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlWBS.PostEditor();
            WBSLineNode nodeNew;
            //WBSLineNode nodeSelected = tlWBS.GetDataRecordByNode(tlWBS.FocusedNode) as WBSLineNode;
            //if (nodeSelected != null && nodeSelected.RelationList != null && nodeSelected.RelationList.Count > 0)
            //{
            //    XtraMessageBox.Show("选中的节点中存在合同清单关联项，请删除关联项后再新增该节点的合同清单关联项。");
            //    return;
            //}
            nodeNew = viewModel.InsertNode(/*nodeSelected != null ? nodeSelected.ParentBoiNode :*/ null);
            TreeListNode tlnNode = tlWBS.FindNodeByKeyID(nodeNew.WbsSysCode);
            tlWBS.MakeNodeVisible(tlnNode);
            tlWBS.SetFocusedNode(tlnNode);
        }

        /// <summary>
        /// 新增WBS子项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarWBSNewChild_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlWBS.PostEditor();
            WBSLineNode nodeNew;
            WBSLineNode nodeSelected = tlWBS.GetDataRecordByNode(tlWBS.FocusedNode) as WBSLineNode;
            if (nodeSelected == null)
            {
                XtraMessageBox.Show("请选中项!");
                return;
            }
            if (nodeSelected != null && nodeSelected.RelationList != null && nodeSelected.RelationList.Count > 0)
            {
                XtraMessageBox.Show("选中的节点中存在合同清单关联项，请删除关联项后再新增该节点的合同清单关联项。");
                return;
            }
            nodeNew = viewModel.InsertNode(nodeSelected);
            TreeListNode tlnNode = tlWBS.FindNodeByKeyID(nodeNew.WbsSysCode);
            tlWBS.MakeNodeVisible(tlnNode);
            tlWBS.SetFocusedNode(tlnNode);
        }

        /// <summary>
        /// 删除WBS节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarWBSDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<WBSLineNode> lstDeleteNode;
            List<TreeListNode> lstNode = tlWBS.GetAllCheckedNodes();
            List<WBSLineNode> lstBoiNode = lstNode.ConvertAll<WBSLineNode>(m => tlWBS.GetDataRecordByNode(m) as WBSLineNode);
            if (lstBoiNode.Count == 0 && (tlWBS.GetDataRecordByNode(tlWBS.FocusedNode) as WBSLineNode) == null)
            {

                XtraMessageBox.Show("请选中项!");
                return;
            }
            List<WBSline_boi> relationLst = new List<WBSline_boi>();
            lstBoiNode.ForEach(m=>
                {
                    relationLst.AddRange(viewModel.RecursionFindRelationShip(m));
                });
            if (relationLst != null && relationLst.Count > 0)
            {
                XtraMessageBox.Show("选中的节点或者其子节点中存在合同清单关联项，请删除关联项后再删除WBS清单项。");
                return;
            }
            //标识是否已同意删除
            bool blReadyGo = false;
            if (lstBoiNode.Exists(m => m.Children.Count > 0))
            {
                if (XtraMessageBox.Show("删除或禁用父项，将会删除其全部子项！是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
                blReadyGo = true;
            }

            if (!blReadyGo)
            {
                if (XtraMessageBox.Show("是否删除项？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }

            lstDeleteNode = lstBoiNode.FindAll(m => !lstBoiNode.Exists(n => n.Children.Contains(m)));
            if (lstDeleteNode != null && lstDeleteNode.Count != 0)
            {
                lstDeleteNode.ForEach(m =>
                {
                    viewModel.DeleteNode(m);
                });
            }
            else
            {
                viewModel.DeleteNode(tlWBS.GetDataRecordByNode(tlWBS.FocusedNode) as WBSLineNode);
            }
        }

        /// <summary>
        /// 导入WBS清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarWBSImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 显示剩余数量和剩余金额，并将剩余金额为负数的项标红
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRelationDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            WBSline_boi node = gvRelationDetail.GetFocusedRow() as WBSline_boi;
            //if (node != null && (e.Column == colRemainQty || e.Column == colRemainAmount))
            //{
            //    ContractBoi remainBoi = viewModel.remainContractBoiList.Find(m => m.ItemNo == node.ItemNo);
            //    if (remainBoi != null)
            //    {
            //        if (e.Column == colRemainQty)
            //        {
            //            decimal remainQty = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctQty);
            //            node.remainQty = remainQty;
            //            if (remainQty < 0)
            //            {
            //                e.Appearance.BackColor = Color.Red;
            //                e.Appearance.ForeColor = Color.White;
            //            }
            //        }
            //        else if (e.Column == colRemainAmount)
            //        {
            //            decimal remainAmount = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctAmount);
            //            node.remainAmount = remainAmount;
            //            if (remainAmount < 0)
            //            {
            //                e.Appearance.BackColor = Color.Red;
            //                e.Appearance.ForeColor = Color.White;
            //            }
            //        }

            //    }
            //}
        }

        private void qtyEdit_Validating(object sender, CancelEventArgs e)
        {
            WBSline_boi node = gvRelationDetail.GetFocusedRow() as WBSline_boi;
            CalcEdit edit = sender as CalcEdit;
            decimal qty = edit.Value;
            if (node != null)
            {
                if (qty < 0)
                {
                    //edit.ErrorText = "数量不可为负数";
                    gvRelationDetail.SetColumnError(gvRelationDetail.FocusedColumn, "数量不可为负数");
                    e.Cancel = true;
                    return;
                }
                List<WBSline_boi> lstExceptFocusedRow = viewModel.boq.AllRelationList.FindAll(m => m.ItemNo == node.ItemNo && m.WBSLineNo != node.WBSLineNo);
                ContractBoi originBoi = viewModel.originContractBoiList.Find(m => m.ItemNo == node.ItemNo);
                if (lstExceptFocusedRow != null && lstExceptFocusedRow.Count > 0) //如果存在其他相同合同项，判断相同合同项
                {
                    decimal remainQty = 0, remainAmount = 0;
                    lstExceptFocusedRow.ForEach(m =>
                        {
                            remainQty = remainQty + ObjectHelper.GetDefaultDecimal(m.Qty);
                            remainAmount = remainAmount + ObjectHelper.GetDefaultDecimal(m.Amount);
                        });
                    remainQty = ObjectHelper.GetDefaultDecimal(originBoi.CtrctQty) - remainQty;
                    remainAmount = ObjectHelper.GetDefaultDecimal(originBoi.CtrctAmount) - remainAmount;
                    if (remainQty - ObjectHelper.GetDefaultDecimal(edit.EditValue) < 0)
                    {
                        gvRelationDetail.SetColumnError(gvRelationDetail.FocusedColumn, "该项的关联数量总值大于该项合同清单数量的总值，请核对后再输入");
                        //edit.ErrorText = "该项的关联数量总值大于该项合同清单数量的总值，请核对后再输入";
                        e.Cancel = true;
                    }
                }
                else
                {
                    decimal remainQty = 0, remainAmount = 0;
                    remainQty = ObjectHelper.GetDefaultDecimal(originBoi.CtrctQty);
                    remainAmount = ObjectHelper.GetDefaultDecimal(originBoi.CtrctAmount);
                    if (remainQty - ObjectHelper.GetDefaultDecimal(edit.EditValue) < 0)
                    {
                        gvRelationDetail.SetColumnError(gvRelationDetail.FocusedColumn, "该项的关联数量总值大于该项合同清单数量的总值，请核对后再输入");
                        //edit.ErrorText = "该项的关联数量总值大于该项合同清单数量的总值，请核对后再输入";
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// 导入关联项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarImportRelation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = Hondee.Common.Export.ExeclOperation.ToDataTable(openFileDialog1.FileName);
                try
                {
                    viewModel.ImportUpdateBoq(ds.Tables[0]);
                    gcRelationDetail.RefreshDataSource();
                    XtraMessageBox.Show("数据成功导入到程序中，保存后生效！");
                }
                catch (BusinessException be)
                {
                    XtraMessageBox.Show(be.BusMessage);
                }
            }
        }

        private void bbi_import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////导入WBS
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    List<WBSline> lines = ExcelHelper.Import(openFileDialog1.FileName);
            //    lines.ForEach(m =>
            //    {
            //        m.ProjectNo = viewModel.ProjectNo;
            //    });
            //    IWBSBoq wbsBoq = new MeteringPaymentClient().GetIWBSBoqService();
            //    wbsBoq.ImportWBS(viewModel.Boq.WbsNo, lines);
            //}


            //导入关联
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<WbsInfo> lines = ExcelHelper.ImportWbsInfo(openFileDialog1.FileName);
                IWBSBoq wbsBoq = new MeteringPaymentClient().GetIWBSBoqService();
                List<WbsInfo> leafLines = new List<WbsInfo>();
                lines.ForEach(m =>
                {
                    GetLeafNodes(leafLines, m);
                });
                ContractBoqViewModel cVModel = new ContractBoqViewModel(viewModel.ProjectNo, viewModel.ProjectName);
                cVModel.Load();
                int k = 0;
                leafLines.ForEach(m =>
                {
                    m.Details.ForEach(n =>
                    {
                        ContractBoi boi = cVModel.Boq.BoiList.Find(x => x.IItemCoe == n.BoiCode /*&& x.ItemName == n.BoiName*/);
                        if (boi != null)
                            viewModel.InsertNodeRelation(m.WbsCode, boi.ItemNo, n.Qty);
                        k++;
                    });
                });
            }


            ////更新图纸桩号
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    List<WbsInfo> lines = ExcelHelper.ImportWbsInfo(openFileDialog1.FileName);
            //    IWBSBoq wbsBoq = new MeteringPaymentClient().GetIWBSBoqService();
            //    List<WbsInfo> leafLines = new List<WbsInfo>();
            //    lines.ForEach(m =>
            //    {
            //        GetLeafNodes(leafLines, m);
            //    });
            //    List<WBSLineNode> nodeList = new List<WBSLineNode>(viewModel.NodeBindingSource);
            //    leafLines.ForEach(m =>
            //    {
            //        WBSLineNode line = nodeList.Find(n => n.WbsSysCode == m.WbsCode);
            //        line.DrawNo = m.DrawNo;
            //        line.StartStakesNo = m.StartNo;
            //        line.EndStakesNo = m.EndNo;
            //    });
            //}

        }
        private void GetLeafNodes(List<WbsInfo> list, WbsInfo wbsInfo)
        {
            if (wbsInfo.WbsInfos.Count > 0)
            {
                wbsInfo.WbsInfos.ForEach(m =>
                {
                    GetLeafNodes(list, m);
                });
            }
            else
            {
                list.Add(wbsInfo);
            }
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarFix_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(XtraMessageBox.Show("确认发布？", "确认操作", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DoWork("发布中，请稍候......", "发布",
                    () =>
                    {
                        viewModel.FixOrUnfix(false);
                    },
                    (ex) =>
                    {
                        tlWBS.DataSource = viewModel.NodeBindingSource;
                        tlWBS.ExpandAll();
                        gcRelationDetail.DataSource = viewModel.RelationBindingSource;
                        gvRelationDetail.RefreshData();
                        RefreshStat();
                    });
            }
        }

        /// <summary>
        /// 撤销发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarUnFix_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确认撤销发布？", "确认操作", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DoWork("撤销发布中，请稍候......", "撤销发布",
                    () =>
                    {
                        viewModel.FixOrUnfix(true);
                    },
                    (ex) =>
                    {
                        RefreshStat();
                    });
            }
        }


        private void bbiCollapse1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlWBS.CollapseAll();
        }

        private void bbiExpande_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlWBS.ExpandAll();
        }

        private void tlWBS_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (tlWBS.FocusedNode.Id < 0)
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Valid = false;
                e.ErrorText = "名称不可以为空";
                return;
            }
            if (e.Value.ToString().Contains("_"))
            {
                e.Valid = false;
                e.ErrorText = "名称中不可包含以下字符\n \t_";
                return;
            }
            if (tlWBS.FocusedNode.ParentNode != null)
            {
                foreach (TreeListNode node in tlWBS.FocusedNode.ParentNode.Nodes)
                {
                    if (node == tlWBS.FocusedNode)
                        continue;
                    WBSLineNode wbsLineNode = (WBSLineNode)tlWBS.GetDataRecordByNode(node);
                    if (wbsLineNode!=null&& wbsLineNode.WbsLineName == e.Value.ToString())
                    {
                        e.Valid = false;
                        e.ErrorText = "父节点存在同名WBS子项";
                        return;
                    }
                }
            }
            else
            {
                foreach (TreeListNode node in tlWBS.Nodes)
                {
                    if (node == tlWBS.FocusedNode)
                        continue;
                    if (node.GetValue(colWbsLineName) == null)
                        continue;
                    if (node.GetValue(colWbsLineName).ToString() == e.Value.ToString())
                    {
                        e.Valid = false;
                        e.ErrorText = "存在同名WBS子项";
                        return;
                    }
                }
            }
        }

    }
}
