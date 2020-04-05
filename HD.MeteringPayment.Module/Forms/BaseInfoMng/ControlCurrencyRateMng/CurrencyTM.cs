using Erp.CommonData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.OutputValue.Module.Gateway
{
    public class CurrencyTM:GPTableModule
    {
        public CurrencyTM(DataSet ds)
            : base(ds, TableName)
        { }
        public DataRow this[Int32 Id]
        {
            get
            {
                return Table.Select(String.Format("Id={0}", Id))[0];
            }
        }
        public DataRow this[String currencyCode]
        {
            get
            {
                DataRow[] drs= Table.Select(String.Format("CurrencyCode='{0}'", currencyCode));
                if (drs != null && drs.Length > 0)
                    return drs[0];
                return null;
            }
        }
        public void Insert(String currencyCode, String currencyName, String currencyNo,Boolean cFixed)
        {
            Int32 id = CurrencyGW.Insert(currencyCode, currencyName, currencyNo, cFixed);
            DataRow newRow =this.Table.NewRow();
            newRow["CurrencyCode"] = currencyCode;
            newRow["Currency"] = currencyName;
            newRow["CurrencyNo"] = currencyNo;
            newRow["Fixed"] = cFixed;
            newRow["Id"] = id;
            this.Table.Rows.Add(newRow);
 
        }
        public void Delete(Int32 id)
        {
            CurrencyGW.Delete(id);
            Table.Rows.Remove(this[id]);
        }
        public void Update(Int32 id, String currencyCode, String currencyName, String currencyNo, Boolean cFixed)
        {
            CurrencyGW.Update(id, currencyCode, currencyName, currencyNo, cFixed);
            DataRow dr = this[id];
          dr["Currency"] = currencyName;
          dr["CurrencyCode"] = currencyCode;
          dr["CurrencyNo"] = currencyNo;
          dr["Fixed"] = cFixed;
        }
        public const String TableName="Currecy";
    }
}
