using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.WBSBoqEntity
{
    /// <summary>
    /// 项目清单项
    /// </summary>
    public partial class WBSline
    {
        public WBSline()
        {
            StatId = 1;
            Edit = true;
            Locked = false;
        }
    }
    /// <summary>
    /// 用于传递ItemNo和ItemCode
    /// </summary>
    public partial class WBSlineNoInfo
    { 
        public String ItemCode { get; set; }
        public String ItemNo { get; set; }
        public String BoqNo { get; set; }
    }
}
