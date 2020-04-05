using HD.MeteringPayment.DAL.DBContractBoq;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Erp.SharedLib.Distributed_Services.InstanceProviders;
using Hondee.Common.HDException.ErrorHandlers;

namespace HD.MeteringPayment.Service
{
    [ApplicationErrorHandlerAttribute()]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [ErpInstanceProviderServiceBehavior()]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ContractBoqService : DBContractBoq,IContractBoq
    {
    }
}