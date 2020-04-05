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
    public interface IManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<Manager> GetList(string whereQuery);

        /// <summary>
        /// 新增管理员
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        Manager Add(Manager entity, string operationBy);

        /// <summary>
        /// 批量新增管理员
        /// </summary>
        /// <param name="list"></param>
        /// <param name="operationBy"></param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void AddList(List<Manager> list, string operationBy);

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        Manager Update(Manager entity, string operationBy);

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void Delete(Manager entity, string operationBy);

        /// <summary>
        /// 通过登录名找用户机构
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<CacheOrg> FindOrgByLoginName(string LoginName);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="addMng"></param>
        /// <param name="updateMng"></param>
        /// <param name="delMng"></param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void Save(List<Manager> addMng, List<Manager> updateMng, List<Manager> delMng, string OperationBy);

    }
}
