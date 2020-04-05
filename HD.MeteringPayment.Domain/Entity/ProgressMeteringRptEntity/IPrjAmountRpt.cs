using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Hondee.Common.HDException;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    [ServiceContract]
    public interface IPrjAmountRpt
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        List<PrjAmountRpt> GetList(string whereQuery);

        /// <summary>
        /// 获取报表实例
        /// </summary>
        /// <param name="PrjamountNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        PrjAmountRpt Get(string PrjamountNo);

        /// <summary>
        /// 获取中间计量表实例
        /// </summary>
        /// <param name="WbsLineNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        PrjAmountWbsRpt GetWbsRpt(string WbsLineNo, String PrjAmountNo);

        /// <summary>
        /// 获取签名图片
        /// </summary>
        /// <param name="WbsLineNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        byte[] GetSignImg(string LoginName);
    }
}
