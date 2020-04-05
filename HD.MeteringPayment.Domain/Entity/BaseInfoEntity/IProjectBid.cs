using Erp.CommonData.Entity.Business;
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
    public interface IProjectBid
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<ProjectBid> GetList(string whereQuery);
    }
}
