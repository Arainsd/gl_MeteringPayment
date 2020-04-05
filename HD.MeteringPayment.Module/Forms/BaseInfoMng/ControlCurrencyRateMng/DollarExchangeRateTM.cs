using Erp.CommonData;
using Erp.OutputValue.Module.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Erp.OutputValue.Module.Currency
{
   public class DollarExchangeRateTM:GPTableModule
    {
       public DollarExchangeRateTM(DataSet ds)
           : base(ds, TableName)
       {
        
       }
       public DataRow this[Int32 fiscalperiodNo, String fromCurrencyCode]
       {
           get
           {
               return Table.Select(String.Format("FiscalPeriod={0} and FromCurrencyCode='{1}'  and Catalog=1", fiscalperiodNo, fromCurrencyCode))[0];
           }
       }
       public const String TableName = "Finance_USDexchangerate";
       public void Insert(String fromCurrencyCode, String currencyName, Int32 FiscalperiodNo, Decimal ExchangeRate)
       {
           FinanceExchangeRateGW.Insert(FiscalperiodNo, fromCurrencyCode, "RMB", ExchangeRate);
           DataRow row = Table.NewRow();
           row["FiscalPeriod"] = FiscalperiodNo;
           row["FromCurrencyCode"] = fromCurrencyCode;
           row["ToCurrencyCode"] = "RMB";
           row["Currency"] = currencyName;
           row["Catalog"] = 1;
           row["ExchangeRate"] = ExchangeRate;
           Table.Rows.Add(row);
       }
       public void Update(String fromCurrencyCode,String currencyName,Int32 fiscalperiodNo, Decimal exchangeRate)
       {
           FinanceExchangeRateGW.Update(fiscalperiodNo, fromCurrencyCode, "RMB", exchangeRate);
           DataRow drCurrent = this[fiscalperiodNo, fromCurrencyCode];
           drCurrent["ExchangeRate"] = exchangeRate;
           drCurrent["Currency"] = currencyName;

       }
       public void Delete(String fromCurrencyCode, Int32 fiscalperiodNo)
       {
           FinanceExchangeRateGW.Delete(fiscalperiodNo, fromCurrencyCode, "RMB");
           Table.Rows.Remove(this[fiscalperiodNo, fromCurrencyCode]);
       }  
    }
}
