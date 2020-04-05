using Hondee.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
   public partial class ContractBoqChange : StatData
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
        /// <summary>
        /// 项目编号
        /// </summary>	
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
        /// <summary>
        /// 变更编号
        /// </summary>	
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
        /// <summary>
        /// 变更编码
        /// </summary>	
        private string _changeCode;
        public string ChangeCode
        {
            get { return _changeCode; }
            set
            {
                if (value != _changeCode)
                {
                    this._changeCode = value;
                    this.OnFireNotifyPropertyChanged(e => e.ChangeCode);
                }
            }
        }
        /// <summary>
        /// 变更名称
        /// </summary>	
        private string _changeName;
        public string ChangeName
        {
            get { return _changeName; }
            set
            {
                if (value != _changeName)
                {
                    this._changeName = value;
                    this.OnFireNotifyPropertyChanged(e => e.ChangeName);
                }
            }
        }
        /// <summary>
        /// 变更类型
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
        /// <summary>
        /// 变更时间
        /// </summary>	
        private DateTime _changeDate;
        public DateTime ChangeDate
        {
            get { return _changeDate; }
            set
            {
                if (value != _changeDate)
                {
                    this._changeDate = value;
                    this.OnFireNotifyPropertyChanged(e => e.ChangeDate);
                }
            }
        }
        /// <summary>
        /// 变更金额
        /// </summary>	
        private decimal _changeAmount;
        public decimal ChangeAmount
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
        /// <summary>
        /// 清单数量
        /// </summary>	
        private int _boiNum;
        public int BoiNum
        {
            get { return _boiNum; }
            set
            {
                if (value != _boiNum)
                {
                    this._boiNum = value;
                    this.OnFireNotifyPropertyChanged(e => e.BoiNum);
                }
            }
        }
        /// <summary>
        /// 变更内容及原因
        /// </summary>	
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
        /// <summary>
        /// 填单人
        /// </summary>	
        private string _prepareBy;
        public string PrepareBy
        {
            get { return _prepareBy; }
            set
            {
                if (value != _prepareBy)
                {
                    this._prepareBy = value;
                    this.OnFireNotifyPropertyChanged(e => e.PrepareBy);
                }
            }
        }
        /// <summary>
        /// 填单时间
        /// </summary>	
        private DateTime _prepareDate;
        public DateTime PrepareDate
        {
            get { return _prepareDate; }
            set
            {
                if (value != _prepareDate)
                {
                    this._prepareDate = value;
                    this.OnFireNotifyPropertyChanged(e => e.PrepareDate);
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
        private bool _fixed;
        public bool Fixed
        {
            get { return _fixed; }
            set
            {
                if (value != _fixed)
                {
                    this._fixed = value;
                    this.OnFireNotifyPropertyChanged(e => e.Fixed);
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
