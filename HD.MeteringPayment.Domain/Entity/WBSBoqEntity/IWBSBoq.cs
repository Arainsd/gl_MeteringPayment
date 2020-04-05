using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.WBSBoqEntity
{
    /// <summary>
    /// 清单的服务接口
    /// </summary>
    [ServiceContract]
    public interface IWBSBoq
    {
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        WBSBoq GetByProjectNo(String ProjectNo);
        /// <summary>
        /// 保存清单更改
        /// </summary>
        /// <param name="ProjectNo"></param>
        /// <param name="AddList"></param>
        /// <param name="UpdateList"></param>
        /// <param name="DeleteItemNoList"></param>
        /// <returns>添加的WBS项和关联关系</returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        WBSBoq Save(string ProjectNo, string BidNo, String BoqName, decimal TotalAmount
                   ,List<WBSline> AddList, List<WBSline> UpdateList, List<string> DeleteItemNoList
                   , List<WBSline_boi> AddRelation, List<WBSline_boi> UpdateRelation, List<WBSline_boi> DeleteRelation);
        /// <summary>
        /// 更改清单状态
        /// </summary>
        /// <param name="BoqNo">清单No</param>
        /// <param name="ExcuteStat">1-未锁定，2-锁定，3-变更中</param>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void ChangeStat(WBSBoq boq);
        /// <summary>
        /// 发布
        /// </summary>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void Release(WBSBoq boq);
        /// <summary>
        /// 撤销发布
        /// </summary>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void Unfix(WBSBoq boq);

        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        void ImportWBS( string WbsNo,List<WBSline> ImportList);

        /// <summary>
        /// 获取Boq并将其压缩成字节流
        /// </summary>
        /// <param name="ApplyNo"></param>
        /// <returns></returns>
        [FaultContract(typeof(ApplicationServiceError))]
        [OperationContract]
        Byte[] GetBytesByProjectNo(string ProjectNo);

    }
}
