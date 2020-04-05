using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using Erp.CommonData;
using Erp.GpServiceClient;
using Erp.SharedLib.Presentation.CodeExportExcel;
using GP.DistributedServices.Seedwork.ErrorHandlers;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HD.MeteringPayment.Module.Forms.WBSBoqMng
{

    public class ExcelHelper
    {
        private static List<LevelObject> AllObjects = new List<LevelObject>();
        private static List<WbsInfo> wbsRootInfos = new List<WbsInfo>();
        public static List<WBSline> Import(string path)
        {               
            
            Dictionary<String, List<ICell>> rpList = new Dictionary<string, List<ICell>>();
            HSSFWorkbook wk = null;
            FileStream fileStream = new FileStream(path, FileMode.Open);

                wk = new HSSFWorkbook(fileStream);   //把xls文件中的数据写入wk中  
          
            ISheet totalSheet = wk.GetSheetAt(0);
            List<WBSline> lines= ImportTotalSheetContent(totalSheet);
            wk.Close();
            fileStream.Close();
            return lines;
        }
        public static List<WbsInfo> ImportWbsInfo(string path)
        {

            Dictionary<String, List<ICell>> rpList = new Dictionary<string, List<ICell>>();
            HSSFWorkbook wk = null;
            FileStream fileStream = new FileStream(path, FileMode.Open);

            wk = new HSSFWorkbook(fileStream);   //把xls文件中的数据写入wk中  

            ISheet totalSheet = wk.GetSheetAt(0);
            List<WbsInfo> lines = GetWbsInfos(totalSheet);
            wk.Close();
            fileStream.Close();
            return lines;
        }
        private static LevelObject Match(ISheet sheet,WbsInfo parentInfo, int rowIndex, int colIndex)
        {
            LevelObject result= AllObjects.FirstOrDefault(m=> m.StrartRowIndex<=rowIndex&&m.EndRowIndex>=rowIndex&&m.ColIndex==colIndex);
            if (result == null)
            {
                ICell cell = sheet.GetRow(rowIndex).GetCell(colIndex);
                if (cell == null || String.IsNullOrEmpty(cell.StringCellValue)) return null;

                WbsInfo info = new WbsInfo();
                info.WbsName = cell.StringCellValue;
                if (parentInfo != null)
                {
                    parentInfo.WbsInfos.Add(info);
                }
                else
                {
                    wbsRootInfos.Add(info);
                }
                int rangeIndex = 0;
                CellRangeAddress range = GetMergedRegionIndex(sheet, rowIndex, colIndex, out rangeIndex);

                LevelObject tmp = new LevelObject();
                tmp.ColIndex = colIndex;
                tmp.StrartRowIndex = rowIndex;
                if (range != null)
                {
                    tmp.EndRowIndex = range.LastRow;
                }
                else
                {
                    tmp.EndRowIndex = rowIndex;
                }
                tmp.WbsInfo = info;
                AllObjects.Add(tmp);
                result = tmp;
            }
            return result;
        }
        private static List<WBSline> ImportTotalSheetContent(ISheet sheet)
        {
            //工资项标题行序号
            int rowIndex = -1;
            //部门名称开始列序号
            int colIndex = -1;                                
            List<int> removeColumns = new List<int>();
            IRow titleRow = null;    
            for (int j = 0; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
            {
                IRow row = sheet.GetRow(j);  //读取当前行数据
                if (row != null)
                {

                    ICell cell = row.GetCell(0);  //当前表格
                    if (cell != null)
                    {
                        String strCell = cell.ToString();   //获取表格中的数据并转换为字符串类型
                        if (strCell.Equals("单位工程"))
                        {
                            titleRow = row;
                            colIndex = cell.ColumnIndex;
                            break;
                        }    
                    }
                }
            }
            if (titleRow != null)
            {

                rowIndex = titleRow.RowNum;//获取占位符所在行   
                int currentRowIndex = rowIndex + 1;
                int rangeIndex = 0;
                LevelObject currentObject = null;
                wbsRootInfos.Clear();
                AllObjects.Clear();
                List<WbsInfo> RootInfo = new List<WbsInfo>();
                WbsInfo parentInfo = null;
                for (int i = currentRowIndex; i <= sheet.LastRowNum; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (j == 0) parentInfo = null;
                        LevelObject tmp = Match(sheet, parentInfo, i, j);
                        if (tmp != null)
                        {
                            currentObject = tmp;
                            parentInfo = tmp.WbsInfo;
                        }
                    }              
                }
            }
            int codeIndex = 1;
            List<WBSline> lines = new List<WBSline>();
            wbsRootInfos.ForEach(m=> {
                WBSline line = new WBSline();
                line.WbsSysCode = String.Format("{0:D2}", codeIndex);
                line.WbsLineName = m.WbsName;
                m.WbsCode = line.WbsSysCode;   
                lines.Add(line);
                DGLines(lines, m);
                codeIndex++;
            });
            return lines;
        }
        private static List<WbsInfo> GetWbsInfos(ISheet sheet)
        {
            //工资项标题行序号
            int rowIndex = -1;
            //部门名称开始列序号
            int colIndex = -1;
            List<int> removeColumns = new List<int>();
            IRow titleRow = null;
            for (int j = 0; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
            {
                IRow row = sheet.GetRow(j);  //读取当前行数据
                if (row != null)
                {

                    ICell cell = row.GetCell(0);  //当前表格
                    if (cell != null)
                    {
                        String strCell = cell.ToString();   //获取表格中的数据并转换为字符串类型
                        if (strCell.Equals("单位工程"))
                        {
                            titleRow = row;
                            colIndex = cell.ColumnIndex;
                            break;
                        }
                    }
                }
            }
            if (titleRow != null)
            {

                rowIndex = titleRow.RowNum;//获取占位符所在行   
                int currentRowIndex = rowIndex + 1;
                int rangeIndex = 0;
                LevelObject currentObject = null;
                wbsRootInfos.Clear();
                AllObjects.Clear();
                List<WbsInfo> RootInfo = new List<WbsInfo>();
                WbsInfo parentInfo = null;
                for (int i = currentRowIndex; i <= sheet.LastRowNum; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (j == 0) parentInfo = null;
                        LevelObject tmp = Match(sheet, parentInfo, i, j);
                        if (tmp != null)
                        {
                            currentObject = tmp;
                            parentInfo = tmp.WbsInfo;
                        }
                    }
                    //图纸号等信息
                    String startZNo = sheet.GetRow(i).GetCell(7).ToString();
                    String endZNo = sheet.GetRow(i).GetCell(8).ToString();
                    String drawNo = sheet.GetRow(i).GetCell(9).ToString();
                    currentObject.WbsInfo.DrawNo = drawNo;
                    currentObject.WbsInfo.StartNo = startZNo;
                    currentObject.WbsInfo.EndNo = endZNo;
                    String itemCode = sheet.GetRow(i).GetCell(10).ToString();
                    String itemName = sheet.GetRow(i).GetCell(11).ToString();        
                    if (!String.IsNullOrEmpty(itemCode) || !String.IsNullOrEmpty(itemName))
                    {
                        BoiInfo boiInfo = new BoiInfo();
                        boiInfo.BoiCode = itemCode;
                        boiInfo.BoiName = itemName;
                        boiInfo.Qty = Convert.ToDecimal(sheet.GetRow(i).GetCell(13).NumericCellValue);
                        currentObject.WbsInfo.Details.Add(boiInfo);
                    }
                }
                int codeIndex = 1;                            
                wbsRootInfos.ForEach(m => {                                   
                    m.WbsCode = String.Format("{0:D2}", codeIndex);     
                    DGLines(m);
                    codeIndex++;
                });
            }                
            return wbsRootInfos;
        }
        private static void DGLines(WbsInfo ParentInfo)
        {
            int i = 1;
            if (ParentInfo != null && ParentInfo.WbsInfos.Count > 0)
            {
                ParentInfo.WbsInfos.ForEach(m => {      
                    m.WbsCode = ParentInfo.WbsCode + String.Format("{0:D3}", i);   
                    DGLines(m);
                    i++;
                });
            }
        }
        private static void DGLines(List<WBSline> Lines,WbsInfo ParentInfo)
        {
            int i = 1;
            if (ParentInfo != null&&ParentInfo.WbsInfos.Count>0)
            {
                ParentInfo.WbsInfos.ForEach(m => {
                    WBSline line = new WBSline();
                    line.WbsSysCode = ParentInfo.WbsCode+String.Format("{0:D3}", i);
                    line.WbsLineName = m.WbsName;
                    line.StartStakesNo = m.StartNo;
                    line.EndStakesNo = m.EndNo;
                    line.DrawNo = m.DrawNo;
                    line.ParentCode = ParentInfo.WbsCode;
                    m.WbsCode = line.WbsSysCode;
                    Lines.Add(line);   
                    DGLines(Lines, m);
                    i++;
                });
            }
        }
        public static CellRangeAddress GetMergedRegionIndex(ISheet sheet, int row, int column, out int index)
        {
            int sheetMergeCount = sheet.NumMergedRegions;

            for (int i = 0; i < sheetMergeCount; i++)
            {
                CellRangeAddress ca = sheet.GetMergedRegion(i);
                int firstColumn = ca.FirstColumn;
                int lastColumn = ca.LastColumn;
                int firstRow = ca.FirstRow;
                int lastRow = ca.LastRow;
                if (row >= firstRow && row <= lastRow)
                {
                    if (column >= firstColumn && column <= lastColumn)
                    {
                        index = i;
                        return ca;
                    }
                }
            }
            index = -1;
            return null;

        }
    
    }
    public class WbsInfo
    {
        public String WbsNo { get; set; }
        public String WbsCode { get; set; }
        public String WbsName { get; set; }
        public String DrawNo { get; set; }
        public String StartNo { get; set; }
        public String EndNo { get; set; }
        public List<BoiInfo> Details { get; set; }
        public List<WbsInfo> WbsInfos { get; set; }
        public WbsInfo() {
            Details = new List<BoiInfo>();
            WbsInfos = new List<WbsInfo>();
        }
    }
    public class BoiInfo
    {
        public String No1 { get; set; }
        public String No2 { get; set; }
        public String DrawNo { get; set; }
        public String BoiNo { get; set; }
        public String BoiCode { get; set; }
        public String BoiName { get; set; }
        public String Uom { get; set; }
        public Decimal Qty { get; set; }     
    }
    public class LevelObject
    {
        public int StrartRowIndex { get; set; }
        public int EndRowIndex { get; set; }
        public int ColIndex { get; set; }
        public WbsInfo WbsInfo { get; set; }
    }
}
