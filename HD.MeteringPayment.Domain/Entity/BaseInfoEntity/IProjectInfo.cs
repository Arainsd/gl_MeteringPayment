using Erp.CommonData.Interface;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.BaseInfoEntity
{
    [ServiceContract]
    public interface IProjectInfo
    {
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<ProjectInfo> GetList(string glBidRootNo);

        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        ProjectInfo GetCurrencyAndExchangeRate(ProjectInfo project);

    }
}
