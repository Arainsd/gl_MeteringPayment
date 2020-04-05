using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using Erp.SharedLib.Presentation.ControlBases;
using HD.MeteringPayment.Domain.Client;
using DevExpress.XtraNavBar;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.BootLoader;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;

namespace HD.MeteringPayment.Module.Forms.MeteringRptMng
{
   // public partial class MeteringRptDetailControl : GpControlBase
    public partial class MeteringRptDetailControl : GpControlBase
    {
        #region 变量声明
        private PrjAmountRpt _datasource = new PrjAmountRpt();
        public PrjAmountRpt DataSource
        {
            get
            {
                return _datasource;
            }
            set
            {
                _datasource = value;
                if (value != null)
                {
                    bdPrjAmountRpt.DataSource = value;
                    bdPrjAmountRpt.ResetBindings(true);

                    gcPrjAmountPayRpt.DataSource = value.lstPayRpt;
                    //value.ThisPeriodPay = NumberToChinese.CmycurD(value.ThisPeriodShouldPay);
                    value.ThisPeriodPay = ConvertToChinese(Math.Round(value.ThisPeriodShouldPay, 0, MidpointRounding.AwayFromZero));
                    textEdit1.Text = ConvertToChinese(Math.Round(value.ThisPeriodShouldPay, 0, MidpointRounding.AwayFromZero));

                    gvPrjAmountPayRpt.RefreshData();

                    gcPrjAmountBoiRpt.DataSource = value.lstBoiPayRpt;
                    gvPrjAmountBoiRpt.RefreshData();

                    gcPrjAmountWbsBoiRpt.DataSource = value.lstWbsBoiRpt;
                    gvPrjAmountWbsBoiRpt.RefreshData();

                    gcPrjAmountWbsRpt.DataSource = value.lstWbsRpt;
                    gvPrjAmountWbsRpt.RefreshData();


                    barCodeControl1.Text = String.Format("{0}{1}", AppConfig.PrjAmountQRCodeAddress, value.PrjamountNo);
                    lbPeriods.Text = string.Format("（报表期数：第 {0} 期）", value.Periods);
                    //barCodeControl1.Text = String.Format("http://172.16.0.63:8081/wfPlatform/meteringPayment/noFilter/{0}", DataSource.PrjamountNo);
                }
            }
        }
        private IPrjAmountRpt client;
        private PrjAmountWbsRptDetailForm detailControl;
        #endregion

        public static String ConvertToChinese(Decimal number)
        {
            var s = number.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var r = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            if (number < 0)  r = "负" + r;
            if (number == 0) r = "零元";
            if ((int)number == number)
            {
                r += "整";
            }
            return r;
        }

        public MeteringRptDetailControl()
        {
            InitializeComponent(); 
            client = new MeteringPaymentClient().GetIPrjAmountRptService();
            Init();
        }

        private void Init()
        {
            detailControl = new PrjAmountWbsRptDetailForm();
            detailControl.Dock = DockStyle.Fill;
            detailControl.Visible = false;
            plPrjAmountWbsRptDetailContainer.Controls.Add(detailControl);
            gvPrjAmountBoiRpt.ActiveFilterString = "(EndingAmount IS NOT NULL AND EndingAmount <> 0 AND NOT Contains([ItemName], '合计')) OR (Contains([ItemName], '合计') AND Sequence = 9999)";
        }

        /// <summary>
        /// 报表首页控件调整位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabPage1_Resize(object sender, EventArgs e)
        {
            int width = xtraTabPage1.Width;
            
            lbCtrcPrjName.Width = width;
            lbTitle.Width = width;

            if ((width / 2) - (plCenter.Width / 2) >= 0)
            {
                plCenter.Left = (width / 2) - (plCenter.Width / 2);
            }
            else
            {
                plCenter.Left = 0;
            }
        }

        private void BarRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DoWorkRun("读取数据中,请稍候......", "读取数据" ,
                ()=>
                {
                    PrjAmountRpt result = client.Get(DataSource.PrjamountNo);

                    return result;
                },
                (result, ex)=>
                {
                    if(ex == null && result != null)
                    {
                        DataSource = result as PrjAmountRpt;

                    }
                });
        }

