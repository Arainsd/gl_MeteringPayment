using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Hondee.Common.HDException;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity
{
    [ServiceContract]
    public interface IPrjAmount
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<PrjAmount> GetList(string whereQuery);

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="PrjamountNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        PrjAmount Get(string PrjamountNo);

        /// <summary>
        /// 获取最大期数
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        int GetMaxPeriod(string ProjectNo);
        /// <summary>
        /// 获取最大期数
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        int GetMaxPeriod01(string ProjectNo);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        PrjAmount Add(PrjAmount entity, string operationBy);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void UpdateAll(PrjAmount changedAmount, List<PrjAmountDetail> changedDetail, List<PrjAmountDetail> changedWBS, List<PrjAmountOther> changedOther, string operationBy);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void Delete(String prjAmountNo, string operationBy);

        /// <summary>
        /// 提交版本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        int Commit(String prjAmountNo, string operationBy);

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void Release(String prjAmountNo,String releaseBy, string operationBy);

        /// <summary>
        /// 撤销发布
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationBy"></param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void UnFix(String prjAmountNo, string operationBy);

         /// <summary>
        /// 获取该项目的WBS清单信息和关联项信息
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        WBSBoq GetWbsInfo(string ProjectNo);

        /// <summary>
        /// 是否有上期未发布数据
        /// </summary>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        bool hasUnFixed(String ProjectNo, int currentPeriod);

        /// <summary>
        /// 是否有下期未撤销发布数据
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="currentPeriod"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        bool hasFixed(String ProjectNo, int currentPeriod);

        /// <summary>
        /// 更新监理或者业主的本期完成数量和金额
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="currentPeriod"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void updateAmountAndQty(List<PrjAmountDetail> entity, string operationBy);

        /// <summary>
        /// 更改数据提交状态
        /// </summary>
        /// <param name="WbsLineNo"></param>
        /// <param name="PrjAmountNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void ChangeBusState(PrjAmount prjAmount, String operationBy);
    }
}
