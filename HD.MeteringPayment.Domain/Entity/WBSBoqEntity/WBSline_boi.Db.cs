using Hondee.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.WBSBoqEntity
{
    public partial class WBSline_boi : StatData
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    this._id = value;
                    this.OnFireNotifyPropertyChanged(e => e.Id);
                }
            }
        }

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

        private string _boQNo;
        public string BoQNo
        {
            get { return _boQNo; }
            set
            {
                if (value != _boQNo)
                {
                    this._boQNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.BoQNo);
                }
            }
        }
        private string _itemNo;
        public string ItemNo
        {
            get { return _itemNo; }
            set
            {
                if (value != _itemNo)
                {
                    this._itemNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.ItemNo);
                }
            }
        }

        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (value != _itemName)
                {
                    this._itemName = value;
                    this.OnFireNotifyPropertyChanged(e => e.ItemName);
                }
            }
        }

        private string _wbsLineNo;
        public string WBSLineNo
        {
            get { return _wbsLineNo; }
            set
            {
                if (value != _wbsLineNo)
                {
                    this._wbsLineNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.WBSLineNo);
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

        private string _uom;
        public string Uom
        {
            get { return _uom; }
            set
            {
                if (value != _uom)
                {
                    this._uom = value;
                    this.OnFireNotifyPropertyChanged(e => e.Uom);
                }
            }
        }

        private decimal? _qty;
        public decimal? Qty
        {
            get { return _qty; }
            set
            {
                if (value != _qty)
                {
                    this._qty = value;
                    this.OnFireNotifyPropertyChanged(e => e.Qty);
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

        private decimal? _ctrctQty;
        public decimal? CtrctQty
        {
            get { return _ctrctQty; }
            set
            {
                if (value != _ctrctQty)
                {
                    this._ctrctQty = value;
                    this.OnFireNotifyPropertyChanged(e => e.CtrctQty);
                }
            }
        }

        private decimal? _ctrctPrjPrice;
        public decimal? CtrctPrjPrice
        {
            get { return _ctrctPrjPrice; }
            set
            {
                if (value != _ctrctPrjPrice)
                {
                    this._ctrctPrjPrice = value;
                    this.OnFireNotifyPropertyChanged(e => e.CtrctPrjPrice);
                }
            }
        }

        private decimal _ctrctAmount;
        public decimal CtrctAmount
        {
            get { return _ctrctAmount; }
            set
            {
                if (value != _ctrctAmount)
                {
                    this._ctrctAmount = value;
                    this.OnFireNotifyPropertyChanged(e => e.CtrctAmount);
                }
            }
        }

        private decimal? _latestQty;
        public decimal? LatestQty
        {
            get { return _latestQty; }
            set
            {
                if (value != _latestQty)
                {
                    this._latestQty = value;
                    this.OnFireNotifyPropertyChanged(e => e.LatestQty);
                }
            }
        }

        private decimal? _latestPrice;
        public decimal? LatestPrice
        {
            get { return _latestPrice; }
            set
            {
                if (value != _latestPrice)
                {
                    this._latestPrice = value;
                    this.OnFireNotifyPropertyChanged(e => e.LatestPrice);
                }
            }
        }

        private decimal _latestAmount;
        public decimal LatestAmount
        {
            get { return _latestAmount; }
            set
            {
                if (value != _latestAmount)
                {
                    this._latestAmount = value;
                    this.OnFireNotifyPropertyChanged(e => e.LatestAmount);
                }
            }
        }

        private decimal? _changeQty;
        public decimal? ChangeQty
        {
            get { return _changeQty; }
            set
            {
                if (value != _changeQty)
                {
                    this._changeQty = value;
                    this.OnFireNotifyPropertyChanged(e => e.ChangeQty);
                }
            }
        }

        private decimal? _changePrice;
        public decimal? ChangePrice
        {
            get { return _changePrice; }
            set
            {
                if (value != _changePrice)
                {
                    this._changePrice = value;
                    this.OnFireNotifyPropertyChanged(e => e.ChangePrice);
                }
            }
        }

        private decimal? _changeAmount;
        public decimal? ChangeAmount
        {
            get { return _changeAmount; }
            set
            {
                if (value != _changeAmount)
                {
                    this._changeAmount = value;
                    this.OnFireNotifyPropertyChanged(e => e.ChangeAmount);
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    this._description = value;
                    this.OnFireNotifyPropertyChanged(e => e.Description);
                }
            }
        }

        private string _categoryNo;
        public string CategoryNo
        {
            get { return _categoryNo; }
            set
            {
                if (value != _categoryNo)
                {
                    this._categoryNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.CategoryNo);
                }
            }
        }

        private bool _isCntrcItem;
        public bool isCntrcItem
        {
            get { return _isCntrcItem; }
            set
            {
                if (value != _isCntrcItem)
                {
                    this._isCntrcItem = value;
                    this.OnFireNotifyPropertyChanged(e => e.isCntrcItem);
                }
            }
        }

        private bool _isImportant;
        public bool isImportant
        {
            get { return _isImportant; }
            set
            {
                if (value != _isImportant)
                {
                    this._isImportant = value;
                    this.OnFireNotifyPropertyChanged(e => e.isImportant);
                }
            }
        }

        private bool _isTax;
        public bool IsTax
        {
            get { return _isTax; }
            set
            {
                if (value != _isTax)
                {
                    this._isTax = value;
                    this.OnFireNotifyPropertyChanged(e => e.IsTax);
                }
            }
        }

        private decimal? _endingComputeQty;
        public decimal? EndingComputeQty
        {
            get { return _endingComputeQty; }
            set
            {
                if (value != _endingComputeQty)
                {
                    this._endingComputeQty = value;
                    this.OnFireNotifyPropertyChanged(e => e.EndingComputeQty);
                }
            }
        }

        private decimal? _endingComputeAmount;
        public decimal? EndingComputeAmount
        {
            get { return _endingComputeAmount; }
            set
            {
                if (value != _endingComputeAmount)
                {
                    this._endingComputeAmount = value;
                    this.OnFireNotifyPropertyChanged(e => e.EndingComputeAmount);
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
    }
}
