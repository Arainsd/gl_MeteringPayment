using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Erp.SharedLib.Presentation.ControlBases;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.BootLoader;

namespace HD.MeteringPayment.Module.Forms.MeteringRptMng
{
    public partial class MeteringRptLstControl : GpControlBase
    {
        #region 
        #region Form标识
        private static Guid signGuid = Guid.NewGuid();
        public override Guid FormId
        {
            get
            {
                return signGuid;
            }
        }
        #endregion
        private List<PrjAmountRpt> _datasource = new List<PrjAmountRpt>();
        public List<PrjAmountRpt> DataSource
        {
            get
            {
                return _datasource;
            }
            set
            {
                _datasource = value;
                gcPrjamountRpt.DataSource = value;
                gvPrjamountRpt.RefreshData();
            }
        }

        private IPrjAmountRpt client;
        private String ProjectNo = "";
        private String ProjectName = "";
        #endregion

        public MeteringRptLstControl(string ProjectNo, string ProjectName)
        {
            InitializeComponent();
            this.ProjectNo = ProjectNo;
            this.ProjectName = ProjectName;
            client = new MeteringPaymentClient().GetIPrjAmountRptService();
           
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="args"></param>
        public override void LoadParameter(params object[] args)
        {
            LoadData();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             LoadData();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        public void LoadData()
        {
            DoWorkRun("读取数据中,请稍候......", "读取数据",
                () =>
                {
                    List<PrjAmountRpt> list = new List<PrjAmountRpt>();
                    list = client.GetList(string.Format(" WHERE ProjectNo = '{0}'", ProjectNo));
                    return list;
                },
                (result, ex) =>
                {
                    if (ex == null)
                    {
                        List<PrjAmountRpt> tempAmountLst = (result as List<PrjAmountRpt>);
                        //如果是审批通过的期数，就计算他的累计业主确认值
                        foreach (PrjAmountRpt temp in tempAmountLst)
                        {
                            if (temp.Fixed)
                            {
                                temp.SumEndingOwnerAmount = 0;
                                foreach (PrjAmountRpt temp2 in tempAmountLst)
                                {
                                    if (temp2.Periods <= temp.Periods)
                                        temp.SumEndingOwnerAmount += temp2.SumOwnerAmount ?? 0;
                                }
                                temp.SumEndingOwnerAmount = temp.SumEndingOwnerAmount == 0 ? null : temp.SumEndingOwnerAmount;
                            }
                            else
                            {
                                temp.SumEndingOwnerAmount = null;
                            }
                            if (temp.ExecuteStat <= 2)
                            {
                                temp.SumSupervisionAmount = null;
                            }
                            if (temp.ExecuteStat <= 3)
                            {
                                temp.SumOwnerAmount = null;
                            }
                        }
                        DataSource = tempAmountLst;
                    }
                });
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenDetail();
        }

        /// <summary>
        /// 双击打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPrjamountRpt_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks >= 2)
            {
                OpenDetail();
            }
        }

        private void OpenDetail()
        {
            if (gvPrjamountRpt.FocusedRowHandle < 0)
            {
                return;
            }
            PrjAmountRpt rpt = gvPrjamountRpt.GetFocusedRow() as PrjAmountRpt;
            if (rpt != null && !String.IsNullOrEmpty(rpt.PrjamountNo))
            {
                DoWorkRun("读取数据中,请稍候......", "读取数据",
                   () =>
                   {
                       PrjAmountRpt result = new PrjAmountRpt();
                       result = client.Get(rpt.PrjamountNo);
                       return result;
                   },
                   (result, ex) =>
                   {
                       if (ex == null && result != null)
                       {
                           MeteringRptDetailControl form = new MeteringRptDetailControl();
                           form.DataSource = result as PrjAmountRpt;
                           AppForm.CurrentForm.ChangeForm(String.Format("项目：{0}-第{1}期报表", ProjectName, (result as PrjAmountRpt).Periods ?? 0), form);
                       }
                   });
            }
        }
    }
}
