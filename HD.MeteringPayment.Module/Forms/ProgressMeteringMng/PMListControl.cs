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
using HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.BootLoader;
using Erp.SharedLib.Presentation.Lib;
using Erp.CommonData;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;

namespace HD.MeteringPayment.Module.Forms.ProgressMeteringMng
{
    public partial class PMListControl : GpControlBase
    {
        #region 变量声明
        #region Form标识
        private static Guid signId = Guid.NewGuid();
        public override Guid FormId
        {
            get
            {
                return signId;
            }
        }
        #endregion
        private List<PrjAmount> _lstPrjAmount = new List<PrjAmount>();
        public List<PrjAmount> LstAmount
        {
            get
            {
                return _lstPrjAmount;
            }
            set
            {
                _lstPrjAmount = value;
                gcProgressMetering.DataSource = value;
                gcProgressMetering.RefreshDataSource();
            }
        }
        private IPrjAmount client;
        /// <summary>
        /// 项目编号
        /// </summary>
        public String ProjectNo { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public String ProjectName { get; set; }
        /// <summary>
        /// 角色级别
        /// 0在项目标段、1在承包单位 、2监理、3在业主合约部（项目公司合约部）、4在项目公司总经理、5在项目公司合约部经理
        /// </summary>
        public int RoleLevel { get; set; }
        #endregion
        public PMListControl(String ProjectNo, String ProjectName,int RoleLevel)
        {
            this.ProjectNo = ProjectNo;
            this.ProjectName = ProjectName;
            this.RoleLevel = RoleLevel;
            InitializeComponent();
            client = new MeteringPaymentClient().GetIPrjAmountService();
            RefreshStat();
        }

        private void PMListControl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            DoWorkRun("读取数据中，请稍后......", "读取中",
                () =>
                {
                    //return client.GetList(String.Format("WHERE ProjectNo = '{0}' AND ExecuteStat>={1} and Edit = 1 ORDER BY Periods DESC", this.ProjectNo, this.RoleLevel));
                    return client.GetList(String.Format("WHERE ProjectNo = '{0}' and Edit = 1 ORDER BY Periods DESC", this.ProjectNo ));
                },
                (result, ex) =>
                {
                    if (ex == null)
                    {
                        List<PrjAmount> tempAmountLst = (result as List<PrjAmount>);
                        //如果是审批通过的期数，就计算他的累计业主确认值
                        foreach (PrjAmount temp in tempAmountLst)
                        {
                            if (temp.Fixed)
                            {
                                temp.SumEndingOwnerAmount = 0;
                                foreach (PrjAmount temp2 in tempAmountLst)
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
                        LstAmount = tempAmountLst;
                        PeopleSelector.DataSource = Erp.GpServiceClient.GpClient.UserTb;
                    }     
                });
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (LstAmount.Find(m => m.ProjectNo == AppConfig.SelectProject.ProjectNo && !m.Fixed) != null)
            {
                XtraMessageBox.Show("该项目存在未发布的数据,请确认发布后再进行填报", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WBSBoq originWbsInfo = client.GetWbsInfo(AppConfig.SelectProject.ProjectNo);
            if(originWbsInfo == null || (originWbsInfo != null && string.IsNullOrEmpty(originWbsInfo.WbsNo)))
            {
                XtraMessageBox.Show("找不到该项目的WBS清单，请编写WBS清单后再新增进度计量数据" , "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrjAmount newData = new PrjAmount();
            newData.ProjectNo = AppConfig.SelectProject.ProjectNo;
            newData.WbsNo = originWbsInfo.WbsNo;
            newData.PrepareBy = LoginInfor.LoginName;
            int MaxPeriod = client.GetMaxPeriod(AppConfig.SelectProject.ProjectNo) + 1;
            frmAddNew form = new frmAddNew(MaxPeriod);
            if (form.ShowDialog() == DialogResult.OK)
            {
                newData.Periods = form.Periods;
                newData.PeriodsName = form.PeriodsName;
                newData.PrjamountName = String.Format("{0}-第{1}期计量", AppConfig.SelectProject.ProjectName, newData.Periods);
                newData.CreateDate = form.CreateDate;
                newData.PrepareDate = form.CreateDate;


                DoWork("新增数据中，请稍后......", "新增数据",
                    () =>
                    {
                        client.Add(newData, LoginInfor.LoginName);
                    },
                    (ex) =>
                    {
                        if (ex == null)
                        {
                            LoadData();
                        }
                    });
            }
        }

        /// <summary>
        /// 点击打开按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenDetail();
        }


        /// <summary>
        /// 双击打开行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProgressMetering_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks >= 2)
            {
                OpenDetail();
            }
        }

        /// <summary>
        /// 打开
        /// </summary>
        private void OpenDetail()
        {
            PrjAmount selectPrjAmount = gvProgressMetering.GetFocusedRow() as PrjAmount;
            if (selectPrjAmount != null)
            {
                DoWorkRun("读取中，请稍后......", "读取数据中",
                    () =>
                    {
                        PrjAmount result = client.Get(selectPrjAmount.PrjamountNo);
                        return result;
                    },
                    (result, ex) =>
                    {
                        if (ex == null)
                        {
                            PMDetailContainer form = new PMDetailContainer();
                            form.mainHandler = this;
                            form.isEdit = false;
                            form.DataSource = result as PrjAmount;
                            AppForm.CurrentForm.ChangeForm(String.Format("第{0}期进度计量详细情况", form.DataSource.Periods), form, selectPrjAmount.PrjamountNo);
                        }
                    });
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrjAmount deleteRow = gvProgressMetering.GetFocusedRow() as PrjAmount;
            if (deleteRow != null)
            {
                if (deleteRow.Fixed)
                {
                    XtraMessageBox.Show("已发布，不可删除");
                    return;
                }
                if (LstAmount.Find(m => m.Periods > deleteRow.Periods && m.ProjectNo == deleteRow.ProjectNo) != null)
                {
                    XtraMessageBox.Show("存在期数更大的数据，请按照期数从后往前删除。");
                    return;
                }

                if (XtraMessageBox.Show("确认删除？", "确认操作", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    client.Delete(deleteRow.PrjamountNo, LoginInfor.LoginName);
                    LoadData();
                }
            }
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
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridExportHelper.ExportExcel(gvProgressMetering, string.Format("项目：{0}-进度计量", AppConfig.SelectProject.ProjectName));
        }
        private void RefreshStat()
        {
            //只有项目部能新建和删除
            BarAdd.Enabled = RoleLevel == 0;
            BarDelete.Enabled = RoleLevel == 0;      
        }

       
    }
}
