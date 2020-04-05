using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Erp.CommonData;
using Erp.CommonData.Entity.Business;
using Erp.GpServiceClient;
using Erp.SharedLib.Presentation.ControlBases;
using Erp.SharedLib.Presentation.FormBases;
using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng;
using HD.MeteringPayment.Module.Forms.ProjectInfoMng;
using Hondee.Common.DataConvertor;
using HD.MeteringPayment.Module.BootLoader.Config;
using Erp.SharedLib.Presentation.OperationForm;
using DevExpress.XtraEditors;

namespace HD.MeteringPayment.Module.Forms.ProjectContractInfoMng.XMBProjectInfoMng
{
    public partial class frmXMBProjectInfo : GpControlBase
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
        private XMBProjectInfo _refXMBProjectInfo;
        public List<XMBProjectInfo> XMBProjectInfoList { get; set; }

        public XMBProjectInfo RefXMBProjectInfo
        {
            get { return _refXMBProjectInfo; }
            set
            {
                _refXMBProjectInfo = value;
                XMBPrjInfo = new XMBProjectInfo();
                XMBPrjInfo.Copy(value);
                bindingSource1.DataSource = value;

                if (norm == null)
                {
                    norm = new HDNorm();
                }

                ////绑定增值税率
                //if (value.VATRate != 0)  //移除后面的0和小数点
                //{
                //    VATRateEdit.Text = (value.VATRate * 100).ToString().TrimEnd('0').TrimEnd('.') + "%"; 
                //}
                //绑定支付比例
                if (value.PaymentRatio != 0) //移除后面的0和小数点
                {
                    PaymentRatioEdit.Text = (value.PaymentRatio * 100).ToString().TrimEnd('0').TrimEnd('.') + "%"; 
                }
                //绑定保修金比例
                if (value.WarrantyRatio != 0) //移除后面的0和小数点
                {
                    WarrantyRatioEdit.Text = (value.WarrantyRatio * 100).ToString().TrimEnd('0').TrimEnd('.') + "%"; 
                }
            }
        }
        /// <summary>
        /// 项目结算备份数据
        /// </summary>
        string BalanceNo = "101";  //默认为未结算
        private XMBProjectInfo XMBPrjInfo;
        public ucXMBProjectInfo MainHandler = null;
        private MeteringPaymentClient client = new MeteringPaymentClient();

