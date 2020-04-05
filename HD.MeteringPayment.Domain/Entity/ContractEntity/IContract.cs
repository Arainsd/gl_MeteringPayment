using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Hondee.Common.HDException;

namespace HD.MeteringPayment.Domain.Entity.ContractEntity
{
     [ServiceContract]
   public interface IContract
    {
         [FaultContract(typeof(ApplicationServiceError))]
         [OperationContract]
         List<Contract> GetList(string whereQuery);
    }
}
