using Erp.CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
    /// <summary>
    /// 项目清单
    /// </summary>
    public partial class ContractBoq
    {
        public ContractBoq()
        {
            BoiList = new List<ContractBoi>();
        }
        /// <summary>
        /// 清单项列表
        /// </summary>
        [DBField(false)]
        public List<ContractBoi> BoiList { get; set; }
    }

}
