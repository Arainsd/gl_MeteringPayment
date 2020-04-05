using Hondee.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ContractBoqEntity
{
   public class ContractBoqChangeInfo:StatData
    {
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
        private Decimal _changeAmount;
        public Decimal ChangeAmount
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

        /// <summary>
        /// 发布状态
        /// </summary>	
        private Nullable<bool> _fixed;
        public Nullable<bool> Fixed
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

        /// <summary>
        /// 是否存在附件
        /// </summary>	
        private bool _hasAttachmnt;
        public bool HasAttachmnt
        {
            get { return _hasAttachmnt; }
            set
            {
                if (value != _hasAttachmnt)
                {
                    this._hasAttachmnt = value;
                    this.OnFireNotifyPropertyChanged(e => e.HasAttachmnt);
                }
            }
        }
    }
}
