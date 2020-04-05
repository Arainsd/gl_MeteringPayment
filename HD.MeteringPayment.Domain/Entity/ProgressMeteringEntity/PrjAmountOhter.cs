using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity
{
    public partial class PrjAmountOther : BaseEntity<PrjAmountOther>
    {
        public PrjAmountOther() 
        {
            isChanged = false;
        }

        /// <summary>
        /// 是否更改
        /// </summary>
        public bool isChanged
        {
            get;
            set;
        }
    }
}
