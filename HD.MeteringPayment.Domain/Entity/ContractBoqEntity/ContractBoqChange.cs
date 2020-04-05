using Hondee.Common.Data;
using Hondee.Common.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
    /// <summary>
    /// 清单变更
    /// </summary>
   public partial class ContractBoqChange 
    {
        /// <summary>
        /// 变更清单明细
        /// </summary>
        public List<ContractBoqChangeDetail> Details { get; set; }

        public ContractBoqChange()
        {
            Details = new List<ContractBoqChangeDetail>();
            StatId = 1;
            PrepareBy = ServiceContext.UserName;
            PrepareDate = DateTime.Now;
            ChangeDate = DateTime.Now;
        }
    }
}