        /// <summary>
        /// 双击打开中间计量表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPrjAmountWbsRpt_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks >= 2)
            {
                PrjAmountWbsRpt datasource = gvPrjAmountWbsRpt.GetRow(e.RowHandle) as PrjAmountWbsRpt;
                if (datasource != null && !String.IsNullOrEmpty(datasource.WbsLineNo))
                {
                    DoWorkRun("读取数据中，请稍候......", "读取数据",
                        () =>
                        {
                            PrjAmountWbsRpt result = client.GetWbsRpt(datasource.WbsLineNo, datasource.PrjamountNo);
                            return result;
                        },
                        (result, ex) =>
                        {
                            if (ex == null)
                            {
                                PrjAmountWbsRptDetailForm form = new PrjAmountWbsRptDetailForm();
                                form.DataSource = result as PrjAmountWbsRpt;
                                AppForm.CurrentForm.ChangeForm(String.Format("{0}-中间计量表", (result as PrjAmountWbsRpt).WbsLineName), form);
                            }
                        });
                }
            }
        }

        /// <summary>
        /// 合并行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPrjAmountPayRpt_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            PrjAmountPayRpt row1 = gvPrjAmountPayRpt.GetRow(e.RowHandle1) as PrjAmountPayRpt;
            PrjAmountPayRpt row2 = gvPrjAmountPayRpt.GetRow(e.RowHandle2) as PrjAmountPayRpt; 
            if (row1 != null && row2 != null && (row1.Type != row2.Type && row1.Type != 1|| DataSource.IsNewData && row1.Type == 3 ))
            {
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void navBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            int index = Convert.ToInt32((sender as NavBarItem).Tag);

            xtraTabControl1.SelectedTabPageIndex = index;
        }

        /// <summary>
        /// 导出/打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DataSource == null)
            {
                XtraMessageBox.Show("无数据");
                return;
            } 
            PrintingControlControl form = new PrintingControlControl(Convert.ToInt32(e.Item.Tag));
            form.InitPrintControl(DataSource);
            form.ShowDialog();
        }

        private void gvPrjAmountWbsRpt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                PrjAmountWbsRpt selectedRow = gvPrjAmountWbsRpt.GetRow(e.FocusedRowHandle) as PrjAmountWbsRpt;
                if (selectedRow != null)
                {
                    if(selectedRow.detail != null && selectedRow.detail.Count > 0)  //如果已经加载过，则不需要再次加载
                    {
                        detailControl.Visible = true;
                        detailControl.DataSource = selectedRow;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(selectedRow.WbsLineNo))  //如果未加载过，则需要重新加载明细数据，并缓存到
                        {
                            DoWorkRun("读取数据中，请稍候......", "读取数据",
                                () =>
                                {
                                    PrjAmountWbsRpt result = client.GetWbsRpt(selectedRow.WbsLineNo, selectedRow.PrjamountNo);
                                    return result;
                                },
                                (result, ex) =>
                                {
                                    if (ex == null)
                                    {
                                        PrjAmountWbsRpt current = result as PrjAmountWbsRpt;
                                        PrjAmountWbsRpt origin = DataSource.lstWbsRpt.FirstOrDefault(m => m.WbsLineNo == selectedRow.WbsLineNo && m.PrjamountNo == selectedRow.PrjamountNo);
                                        origin.Copy(current);
                                        detailControl.Visible = true;
                                        detailControl.DataSource = origin;
                                    }
                                });
                        }
                    }

                }
            }
        }

        private void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        { 
            decimal value = 0;
            string cellValue = e.CellValue != null ? e.CellValue.ToString() : "";
            if (decimal.TryParse(cellValue, out value) && value == 0 
                || e.CellValue!=null&&e.CellValue.ToString() == "奖励\\罚款" && e.Column.FieldName== "IItemCoe")
            {
                e.DisplayText = "";
            }
        }

        /// <summary>
        /// 显示所有报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarShowAllReport_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string strActiveFilter = "";
            if (BarShowAllReport.Checked)  //显示所有报表
            {
                strActiveFilter = "Sequence != 9999";
            }
            else  //显示筛选后报表
            {
                //strActiveFilter = "(EndingAmount IS NOT NULL AND EndingAmount <> 0 AND NOT Contains([ItemName], '合计')) OR (Contains([ItemName], '合计') AND Sequence = 9999)";
                strActiveFilter = "(EndingAmount IS NOT NULL AND EndingAmount <> 0 AND NOT Contains([ItemName], '合计') AND IItemCoe<>'100' AND IItemCoe<>'200' AND IItemCoe<>'300' AND IItemCoe<>'400' AND IItemCoe<>'500' AND IItemCoe<>'600' AND IItemCoe<>'700') OR ((Contains([ItemName], '合计') OR IItemCoe in ('100','200','300','400','500','600','700')) AND Sequence = 9999)";
            }
            gvPrjAmountBoiRpt.ActiveFilterString = strActiveFilter;
        }

        private void gvPrjAmountBoiRpt_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            PrjAmountBoiRpt row = gvPrjAmountBoiRpt.GetRow(e.RowHandle) as PrjAmountBoiRpt;
            if (row == null) return;
            if (row.Sequence == 9998 || row.Sequence == 9999)
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
                BarShowAllReport_CheckedChanged(null, null);
        }

        private void gvPrjAmountWbsBoiRpt_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;
            string firstColumnFieldName = "ItemNo";
            if (e.Column.FieldName == "IItemCoe" || e.Column.FieldName == "ItemName"
                || e.Column.FieldName == "Uom" || e.Column.FieldName == "Price" )
            {
                string valueFirstColumn1 = Convert.ToString(view.GetRowCellValue(e.RowHandle1, view.Columns[firstColumnFieldName]));
                string valueFirstColumn2 = Convert.ToString(view.GetRowCellValue(e.RowHandle2, view.Columns[firstColumnFieldName]));
                e.Merge = valueFirstColumn1 == valueFirstColumn2;
                e.Handled = true;
            }
        }

        private void gvPrjAmountWbsBoiRpt_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
             
        }
    }
}
