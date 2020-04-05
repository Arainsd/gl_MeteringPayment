using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
    /// <summary>
    /// 清单变更记录
    /// </summary>
  public  class ContractBoqChangeLog:ContractBoqChangeDetail
    {
        public String AdjustBy { get; set; }
        public DateTime AdjustDate { get; set; }
        public int TypeShow
        {
            get
            {
                int result = 0;//1-新增，2-修改单价，3-修改数量，4-修改单价数量，5-修改其他信息,6-删除
                if (Type == 1 || Type == 5)
                    result = 1;
                if (Type == 2)
                {
                    if (IsUpPrice && IsUpQty)
                        result = 4;
                    else if (IsUpPrice)
                    {
                        result = 2;
                    }
                    else if (IsUpQty)
                    {
                        result = 3;
                    }
                    else
                        result = 5;
                }
                if (Type == 3 || Type == 4)
                {
                    result = 6;
                }
                return result;

            }
        }
    }
}
