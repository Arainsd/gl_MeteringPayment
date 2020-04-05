using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Erp.SharedLib.Presentation.ControlBases;
//using Erp.OutputValue.Module.Related.FiscalPeriodRelated;
using Erp.OutputValue.Module.Gateway;



namespace Erp.OutputValue.Module.Currency
{
    public partial class DollarExchangeRateForm : GpControlBase
    {
        public DollarExchangeRateForm()
        {
            InitializeComponent();
            DataSet ds = FinanceExchangeRateGW.findFiscalPeriodAll();
            DataTable table = ds.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbAccountPeroid.Properties.Items.Add(table.Rows[i]["FiscalperiodNo"]);
            }
            ds = CurrencyGW.GetFixed("fixed");
            table = ds.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbCurrency.Properties.Items.Add(new GrpCurrency(Convert.ToInt32(table.Rows[i]["Id"]), table.Rows[i]["CurrencyCode"].ToString(), table.Rows[i]["CurrencyNo"].ToString(), table.Rows[i]["Currency"].ToString(), Convert.ToBoolean(table.Rows[i]["Fixed"])));
            }
            tFinance_USDexchangerate = new DollarExchangeRateTM(FinanceExchangeRateGW.FindToRMB(DollarExchangeRateTM.TableName));
            grdcntrlExchangeRate.DataSource = tFinance_USDexchangerate.Table;
        }
        public static Guid SignFormId = Guid.NewGuid();
        public override Guid FormId
        {
            get { return SignFormId; }
        }
        private DollarExchangeRateTM tFinance_USDexchangerate;

        private void btOk_Click(object sender, EventArgs e)
        {
            if (cbAccountPeroid.SelectedItem == null || String.IsNullOrEmpty(clExchangeRate.Text)||cbCurrency.SelectedItem==null)
            {
                XtraMessageBox.Show(@"请选择会计期间\汇率\币种");
                return;
            }
            if (XtraMessageBox.Show("是否设置汇率？", "设置提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            bool isExist=false;
            for (int i = 0; i < tFinance_USDexchangerate.Table.Rows.Count; i++)
            {
                if (tFinance_USDexchangerate.Table.Rows[i]["FiscalPeriod"].ToString() == cbAccountPeroid.SelectedItem.ToString() && tFinance_USDexchangerate.Table.Rows[i]["FromCurrencyCode"].ToString() == (cbCurrency.SelectedItem as GrpCurrency).CurrencyCode)
                {
                    isExist = true;
                    break;
                }
            }
            if (isExist)
                tFinance_USDexchangerate.Update((cbCurrency.SelectedItem as GrpCurrency).CurrencyCode, (cbCurrency.SelectedItem as GrpCurrency).CurrencyName,Convert.ToInt32(cbAccountPeroid.SelectedItem), clExchangeRate.Value);
            else
                tFinance_USDexchangerate.Insert((cbCurrency.SelectedItem as GrpCurrency).CurrencyCode, (cbCurrency.SelectedItem as GrpCurrency).CurrencyName, Convert.ToInt32(cbAccountPeroid.SelectedItem), clExchangeRate.Value);
            XtraMessageBox.Show("设置成功");
        }
        class GrpCurrency
        {
            public Int32 Id { get; set; }
            public String CurrencyCode { get; set; }
            public String CurrencyNo { get; set; }
            public String CurrencyName { get; set; }
            public Boolean Fixed { get; set; }
            public GrpCurrency(Int32 id, String currencyCode, String currencyNo, String currencyName, Boolean cFixed)
            {
                Id = id;
                CurrencyCode = currencyCode;
                CurrencyNo = currencyNo;
                CurrencyName = currencyName;
                Fixed = cFixed;
            }
            public override string ToString()
            {
                return CurrencyName;
            }
        }
    }
}
