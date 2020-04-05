using Hondee.Common.HDException.ErrorHandlers;
using HD.MeteringPayment.DAL.DBBaseInfo;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Erp.SharedLib.Distributed_Services.InstanceProviders;

namespace HD.MeteringPayment.Service
{
    [ApplicationErrorHandlerAttribute()]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [ErpInstanceProviderServiceBehavior()]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProjectInfoService :DBProjectInfo,IProjectInfo
    {
    }
}