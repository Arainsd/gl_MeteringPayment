using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using Erp.CommonData.Entity.Business;


namespace HD.MeteringPayment.DAL.DBBaseInfo
{
    public class DBProjectBid : IProjectBid
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<ProjectBid> GetList(string whereQuery)
        {
            return hdDbCmdManager.QueryForList<ProjectBid>(@"SELECT * FROM [ERP_Project].[dbo].[dtl_gl_projectorg] " + whereQuery, System.Data.CommandType.Text, null);
        }
      
        #region 内部方法
        #endregion
    }
}
