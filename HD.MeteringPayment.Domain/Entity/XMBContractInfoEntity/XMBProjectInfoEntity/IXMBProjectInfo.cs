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
    public interface IXMBProjectInfo
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<XMBProjectInfo> GetList(string whereQuery);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        //[FaultContract(typeof(ApplicationServiceError))]
        //[OperationContract]
        //XMBProjectInfo Add(XMBProjectInfo entity, string operationBy);

        /// <summary>
        /// 修改更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        XMBProjectInfo Update(XMBProjectInfo entity, string operationBy);

        /// <summary>
        /// 删除期数(只能删除未使用过的期数,使用过的期数不能删除[存储过程里实现])
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        //[FaultContract(typeof(ApplicationServiceError))]
        //[OperationContract]
        //ProjectDepInfo Delete(ProjectDepInfo entity, string operationBy);
        /// <summary>
        /// 获取当前最大期数
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        //[FaultContract(typeof(ApplicationServiceError))]
        //[OperationContract]
        //int GetMaxNumber();
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        XMBProjectInfo Get(string key);

        /// <summary>
        /// 根据新项目编号来获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        XMBProjectInfo GetByProjectNoNew(string key);
    }
}
