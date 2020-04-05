using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using HD.MeteringPayment.Module.Forms.WBSBoqMng.ViewModel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Erp.SharedLib.Presentation.Lib;
using DevExpress.LookAndFeel;

namespace HD.MeteringPayment.Module.Forms.WBSBoqMng
{
    public partial class frmPrjBoqExport : DevExpress.XtraEditors.XtraForm
    {
        List<WBSLineNode> AllList;
        List<WBSline_boi> BoiList;
        String FileName;
        public frmPrjBoqExport(String FileName, List<WBSline_boi> BoiList, List<WBSLineNode> AllList)
        {
            InitializeComponent();
            this.AllList = AllList;
            this.BoiList = BoiList;
            gvExport.OptionsPrint.AutoWidth = true;
            gvExport.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvExport.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gvExport.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvExport.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gvExport.OptionsView.AllowCellMerge = false;
            gcExport.DataSource = GetExportTable();
            gvExport.PopulateColumns();
            gvExport.CellMerge += GvExport_CellMerge;
            this.FileName = FileName;
        }

        private void GvExport_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            String result1 = GetPreValue(gvExport, e.Column, e.RowHandle1);
            String result2 = GetPreValue(gvExport, e.Column, e.RowHandle2);
            if (String.Equals(result1, result2))
            {
                e.Merge = true;
            }
            else
            {
                e.Merge = false;
            }
        }
        private String GetPreValue(GridView view, GridColumn cur, int rowHandle)
        {
            String result = null;
            DataRow row = view.GetDataRow(rowHandle);
            if (row == null) return result;
            int index = cur.AbsoluteIndex;
            while (index >= 1)
            {
                GridColumn gridColumn = view.Columns[index - 1];
                if (!String.IsNullOrEmpty(String.Format("{0}", row[gridColumn.FieldName])))
                {
                    result += "!" + row[gridColumn.FieldName + "Code"].ToString();
                }
                index--;
            }
            return result;
        }
        List<String> columns = new List<string>() {
          "单位工程" ,"单位工程单元","分部","分项"
        };
        List<String> newColumns = new List<string>();
        public DataTable GetExportTable()
        {
            DataTable exportTable = new DataTable();
            exportTable.Columns.Add("单位工程");
            exportTable.Columns.Add("单位工程单元");
            exportTable.Columns.Add("分部");
            exportTable.Columns.Add("分项");
            DataColumn dcPart = exportTable.Columns.Add("部位");
            exportTable.Columns.Add("清单编号");
            exportTable.Columns.Add("清单项目");
            exportTable.Columns.Add("单位");
            exportTable.Columns.Add("数量");
            exportTable.Columns.Add("系统编号1");
            exportTable.Columns.Add("系统编号2");
            exportTable.Columns.Add("单位工程Code");
            exportTable.Columns.Add("单位工程单元Code");
            exportTable.Columns.Add("分项Code");
            exportTable.Columns.Add("分部Code");
            exportTable.Columns.Add("部位Code");
            for (int i = 0; i < BoiList.Count; i++)
            {
                DataRow dr = exportTable.NewRow();
                exportTable.Rows.Add(dr);
                WBSLineNode node = AllList.Find(m => m.WbsLineNo == BoiList[i].WBSLineNo);
                if (node == null) continue;
                dr["部位"] = node.WbsLineName;
                dr["部位Code"] = node.WbsSysCode;
                List<WBSLineNode> result = GetPath(node);
                for (int j = 0; j < result.Count; j++)
                {
                    String columnName = null;
                    if (j < columns.Count)
                    {
                        columnName = columns[j];
                    }
                    else
                    {
                        int nameIndex = 1;
                        if (newColumns.Count > 0)
                        {
                            nameIndex = newColumns.Count + 1;
                            newColumns.Add("分部分项/部位" + nameIndex);
                        }
                        columnName = "分部分项/部位" + nameIndex;
                        columns.Add(columnName);
                        DataColumn colTemp = exportTable.Columns.Add(columnName);
                        exportTable.Columns.Add(columnName + "Code");
                        colTemp.SetOrdinal(dcPart.Ordinal);
                    }
                    dr[columnName] = result[j].WbsLineName;
                    dr[columnName + "Code"] = result[j].WbsSysCode;
                }
                dr["清单编号"] = BoiList[i].IItemCoe;
                dr["清单项目"] = BoiList[i].ItemName;
                dr["数量"] = BoiList[i].Qty.HasValue ? BoiList[i].Qty.Value.ToString("0.00") : "";
                dr["单位"] = BoiList[i].Uom;
                dr["系统编号1"] = BoiList[i].WBSLineNo;
                dr["系统编号2"] = BoiList[i].ItemNo;
            }
            return exportTable;
        }
        private List<WBSLineNode> GetPath(WBSLineNode node)
        {
            List<WBSLineNode> resultNodes = new List<WBSLineNode>();
            while (node.ParentBoiNode != null)
            {
                resultNodes.Insert(0, node.ParentBoiNode);
                node = node.ParentBoiNode;
            }
            return resultNodes;
        }

        private void frmPrjBoqExport_Shown(object sender, EventArgs e)
        {
        }

        private void btExport_Click(object sender, EventArgs e)
        {
        }
        public void Export()
        {
            for (int i = 0; i < gvExport.Columns.Count; i++)
            {
                GridColumn column = gvExport.Columns[i];
                column.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                if (column.Name.Contains("Code"))
                {
                    column.Visible = false;
                }
            }
            int postion = gvExport.Columns["部位"].AbsoluteIndex;
            for (int i = 0; i <= postion; i++)
            {
                gvExport.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            }
            gvExport.OptionsView.AllowCellMerge = true;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存WBS关联项导入模板文件";
            saveFileDialog.FileName = FileName;
            saveFileDialog.Filter = "Excel文件|*.xls";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gvExport.ExportToXls(saveFileDialog.FileName);
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
    }
}