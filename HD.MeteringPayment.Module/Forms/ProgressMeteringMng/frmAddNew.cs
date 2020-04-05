using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Erp.SharedLib.Presentation.Lib;
using Erp.SharedLib.Presentation.FormBases;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Domain.Client;
using DevExpress.XtraEditors.Controls;

namespace HD.MeteringPayment.Module.Forms.ProgressMeteringMng
{     public partial class frmAddNew : GpFormBase
    {
        #region 
        private HDNorm _norm;
        public HDNorm Norm
        {
            get
            {
                return _norm;
            }
            set
            {
                _norm = value;
                if (_norm != null)
                {
                    if (Periods != 1)
                    {
                        _norm.AddCtrl(PeriodsEdit, NormCtrlEditParameter.READONLY);
                    }
                    else
                    {
                        _norm.AddCtrl(PeriodsEdit, NormCtrlEditParameter.REQUIRED);
                    }
                    _norm.AddCtrl(imageComboBoxEdit1, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(CreateDateEdit, NormCtrlEditParameter.READONLY);
                    _norm.RefreshEditChanged();
                }
            }
        }
        private IPrjAmount client;
        public int Periods
        {
            get
            {
                return Convert.ToInt32(PeriodsEdit.EditValue);
            }
            set
            {
                PeriodsEdit.EditValue = value;
            }
        }
        public string PeriodsName
        {
            get { return imageComboBoxEdit1.EditValue.ToString(); }
            set { imageComboBoxEdit1.EditValue = value; }
        }
        public DateTime CreateDate
        {
            get
            {
                return CreateDateEdit.DateTime;
            }
            set
            {
                CreateDateEdit.DateTime = value;
            }
        }
        #endregion
        public frmAddNew(int PeriodsNum)
        {
            InitializeComponent();             client = new MeteringPaymentClient().GetIPrjAmountService();
            PeriodsEdit.EditValue = PeriodsNum; 
            CreateDateEdit.DateTime = DateTime.Now;
            Norm = new HDNorm();
            LoadPeriodData();
        }
        /// <summary>
        /// 加载期数数据
        /// </summary>
        private void LoadPeriodData()
        {
            DoWorkRun("读取数据中，请稍后......", "读取中", () =>
            {
                return client.GetMaxPeriod01(AppConfig.SelectProject.ProjectNo);
            },
            (result, ex) =>
            {
                if (ex == null)
                {
                    int idateMow = DateTime.Now.Year * 100 + DateTime.Now.Month;
                    int idateMax = 0;
                    int prePeriod = int.Parse(result.ToString());

                    if (prePeriod != 0)
                    {
                        if (prePeriod % 100 < 12)
                        {
                            idateMax = ++prePeriod;
                        }
                        else
                        {
                            idateMax = (prePeriod / 100 + 1) * 100 + 1;
                        }
                    }
                    else
                    {
                        idateMax = (DateTime.Now.Year - 5) * 100 + 1;
                    }

                    List<ImageComboBoxItem> lstItem = new List<ImageComboBoxItem>();
                    for (int date = idateMax; date <= idateMow;)
                    {
                        ImageComboBoxItem item = new ImageComboBoxItem(string.Format("{0}年{1}月", date / 100, date % 100), date, -1);
                        lstItem.Insert(0, item);

                        if (date % 100 < 12)
                        {
                            date++;
                        }
                        else
                        {
                            date = (date / 100 + 1) * 100 + 1;
                        }
                    }
                    imageComboBoxEdit1.Properties.Items.AddRange(lstItem);
                    if (lstItem.Count > 0)
                    {
                        imageComboBoxEdit1.SelectedIndex = 0;
                    }
                }
            });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.Validate())
            {
                XtraMessageBox.Show("输入错误，请验证后再保存.");
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void PeriodsEdit_Validating(object sender, CancelEventArgs e)
        {
            if (Periods <= 0 || Periods >= 99999)
            {
                PeriodsEdit.ErrorText = "期数输入不正确：超出范围！";
                e.Cancel = true;
            }
        }
    }
}