using HD.MeteringPayment.DAL.DBProgressMetering;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity;
using Hondee.CommonAdvance.EndpointBehaviors;
using Hondee.Common.HDException.ErrorHandlers;
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
    public class PrjAmountService : DBPrjAmount
    {
    }
}