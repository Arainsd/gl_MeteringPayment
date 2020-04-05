using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
    [ServiceContract]

    public interface IContractBoqChange
    {
        /// <summary>
        /// 获取变更列表
        /// </summary>
        /// <param name="BoqNo">清单编号</param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]

        List<ContractBoqChangeInfo> GetList(String BoqNo);
        /// <summary>
        /// 获取变更列表
        /// </summary>
        /// <param name="ProjectNo">项目编号</param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<ContractBoqChangeInfo> GetListByProjectNo(String ProjectNo);
        /// <summary>
        /// 获取变更对象
        /// </summary>
        /// <param name="ChangeNo">变更编号</param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]

         ContractBoqChange Get(String ChangeNo); 
        /// <summary>
        /// 删除变更
        /// </summary>
        /// <param name="ChangeNo">变更编号</param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]

        void Delete(String ChangeNo);
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="BoqChange"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]

        ContractBoqChange Save(ContractBoqChange BoqChange);
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="BoqChange"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]

        int Commit(String ChangeNo);
        /// <summary>
        /// 发布/撤销数据
        /// </summary>
        /// <param name="ChangeNo"></param>
        /// <param name="Fixed">True-发布，False-撤销</param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]

        void Fixed(String ChangeNo,Boolean Fixed);
        /// <summary>
        /// 获取清单项变更记录
        /// </summary>
        /// <param name="ItemNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<ContractBoqChangeLog> GetItemChangeLog(String ItemNo);
    }
}
