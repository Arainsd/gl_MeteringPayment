using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity
{
    public partial class PrjAmountDetail : BaseEntity<PrjAmountDetail>
    {
        public PrjAmountDetail() 
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


        public string BindItemName
        {
            get
            {
                if (!String.IsNullOrEmpty(this.PrjamountdetailNo))
                {
                    return this.ItemName;
                }
                else
                {
                    return this.WbsLineName;
                }
            }
        }
    }
}
