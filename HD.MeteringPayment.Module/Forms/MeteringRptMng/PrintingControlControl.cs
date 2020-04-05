using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting;
using HD.MeteringPayment.Module.Forms.MeteringRptMng.PrintListPayForms;
using HD.MeteringPayment.Module.Forms.MeteringRptMng.PrintTotalForms;

namespace HD.MeteringPayment.Module.Forms.MeteringRptMng
{
    public partial class PrintingControlControl : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 打印类型：0-全部打印  1-首页 2-中间支付证书 3-清单支付报表 4-中间计量支付汇总表
        /// </summary>
        private int PrintType;
        public PrintingControlControl(int _tag)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.PrintType = _tag;
        }

        public void InitPrintControl(PrjAmountRpt DataSource)
        {
            PrintTitleForm reportTitlePrint = new PrintTitleForm();
            PrintPrjAmountPayRptForm reportPrjAmountPayRptPrint = new PrintPrjAmountPayRptForm();
            PrintPrjAmountPayRptFormOld reportPrjAmountPayRptPrintOld = new PrintPrjAmountPayRptFormOld();
            PrintListPayForm listPayFormPrint = new PrintListPayForm();
            PrintTotalForm totalFormPrint = new PrintTotalForm();

            reportTitlePrint.Datasource = DataSource;
            PrintControl printControl1 = new PrintControl();

            PrintBarManager printBarManager = new PrintBarManager();
            printBarManager.Form = printControl1;
            printBarManager.Initialize(printControl1);
            printBarManager.MainMenu.Visible = false;
            printBarManager.AllowCustomization = false;


            MeteringPayRptRoot root = new MeteringPayRptRoot();
            root.Header = DataSource;
            root.AllList = DataSource.lstPayRpt;
            root.BoiDetails = DataSource.lstBoiPayRpt.FindAll(m => m.EndingQty.HasValue && m.EndingQty != 0 && !m.ItemName.Contains("合计") || m.ItemName.Contains("合计") && m.Sequence == 9999);  //清单支付——details


            root.WbsBoiRptDetails = DataSource.lstWbsBoiRpt;    //中间计量支付汇总表——details

            //中间支付
            if (DataSource.IsNewData) reportPrjAmountPayRptPrint.Root = root;
            else reportPrjAmountPayRptPrintOld.Root = root;
            //清单支付
            listPayFormPrint.Root = root;

            //中间计量支付汇总表
            totalFormPrint.Root = root; 
            this.PrintContainer.Controls.Add(printControl1);
            switch (PrintType)
            {
                case 0:
                case 1: printControl1.PrintingSystem = reportTitlePrint.PrintingSystem; break;
                case 2: printControl1.PrintingSystem = DataSource.IsNewData?reportPrjAmountPayRptPrint.PrintingSystem: reportPrjAmountPayRptPrintOld.PrintingSystem; break;
                case 3: printControl1.PrintingSystem = listPayFormPrint.PrintingSystem; break;
                case 4: printControl1.PrintingSystem = totalFormPrint.PrintingSystem; break;
            } 
            //操作要显示什么按钮
            printControl1.PrintingSystem.SetCommandVisibility(new PrintingSystemCommand[]{
                PrintingSystemCommand.ClosePreview,
                PrintingSystemCommand.Customize,
                PrintingSystemCommand.SendCsv,
                PrintingSystemCommand.SendFile,
                PrintingSystemCommand.SendGraphic,
                PrintingSystemCommand.SendMht,
                PrintingSystemCommand.SendPdf,
                PrintingSystemCommand.SendRtf,
                PrintingSystemCommand.SendTxt,
                PrintingSystemCommand.SendXlsx,
                PrintingSystemCommand.SendXls,
                //PrintingSystemCommand.ExportCsv,
                //PrintingSystemCommand.ExportFile,
                //PrintingSystemCommand.ExportGraphic,
                //PrintingSystemCommand.ExportHtm,
                //PrintingSystemCommand.ExportMht,
                //PrintingSystemCommand.ExportPdf,
                //PrintingSystemCommand.ExportRtf,
                //PrintingSystemCommand.ExportTxt, 
                //PrintingSystemCommand.ExportXlsx,
                PrintingSystemCommand.ExportXls, 
                //PrintingSystemCommand.ExportXps
            }, CommandVisibility.None);

            reportTitlePrint.CreateDocument();
            if (DataSource.IsNewData) reportPrjAmountPayRptPrint.CreateDocument();
            else reportPrjAmountPayRptPrintOld.CreateDocument();
            listPayFormPrint.CreateDocument();
            totalFormPrint.CreateDocument();
            if (PrintType == 0)
            {
                reportTitlePrint.Pages.AddRange(DataSource.IsNewData? reportPrjAmountPayRptPrint.Pages: reportPrjAmountPayRptPrintOld.Pages);
                reportTitlePrint.Pages.AddRange(listPayFormPrint.Pages); 
                reportTitlePrint.Pages.AddRange(totalFormPrint.Pages);
            }

            printControl1.Dock = DockStyle.Fill;
        }

        class Handler : ICommandHandler
        {
            public bool CanHandleCommand(PrintingSystemCommand command, IPrintControl printControl)
            {
                bool handle = false;
                if (command == PrintingSystemCommand.ExportXls)
                {
                    handle = true;
                }
                return handle;
            }

            public void HandleCommand(PrintingSystemCommand command, object[] args, IPrintControl printControl, ref bool handled)
            {
                if (command == PrintingSystemCommand.ExportXls)
                {
                    handled = true;
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Title = "导出Excel";
                    dlg.DefaultExt = ""; 
                    dlg.Filter = "Microsoft Excel 97-2003 工作表|*.xls";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = dlg.FileName;
                        printControl.PrintingSystem.ExportOptions.Xls.ExportMode = XlsExportMode.DifferentFiles;
                        printControl.PrintingSystem.ExportOptions.Xls.PageRange = "1-" + printControl.PrintingSystem.Pages.Count;
                        printControl.PrintingSystem.ExportToXls(fileName);
                    }

                }
            }
        }
    }
}