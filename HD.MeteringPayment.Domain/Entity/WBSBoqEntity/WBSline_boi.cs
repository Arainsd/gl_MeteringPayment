using Hondee.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.WBSBoqEntity
{
    /// <summary>
    /// WBS清单和合同清单关联关系类
    /// </summary>
    public partial class WBSline_boi
    {
        #region 视图字段
        [DBField(false)]
        public String IItemCoe
        {
            get;
            set;
        }
        #endregion 

        public WBSline_boi()
        {
            StatId = 1;
        }

        /// <summary>
        /// 剩余数量
        /// </summary>
        [DBField(false)]
        public decimal remainQty
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余金额
        /// </summary>
        [DBField(false)]
        public decimal remainAmount
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 用于传递ItemNo和WBSLineNo
    /// </summary>
    public partial class WBSlineBoiNoInfo
    {
        public String ItemNo { get; set; }
        public String WBSLineNo { get; set; }
        public String WbsNo { get; set; }
    }
}
