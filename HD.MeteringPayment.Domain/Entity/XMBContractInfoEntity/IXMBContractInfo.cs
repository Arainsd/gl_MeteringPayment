using Erp.CommonData.Interface;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;

namespace HD.MeteringPayment.Domain.Entity.ContractEntity
{
    [ServiceContract]
    public interface IXMBContractInfo
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<XMBContractInfo> GetList(string whereQuery);

        

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        XMBContractInfo Update(XMBContractInfo entity, string operationBy);
     }
}
