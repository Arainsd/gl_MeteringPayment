using Erp.CommonData.Entity.Business;
using GP.DistributedServices.Seedwork.ErrorHandlers;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace HD.MeteringPayment.DAL.DBBaseInfo
{ 
    public class DBGlUsers : IGlUsers
    {
        public List<CacheGpuser> GetList(string key)
        {
            //return HdDbCmdManager.GetInstance
            //    ().QueryForList<CacheGpuser>(String.Format(@"Select LoginName,UserName 
            //                                                From ERP_Identity.Auth.Gpuser 
            //                                                Where CreateByOrgNo in
            //                                                (Select OrgNo From ERP_Identity.Org.Org
            //                                                Where OrgCode like '{0}%')", key), CommandType.Text, null);
            return HdDbCmdManager.GetInstance().QueryForList<CacheGpuser>(String.Format(@"SELECT LoginName, UserName FROM ERP_Identity.Auth.Gpuser
WHERE LoginName in (SELECT LoginName FROM ERP_Identity.Org.OrgMember where OrgNo in (SELECT OrgNo from ERP_Identity.Org.Org where OrgCode like '{0}%'))", key), CommandType.Text, null);
        }
    }
}
