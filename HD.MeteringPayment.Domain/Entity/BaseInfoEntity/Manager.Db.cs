using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.BaseInfoEntity
{
    public partial class Manager
    {
        public int Id
        {
            get;
            set;
        }
        public string ManagerNo
        {
            get;
            set;
        }
        public string LoginName
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public int UserType
        {
            get;
            set;
        }
        public string OrgNo
        {
            get;
            set;
        }
        public string OrgName
        {
            get;
            set;
        }
        public string ProjectName
        {
            get;
            set;
        }
        public string ProjectNo
        {
            get;
            set;
        }
        public int StatId
        {
            get;
            set;
        }
        public bool RecordValidity
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public DateTime CreateDate
        {
            get;
            set;
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public DateTime RecordDate
        {
            get;
            set;
        }
        public DateTime RowPointer
        {
            get;
            set;
        }
    }
}
