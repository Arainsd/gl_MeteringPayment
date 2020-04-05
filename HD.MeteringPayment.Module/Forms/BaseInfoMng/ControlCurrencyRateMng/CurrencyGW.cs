using Erp.CommonData;
using GP.DistributedServices.Seedwork.ErrorHandlers;
using HD.MeteringPayment.Module.BootLoader.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Erp.OutputValue.Module.Gateway
{
    public class CurrencyGW
    {
       
        public static DataSet GetAll(String tableName)
        {

            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute("SELECT * FROM [ERP_Basedata].[dbo].[vCurrencyControl] where Catalog=1", CommandType.Text);
            if (!String.IsNullOrEmpty(tableName))
            ds.Data.Tables[0].TableName = tableName;
            return ds.Data;
        }
        public static DataSet GetFixed(String tableName)
        {

            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute("SELECT * FROM [ERP_Basedata].[dbo].[vCurrencyControl] where Fixed=1 and  Catalog=1", CommandType.Text);
            if (!String.IsNullOrEmpty(tableName))
                ds.Data.Tables[0].TableName = tableName;
            return ds.Data;
        }
        public static String FindName(String CurrencyCode)
        {
            List<CmdParameter> gpParameters = new List<CmdParameter>();
            gpParameters.Add(new CmdParameter("@CurrencyCode",CurrencyCode));
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute("SELECT * FROM [ERP_Basedata].[dbo].[vCurrencyControl] WHERE CurrencyCode=@CurrencyCode and  Catalog=1", CommandType.Text, gpParameters);
            String Currency="";
            if (ds.Table.Rows.Count>0)
                Currency=ds["Currency"].ToString();
            return Currency;
        }
        public static Int32 Insert(String currencyCode, String currencyName, String currencyNo, Boolean cFixed)
        {
            List<CmdParameter> gpParameters = new List<CmdParameter>();
            gpParameters.Add(new CmdParameter("@CurrencyNo", currencyNo));
            gpParameters.Add(new CmdParameter("@CurrencyCode", currencyCode));
            gpParameters.Add(new CmdParameter("@Currency", currencyName));
            gpParameters.Add(new CmdParameter("@Fixed", cFixed));
            gpParameters.Add(new CmdParameter("@Catalog", 1));
            gpParameters.Add(new CmdParameter("@OperationBy", LoginInfor.LoginName));
            gpParameters.Add(new CmdParameter("@Id", 0, ParameterDirection.Output));
            gpParameters.Add(new CmdParameter("@Infor", "", ParameterDirection.Output));
            gpParameters.Add(new CmdParameter("@Ok", 0, ParameterDirection.Output));
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute("ERP_Basedata.[Finance].[Currency_Add]", CommandType.StoredProcedure, gpParameters);
         if(!Convert.ToBoolean(ds.GetParameter("@Ok").Value))
             throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
             {
                 ErrorMessage = ds.GetParameter("@Infor").Value.ToString()
             });
         return Convert.ToInt32(ds.GetParameter("@Id").Value);
           
        }
        public static void Delete(Int32 id)
        {
            List<CmdParameter> gpParameters = new List<CmdParameter>();
            gpParameters.Add(new CmdParameter("@Id", id));
            gpParameters.Add(new CmdParameter("@Infor", "", ParameterDirection.Output));
            gpParameters.Add(new CmdParameter("@Ok", 0, ParameterDirection.Output));
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute("ERP_Basedata.[Finance].[Currency_Delete]", CommandType.StoredProcedure, gpParameters);
            if (!Convert.ToBoolean(ds.GetParameter("@Ok").Value))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = ds.GetParameter("@Infor").Value.ToString()
                });
        }
        public static void Update(Int32 id, String currencyCode, String currencyName, String currencyNo, Boolean cFixed)
        {
            List<CmdParameter> gpParameters = new List<CmdParameter>();
            gpParameters.Add(new CmdParameter("@Id", id));
            gpParameters.Add(new CmdParameter("@Catalog", 1));
            gpParameters.Add(new CmdParameter("@CurrencyNo", currencyNo));
            gpParameters.Add(new CmdParameter("@CurrencyCode", currencyCode));
            gpParameters.Add(new CmdParameter("@Currency", currencyName));
            gpParameters.Add(new CmdParameter("@Fixed", cFixed));
            gpParameters.Add(new CmdParameter("@OperationBy", LoginInfor.LoginName));
            gpParameters.Add(new CmdParameter("@Infor", "", ParameterDirection.Output));
            gpParameters.Add(new CmdParameter("@Ok", 0, ParameterDirection.Output));
            ServiceDataSet ds = AppConfig.mmForm.GpSvcClient.Execute("ERP_Basedata.[Finance].[Currency_Update]", CommandType.StoredProcedure, gpParameters);
            if (!Convert.ToBoolean(ds.GetParameter("@Ok").Value))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = ds.GetParameter("@Infor").Value.ToString()
                });

        }
    }
}
