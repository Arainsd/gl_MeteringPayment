using HD.MeteringPayment.DAL.DBWBSBoq;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Hondee.Common.HDException.ErrorHandlers;
using Hondee.CommonAdvance.EndpointBehaviors;

namespace HD.MeteringPayment.Service
{
    [ApplicationErrorHandlerAttribute()]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [ErpInstanceProviderServiceBehavior()]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WBSBoqService : DBWBSBoq,IWBSBoq
    {
    }
}