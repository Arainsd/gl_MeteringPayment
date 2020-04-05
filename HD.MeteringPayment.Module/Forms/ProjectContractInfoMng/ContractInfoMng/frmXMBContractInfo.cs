using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Erp.SharedLib.Presentation.ControlBases;
using HD.MeteringPayment.Module.Forms.ProjectInfoMng;
using HD.MeteringPayment.Domain.Entity.ContractEntity;
using HD.MeteringPayment.Domain.Client;
using Hondee.Common.DataConvertor;
using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;

namespace HD.MeteringPayment.Module.Forms.ProjectContractInfoMng.ContractInfoMng
{
    public partial class frmXMBContractInfo : GpControlBase
    {
        #region Form标识
        public static Guid SignGuid = Guid.NewGuid();
        public override System.Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }
        #endregion
        #region 变量定义
        /// <summary>
        /// 
        /// </summary>
        private XMBContractInfo _refXMBContractInfo;
        public List<XMBContractInfo> XMBContractInfoList { get; set; }

        public XMBContractInfo RefXMBContractInfo
        {
            get { return _refXMBContractInfo; }
            set
            {
                _refXMBContractInfo = value;
                ContractInfo = new XMBContractInfo();
                ContractInfo.Copy(value);
                bindingSource1.DataSource = value;
                LstCtrctAgreement = client.GetICtrctAgreementService().GetList(String.Format(" WHERE ContractNo='{0}'",value.ContractNo));
                //LstCtrctAgreement = client.GetICtrctAgreementService().GetList("");
                gcCtrctAgreement.DataSource = LstCtrctAgreement;
                //norm = new HDNorm();
            }
        }
        private XMBContractInfo ContractInfo;
        public ucXMBContractInfo MainHandler = null;
        private MeteringPaymentClient client = new MeteringPaymentClient();

        /// <summary>
        /// 编辑状态
        /// </summary>
        //private bool _isEdit;
        //public bool IsEdit
        //{
        //    get { return _isEdit; }
        //    set
        //    {
        //        _isEdit = value;
        //        RefreshButton();
        //    }
        //}
        ///// <summary>
        /// 验证器
        /// </summary>
        //private HDNorm _norm;
        //public HDNorm norm
        //{
        //    get
        //    {
        //        return _norm;
        //    }
        //    set
        //    {
        //        _norm = value;
        //        _norm.Ctrls.Clear();
        //        if (RefXMBContractInfo != null)
        //        {
        //            _norm.AddCtrl(ceExchangeRate, NormCtrlEditParameter.REQUIRED);
        //            _norm.AddCtrl(icbIsThree, NormCtrlEditParameter.REQUIRED);
        //            _norm.AddCtrl(icbIsBad, NormCtrlEditParameter.REQUIRED);
        //            _norm.RefreshEditChanged();
        //        }
        //    }
        //}
        #endregion

        public frmXMBContractInfo()
        {
            InitializeComponent();
        }

        private List<CtrctAgreement> LstCtrctAgreement; 

    }
}
