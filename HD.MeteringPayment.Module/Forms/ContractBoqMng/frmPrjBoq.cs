using DevExpress.Data.Filtering.Helpers;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Erp.SharedLib.Presentation.CodeExportExcel;
using Erp.SharedLib.Presentation.ControlBases;
using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using HD.MeteringPayment.Module.BootLoader;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.Forms.PrjBoqInfo;
using HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel;
using Hondee.Common.Export;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng
{
    public partial class frmPrjBoq : GpControlBase
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
        private ContractBoqViewModel viewModel;
        private IContractBoq projectBoq;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ProjectNo">项目编号</param>
        public frmPrjBoq(String ProjectNo,String ProjectName)
        {
            InitializeComponent();
            this.ProjectNo = ProjectNo;
            this.ProjectName = ProjectName;
            projectBoq = new MeteringPaymentClient().GetIContractBoqService();
            tlDetail.ActiveFilterString = "StatId=1";
            tlDetail.FilterNode += OnFilterNode;
        }
        /// <summary>
        /// 窗体装载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrjBoq_Load(object sender, EventArgs e)
        {
            DoWork("加载清单中...", "加载清单中...", () =>
            {
                viewModel = new ContractBoqViewModel(ProjectNo, ProjectName);
                viewModel.ListChanged += RefreshBottomBar;
                viewModel.Load();
            }, (myException) =>
            {
                if (myException == null)
                {
                    tlDetail.DataSource = viewModel.NodeList;
                    RefreshStat();
                }
            });
            //  this.ParentForm.Shown += ParentForm_Shown;
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
        /// 计算累计量并刷新底部状态栏
        /// </summary>
        private void RefreshBottomBar()
        {
            //计算合计---------------------------------------------------------------
            decimal totalAmount = 0;//金额合计
            decimal totalLatestAmount = 0; //变更后金额合计
            decimal totalChangeAmount = 0; //变更金额合计
            foreach (ContractBoiNode line in viewModel.NodeList)
            {
                if (line.ParentBoiNode == null)
                {
                    //只需计算根节点金额
                    totalAmount += line.CtrctAmount;
                    totalLatestAmount += line.LatestAmount;
                    totalChangeAmount += line.ChangeAmount ?? 0;
                }
            }

            BarAmount.Caption = string.Format("合计金额:{0}", totalAmount.ToString("g0"));
            BarLatestAmount.Caption = string.Format("变更后合计金额:{0}", totalLatestAmount.ToString("g0"));
            BarChangeAmount.Caption = string.Format("金额增减合计:{0}", totalChangeAmount.ToString("g0"));

        }
        /// <summary>
        /// 更新窗体各控件的可编辑和显示性
        /// </summary>
        public void RefreshStat()
        {
            bool isPrjMng = true /*AppConfig.CurrentLoginUser.LoginRole == AppConfig.ManagerRole.XMBMngr ? true : false*/;
            //刷新更菜单的可见性
            bbiChange.Visibility = !viewModel.Editing && viewModel.BoqChange ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiChange.Enabled = !viewModel.Editing && viewModel.BoqChange;
            bbiEdit.Visibility = isPrjMng && !viewModel.Editing && viewModel.BoqEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiEdit.Enabled = isPrjMng && !viewModel.Editing && viewModel.BoqEdit;
            bbiSave.Visibility = isPrjMng && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiSave.Enabled = isPrjMng && viewModel.Editing;
            bbiCancel.Visibility = isPrjMng && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCancel.Enabled = isPrjMng && viewModel.Editing;
            bbiNew.Visibility = isPrjMng && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNew.Enabled = isPrjMng && viewModel.Editing;
            bbiNewChild.Visibility = isPrjMng && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiNewChild.Enabled = isPrjMng && viewModel.Editing;
            bbiDelete.Visibility = isPrjMng && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDelete.Enabled = isPrjMng && viewModel.Editing;
            //导入
            bbiImport.Visibility = isPrjMng && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImport.Enabled = isPrjMng && viewModel.Editing;
            //按子目号导入
            bbiImportByItemCode.Visibility = isPrjMng && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiImportByItemCode.Enabled = isPrjMng && viewModel.Editing;
            //导出
            bbiExport.Visibility = !viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiExport.Enabled = !viewModel.Editing;

            bbiLock.Visibility = isPrjMng && !viewModel.Editing && viewModel.BoqEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiLock.Enabled = isPrjMng && !string.IsNullOrEmpty(viewModel.boq.BoQNo) && !viewModel.Editing && viewModel.BoqEdit;
            bbiRefresh.Visibility = !viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiRefresh.Enabled = !viewModel.Editing;
            tlDetail.OptionsBehavior.Editable = isPrjMng && viewModel.Editing;
            bbiChangeStat.Visibility = isPrjMng && viewModel.Editing ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            bbiChangeStat.Enabled = isPrjMng && viewModel.Editing;
            bbiShowStat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tlDetail.OptionsView.ShowBands = viewModel.BoqChange;
            treeListColumn13.Visible = false;
            treeListColumn14.Visible = false;
            treeListColumn15.Visible = false;
            treeListColumn16.Visible = false;
            treeListColumn17.Visible = false;
            treeListColumn18.Visible = false;
            treeListColumn19.Visible = false;
            treeListColumn21.Visible = false;
            treeListBand3.Visible = false;
            BarLatestAmount.Visibility = false ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;

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
            tlDetail.CollapseAll();
        }
        /// <summary>
        /// 展示显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExpand_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //tlDetail.ExpandToLevel(1);
            tlDetail.ExpandAll();

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
                viewModel = new ContractBoqViewModel(ProjectNo,ProjectName);
                viewModel.ListChanged += RefreshBottomBar;
                viewModel.Load();
            }, (myException) =>
            {
                if (myException == null)
                {
                    tlDetail.DataSource = viewModel.NodeList;
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
            tlDetail.PostEditor();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存合同清单文件";
            saveFileDialog.FileName = ProjectName + " - 合同清单";
            saveFileDialog.Filter = "Excel文件|*.xls";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tlDetail.ExportToXls(saveFileDialog.FileName);
                HD.MeteringPayment.Module.Forms.ProgressMeteringMng.ExcelHelper.OpenFile(saveFileDialog.FileName);
            }

            //GridExportHelper.ExportExcel(tlDetail, ProjectName + "-合同清单");
        }
        /// <summary>
        /// 新增项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlDetail.PostEditor();
            ContractBoiNode nodeNew;
            ContractBoiNode nodeSelected = tlDetail.GetDataRecordByNode(tlDetail.FocusedNode) as ContractBoiNode;
            nodeNew = viewModel.InsertNode(nodeSelected != null ? nodeSelected.ParentBoiNode : null);
            TreeListNode tlnNode = tlDetail.FindNodeByKeyID(nodeNew.ItemCode);
            tlDetail.MakeNodeVisible(tlnNode);
            tlDetail.SetFocusedNode(tlnNode);
        }
        /// <summary>
        /// 新增子项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiNewChild_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlDetail.PostEditor();

            ContractBoiNode nodeNew;
            ContractBoiNode nodeSelected = tlDetail.GetDataRecordByNode(tlDetail.FocusedNode) as ContractBoiNode;
            if (nodeSelected == null)
            {
                XtraMessageBox.Show("请选中项!");
                return;
            }
            nodeNew = viewModel.InsertNode(nodeSelected);
            TreeListNode tlnNode = tlDetail.FindNodeByKeyID(nodeNew.ItemCode);
            tlDetail.MakeNodeVisible(tlnNode);
            tlDetail.SetFocusedNode(tlnNode);
        }
        /// <summary>
        /// 删除或禁用项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlDetail.PostEditor();
            List<ContractBoiNode> lstDeleteNode;
            List<TreeListNode> lstNode = tlDetail.GetAllCheckedNodes();
            List<ContractBoiNode> lstBoiNode = lstNode.ConvertAll<ContractBoiNode>(m => tlDetail.GetDataRecordByNode(m) as ContractBoiNode);
            if (lstBoiNode.Count == 0)
            {
                XtraMessageBox.Show("请选中项!");
                return;
            }
            //标识是否已同意删除
            bool blReadyGo = false;
            if (lstBoiNode.Exists(m => m.Children.Count>0))
            {
                if (XtraMessageBox.Show("删除或禁用父项，将会删除或禁用其全部子项！是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
                blReadyGo = true;
            }

            else if(!blReadyGo)
            {
                if (XtraMessageBox.Show("是否删除项？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }
            if (!blReadyGo&&lstBoiNode.Exists(m => m.IsUse))
            {
                //if (XtraMessageBox.Show("存在已使用项，将会禁用使用项！是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                //    return;
                XtraMessageBox.Show("存在已关联项，已关联项将不会被删除！", "警告", MessageBoxButtons.OK);
            }
            lstDeleteNode = lstBoiNode.FindAll(m => !lstBoiNode.Exists(n => n.Children.Contains(m)));
            lstDeleteNode.ForEach(m =>
            {
                if (!m.IsUse)
                {
                    viewModel.DeleteNode(m);
                }
                else
                {
                    //viewModel.ChangeNodeStat(m, false);
                }
            });
        }
        /// <summary>
        /// 启用项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiChangeStat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ContractBoiNode> lstDeleteNode;
            List<TreeListNode> lstNode = tlDetail.GetAllCheckedNodes();
            List<ContractBoiNode> lstBoiNode = lstNode.ConvertAll<ContractBoiNode>(m => tlDetail.GetDataRecordByNode(m) as ContractBoiNode);
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
                    viewModel.DeleteNode(m);
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
            ContractBoiNode boiNode = tlDetail.GetDataRecordByNode(e.Node) as ContractBoiNode;
            if (boiNode != null && !String.IsNullOrEmpty(boiNode.ItemNo) && boiNode.StatId == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                e.Appearance.ForeColor = Color.Gray;
            }
            if (boiNode != null && boiNode.IsChanged && boiNode.Children.Count == 0)
            { 
                e.Appearance.BackColor = Color.Yellow;  
            }
            //if (viewModel.Editing)
            //{
            //   // if(boiNode.StatId==1)
            //}
        }

        private void bbiImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataSet ds= ExeclOperation.ToDataTable(openFileDialog1.FileName);
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
        /// 按子目号导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiImportByItemCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = ExeclOperation.ToDataTable(openFileDialog1.FileName);
                try
                {
                    viewModel.ImportBoqByCode(ds.Tables[0]);
                    XtraMessageBox.Show("数据成功导入到程序中，保存后生效！");
                }
                catch (BusinessException be)
                {
                    XtraMessageBox.Show(be.BusMessage);
                }
            }

        }

        /// <summary>
        /// 锁定清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确认锁定清单？\n锁定后，只能通过清单变更来调整合同清单。", "确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                viewModel.Lock();
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
            tlDetail.PostEditor();
            DoWork("保存中...", "提示", () =>
            {
                viewModel.Save(); 
            }, (myException) =>
            {
                if (myException == null)
                { 
                    RefreshStat();
                    XtraMessageBox.Show("保存成功！");
                }
            });
        }

        private void bbiShowStat_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bbiShowStat.Checked)
            {
                tlDetail.ActiveFilterString = "";
            }
            else
            {
                tlDetail.ActiveFilterString = "StatId=1";
            }
        }

        private void bbiChange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrjBoqChange form = new frmPrjBoqChange(ProjectNo, ProjectName);
            AppForm.CurrentForm.ChangeForm("清单变更", form);
        }

        private void tlBoi_MouseClick(object sender, MouseEventArgs e)
        {
            TreeListHitInfo info = tlDetail.CalcHitInfo(e.Location);

            if (info.Node != null && info.Column != null && info.Column.FieldName == "IsChanged" )
            {
                ContractBoiNode boiNode = tlDetail.GetDataRecordByNode(info.Node) as ContractBoiNode;
                if (boiNode != null && boiNode.IsChanged)
                {
                    frmPrjBoqChangeLog form = new frmPrjBoqChangeLog(boiNode.ItemNo);
                    form.ShowDialog();

                }
            }
        }

        private void bbiExportTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存合同清单导入模板文件";
            saveFileDialog.FileName = "合同清单导入模板.xls";
            saveFileDialog.Filter = "Excel文件|*.xls";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
                Stream sm = asm.GetManifestResourceStream("HD.MeteringPayment.Module.TemplateXls.xls");
                using (FileStream sr = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    int b = 0;
                    while ((b = sm.ReadByte()) != -1)
                    {
                        sr.WriteByte((byte)b);
                    }
                }
                OpenFile(saveFileDialog.FileName);
            }
        }
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

        private void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("是否取消编辑？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                viewModel.Load();
                viewModel.Editing = false;
                RefreshStat();
            }
        }

        private void tlBoi_ShowingEditor(object sender, CancelEventArgs e)
        {
            ContractBoiNode boiNode = tlDetail.GetDataRecordByNode(tlDetail.FocusedNode) as ContractBoiNode;
            if (boiNode != null && boiNode.Children.Count > 0 &&(tlDetail.FocusedColumn.FieldName== "CtrctQty"|| tlDetail.FocusedColumn.FieldName == "CtrctPrjPrice"))
                e.Cancel = true;
        }

        #region 行过滤查找  ，避免treelist只筛选第一层的BUG
        private void OnFilterNode(object sender, FilterNodeEventArgs e)
        {
            if (!Object.ReferenceEquals(tlDetail.ActiveFilterCriteria, null))
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
                ExpressionEvaluator ee = new ExpressionEvaluator(TypeDescriptor.GetProperties(typeof(ContractBoiNode)), node.TreeList.ActiveFilterCriteria, false);
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

        private void tlDetail_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (e.CellValue == null)
                return;
            decimal value = -1;
            if (decimal.TryParse(e.CellValue.ToString(), out value) && value == 0)
            {
                e.CellText = string.Empty;
            }
        }
    }
}
