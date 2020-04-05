using Erp.CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.WBSBoqEntity
{
    /// <summary>
    /// 项目清单
    /// </summary>
    public partial class WBSBoq
    {
        public WBSBoq()
        {
            WBSlineList = new List<WBSline>();
            AllRelationList = new List<WBSline_boi>();
        }
        #region 视图字段
        public bool Released { get; set; }
        public bool Fixed { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ReleaseBy { get; set; }
        #endregion
        /// <summary>
        /// 清单项列表
        /// </summary>
        [DBField(false)]
        public List<WBSline> WBSlineList { get; set; }

        /// <summary>
        /// 清单关联关系项列表
        /// </summary>
        [DBField(false)]
        public List<WBSline_boi> AllRelationList { get; set; }
    }

}
