using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using System.Collections.Generic;
using Erp.CommonData;

namespace HD.MeteringPayment.Module.Forms.MeteringRptMng
{
    public partial class PrintPrjAmountPayRptFormOld : DevExpress.XtraReports.UI.XtraReport
    {
        private MeteringPayRptRoot _root = new MeteringPayRptRoot();

        public MeteringPayRptRoot Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
                if (value != null)
                {
                    bindingSource1.DataSource = value;

                    xp1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                    xp2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                    xp3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                    xp4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                    xp5.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                    xp6.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;

                    SetX1Value(value.OtherRptLst1);
                    SetX2Value(value.OtherRptLst2);
                    SetX3Value(value.OtherRptLst3); //应支付费用
                }
            }
        }

        public PrintPrjAmountPayRptFormOld()
        {
            InitializeComponent();
        }


        private void SetX1Value(List<PrjAmountPayRpt> detail)
        {
            int Range = detail.Count < 5 ? detail.Count : 5;
            for (int i = 1; i <= Range; i++)
            {
                switch (i)
                {
                    case 1:
                        {
                            x1_1_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x1_1_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x1_1_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x1_1_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x1_1_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x1_1_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x1_1_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x1_1_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x1_1_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 2:
                        {
                            x1_5_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x1_5_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x1_5_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x1_5_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x1_5_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x1_5_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x1_5_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x1_5_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x1_5_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 3:
                        {
                            x1_2_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x1_2_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x1_2_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x1_2_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x1_2_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x1_2_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x1_2_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x1_2_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x1_2_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 4:
                        {
                            x1_3_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x1_3_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x1_3_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x1_3_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x1_3_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x1_3_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x1_3_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x1_3_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x1_3_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 5:
                        {
                            x1_4_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x1_4_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x1_4_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x1_4_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x1_4_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x1_4_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x1_4_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x1_4_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x1_4_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                }

            }
        }


        private void SetX2Value(List<PrjAmountPayRpt> detail)
        {
            int Range = detail.Count < 6 ? detail.Count : 6;
            for (int i = 1; i <= Range; i++)
            {
                switch (i)
                {
                     case 1:
                        {
                            x2_3_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x2_3_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x2_3_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x2_3_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x2_3_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x2_3_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x2_3_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x2_3_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x2_3_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 2:
                        {
                            x2_8_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x2_8_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x2_8_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x2_8_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x2_8_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x2_8_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x2_8_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x2_8_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x2_8_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 3:
                        {
                            x2_4_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x2_4_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x2_4_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x2_4_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x2_4_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x2_4_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x2_4_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x2_4_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x2_4_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 4:
                        {
                            x2_5_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x2_5_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x2_5_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x2_5_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x2_5_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x2_5_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x2_5_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x2_5_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x2_5_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 5:
                        {
                            x2_6_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x2_6_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x2_6_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x2_6_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x2_6_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x2_6_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x2_6_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x2_6_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x2_6_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                    case 6:
                        {
                            x2_7_1.Text = (detail[i - 1].CtrctAmount ?? 0) != 0 ? (detail[i - 1].CtrctAmount ?? 0).ToString("n0") : "";
                            x2_7_2.Text = (detail[i - 1].ChangeAmount ?? 0) != 0 ? (detail[i - 1].ChangeAmount ?? 0).ToString("n0") : "";
                            x2_7_3.Text = (detail[i - 1].LatestAmount ?? 0) != 0 ? (detail[i - 1].LatestAmount ?? 0).ToString("n0") : "";
                            x2_7_4.Text = (detail[i - 1].EndingAmount ?? 0) != 0 ? (detail[i - 1].EndingAmount ?? 0).ToString("n0") : "";
                            x2_7_5.Text = (detail[i - 1].EndingProp ?? 0) != 0 ? (detail[i - 1].EndingProp ?? 0).ToString("p") : "";
                            x2_7_6.Text = (detail[i - 1].StartingAmount ?? 0) != 0 ? (detail[i - 1].StartingAmount ?? 0).ToString("n0") : "";
                            x2_7_7.Text = (detail[i - 1].LastProp ?? 0) != 0 ? (detail[i - 1].LastProp ?? 0).ToString("p") : "";
                            x2_7_8.Text = (detail[i - 1].Amount ?? 0) != 0 ? (detail[i - 1].Amount ?? 0).ToString("n0") : "";
                            x2_7_9.Text = (detail[i - 1].Prop ?? 0) != 0 ? (detail[i - 1].Prop ?? 0).ToString("p") : "";
                        }
                        break;
                }

            }
        }

        /// <summary>
        /// 应支付费用
        /// </summary>
        /// <param name="detail"></param>
        private void SetX3Value(List<PrjAmountPayRpt> detail)
        {
            x3_1_1.Text = (detail[0].CtrctAmount ?? 0) != 0 ? (detail[0].CtrctAmount ?? 0).ToString("n0") : "";
            x3_1_2.Text = (detail[0].ChangeAmount ?? 0) != 0 ? (detail[0].ChangeAmount ?? 0).ToString("n0") : "";
            x3_1_3.Text = (detail[0].LatestAmount ?? 0) != 0 ? (detail[0].LatestAmount ?? 0).ToString("n0") : "";
            x3_1_4.Text = (detail[0].EndingAmount ?? 0) != 0 ? (detail[0].EndingAmount ?? 0).ToString("n0") : "";
            x3_1_5.Text = (detail[0].EndingProp ?? 0) != 0 ? (detail[0].EndingProp ?? 0).ToString("p") : "";
            x3_1_6.Text = (detail[0].StartingAmount ?? 0) != 0 ? (detail[0].StartingAmount ?? 0).ToString("n0") : "";
            x3_1_7.Text = (detail[0].LastProp ?? 0) != 0 ? (detail[0].LastProp ?? 0).ToString("p") : "";
            x3_1_8.Text = (detail[0].Amount ?? 0) != 0 ? (detail[0].Amount ?? 0).ToString("n0") : ""; 
            x3_1_9.Text = (detail[0].Prop ?? 0) != 0 ? (detail[0].Prop ?? 0).ToString("p") : "";
        }

        /// <summary>
        /// 如果绑定的时候值为0，则不绑定任何值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCell_EvaluateBinding(object sender, BindingEventArgs e)
        {
            decimal value = ObjectHelper.GetDefaultDecimal(e.Value);
            if (value == 0)
            {
                e.Value = null;
            }
        }
    }
}