        /// <summary>
        /// 编辑状态
        /// </summary>
        private bool _isEdit;
        public bool IsEdit
        {
            get { return _isEdit; }
            set
            {
                _isEdit = value;
                RefreshButton();
            }
        }
        /// <summary>
        /// 验证器
        /// </summary>
        private HDNorm _norm;
        public HDNorm norm
        {
            get
            {
                return _norm;
            }
            set
            {
                _norm = value;
                _norm.Ctrls.Clear();
                if (RefXMBProjectInfo != null)
                {
                    _norm.AddCtrl(ProjectNameEdit, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(PrjTypeEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(StageEdit, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(ProjectBalanceEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(SignDeptEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(EngnrDeptEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(CustomerNameEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(ResponseDeptEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(DesignDeptEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(GpUserSelector, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(OwnerAttributeEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(ProjectTypeEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(ProjectType1Edit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(AddressNameEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(DomOvseaEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(CountryEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(RegionEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(ProvinceEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(CityEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(OwnerAttributeEdit, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(ContractAmountEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(CtrctCurrencyEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(BeginningDateEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(EndingdateEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(DurationEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(DescriptionEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(PaymentRatioEdit, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(WarrantyRatioEdit, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(VATRateEdit, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(WarrantyStartingDateEdit, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(WarrantyPeriodEdit, NormCtrlEditParameter.REQUIRED);
                    _norm.AddCtrl(ActualBeginningDateEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(AcceptanceDateEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(SettlementDateEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(CompletedAccDateEdit, NormCtrlEditParameter.NORMAL);
                    _norm.AddCtrl(memoEdit2, NormCtrlEditParameter.NORMAL);
                    _norm.RefreshEditChanged();
                }
            }
        }
        #endregion
        public frmXMBProjectInfo()
        {
            InitializeComponent();
            init();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void init()
        {
            ProjectType1Edit.Closed += (sender, e) => { bindingSource1.EndEdit(); };
            ProjectType1Edit.ClearEvent += () => { bindingSource1.EndEdit(); };

            DataTable tbStage = new DataTable();  //阶段表
            DataTable tbOrg = new DataTable();//部门表
            DataTable tbProjectBalance = new DataTable(); //项目结算表
            DataTable tbCurrency = new DataTable(); //币种表
            DataTable tbProjectType = new DataTable(); //项目类型表
            List<RateSetting> lstRateSetting = new List<RateSetting>(); //增值税率设置列表
            DoWork("加载详细信息中，请稍候...", "加载中......",
                () =>
                {
                    //加载项目结算表
                    tbProjectBalance = Erp.GpServiceClient.GpClient.CreateInstance().Execute(String.Format(@"SELECT *
                                                                                                                 FROM ERP_Project.dbo.project_balance"), CommandType.Text).Data.Tables[0];

                    //加载项目阶段信息
                    tbStage = Erp.GpServiceClient.GpClient.CreateInstance().Execute(String.Format(@"SELECT * 
                                                                                                    FROM ERP_Project.dbo.prj_stage
                                                                                                    WHERE ParentNo = '03' AND StageName <> '完工'
                                                                                                    ORDER BY StageNo ASC"), CommandType.Text).Data.Tables[0];
                    //加载部门表
                    tbOrg = Erp.GpServiceClient.GpClient.CreateInstance().Execute(String.Format(@"SELECT * 
                                                                                                  FROM ERP_Identity.Org.Org
                                                                                                  WHERE OrgCatalogNo = 'FGS'"), CommandType.Text).Data.Tables[0];
                    //加载币种表
                    tbCurrency = Erp.GpServiceClient.GpClient.CreateInstance().Execute(String.Format(@"SELECT * 
                                                                                                       FROM [ERP_Basedata].[Finance].[Currency]"), CommandType.Text).Data.Tables[0];
                    //加载项目类型表
                    tbProjectType = Erp.GpServiceClient.GpClient.CreateInstance().Execute(String.Format(@"SELECT *
                                                                                                          FROM ERP_Project.dbo.project_type1
                                                                                                          WHERE TypeLevel = 1"), CommandType.Text).Data.Tables[0];

                    //加载增值税率列表
                    DataTable tbRateSetting = Erp.GpServiceClient.GpClient.CreateInstance().Execute(@"SELECT *
                                                                                                        FROM ERP_SettlementCollection.dbo.gov_rateSetting
                                                                                                       ORDER BY RateValue ASC", CommandType.Text).Data.Tables[0];
                },
                (ex) =>
                {
                    ProjectBalanceEdit.Properties.DataSource = tbProjectBalance;
                    StageEdit.Properties.DataSource = tbStage;
                    EngnrDeptEdit.Properties.DataSource = tbOrg;
                    CtrctCurrencyEdit.Properties.DataSource = tbCurrency;
                    ProjectTypeEdit.Properties.DataSource = tbProjectType;

                    VATRateEdit.Properties.DataSource = lstRateSetting;
                });
        }

        // <summary>
        // 刷新按钮事件
        // </summary>
        private void RefreshButton()
        {
            XMBProjectInfoEdit.Visibility = !IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            XMBProjectInfoCanle.Visibility = IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            XMBProjectInfoSave.Visibility = IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            norm.Edit = IsEdit;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XMBProjectInfoCanle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefXMBProjectInfo = XMBPrjInfo;
            //bindingSource1.ResetBindings(true);
            IsEdit = false;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XMBProjectInfoEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsEdit = true;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XMBProjectInfoSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bindingSource1.EndEdit();
            RefXMBProjectInfo.ProjectBalance = ProjectBalanceEdit.Text;

            if (!norm.Validate())
            {
                XtraMessageBox.Show("填写项不符合验证规则");
                return;
            }
            DoWork("保存数据", "保存数据", () =>
            {
                client.GetIXMBProjectInfoService().Update(RefXMBProjectInfo, LoginInfor.LoginName);


                //添加操作记录
                cmn_operation opreation = new cmn_operation();
                opreation.SystemNo = AppConfig.SYSTEM_NO;
                opreation.BusinessNo = RefXMBProjectInfo.ProjectNo;
                opreation.BusinessName = RefXMBProjectInfo.ProjectName;
                opreation.Version = RefXMBProjectInfo.Version;
                opreation.OperationNo = "0";
                opreation.OperationName = "修改";
                //获取服务器时间
                DataTable dt = GpClient.CreateInstance().Execute("SELECT GETDATE() AS NowDate", CommandType.Text).Table;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                        opreation.OperationDate = DateTime.Parse(dt.Rows[0]["NowDate"].ToString());
                }
                else
                {
                    opreation.OperationDate = DateTime.Now;
                }
                opreation.OperByUserName = LoginInfor.UserName;
                opreation.Remark = String.Format("{0} 在 {1} 修改了本条记录", LoginInfor.UserName, opreation.OperationDate);
                Erp.GpServiceClient.GpClient.CreateInstance().CmnOperationAdd(opreation, LoginInfor.LoginName);
            }, (ex) =>
            {
                if (ex == null)
                {
                    IsEdit = false;

                    #region 刷新当前选择项目
                    if (AppConfig.SelectProject != null && AppConfig.SelectProject.ProjectNo == RefXMBProjectInfo.ProjectNo)
                    {
                        ProjectInfo project = new ProjectInfo();
                        project.ProjectName = RefXMBProjectInfo.ProjectName;
                        project.ProjectNo = RefXMBProjectInfo.ProjectNo;
                        AppConfig.SelectProject = new MeteringPaymentClient().GetIProjectInfoService().GetCurrencyAndExchangeRate(project);
                    }
                    #endregion 
                    //if (MainHandler != null)
                    //    MainHandler.LoadGrid();
                    //this.Close();
                }
            });
        }
        /// <summary>
        /// 选择项目经理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GpUserSelector_Click(object sender, EventArgs e)
        {
            AddPrjManager form = new AddPrjManager();
           
            if (form.ShowDialog() == DialogResult.OK)
            {
                string UserText = "";
                foreach (CacheGpuser user in form.LstSelectUser)
                {
                    if (String.IsNullOrEmpty(UserText))
                        UserText = user.UserName;
                    else
                        UserText = UserText + "," + user.UserName;
                }
                RefXMBProjectInfo.PrjManager = UserText;
                bindingSource1.ResetBindings(true);
            }
        }
        /// <summary>
        /// 选择项目经理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GpUserSelector_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }

        /// <summary>
        /// 查看操作记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarOperationHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GpOperationForm form = new GpOperationForm();
            form.SystemNo = AppConfig.SYSTEM_NO;
            if (RefXMBProjectInfo != null)
                form.RefNo = RefXMBProjectInfo.ProjectNo;
            form.LoadParameter();
            form.ShowDialog("操作记录");

        }

        /// <summary>
        /// 根据项目阶段决定项目结算信息是否显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageEdit_EditValueChanged(object sender, EventArgs e)
        {
            string stageNo = (sender as GridLookUpEdit).EditValue.ToString();
            if (RefXMBProjectInfo != null && (sender as GridLookUpEdit).Properties.DataSource != null)
            {
                try
                {
                    //设置数据源
                    string stage = ((sender as GridLookUpEdit).Properties.DataSource as DataTable).Select(String.Format("StageNo = {0}", stageNo))[0]["StageName"].ToString();
                    RefXMBProjectInfo.Stage = stage;
                }
                catch
                {

                }
            }

            switch (stageNo)
            {
                case "0304":    //完工
                case "0305":    //交工
                case "0306":    //竣工
                case "0307":    //意外终止
                case "0105":    //意外终止
                    {
                        ProjectBalanceEdit.Enabled = IsEdit;
                        ProjectBalanceEdit.EditValue = BalanceNo;
                        break;
                    }
                default:
                    {
                        ProjectBalanceEdit.Enabled = false;
                        ProjectBalanceEdit.EditValue = "";
                        break;
                    }
            }
            ProjectBalanceEdit.Focus();
            ProjectBalanceEdit.Focus();
        }

        /// <summary>
        /// 项目结算更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectBalanceEdit_EditValueChanged(object sender, EventArgs e)
        {
            string balanceNo = (sender as GridLookUpEdit).EditValue.ToString();
            if(balanceNo != BalanceNo && !String.IsNullOrEmpty(balanceNo))
                BalanceNo = balanceNo;  //如果项目结算有变更，则记录本次变更
           
            if (RefXMBProjectInfo != null && (sender as GridLookUpEdit).Properties.DataSource != null)
            {
                try
                {
                    //设置数据源
                    string balance = ((sender as GridLookUpEdit).Properties.DataSource as DataTable).Select(String.Format("BalanceNo = {0}", balanceNo))[0]["BalanceName"].ToString();
                    RefXMBProjectInfo.ProjectBalance = balance;
                }
                catch
                {

                }
            }

        }

        /// <summary>
        /// 实施单位更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngnrDeptEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (RefXMBProjectInfo != null && (sender as GridLookUpEdit).Properties.DataSource != null)
            {
                try
                {
                    string deptNo = (sender as GridLookUpEdit).EditValue.ToString();
                    string deptName = ((sender as GridLookUpEdit).Properties.DataSource as DataTable).Select(String.Format("OrgNo = {0}", deptNo))[0]["OrgName"].ToString();
                    RefXMBProjectInfo.EngnrDept = deptName;
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 合同币种更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtrctCurrencyEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (RefXMBProjectInfo != null && (sender as GridLookUpEdit).Properties.DataSource != null)
            {
                try
                {
                    string currencyCode = (sender as GridLookUpEdit).EditValue.ToString();
                    string currency = ((sender as GridLookUpEdit).Properties.DataSource as DataTable).Select(String.Format("CurrencyCode = {0}", currencyCode))[0]["Currency"].ToString();
                    RefXMBProjectInfo.CtrctCurrency = currency;
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 地点选择器改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddressNameEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (e.CloseMode == PopupCloseMode.Normal)
            {
                if (string.IsNullOrEmpty(AddressNameEdit.Text))
                {
                    RefXMBProjectInfo.AddressName = "";
                    RefXMBProjectInfo.Country = "";
                    RefXMBProjectInfo.Province = "";
                    RefXMBProjectInfo.City = ""; ;
                    RefXMBProjectInfo.Region = "";
                    RefXMBProjectInfo.DomOvsea = "";
                }
                else
                {
                    RefXMBProjectInfo.AddressName = AddressNameEdit.Text;
                    RefXMBProjectInfo.Country = AddressNameEdit.Country;
                    RefXMBProjectInfo.Province = AddressNameEdit.Province;
                    RefXMBProjectInfo.City = AddressNameEdit.City;
                    RefXMBProjectInfo.Region = AddressNameEdit.RegionName;
                    RefXMBProjectInfo.DomOvsea = AddressNameEdit.InOver;
                    if (AddressNameEdit.InOver == "境外")
                        RefXMBProjectInfo.Country = AddressNameEdit.OverCountry;
                    if (AddressNameEdit.Province == "台湾省" || AddressNameEdit.Province == "澳门特别行政区" || AddressNameEdit.Province == "香港特别行政区")
                    {
                        RefXMBProjectInfo.DomOvsea = "境外";
                    }
                }
                bindingSource1.ResetBindings(true);
            }
        }

        /// <summary>
        /// 项目类型改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectTypeEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (RefXMBProjectInfo != null && (sender as GridLookUpEdit).Properties.DataSource != null)
            {
                try
                {

                    string OvTypeNo = (sender as GridLookUpEdit).EditValue.ToString();
                    string ProjectTypeName = ((sender as GridLookUpEdit).Properties.DataSource as DataTable).Select(String.Format("OvTypeNo = {0}", OvTypeNo))[0]["ProjectTypeName"].ToString();
                    RefXMBProjectInfo.ProjectType = ProjectTypeName;

                    //清空二级类型
                    ProjectType1Edit.EditValue = null;
                    ProjectType1Edit.Text = null;
                    ProjectType1Edit.Value = null;
                    bindingSource1.EndEdit();
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 开工日期改变，自动计算工期天数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeginningDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            //如果开工日期和竣工日期均不为空，则开始计算
            if(BeginningDateEdit.EditValue != null &&　EndingdateEdit.EditValue != null)
            {
                DateTime beginDate = BeginningDateEdit.DateTime;
                DateTime endDate = EndingdateEdit.DateTime;
                TimeSpan ts = endDate - beginDate;
                if (ts.Days > 0)
                {
                    DurationEdit.Text = ts.Days.ToString();
                    if (RefXMBProjectInfo != null)
                        RefXMBProjectInfo.Duration = ts.Days.ToString();
                }
            }
        }

        /// <summary>
        /// 竣工日期改变，自动计算工期天数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndingdateEdit_EditValueChanged(object sender, EventArgs e)
        {
            //如果开工日期和竣工日期均不为空，则开始计算
            if (BeginningDateEdit.EditValue != null && EndingdateEdit.EditValue != null)
            {
                DateTime beginDate = BeginningDateEdit.DateTime;
                DateTime endDate = EndingdateEdit.DateTime;
                TimeSpan ts = endDate - beginDate;
                if (ts.Days > 0)
                {
                    DurationEdit.Text = ts.Days.ToString();
                    if (RefXMBProjectInfo != null)
                        RefXMBProjectInfo.Duration = ts.Days.ToString();
                }
            }
        }

        ///// <summary>
        ///// 增值税率改变
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void VATRateEdit_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Decimal rate = Convert.ToDecimal((sender as TextEdit).Text.TrimEnd('%')) / 100;
        //        if (RefXMBProjectInfo != null)
        //        {
        //            RefXMBProjectInfo.VATRate = rate;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        /// <summary>
        /// 支付比例改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentRatioEdit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal rate = Convert.ToDecimal((sender as TextEdit).Text.TrimEnd('%')) / 100;
                if (RefXMBProjectInfo != null)
                {
                    RefXMBProjectInfo.PaymentRatio = rate;
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 保修金比例改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarrantyRatioEdit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal rate = Convert.ToDecimal((sender as TextEdit).Text.TrimEnd('%')) / 100;
                if (RefXMBProjectInfo != null)
                {
                    RefXMBProjectInfo.WarrantyRatio = rate;
                }
            }
            catch
            {
            }
        }

       
    }
}
