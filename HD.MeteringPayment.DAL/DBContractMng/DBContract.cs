using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using HD.MeteringPayment.Domain.Entity.ContractEntity;


namespace HD.MeteringPayment.DAL.DBContractMng
{
    public class DBContract : IContract
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<Contract> GetList(string whereQuery)
        {
            return hdDbCmdManager.QueryForList<Contract>(@"SELECT * FROM [ERP_Contract].[dbo].[ctrct_contract] " + whereQuery, System.Data.CommandType.Text, null);
        }
         
    }
}
