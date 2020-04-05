using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.BaseInfoEntity
{
    public partial class ProjectInfo
    {
        public string ProjectNo
        {
            get;
            set;
        }
        public string ProjectName
        {
            get;
            set;
        }
        public string CurrencyCode
        {
            get;
            set;
        }
        public string Currency
        {
            get;
            set;
        }
        public Nullable<bool> Fixed
        {
            get;
            set;
        }
        public int Category
        {
            get;
            set;
        }
    }
}
