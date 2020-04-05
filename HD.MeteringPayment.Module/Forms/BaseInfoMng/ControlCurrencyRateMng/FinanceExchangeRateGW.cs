using Erp.CommonData;
//using Erp.OutputValue.Module.Config;
using GP.DistributedServices.Seedwork.ErrorHandlers;
using HD.MeteringPayment.Module.BootLoader.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Erp.OutputValue.Module.Currency
{
    public class FinanceExchangeRateGW
    {
        public static DataSet findFiscalPeriodAll()
        {
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute("SELECT *  FROM  [ERP_Basedata].[Finance].[Fiscalperiod] order by FiscalperiodNo desc", CommandType.Text);
            return ds.Data;
        }

        public static DataSet findAll()
        {
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute("SELECT *  FROM  [ERP_Basedata].[dbo].[finance_exchangerate] WHERE  Catalog=1 order by FiscalPeriod desc", CommandType.Text);
            return ds.Data;
        }
        public static DataTable GetPeriodExChangeRate(Int32 FiscalPeriod)
        {
            List<CmdParameter> gpParameter = new List<CmdParameter>();
            gpParameter.Add(new CmdParameter("@FiscalPeriod", FiscalPeriod));
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute(@"SELECT xch.* ,crncy.Currency FROM  [ERP_Basedata].[dbo].[finance_exchangerate] xch
                                                inner join [ERP_Basedata].[Finance].[Currency] crncy on xch.FromCurrencyCode=crncy.CurrencyCode and crncy.Fixed=1
                                                WHERE  xch.ToCurrencyCode='RMB' and  xch.Catalog=1 and xch.FiscalPeriod=@FiscalPeriod order by xch.FromCurrencyCode desc", CommandType.Text, gpParameter);
            return ds.Data.Tables[0];
        }
        public static Decimal GetPeriodExChangeRateValue(Int32 FiscalPeriod,String formCurrencyCode)
        {
            List<CmdParameter> gpParameter = new List<CmdParameter>();
            gpParameter.Add(new CmdParameter("@FiscalPeriod", FiscalPeriod));
            gpParameter.Add(new CmdParameter("@FromCurrencyCode", formCurrencyCode));
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute(@"SELECT xch.ExchangeRate  FROM  [ERP_Basedata].[dbo].[finance_exchangerate] xch
                                                inner join [ERP_Basedata].[Finance].[Currency] crncy on xch.FromCurrencyCode=crncy.CurrencyCode and crncy.Fixed=1
                                                WHERE  xch.ToCurrencyCode='RMB' and  xch.Catalog=1 and xch.FiscalPeriod=@FiscalPeriod and xch.FromCurrencyCode=@FromCurrencyCode", CommandType.Text, gpParameter);
            decimal value = -1;
            if (ds.Data.Tables[0].Rows.Count > 0)
            {
                value = Convert.ToDecimal(ds.Data.Tables[0].Rows[0]["ExchangeRate"]);
            }
            return value;
        }
        public static DataTable GetLastPeriodExChangeRate()
        {
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute(@"SELECT xch.*,crncy.Currency  FROM  [ERP_Basedata].[dbo].[finance_exchangerate] xch
                                                inner join [ERP_Basedata].[Finance].[Currency] crncy on xch.FromCurrencyCode=crncy.CurrencyCode and crncy.Fixed=1
                                                WHERE  xch.ToCurrencyCode='RMB' and xch.FiscalPeriod = (Select Max(FiscalPeriod) from [ERP_Basedata].[dbo].[finance_exchangerate] where Catalog=1 and FromCurrencyCode=xch.FromCurrencyCode ) order by xch.FiscalPeriod,xch.FromCurrencyCode desc", CommandType.Text);
            return ds.Data.Tables[0];
        }
        public static Decimal GetLastPeriodExChangeRate(String fromCurrencyCode)
        {
            List<CmdParameter> gpParameter = new List<CmdParameter>();
            gpParameter.Add(new CmdParameter("@FromCurrencyCode", fromCurrencyCode));
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute(@"SELECT xch.* ,crncy.Currency FROM  [ERP_Basedata].[dbo].[finance_exchangerate] xch
                                                inner join [ERP_Basedata].[Finance].[Currency] crncy on xch.FromCurrencyCode=crncy.CurrencyCode and crncy.Fixed=1
                                                WHERE  xch.ToCurrencyCode='RMB' and  xch.Catalog=1 and xch.FiscalPeriod = (Select Max(FiscalPeriod) from [ERP_Basedata].[dbo].[finance_exchangerate] where Catalog=1   and FromCurrencyCode=xch.FromCurrencyCode ) and  xch.FromCurrencyCode =@FromCurrencyCode order by xch.FiscalPeriod,xch.FromCurrencyCode desc", CommandType.Text, gpParameter);
            Decimal exchangeRate=-1;
            if (ds.Data.Tables[0].Rows.Count > 0)
                exchangeRate = Convert.ToDecimal(ds.Data.Tables[0].Rows[0]["ExchangeRate"]);
            return exchangeRate;

        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="fiscalPeriod"></param>
        /// <param name="fromCurrencyCode"></param>
        /// <returns></returns>
      
        public static DataSet FindToRMB(String TableName)
        {
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute(@"SELECT xch.*,crncy.Currency  FROM  [ERP_Basedata].[dbo].[finance_exchangerate] xch
                                                inner join [ERP_Basedata].[Finance].[Currency] crncy on xch.FromCurrencyCode=crncy.CurrencyCode and crncy.Fixed=1
                                                WHERE  xch.ToCurrencyCode='RMB' and  xch.Catalog=1  order by xch.FiscalPeriod,xch.FromCurrencyCode desc", CommandType.Text);
            ds.Table.TableName = TableName;
            return ds.Data;
        }
        public static Int32 Insert(Int32 FiscalPeriod, String FromCurrencyCode, String ToCurrencyCode, Decimal ExchangeRate)
       {
           List<CmdParameter> gpParameter = new List<CmdParameter>();
           gpParameter.Add(new CmdParameter("@FiscalPeriod", FiscalPeriod));
           gpParameter.Add(new CmdParameter("@Catalog", 1));
           gpParameter.Add(new CmdParameter("@ExchangeRate", ExchangeRate));
           gpParameter.Add(new CmdParameter("@FromCurrencyCode", FromCurrencyCode));
           gpParameter.Add(new CmdParameter("@ToCurrencyCode", ToCurrencyCode));
           gpParameter.Add(new CmdParameter("@OperationBy", LoginInfor.LoginName));
           gpParameter.Add(new CmdParameter("@Id", 0, ParameterDirection.Output));
           gpParameter.Add(new CmdParameter("@Infor", "", ParameterDirection.Output));
           gpParameter.Add(new CmdParameter("@Ok", 0, ParameterDirection.Output));
           ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute(@"[ERP_Basedata].[dbo].[FinanceExchangeRate_Add]", CommandType.StoredProcedure, gpParameter);
           if (!Convert.ToBoolean(ds.GetParameter("@Ok").Value))
               throw new FaultException<ApplicationServiceError>(new ApplicationServiceError() { ErrorMessage = ds.GetParameter("@Infor").Value.ToString() });
           return Convert.ToInt32(ds.GetParameter("@Id").Value);
       }
        public static void Update(Int32 FiscalPeriod, String FromCurrencyCode, String ToCurrencyCode, Decimal ExchangeRate)
       {
           List<CmdParameter> gpParameter = new List<CmdParameter>();
           gpParameter.Add(new CmdParameter("@FiscalPeriod", FiscalPeriod));
           gpParameter.Add(new CmdParameter("@Catalog", 1));
           gpParameter.Add(new CmdParameter("@ExchangeRate", ExchangeRate));
           gpParameter.Add(new CmdParameter("@FromCurrencyCode", FromCurrencyCode));
           gpParameter.Add(new CmdParameter("@ToCurrencyCode", ToCurrencyCode));
           gpParameter.Add(new CmdParameter("@OperationBy", LoginInfor.LoginName)); 
           gpParameter.Add(new CmdParameter("@Infor", "", ParameterDirection.Output));
           gpParameter.Add(new CmdParameter("@Ok", 0, ParameterDirection.Output));
           ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute(@"[ERP_Basedata].[dbo].[FinanceExchangeRate_Update]", CommandType.StoredProcedure, gpParameter);
           if (!Convert.ToBoolean(ds.GetParameter("@Ok").Value))
               throw new FaultException<ApplicationServiceError>(new ApplicationServiceError() { ErrorMessage = ds.GetParameter("@Infor").Value.ToString() });
         
       }
        public static void Delete(Int32 FiscalPeriod, String FromCurrencyCode, String ToCurrencyCode)
       {
           List<CmdParameter> gpParameter = new List<CmdParameter>();
           gpParameter.Add(new CmdParameter("@FiscalPeriod", FiscalPeriod));
           gpParameter.Add(new CmdParameter("@Catalog", 1));
           gpParameter.Add(new CmdParameter("@FromCurrencyCode", FromCurrencyCode));
           gpParameter.Add(new CmdParameter("@ToCurrencyCode", ToCurrencyCode)); 
           gpParameter.Add(new CmdParameter("@Infor", "", ParameterDirection.Output));
           gpParameter.Add(new CmdParameter("@Ok", 0, ParameterDirection.Output));
           ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute(@"[ERP_Basedata].[dbo].[FinanceExchangeRate_Delete]", CommandType.StoredProcedure, gpParameter);
           if (!Convert.ToBoolean(ds.GetParameter("@Ok").Value))
               throw new FaultException<ApplicationServiceError>(new ApplicationServiceError() { ErrorMessage = ds.GetParameter("@Infor").Value.ToString() });

       }
   
    }
}
