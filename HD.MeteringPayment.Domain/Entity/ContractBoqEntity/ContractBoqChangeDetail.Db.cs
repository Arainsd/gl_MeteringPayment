using Hondee.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
  public partial class ContractBoqChangeDetail : StatData
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
        private string _changeNo;
        public string ChangeNo
        {
            get { return _changeNo; }
            set
            {
                if (value != _changeNo)
                {
                    this._changeNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.ChangeNo);
                }
            }
        }
        private string _changeDetailNo;
        public string ChangeDetailNo
        {
            get { return _changeDetailNo; }
            set
            {
                if (value != _changeDetailNo)
                {
                    this._changeDetailNo = value;
                    this.OnFireNotifyPropertyChanged(e => e.ChangeDetailNo);
                }
            }
        }
        /// <summary>
        /// 1、数量修改；2、单价修改；3、新增修改；4、删除项
        /// </summary>	
        private int _type;
        public int Type
        {
            get { return _type; }
            set
            {
                if (value != _type)
                {
                    this._type = value;
                    this.OnFireNotifyPropertyChanged(e => e.Type);
                }
            }
        }
        private bool _isUpInfo;
        public bool IsUpInfo
        {
            get { return _isUpInfo; }
            set
            {
                if (value != _isUpInfo)
                {
                    this._isUpInfo = value;
                    this.OnFireNotifyPropertyChanged(e => e.IsUpInfo);
                }
            }
        }
        private bool _isUpQty;
        public bool IsUpQty
        {
            get { return _isUpQty; }
            set
            {
                if (value != _isUpQty)
                {
                    this._isUpQty = value;
                    this.OnFireNotifyPropertyChanged(e => e.IsUpQty);
                }
            }
        }
        private bool _isUpPrice;
        public bool IsUpPrice
        {
            get { return _isUpPrice; }
            set
            {
                if (value != _isUpPrice)
                {
                    this._isUpPrice = value;
                    this.OnFireNotifyPropertyChanged(e => e.IsUpPrice);
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
        private string _itemCode;
        public string ItemCode
        {
            get { return _itemCode; }
            set
            {
                if (value != _itemCode)
                {
                    this._itemCode = value;
                    this.OnFireNotifyPropertyChanged(e => e.ItemCode);
                }
            }
        }
        private string _iItemCoe;
        public string IItemCoe
        {
            get { return _iItemCoe; }
            set
            {
                if (value != _iItemCoe)
                {
                    this._iItemCoe = value;
                    this.OnFireNotifyPropertyChanged(e => e.IItemCoe);
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
        private decimal? _befQty;
        public decimal? BefQty
        {
            get { return _befQty; }
            set
            {
                if (value != _befQty)
                {
                    this._befQty = value;
                    this.OnFireNotifyPropertyChanged(e => e.BefQty);
                }
            }
        }
        private decimal? _befPrjPrice;
        public decimal? BefPrjPrice
        {
            get { return _befPrjPrice; }
            set
            {
                if (value != _befPrjPrice)
                {
                    this._befPrjPrice = value;
                    this.OnFireNotifyPropertyChanged(e => e.BefPrjPrice);
                }
            }
        }
        private decimal _befAmount;
        public decimal BefAmount
        {
            get { return _befAmount; }
            set
            {
                if (value != _befAmount)
                {
                    this._befAmount = value;
                    this.OnFireNotifyPropertyChanged(e => e.BefAmount);
                }
            }
        }
        private decimal? _afQty;
        public decimal? AfQty
        {
            get { return _afQty; }
            set
            {
                if (value != _afQty)
                {
                    this._afQty = value;
                    this.OnFireNotifyPropertyChanged(e => e.AfQty);
                }
            }
        }
        private decimal? _afPrice;
        public decimal? AfPrice
        {
            get { return _afPrice; }
            set
            {
                if (value != _afPrice)
                {
                    this._afPrice = value;
                    this.OnFireNotifyPropertyChanged(e => e.AfPrice);
                }
            }
        }
        private decimal _afAmount;
        public decimal AfAmount
        {
            get { return _afAmount; }
            set
            {
                if (value != _afAmount)
                {
                    this._afAmount = value;
                    this.OnFireNotifyPropertyChanged(e => e.AfAmount);
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
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set
            {
                if (value != _remark)
                {
                    this._remark = value;
                    this.OnFireNotifyPropertyChanged(e => e.Remark);
                }
            }
        }
        private string _approvalBy;
        public string ApprovalBy
        {
            get { return _approvalBy; }
            set
            {
                if (value != _approvalBy)
                {
                    this._approvalBy = value;
                    this.OnFireNotifyPropertyChanged(e => e.ApprovalBy);
                }
            }
        }
        private DateTime _approvalDate;
        public DateTime ApprovalDate
        {
            get { return _approvalDate; }
            set
            {
                if (value != _approvalDate)
                {
                    this._approvalDate = value;
                    this.OnFireNotifyPropertyChanged(e => e.ApprovalDate);
                }
            }
        }
        private int _approvalStat;
        public int ApprovalStat
        {
            get { return _approvalStat; }
            set
            {
                if (value != _approvalStat)
                {
                    this._approvalStat = value;
                    this.OnFireNotifyPropertyChanged(e => e.ApprovalStat);
                }
            }
        }
        private int _executeStat;
        public int ExecuteStat
        {
            get { return _executeStat; }
            set
            {
                if (value != _executeStat)
                {
                    this._executeStat = value;
                    this.OnFireNotifyPropertyChanged(e => e.ExecuteStat);
                }
            }
        }
        private int _version;
        public int Version
        {
            get { return _version; }
            set
            {
                if (value != _version)
                {
                    this._version = value;
                    this.OnFireNotifyPropertyChanged(e => e.Version);
                }
            }
        }
        private bool _versionValidity;
        public bool VersionValidity
        {
            get { return _versionValidity; }
            set
            {
                if (value != _versionValidity)
                {
                    this._versionValidity = value;
                    this.OnFireNotifyPropertyChanged(e => e.VersionValidity);
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
        private bool _new;
        public bool New
        {
            get { return _new; }
            set
            {
                if (value != _new)
                {
                    this._new = value;
                    this.OnFireNotifyPropertyChanged(e => e.New);
                }
            }
        }
        private int _sequence;
        public int Sequence
        {
            get { return _sequence; }
            set
            {
                if (value != _sequence)
                {
                    this._sequence = value;
                    this.OnFireNotifyPropertyChanged(e => e.Sequence);
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
        private bool _recordValidity;
        public bool RecordValidity
        {
            get { return _recordValidity; }
            set
            {
                if (value != _recordValidity)
                {
                    this._recordValidity = value;
                    this.OnFireNotifyPropertyChanged(e => e.RecordValidity);
                }
            }
        }
        private string _createdBy;
        public string CreatedBy
        {
            get { return _createdBy; }
            set
            {
                if (value != _createdBy)
                {
                    this._createdBy = value;
                    this.OnFireNotifyPropertyChanged(e => e.CreatedBy);
                }
            }
        }
        private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set
            {
                if (value != _createDate)
                {
                    this._createDate = value;
                    this.OnFireNotifyPropertyChanged(e => e.CreateDate);
                }
            }
        }
        private string _updatedBy;
        public string UpdatedBy
        {
            get { return _updatedBy; }
            set
            {
                if (value != _updatedBy)
                {
                    this._updatedBy = value;
                    this.OnFireNotifyPropertyChanged(e => e.UpdatedBy);
                }
            }
        }
        private DateTime _recordDate;
        public DateTime RecordDate
        {
            get { return _recordDate; }
            set
            {
                if (value != _recordDate)
                {
                    this._recordDate = value;
                    this.OnFireNotifyPropertyChanged(e => e.RecordDate);
                }
            }
        }
    }
}
