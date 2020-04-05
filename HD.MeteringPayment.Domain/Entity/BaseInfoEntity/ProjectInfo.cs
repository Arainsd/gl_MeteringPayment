using Erp.CommonData.Entity;
using Erp.CommonData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.BaseInfoEntity
{
    public partial class ProjectInfo
    {
        /// <summary>
        /// 标段名称
        /// </summary>
        public string BidName { get; set; }
        public ProjectInfo() { }
        public override string ToString()
        {
            return ProjectName;
        }
    }
}
