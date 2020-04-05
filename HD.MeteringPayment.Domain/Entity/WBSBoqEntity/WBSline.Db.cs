using Hondee.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.WBSBoqEntity
{
    public partial class WBSline : StatData
    {
        //private int _id;
        //public int Id
        //{
        //    get { return _id; }
        //    set
        //    {
        //        if (value != _id)
        //        {
        //            this._id = value;
        //            this.OnFireNotifyPropertyChanged(e => e.Id);
        //        }
        //    }
        //}
        private string _projectNo;
        public string ProjectNo
        {
            get { return _projectNo; }
            set
            {
                if (value != _projectNo)
                {
                    this._projectNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.ProjectNo);
                }
            }
        }

        private string _bidNo;
        /// <summary>
        /// 标段No
        /// </summary>
        public string BidNo
        {
            get { return _bidNo; }
            set
            {
                if (value != _bidNo)
                {
                    this._bidNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.BidNo);
                }
            }
        }

        private string _wbsNo;
        public string WbsNo
        {
            get { return _wbsNo; }
            set
            {
                if (value != _wbsNo)
                {
                    this._wbsNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.WbsNo);
                }
            }
        }

        private string _wbsLineNo;
        public string WbsLineNo
        {
            get { return _wbsLineNo; }
            set
            {
                if (value != _wbsLineNo)
                {
                    this._wbsLineNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.WbsLineNo);
                }
            }
        }

        private string _wbsLineCode;
        /// <summary>
        /// wbs清单编号
        /// </summary>
        public string WbsLineCode
        {
            get { return _wbsLineCode; }
            set
            {
                if (value != _wbsLineCode)
                {
                    this._wbsLineCode = value;
                    this.OnFireNotifyPropertyChanged(e => e.WbsLineCode);
                }
            }
        }

        private string _wbsSysCode;
        /// <summary>
        /// wbs清单系统编号
        /// </summary>
        public string WbsSysCode
        {
            get { return _wbsSysCode; }
            set
            {
                if (value != _wbsSysCode)
                {
                    this._wbsSysCode = value;
                    this.OnFireNotifyPropertyChanged(e => e.WbsSysCode);
                }
            }
        }

        private string _wbsLineName;
        public string WbsLineName
        {
            get { return _wbsLineName; }
            set
            {
                if (value != _wbsLineName)
                {
                    this._wbsLineName = value;
                    this.OnFireNotifyPropertyChanged(e => e.WbsLineName);
                }
            }
        }

        private string _parentCode;
        public string ParentCode
        {
            get { return _parentCode; }
            set
            {
                if (value != _parentCode)
                {
                    this._parentCode = value;
                    this.OnFireNotifyPropertyChanged(e => e.ParentCode);
                }
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (value != _amount)
                {
                    this._amount = value;
                    this.OnFireNotifyPropertyChanged(e => e.Amount);
                }
            }
        }

        private decimal _amountEx;
        public decimal AmountEx
        {
            get { return _amountEx; }
            set
            {
                if (value != _amountEx)
                {
                    this._amountEx = value;
                    this.OnFireNotifyPropertyChanged(e => e.AmountEx);
                }
            }
        }

        private string _currency;
        public string Currency
        {
            get { return _currency; }
            set
            {
                if (value != _currency)
                {
                    this._currency = value;
                    this.OnFireNotifyPropertyChanged(e => e.Currency);
                }
            }
        }

        private string _currencyCode;
        public string CurrencyCode
        {
            get { return _currencyCode; }
            set
            {
                if (value != _currencyCode)
                {
                    this._currencyCode = value;
                    this.OnFireNotifyPropertyChanged(e => e.CurrencyCode);
                }
            }
        }

        private decimal _exchangeRate;
        public decimal ExchangeRate
        {
            get { return _exchangeRate; }
            set
            {
                if (value != _exchangeRate)
                {
                    this._exchangeRate = value;
                    this.OnFireNotifyPropertyChanged(e => e.ExchangeRate);
                }
            }
        }
        
        private bool _edit;
        public bool Edit
        {
            get { return _edit; }
            set
            {
                if (value != _edit)
                {
                    this._edit = value;
                    this.OnFireNotifyPropertyChanged(e => e.Edit);
                }
            }
        }

        private bool _locked;
        public bool Locked
        {
            get { return _locked; }
            set
            {
                if (value != _locked)
                {
                    this._locked = value;
                    this.OnFireNotifyPropertyChanged(e => e.Locked);
                }
            }
        }

        private int _statId;
        public int StatId
        {
            get { return _statId; }
            set
            {
                if (value != _statId)
                {
                    this._statId = value;
                    this.OnFireNotifyPropertyChanged(e => e.StatId);
                }
            }
        }

        private bool _isUse;
        public bool IsUse
        {
            get { return _isUse; }
            set
            {
                if (value != _isUse)
                {
                    this._isUse = value;
                    this.OnFireNotifyPropertyChanged(e => e.IsUse);
                }
            }
        }
        private String _startStakesNo;
        public String StartStakesNo
        {
            get { return _startStakesNo; }
            set
            {
                if (value != _startStakesNo)
                {
                    this._startStakesNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.StartStakesNo);
                }
            }
        }
        private String _endStakesNo;
        public String EndStakesNo
        {
            get { return _endStakesNo; }
            set
            {
                if (value != _endStakesNo)
                {
                    this._endStakesNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.EndStakesNo);
                }
            }
        }
        private String _drawNo;
        public String DrawNo
        {
            get { return _drawNo; }
            set
            {
                if (value != _drawNo)
                {
                    this._drawNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.DrawNo);
                }
            }
        }
        //private bool _recordValidity;
        //public bool RecordValidity
        //{
        //    get { return _recordValidity; }
        //    set
        //    {
        //        if (value != _recordValidity)
        //        {
        //            this._recordValidity = value;
        //            this.OnFireNotifyPropertyChanged(e => e.RecordValidity);
        //        }
        //    }
        //}
        //private string _createdBy;
        //public string CreatedBy
        //{
        //    get { return _createdBy; }
        //    set
        //    {
        //        if (value != _createdBy)
        //        {
        //            this._createdBy = value;
        //            this.OnFireNotifyPropertyChanged(e => e.CreatedBy);
        //        }
        //    }
        //}
        //private DateTime _createDate;
        //public DateTime CreateDate
        //{
        //    get { return _createDate; }
        //    set
        //    {
        //        if (value != _createDate)
        //        {
        //            this._createDate = value;
        //            this.OnFireNotifyPropertyChanged(e => e.CreateDate);
        //        }
        //    }
        //}
        //private string _updatedBy;
        //public string UpdatedBy
        //{
        //    get { return _updatedBy; }
        //    set
        //    {
        //        if (value != _updatedBy)
        //        {
        //            this._updatedBy = value;
        //            this.OnFireNotifyPropertyChanged(e => e.UpdatedBy);
        //        }
        //    }
        //}
        //private DateTime _recordDate;
        //public DateTime RecordDate
        //{
        //    get { return _recordDate; }
        //    set
        //    {
        //        if (value != _recordDate)
        //        {
        //            this._recordDate = value;
        //            this.OnFireNotifyPropertyChanged(e => e.RecordDate);
        //        }
        //    }
        //}
        //private DateTime _rowPointer;
        //public DateTime RowPointer
        //{
        //    get { return _rowPointer; }
        //    set
        //    {
        //        if (value != _rowPointer)
        //        {
        //            this._rowPointer = value;
        //            this.OnFireNotifyPropertyChanged(e => e.RowPointer);
        //        }
        //    }
        //}
        //private bool _inWorkflow;
        //public bool inWorkflow
        //{
        //    get { return _inWorkflow; }
        //    set
        //    {
        //        if (value != _inWorkflow)
        //        {
        //            this._inWorkflow = value;
        //            this.OnFireNotifyPropertyChanged(e => e.inWorkflow);
        //        }
        //    }
        //}
        //}
        //private bool _new;
        //public bool New
        //{
        //    get { return _new; }
        //    set
        //    {
        //        if (value != _new)
        //        {
        //            this._new = value;
        //            this.OnFireNotifyPropertyChanged(e => e.New);
        //        }
        //    }
        //}
        //private int _wfdefId;
        //public int WfdefId
        //{
        //    get { return _wfdefId; }
        //    set
        //    {
        //        if (value != _wfdefId)
        //        {
        //            this._wfdefId = value;
        //            this.OnFireNotifyPropertyChanged(e => e.WfdefId);
        //        }
        //    }
        //}
        //private int _refCategory;
        //public int RefCategory
        //{
        //    get { return _refCategory; }
        //    set
        //    {
        //        if (value != _refCategory)
        //        {
        //            this._refCategory = value;
        //            this.OnFireNotifyPropertyChanged(e => e.RefCategory);
        //        }
        //    }
        //}
        //private int _sequence;
        //public int Sequence
        //{
        //    get { return _sequence; }
        //    set
        //    {
        //        if (value != _sequence)
        //        {
        //            this._sequence = value;
        //            this.OnFireNotifyPropertyChanged(e => e.Sequence);
        //        }
        //    }
        //}
        //private string _approvalBy;
        //public string ApprovalBy
        //{
        //    get { return _approvalBy; }
        //    set
        //    {
        //        if (value != _approvalBy)
        //        {
        //            this._approvalBy = value;
        //            this.OnFireNotifyPropertyChanged(e => e.ApprovalBy);
        //        }
        //    }
        //}
        //private DateTime _approvalDate;
        //public DateTime ApprovalDate
        //{
        //    get { return _approvalDate; }
        //    set
        //    {
        //        if (value != _approvalDate)
        //        {
        //            this._approvalDate = value;
        //            this.OnFireNotifyPropertyChanged(e => e.ApprovalDate);
        //        }
        //    }
        //}
        //private int _approvalStat;
        //public int ApprovalStat
        //{
        //    get { return _approvalStat; }
        //    set
        //    {
        //        if (value != _approvalStat)
        //        {
        //            this._approvalStat = value;
        //            this.OnFireNotifyPropertyChanged(e => e.ApprovalStat);
        //        }
        //    }
        //}
        //private int _executeStat;
        //public int ExecuteStat
        //{
        //    get { return _executeStat; }
        //    set
        //    {
        //        if (value != _executeStat)
        //        {
        //            this._executeStat = value;
        //            this.OnFireNotifyPropertyChanged(e => e.ExecuteStat);
        //        }
        //    }
        //}
        //private int _version;
        //public int Version
        //{
        //    get { return _version; }
        //    set
        //    {
        //        if (value != _version)
        //        {
        //            this._version = value;
        //            this.OnFireNotifyPropertyChanged(e => e.Version);
        //        }
        //    }
        //}
        //private bool _versionValidity;
        //public bool VersionValidity
        //{
        //    get { return _versionValidity; }
        //    set
        //    {
        //        if (value != _versionValidity)
        //        {
        //            this._versionValidity = value;
        //            this.OnFireNotifyPropertyChanged(e => e.VersionValidity);
        //        }
        //    }
        //}
    }
}
