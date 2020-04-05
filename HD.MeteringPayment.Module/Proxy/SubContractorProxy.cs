using Erp.CommonData.Entity;
using Erp.CommonData.Interface;
using Erp.GpServiceClient;
using HD.SettlementCollection.Module.BootLoader.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.SettlementCollection.Module.Proxy
{

    public class SubContractorProxy<IContainer, TEntity> : GpRepositoryClient<IContainer, TEntity>
        where IContainer : IRepositoryContainer<TEntity>
        where TEntity : BaseEntity<TEntity>, new()
    {
        static SubContractorProxy<IContainer, TEntity> instance;
        public static SubContractorProxy<IContainer, TEntity> CreateInstance()
        {
            if (instance == null)
                instance = new SubContractorProxy<IContainer, TEntity>();
            return instance;
        }
        public SubContractorProxy()
        {
            EndPoint = AppConfig.SEndPoint;
            LoginName = AppConfig.SLoginName;
            Psw = AppConfig.SPsw;
            if (AppConfig.SEndPoint[AppConfig.SEndPoint.Length - 1] != '/')
                EndPoint = AppConfig.SEndPoint + "/";
            Type type = typeof(IContainer);
            String svcName = type.FullName + ".svc/tcp";
            EndPoint += svcName;
        }

    }
}
