using Erp.SharedLib.Distributed_Services.InstanceProviders;
using GP.DistributedServices.Seedwork.ErrorHandlers;
using HD.MeteringPayment.DAL.DBProgressMetering;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace HD.MeteringPayment.Service
{
    [ApplicationErrorHandlerAttribute()]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [ErpInstanceProviderServiceBehavior()]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PrjAmountApprovalService : DBPrjAmount
    {
    }
}