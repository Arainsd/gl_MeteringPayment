using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
    /// <summary>
    /// 清单的服务接口
    /// </summary>
    [ServiceContract]
    public interface IContractBoq
    {
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        ContractBoq GetByProjectNo(String ProjectNo);
        /// <summary>
        /// 保存清单更改
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="AddList"></param>
        /// <param name="UpdateList"></param>
        /// <param name="DeleteItemNoList"></param>
        /// <returns>添加项的返回值No</returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<ContractBoiNoInfo> Save(string ProjectNo, String BidNo,String BoqName, decimal TotalAmount, List<ContractBoi> AddList, List<ContractBoi> UpdateList, List<string> DeleteItemNoList);
        /// <summary>
        /// 更改清单状态
        /// </summary>
        /// <param name="BoqNo">清单No</param>
        /// <param name="ExcuteStat">1-未锁定，2-锁定，3-变更中</param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
         void ChangeStat(string BoqNo,int ExcuteStat);
    }
}
