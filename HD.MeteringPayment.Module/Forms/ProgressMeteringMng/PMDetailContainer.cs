using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Erp.SharedLib.Presentation.ControlBases;
using Erp.SharedLib.Presentation.AttactmentForm;
using HD.MeteringPayment.Module.BootLoader.Config;
using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity;
using HD.MeteringPayment.Domain.Client;
using Erp.CommonData;
using Erp.GpServiceClient;
using Erp.CommonData.Entity;
using Erp.CommonData.Entity.Business;
using Erp.SharedLib.Presentation.WfinfoView;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using HD.MeteringPayment.Module.Forms.MeteringRptMng;
using HD.MeteringPayment.Module.BootLoader;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Data.Filtering.Helpers;
using Hondee.Common.HDException;
using DevExpress.XtraTreeList.Columns;
using HD.MeteringPayment.Domain.EnumEntity;

namespace HD.MeteringPayment.Module.Forms.ProgressMeteringMng
{
    public partial class PMDetailContainer : GpControlBase
    {
        #region 变量声明
        #region GpForm标识
        static Guid SignGuid = Guid.NewGuid();
        IPrjAmountRpt prjAmountRptClient;
        public override Guid FormId
        {
            get
            {
                return SignGuid;
            }
        }
        #endregion
        private PrjAmount ApplyedDataSource;
        public PMListControl mainHandler;
        /// <summary>
        /// 1标段管理员、2监理、3业主总经理
        /// </summary>
        private int LoginRoleType;
        /// <summary>
        ///  0项目标段、1承包单位 、2监理、3项目公司合约部、4项目公司总经理、项目公司合约部经理
        /// </summary>
        private int RoleLevel
        {
            get
            {
                switch (LoginRoleType)
                {
                    case 1:
                        return 0;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                }
                return 0;
            }
        }
        private List<PrjAmountDetail> changedPrjAmountDetail = new List<PrjAmountDetail>();
        private List<PrjAmountDetail> changedPrjAmountWBS = new List<PrjAmountDetail>();
        private List<PrjAmountOther> changedPrjAmountOther = new List<PrjAmountOther>();
        private PrjAmount changedAmount = new PrjAmount();
        /// <summary>
        /// 编辑状态
        /// </summary>
        private bool _edit;
        public bool isEdit
        {
            get
            {
                return _edit;
            }
            set
            {
                _edit = value;
                RefreshStat();

                if (Norm == null)
                {
                    Norm = new HDNorm();
                }
                Norm.Edit = value;

                if (attachmentControl != null)
                {
                    attachmentControl.SetEditEnable(value);
                }
                //if (attachmentControl2 != null)
                //{
                //    attachmentControl2.SetEditEnable(value);
                //}
                //if (attachmentControl3 != null)
                //{
                //    attachmentControl3.SetEditEnable(value);
                //}
            }
        }

        private HDNorm _norm;
        public HDNorm Norm
        {
            get
            {
                return _norm;
            }
            set
            {
                if (value != null)
                {
                    _norm = value;
                    _norm.AddCtrl(dateEdit1, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(PeriodsEdit, NormCtrlEditParameter.READONLY);
                    _norm.AddCtrl(RemarkEdit, NormCtrlEditParameter.NORMAL);
                    _norm.RefreshEditChanged();
                }
            }
        }

        private PrjAmount _prjAmount = new PrjAmount();
        public PrjAmount DataSource
        {
            get
            {
                return _prjAmount;
            }
            set
            {
                _prjAmount = value;
                bsPrjAmount.DataSource = value;
                bsPrjAmount.ResetBindings(true);

                if (value != null)
                {
                    tlConstruction.DataSource = value.LstAmountDetail;
                    tlConstruction.RefreshDataSource();
                    tlConstruction.ExpandAll();

                    tlOther.DataSource = value.LstAmountOther;
                    tlOther.RefreshDataSource();
                    tlOther.ExpandAll();

                    //读取附件
                    if (attachmentControl != null)
                    {
                        attachmentControl.BusinessNo = value.PrjamountNo;
                        attachmentControl.LoadParameter(value.PrjamountNo);
                    }
                    ////读取附件-必传附件1
                    //if (attachmentControl2 != null)
                    //{
                    //    attachmentControl2.BusinessNo = value.PrjamountNo;
                    //    attachmentControl2.LoadParameter(value.PrjamountNo);
                    //}
                    ////读取附件-必传附件2
                    //if (attachmentControl3 != null)
                    //{
                    //    attachmentControl3.BusinessNo = value.PrjamountNo;
                    //    attachmentControl3.LoadParameter(value.PrjamountNo);
                    //}
                }
                RefreshStat();
            }
        }
        /// <summary>
        /// 是否是展示界面
        /// </summary>
        private bool _isShowControl = false;
        public bool isShowControl
        {
            get
            {
                return _isShowControl;
            }
            set
            {
                _isShowControl = value;
                RefreshStat();
            }
        }
        /// <summary>
        /// 附件控件
        /// </summary>
        private GeneralUploadForm attachmentControl;
        //private GeneralUploadForm attachmentControl2;
        //private GeneralUploadForm attachmentControl3;
        /// <summary>
        /// 数据库连接接口
        /// </summary>
        private IPrjAmount client;
        #endregion
        public PMDetailContainer()
        {
            InitializeComponent();
            switch (AppConfig.CurrentLoginUser.LoginRole)
            {
                case AppConfig.ManagerRole.XMBMngr:
                    LoginRoleType = 1; break;
                case AppConfig.ManagerRole.FGSMngr:
                    LoginRoleType = 2; break;
                case AppConfig.ManagerRole.GrpMngr:
                    LoginRoleType = 3; break;
            }
            prjAmountRptClient = new MeteringPaymentClient().GetIPrjAmountRptService();
            Init();
            tlConstruction.FilterNode += OnFilterNode;
            tlConstruction.ActiveFilterString = "";
        }

        #region 行过滤查找  ，避免treelist只筛选第一层的BUG
        private void OnFilterNode(object sender, FilterNodeEventArgs e)
        {
            if (!Object.ReferenceEquals(tlConstruction.ActiveFilterCriteria, null))
            {
                e.Handled = true;
                e.Node.Visible = IsNodeMatchFilter(e.Node);
            }
        }
        static bool IsNodeMatchFilter(TreeListNode node)
        {
            bool isVisible = true;
            if (!Object.ReferenceEquals(node.TreeList.ActiveFilterCriteria, null))
            {
                ExpressionEvaluator ee = new ExpressionEvaluator(TypeDescriptor.GetProperties(typeof(PrjAmountDetail)), node.TreeList.ActiveFilterCriteria, false);
                isVisible = ee.Fit(node.TreeList.GetDataRecordByNode(node));
                if (!isVisible)
                {
                    foreach (TreeListNode n in node.Nodes)
                    {
                        if (IsNodeMatchFilter(n))
                        {
                            isVisible = true;
                            break;
                        }
                    }
                }
            }
            return isVisible;
        }
        #endregion

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void Init()
        {
            //初始化附件控件
            attachmentControl = new GeneralUploadForm();
            attachmentControl.Folder = AppConfig.SYSTEM_NO;
            attachmentControl.CatalogNo = "1";
            attachmentControl.SetEditEnable(false);
            attachmentControl.SetEditPanelVisable(false);
            attachmentControl.Dock = DockStyle.Fill;
            AttaPage3.Controls.Add(attachmentControl);

            ////必传附件1
            //attachmentControl2 = new GeneralUploadForm();
            //attachmentControl2.Folder = AppConfig.SYSTEM_NO;
            //attachmentControl2.CatalogNo = "2";
            //attachmentControl2.SetEditEnable(false);
            //attachmentControl2.SetEditPanelVisable(false);
            //attachmentControl2.Dock = DockStyle.Fill;
            //AttaPage1.Controls.Add(attachmentControl2);

            ////必传附件2
            //attachmentControl3 = new GeneralUploadForm();
            //attachmentControl3.Folder = AppConfig.SYSTEM_NO;
            //attachmentControl3.CatalogNo = "3";
            //attachmentControl3.SetEditEnable(false);
            //attachmentControl3.SetEditPanelVisable(false);
            //attachmentControl3.Dock = DockStyle.Fill;
            //AttaPage2.Controls.Add(attachmentControl3);

            //初始化数据库连接
            client = new MeteringPaymentClient().GetIPrjAmountService();
        }

        /// <summary>
        /// 刷新各个控件状态
        /// </summary>
        private void RefreshStat()
        {
            //顶部Bar按钮
            standaloneBarDockControlTop.Visible = !isShowControl;
            //基本信息
            BaseInfoGroup.Visibility = !isShowControl ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //审批的时候可以编辑

            //编辑
            BarEdit.Visibility = !isEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarEdit.Enabled = DataSource != null
                && ((DataSource.ApprovalStat == 0 && RoleLevel == 0) || (DataSource.ApprovalStat == 1 && DataSource.ExecuteStat == RoleLevel));
            //取消
            BarCancel.Visibility = isEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarCancel.Enabled = isEdit && DataSource != null && !DataSource.Fixed;
            //保存
            BarSave.Visibility = isEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarSave.Enabled = isEdit && DataSource != null && !DataSource.Fixed;
            //发布
            BarFix.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;/* && !isEdit && DataSource != null && !String.IsNullOrEmpty(DataSource.PrjamountNo) && !DataSource.Fixed
                ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;*/
            BarFix.Enabled = false;// && !isEdit && DataSource != null && !String.IsNullOrEmpty(DataSource.PrjamountNo) && !DataSource.Fixed;
            //撤销发布
            BarUnFix.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;/* && !isEdit && BarFix.Visibility == DevExpress.XtraBars.BarItemVisibility.Never
                ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;*/
            BarUnFix.Enabled = false;// && !isEdit && DataSource != null && !String.IsNullOrEmpty(DataSource.PrjamountNo) && DataSource.Fixed;

            tlConstruction.OptionsBehavior.Editable = isEdit;
            tlOther.OptionsBehavior.Editable = isEdit;
            //办理
            BarWork.Visibility = RoleLevel == 0 ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            BarWork.Enabled = !isEdit && DataSource != null && !String.IsNullOrEmpty(DataSource.PrjamountNo) 
                && (DataSource.ExecuteStat == 0 || DataSource.ExecuteStat != 0 && DataSource.ApprovalStat == 0);
            //流程查看
            BarWfInfo.Enabled = !isEdit && DataSource != null && !String.IsNullOrEmpty(DataSource.PrjamountNo);
            //查看计量报表
            viewReportBtn.Enabled = !isEdit;
            //只查看本期申报
            barOnlyApplyed.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //barOnlyApplyed.Enabled = isEdit;

            BarInport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            BarInport.Enabled = isEdit;// && BarEdit.Enabled;

            //同意、退回
            barAgree.Visibility = !isEdit && DataSource != null && DataSource.ApprovalStat == 1 && DataSource.ExecuteStat == RoleLevel ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            barReturn.Visibility = !isEdit && DataSource != null && DataSource.ApprovalStat == 1 && DataSource.ExecuteStat == RoleLevel ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            barReturn.Enabled = barAgree.Enabled = !isEdit && DataSource != null && DataSource.ApprovalStat == 1 && DataSource.ExecuteStat == RoleLevel;


            //区分展示监理-业主-项目的数量和金额    RoleLevel
            switch (LoginRoleType)
            {
                case 1:
                    if (DataSource.ExecuteStat < 2)    //新增且未上报时，不显示后级
                    {
                        SupervisionFinishQty.Visible = false;
                        SupervisionFinishAmount.Visible = false;
                        OwnerFinishQty.Visible = false;
                        OwnerFinishAmount.Visible = false;
                    }
                    else if (DataSource.ExecuteStat == 2)
                    {
                        SupervisionFinishQty.Visible = true;
                        SupervisionFinishAmount.Visible = true;
                    }
                    else if (DataSource.ExecuteStat > 2)
                    {
                        SupervisionFinishQty.Visible = true;
                        SupervisionFinishAmount.Visible = true;
                        OwnerFinishQty.Visible = true;
                        OwnerFinishAmount.Visible = true;
                    }
                    break;
                case 2:
                    if (DataSource.ExecuteStat < 3)
                    {
                        OwnerFinishQty.Visible = false;
                        OwnerFinishAmount.Visible = false;
                    }
                    else
                    {
                        OwnerFinishQty.Visible = true;
                        OwnerFinishAmount.Visible = true;
                    }
                    break;
                case 3:
                    break;
            }
            colApplyQty.OptionsColumn.AllowEdit = LoginRoleType == 1 && DataSource.ApprovalStat == 0;  //标段管理员只能修改标段列的数值，且通过后不可修改
            if (SupervisionFinishQty.Visible)
            {
                SupervisionFinishQty.VisibleIndex = 8;
                SupervisionFinishQty.OptionsColumn.AllowEdit = LoginRoleType == 2 
                    && DataSource.ExecuteStat == 2 && (DataSource.ApprovalStat == 0 || DataSource.ApprovalStat == 1);  //监理管理员只能修改监理列的数值，且通过后不可修改
            }
            if (SupervisionFinishAmount.Visible)
            {
                SupervisionFinishAmount.VisibleIndex = 9;
            }
            if(OwnerFinishQty.Visible)
            {
                OwnerFinishQty.VisibleIndex = 10;
                OwnerFinishQty.OptionsColumn.AllowEdit = LoginRoleType == 3 
                    && DataSource.ExecuteStat == 3 && (DataSource.ApprovalStat == 0 || DataSource.ApprovalStat == 1);  //业主管理员只能修改业主列的数值，且通过后不可修改
            }
            if (OwnerFinishAmount.Visible)
            {
                OwnerFinishAmount.VisibleIndex = 11;
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable taskTable = null;
            if(LoginRoleType != 1)
            {
                if (!JudgeUserIsApprover(out taskTable))
                {
                    XtraMessageBox.Show("未找到审批任务，您可能不是当前项目该阶段的审批人。");
                    return;
                }
                DataSource.SetDefaultValue(LoginRoleType); //设置默认值
            }

            isEdit = true;
            tlConstruction.RefreshDataSource();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
            isEdit = false;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bsPrjAmount.EndEdit();
            tlConstruction.PostEditor();
            tlOther.PostEditor();
            if (!Norm.Validate())
            {
                XtraMessageBox.Show("验证不通过");
                return;
            }
            if (!this.Validate())
            {
                XtraMessageBox.Show("验证不通过");
                return;
            }
            //2019-08-13 加上附件必传   2019-09-03 去掉限制 
            //if (DataSource.LstAmountOther!=null && DataSource.LstAmountOther.Count>0 && DataSource.LstAmountOther.Exists(x=>ObjectHelper.GetDefaultDecimal(x.Amount)>0)
            //    && attachmentControl.Data.Details.Count == 0)
            //{
            //    XtraMessageBox.Show("请先上传附件再保存");
            //    return;
            //}
            //if (attachmentControl3.Data.Details.Count == 0)
            //{
            //    XtraMessageBox.Show("请先上传必传附件2再保存");
            //    return;
            //}
            SaveApplyQtyAndAmount();
            changedPrjAmountDetail.Clear();
            changedPrjAmountWBS.Clear();
            changedPrjAmountOther.Clear();
            //取出所有被修改的detail清单+WBS清单
            if (DataSource.LstAmountDetail != null)
            {
                DataSource.LstAmountDetail.ForEach(m =>
                {
                    if (m.isChanged)
                    {
                        if (!String.IsNullOrEmpty(m.PrjamountdetailNo))
                        {
                            changedPrjAmountDetail.Add(m);
                        }
                        else
                        {
                            changedPrjAmountWBS.Add(m);
                        }
                    }
                });
            }
            //取出所有被修改的其他清单
            if (DataSource.LstAmountOther != null)
            {
                DataSource.LstAmountOther.ForEach(m =>
                {
                    if (m.isChanged)
                    {
                        changedPrjAmountOther.Add(m);
                    }
                });
            }

            DoWork("保存中，请稍后......", "保存",
                () =>
                {
                    PrjAmount prjAmount = DataSource.Clone();
                    prjAmount.LstAmountDetail = new List<PrjAmountDetail>();
                    prjAmount.LstAmountOther = new List<PrjAmountOther>();
                    client.UpdateAll(prjAmount, changedPrjAmountDetail, changedPrjAmountWBS, changedPrjAmountOther, LoginInfor.LoginName);
                },
                (ex) =>
                {
                    if (ex == null)
                    {
                        attachmentControl.BusinessNo = DataSource.PrjamountNo;
                        attachmentControl.Save();
                        attachmentControl.LoadParameter(DataSource.PrjamountNo);

                        //attachmentControl2.BusinessNo = DataSource.PrjamountNo;
                        //attachmentControl2.Save();
                        //attachmentControl2.LoadParameter(DataSource.PrjamountNo);

                        //attachmentControl3.BusinessNo = DataSource.PrjamountNo;
                        //attachmentControl3.Save();
                        //attachmentControl3.LoadParameter(DataSource.PrjamountNo);
                        isEdit = false;
                        LoadData();

                        if (mainHandler != null)
                        {
                            mainHandler.LoadData();
                        }
                    }
                });
        }
        /// <summary>
        /// 同意或退回等操作调用的保存方法
        /// </summary>
        /// <returns></returns>
        public PrjAmount ReSave()
        {
            PrjAmount result = null;
            SaveApplyQtyAndAmount();
            changedPrjAmountWBS.Clear();
            changedPrjAmountDetail.Clear();
            changedPrjAmountOther.Clear();
            //取出所有被修改的detail清单+WBS清单
            if (DataSource.LstAmountDetail != null)
            {
                DataSource.LstAmountDetail.ForEach(m =>
                {
                    if (m.isChanged)
                    {
                        if (!String.IsNullOrEmpty(m.PrjamountdetailNo))
                        {
                            changedPrjAmountDetail.Add(m);
                        }
                        else
                        {
                            changedPrjAmountWBS.Add(m);
                        }
                    }
                });
            }
            //取出所有被修改的其他清单
            if (DataSource.LstAmountOther != null)
            {
                DataSource.LstAmountOther.ForEach(m =>
                {
                    if (m.isChanged)
                    {
                        changedPrjAmountOther.Add(m);
                    }
                });
            }
            if (changedPrjAmountOther.Count + changedPrjAmountWBS.Count + changedPrjAmountDetail.Count > 0)
            {
                PrjAmount prjAmount = DataSource.Clone();
                prjAmount.LstAmountDetail = new List<PrjAmountDetail>();
                prjAmount.LstAmountOther = new List<PrjAmountOther>();
                client.UpdateAll(prjAmount, changedPrjAmountDetail, changedPrjAmountWBS, changedPrjAmountOther, LoginInfor.LoginName);
                client.Commit(prjAmount.PrjamountNo, LoginInfor.LoginName);
                result = client.Get(prjAmount.PrjamountNo);
            }
            return result;
        }

        /// <summary>
        /// 保存时，如果实际值和当前阶段的人对应值不相同，则更新实际值
        /// </summary>
        private void SaveApplyQtyAndAmount()
        {
            if (LoginRoleType == 1)
            {
                foreach (PrjAmountDetail temp in DataSource.LstAmountDetail)
                {
                    if (temp.PrjApplyQty != temp.ApplyQty)
                    {
                        temp.ApplyQty = temp.PrjApplyQty;
                        temp.ApplyAmount = temp.PrjApplyAmount;
                        temp.EndingApplyQty = temp.EndingPrjApplyQty;
                        temp.EndingApplyAmount = temp.EndingPrjApplyAmount;
                        temp.isChanged = true;
                    }
                    //if (DataSource.ExecuteStat == 0)  //设置监理默认值
                    //{
                    //    if (temp.PrjApplyQty != temp.SupervisionQty || temp.PrjApplyAmount != temp.SupervisionAmount)
                    //    {
                    //        temp.SupervisionQty = temp.PrjApplyQty;
                    //        temp.SupervisionAmount = temp.PrjApplyAmount;
                    //        temp.StartingSupervisionAmount = temp.StartingApplyAmount;
                    //        temp.EndingSupervisionAmount = temp.EndingPrjApplyAmount;
                    //        temp.EndingSupervisionQty = temp.EndingPrjApplyQty;
                    //        temp.isChanged = true;
                    //    }
                    //}
                }
            }
            if (LoginRoleType == 2) 
            {
                foreach (PrjAmountDetail temp in DataSource.LstAmountDetail)
                {
                    if (temp.SupervisionQty != temp.ApplyQty)
                    {
                        temp.ApplyQty = temp.SupervisionQty;
                        temp.ApplyAmount = temp.SupervisionAmount;
                        temp.EndingApplyQty = temp.EndingSupervisionQty;
                        temp.EndingApplyAmount = temp.EndingSupervisionAmount;
                        temp.isChanged = true;
                    }
                    //if (DataSource.ExecuteStat == 2) //设置业主默认值
                    //{
                    //    if (temp.OwnerQty != temp.SupervisionQty || temp.SupervisionAmount != temp.OwnerAmount)
                    //    {
                    //        temp.OwnerQty = temp.SupervisionQty;
                    //        temp.OwnerAmount = temp.SupervisionAmount;
                    //        temp.EndingOwnerAmount = temp.EndingSupervisionAmount;
                    //        temp.EndingOwnerQty = temp.EndingSupervisionQty;
                    //        temp.isChanged = true;
                    //    }
                    //}
                }
            }
            if (LoginRoleType == 3)
            {
                foreach (PrjAmountDetail temp in DataSource.LstAmountDetail)
                {
                    if (temp.OwnerQty != temp.ApplyQty)
                    {
                        temp.ApplyQty = temp.OwnerQty;
                        temp.ApplyAmount = temp.OwnerAmount;
                        temp.EndingApplyQty = temp.EndingOwnerQty;
                        temp.EndingApplyAmount = temp.EndingOwnerAmount;
                        temp.isChanged = true;
                    }
                }
            }
            List<PrjAmountDetail> lstPrjAmountDetail = DataSource.LstAmountDetail.FindAll(m => !String.IsNullOrEmpty(m.PrjamountdetailNo));
            Dictionary<String, Decimal> dicAmount = new Dictionary<string, decimal>();
            lstPrjAmountDetail.ForEach(m =>
            {
                Decimal amount = 0;
                if (!dicAmount.TryGetValue(m.WbsParentCode, out amount))
                {
                    dicAmount.Add(m.WbsParentCode, ObjectHelper.GetDefaultDecimal(m.ApplyAmount));
                }
                else
                {
                    dicAmount[m.WbsParentCode] = dicAmount[m.WbsParentCode] + ObjectHelper.GetDefaultDecimal(m.ApplyAmount);
                }
            });
            foreach (String key in dicAmount.Keys)
            {
                PrjAmountDetail prjAmount = DataSource.LstAmountDetail.Find(m => m.WbsSysCode == key);
                if (ObjectHelper.GetDefaultDecimal(prjAmount.ApplyAmount) != ObjectHelper.GetDefaultDecimal(dicAmount[key]))
                {
                    prjAmount.ApplyAmount = dicAmount[key];
                    prjAmount.isChanged = true;
                }
            }
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarFix_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (client.hasUnFixed(DataSource.ProjectNo, DataSource.Periods))
            {
                XtraMessageBox.Show("发布本期前请先发布上期未发布数据");
                return;
            }
            DoWork("发布中，请稍后......", "操作数据中",
                () =>
                {
                    client.Release(DataSource.PrjamountNo, LoginInfor.UserName, LoginInfor.LoginName);
                },
                (ex) =>
                {
                    if (ex == null)
                    {
                        LoadData();
                        if (mainHandler != null)
                        {
                            mainHandler.LoadData();
                        }
                    }
                });
        }

        /// <summary>
        /// 取消发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarUnFix_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (client.hasFixed(DataSource.ProjectNo, DataSource.Periods))
            {
                XtraMessageBox.Show("撤销发布前请先撤销发布后期数据");
                return;
            }
            DoWork("发布中，请稍后......", "操作数据中", () =>
               {
                   client.UnFix(DataSource.PrjamountNo, LoginInfor.LoginName);
               },
               (ex) =>
               {
                   if (ex == null)
                   {
                       LoadData();
                       if (mainHandler != null)
                       {
                           mainHandler.LoadData();
                       }
                   }
               });
        }

        /// <summary>
        /// 重新读取数据
        /// </summary>
        private void LoadData()
        {
            DoWorkRun("读取数据中，请稍候......", "读取中",
                () =>
                {
                    PrjAmount result = new PrjAmount();
                    result = client.Get(DataSource.PrjamountNo);
                    return result;
                },
                (result, ex) =>
                {
                    if (ex == null)
                    {
                        DataSource = (result as PrjAmount);
                        tlConstruction.ExpandAll();
                        tlOther.ExpandAll();
                    }
                });
        }

        /// <summary>
        /// 当（数量）单元格内容更改时，计算金额等数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlConstruction_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            PrjAmountDetail changedNode = tlConstruction.GetDataRecordByNode(e.Node) as PrjAmountDetail;
            if (changedNode != null)
            {
                changedNode.isChanged = true;

                if (e.Column == colApplyQty)    //申报数量改变 中式四舍五入 Math.Round(4.5, 0, MidpointRounding.AwayFromZero) 5
                {
                    changedNode.PrjApplyAmount = Math.Round(ObjectHelper.GetDefaultDecimal(e.Value) * ObjectHelper.GetDefaultDecimal(changedNode.CtrctPrjPrice), 0, MidpointRounding.AwayFromZero);
                    changedNode.EndingPrjApplyQty = ObjectHelper.GetDefaultDecimal(changedNode.StartingApplyQty) + ObjectHelper.GetDefaultDecimal(e.Value);
                    changedNode.EndingPrjApplyAmount = ObjectHelper.GetDefaultDecimal(changedNode.StartingApplyAmount) + ObjectHelper.GetDefaultDecimal(changedNode.PrjApplyAmount);
                    CalculateAmountEx(e.Node, 1);
                }
                else if (e.Column == SupervisionFinishQty)    //监理完成数量改变
                {
                    changedNode.SupervisionAmount = Math.Round(ObjectHelper.GetDefaultDecimal(e.Value) * ObjectHelper.GetDefaultDecimal(changedNode.CtrctPrjPrice), 0, MidpointRounding.AwayFromZero);
                    changedNode.EndingSupervisionQty = ObjectHelper.GetDefaultDecimal(changedNode.StartingApplyQty) + ObjectHelper.GetDefaultDecimal(e.Value);
                    changedNode.EndingSupervisionAmount = ObjectHelper.GetDefaultDecimal(changedNode.StartingApplyAmount) + ObjectHelper.GetDefaultDecimal(changedNode.SupervisionAmount);
                    CalculateAmountEx(e.Node, 2);
                }
                else if (e.Column == OwnerFinishQty)  //业主完成数量改变，同时修改本期完成金额、至本期完成金额、至本期完成数量
                {
                    changedNode.OwnerAmount = Math.Round(ObjectHelper.GetDefaultDecimal(e.Value) * ObjectHelper.GetDefaultDecimal(changedNode.CtrctPrjPrice), 0, MidpointRounding.AwayFromZero);
                    changedNode.EndingOwnerQty = ObjectHelper.GetDefaultDecimal(changedNode.StartingApplyQty) + ObjectHelper.GetDefaultDecimal(e.Value);
                    changedNode.EndingOwnerAmount = ObjectHelper.GetDefaultDecimal(changedNode.StartingApplyAmount) + ObjectHelper.GetDefaultDecimal(changedNode.OwnerAmount);
                    CalculateAmountEx(e.Node, 3);
                }
            }
        }

        /// <summary>
        /// 计算一级以上的父节点的金额——施工计量
        /// </summary>
        /// <param name="node"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private void CalculateAmountEx(TreeListNode node, int changeType)
        {
            if (node.ParentNode != null)
            {
                PrjAmountDetail parentNode = tlConstruction.GetDataRecordByNode(node.ParentNode) as PrjAmountDetail;
                parentNode.isChanged = true;
                switch (changeType)
                {
                    case 1:
                        parentNode.PrjApplyAmount = 0;
                        parentNode.StartingApplyAmount = 0;
                        for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                        {
                            PrjAmountDetail childNode = tlConstruction.GetDataRecordByNode(node.ParentNode.Nodes[i]) as PrjAmountDetail;
                            parentNode.PrjApplyAmount += (childNode.PrjApplyAmount ?? 0);
                            parentNode.StartingApplyAmount += (childNode.StartingApplyAmount ?? 0);
                        }
                        break;
                    case 2:
                        parentNode.SupervisionAmount = 0;
                        parentNode.StartingApplyAmount = 0;
                        for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                        {
                            PrjAmountDetail childNode = tlConstruction.GetDataRecordByNode(node.ParentNode.Nodes[i]) as PrjAmountDetail;
                            parentNode.SupervisionAmount += (childNode.SupervisionAmount ?? 0);
                            parentNode.StartingApplyAmount += (childNode.StartingApplyAmount ?? 0);
                        }
                        break;
                    case 3:
                        parentNode.OwnerAmount = 0;
                        parentNode.StartingApplyAmount = 0;
                        for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                        {
                            PrjAmountDetail childNode = tlConstruction.GetDataRecordByNode(node.ParentNode.Nodes[i]) as PrjAmountDetail;
                            parentNode.OwnerAmount += (childNode.OwnerAmount ?? 0);
                            parentNode.StartingApplyAmount += (childNode.StartingApplyAmount ?? 0);
                        }
                        break;
                }
                CalculateAmountEx(node.ParentNode, changeType);
            }
        }

        /// <summary>
        /// 其他计量——ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlOther_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            PrjAmountOther changedNode = tlOther.GetDataRecordByNode(e.Node) as PrjAmountOther;
            if (changedNode != null)
            {
                changedNode.isChanged = true;

                if (e.Column == colAmount)
                {
                    changedNode.EndingAmount = ObjectHelper.GetDefaultDecimal(changedNode.StartingAmount) + ObjectHelper.GetDefaultDecimal(e.Value);
                    Other_CalculateAmount(e.Node);
                }
            }
        }

        private void Other_CalculateAmount(TreeListNode node)
        {
            if (node.ParentNode != null)
            {
                PrjAmountOther parentNode = tlOther.GetDataRecordByNode(node.ParentNode) as PrjAmountOther;
                parentNode.isChanged = true;
                parentNode.Amount = 0;
                parentNode.StartingAmount = 0;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    PrjAmountOther childNode = tlOther.GetDataRecordByNode(node.ParentNode.Nodes[i]) as PrjAmountOther;
                    parentNode.Amount += (childNode.Amount ?? 0);
                    parentNode.StartingAmount += (childNode.StartingAmount ?? 0);
                }
                Other_CalculateAmount(node.ParentNode);
            }
        }

        /// <summary>
        /// 判断只有特定行才能填写-中间交工证书号要在最后一项wbs清单项才能编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlConstruction_ShowingEditor(object sender, CancelEventArgs e)
        {
            PrjAmountDetail node = tlConstruction.GetDataRecordByNode(tlConstruction.FocusedNode) as PrjAmountDetail;
            if (String.IsNullOrEmpty(node.PrjamountdetailNo))
            {
                if (tlConstruction.FocusedColumn.FieldName == "MidCertifiNum")
                {
                    if (tlConstruction.FocusedNode.HasChildren)
                    {
                        if (String.IsNullOrEmpty((tlConstruction.GetDataRecordByNode(tlConstruction.FocusedNode.Nodes[0]) as PrjAmountDetail).PrjamountdetailNo))
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                        e.Cancel = true;
                }
                else
                    e.Cancel = true;
            }
            else if (tlConstruction.FocusedColumn.FieldName == "MidCertifiNum")
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 判断只有特定行才能填写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlOther_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tlOther.FocusedNode.HasChildren)
            {
                e.Cancel = true;
            }
        }

        private void PMDetailContainer_Load(object sender, EventArgs e)
        {
            this.ParentForm.Shown += ParentForm_Shown;
        }

        void ParentForm_Shown(object sender, EventArgs e)
        {
            tlConstruction.ExpandAll();
            tlOther.ExpandAll();

        }

        /// <summary>
        /// 查看流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarWfInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wfinfoviewer wfForm = new wfinfoviewer();
            wfForm.SysNo = AppConfig.SYSTEM_NO;
            wfForm.RefDataNo = DataSource.PrjamountNo;
            wfForm.GetWfInfo();
            wfForm.ShowDialog("流程信息查看");
        }

        /// <summary>
        /// 施工计量-展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlConstruction.ExpandAll();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlConstruction.CollapseAll();
        }

        /// <summary>
        /// 其他计量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlOther.ExpandAll();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tlOther.CollapseAll();
        }


        /// <summary>
        /// 查看报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewReportBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!String.IsNullOrEmpty(DataSource.PrjamountNo))
            {
                DoWorkRun("读取数据中,请稍候......", "读取数据",
                   () =>
                   {
                       PrjAmountRpt result = new PrjAmountRpt();
                       result = prjAmountRptClient.Get(DataSource.PrjamountNo);
                       return result;
                   },
                   (result, ex) =>
                   {
                       if (ex == null && result != null)
                       {
                           MeteringRptDetailControl form = new MeteringRptDetailControl();
                           form.DataSource = result as PrjAmountRpt;
                           AppForm.CurrentForm.ChangeForm(String.Format("项目：{0}-第{1}期报表", AppConfig.SelectProject.ProjectName, (result as PrjAmountRpt).Periods ?? 0), form);
                       }
                   });
            }
        }

        /// <summary>
        /// 提交审批
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarSubmit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //if(DataSource.ApprovalStat == 1)    //只有在项目部第一次提交审批的时候才需要对监理的本期完成进行初始化
            //    updateSuperOrOwnerData();
            if (!isEdit)
            {
                //if (attachmentControl2.Data.Details.Count == 0)
                //{
                //    XtraMessageBox.Show("请先上传必传附件1再提交办理");
                //    return;
                //}
                //if (attachmentControl3.Data.Details.Count == 0)
                //{
                //    XtraMessageBox.Show("请先上传必传附件2再提交办理");
                //    return;
                //}

                DoWork("提交审批中，请稍候......", "提交审批",
                    () =>
                    {
                        //client.updateAmountAndQty(DataSource.LstAmountDetail, LoginInfor.LoginName);
                        if (!DataSource.Fixed)
                        {
                            DataSource.Version = client.Commit(DataSource.PrjamountNo, LoginInfor.LoginName);
                        }

                        WfRefNoName entity = new WfRefNoName();
                        entity.RefNo = DataSource.PrjamountNo;
                        entity.RefName = DataSource.PrjamountName;
                        List<CacheOrg> loginOrg = new MeteringPaymentClient().GetIManagerService().FindOrgByLoginName(LoginInfor.LoginName);
                        string OrgNo = null;
                        if (loginOrg != null && loginOrg[0] != null) //取当前登陆人默认机构
                        {
                            OrgNo = loginOrg[0].OrgNo;
                        }
                        string strBusCatalogNo = string.Format("{0}_{1}", DataSource.ProjectNo, AppConfig.SYSTEM_NO);
                        GpClientWorkflowClient.CreateInstance().WfSeqTask_StartByMenuNo(strBusCatalogNo, "2208000000000000418", AppConfig.SYSTEM_NO
                            , AppConfig.WfMenuNo_MeteringSubmit, LoginInfor.LoginName, OrgNo, entity, 5, false, 0, null, false, Convert.ToInt32(AppConfig.CurrentLoginUser.LoginRole));
                        PrjAmount tempAmount = DataSource.Clone();
                        tempAmount.LstAmountDetail = new List<PrjAmountDetail>();
                        tempAmount.LstAmountOther = new List<PrjAmountOther>();
                        tempAmount.ApprovalStat = 1;
                        if (tempAmount.ExecuteStat == 0)
                        {
                            tempAmount.ExecuteStat = 1;
                            tempAmount.ShowStat = "承包单位-待审";
                        }
                        else
                        {
                            switch (tempAmount.ShowStat)
                            {
                                case PrjamountShowStat.专业监理工程师2: tempAmount.ShowStat = PrjamountShowStat.专业监理工程师1; break;
                                case PrjamountShowStat.合同监理工程师2: tempAmount.ShowStat = PrjamountShowStat.合同监理工程师1; break;
                                case PrjamountShowStat.总监2: tempAmount.ShowStat = PrjamountShowStat.总监1; break;
                                case PrjamountShowStat.管理处专业工程师2: tempAmount.ShowStat = PrjamountShowStat.管理处专业工程师1; break;
                                case PrjamountShowStat.管理处主任2: tempAmount.ShowStat = PrjamountShowStat.管理处主任1; break;
                                case PrjamountShowStat.业主计量工程师2: tempAmount.ShowStat = PrjamountShowStat.业主计量工程师1; break;
                                case PrjamountShowStat.业主合约部经理2: tempAmount.ShowStat = PrjamountShowStat.业主合约部经理1; break;
                                case PrjamountShowStat.业主生产副总经理2: tempAmount.ShowStat = PrjamountShowStat.业主生产副总经理1; break;
                                case PrjamountShowStat.业主总经理2: tempAmount.ShowStat = PrjamountShowStat.业主总经理1; break;
                            }
                        }
                        client.ChangeBusState(tempAmount, LoginInfor.LoginName);
                        DataSource.ApprovalStat = tempAmount.ApprovalStat;
                        DataSource.ExecuteStat = tempAmount.ExecuteStat;
                    },
                    (ex) =>
                    {
                        if (ex == null)
                        {
                            //if (LoginRoleType == 1)
                            //{
                            //    if (DataSource.ApprovalStat == 1)   //第一次上报
                            //        DataSource.ApprovalStat = 11;
                            //    else if (DataSource.ApprovalStat != 1)  //非第一次上报
                            //        DataSource.ApprovalStat = 13;
                            //    DataSource.ExecuteStat = 2;
                            //}
                            //client.ChangeBusState(DataSource, LoginInfor.LoginName);
                            XtraMessageBox.Show("提交审批成功");
                            RefreshStat();
                            // LoadData();
                        }
                    });
            }
        }

        /// <summary>
        /// 当项目部初次提交审批或者监理初次同意的时候，更新监理或者业主的本期完成数量和金额
        /// </summary>
        private void updateSuperOrOwnerData()
        {
            if (LoginRoleType == 1)
                foreach (PrjAmountDetail temp in DataSource.LstAmountDetail)
                {
                    temp.EndingSupervisionQty = temp.EndingPrjApplyQty;
                    temp.EndingSupervisionAmount = temp.EndingPrjApplyAmount;
                    temp.SupervisionQty = temp.PrjApplyQty;
                    temp.SupervisionAmount = temp.PrjApplyAmount;
                }
            if (LoginRoleType == 2)
                foreach (PrjAmountDetail temp in DataSource.LstAmountDetail)
                {
                    temp.EndingOwnerQty = temp.EndingPrjApplyQty;
                    temp.EndingOwnerAmount = temp.EndingPrjApplyAmount;
                    temp.OwnerQty = temp.PrjApplyQty;
                    temp.OwnerAmount = temp.PrjApplyAmount;
                }
        }

        /// <summary>
        /// 同意
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAgree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (DataSource.ApprovalStat == 11)    //只有在项目部第一次提交审批的时候才需要对监理的本期完成进行初始化
            //    updateSuperOrOwnerData();
            AgreeOrReturn(true);
        }

        /// <summary>
        /// 退回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgreeOrReturn(false);
        }

        private bool JudgeUserIsApprover(out DataTable wfTaskTable)
        {
            bool result = false;
            String MenuItemNo = "";
            if (LoginRoleType == 2)
            {
                MenuItemNo = "2208000000000000562";
            }
            else if (LoginRoleType == 3)
            {
                MenuItemNo = "2208000000000000420";
            }
            wfTaskTable = Erp.GpServiceClient.GpClient.CreateInstance().Execute(String.Format(@"SELECT Top 1 * FROM ERP_BPM.dbo.vWfTask 
                                                WHERE RefNo='{0}' and Holder='{1}' and MenuItemNo='{2}' and WfTaskStatNo='A'  and InstMenuNo='{3}' Order By CreateDate Desc"
                                                , DataSource.PrjamountNo, LoginInfor.LoginName, MenuItemNo, AppConfig.WfMenuNo_MeteringSubmit)
                                                , CommandType.Text).Table;

            if (wfTaskTable != null && wfTaskTable.Rows.Count == 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 同意或者退回
        /// </summary>
        /// <param name="isAgree"></param>
        private void AgreeOrReturn(bool isAgree)
        {
            DataTable taskTable = null;
            if (!JudgeUserIsApprover(out taskTable))
            {
                XtraMessageBox.Show("未找到审批任务，您可能不是当前项目该阶段的审批人。");
                return;
            }

            String optionNote = isAgree ? "同意" : "退回";
            DoWork("提交数据中，请稍候......", optionNote,
                    () =>
                    {
                        //PrjAmount temPrjAmount = ReSave();
                        //if (temPrjAmount == null)
                        //{
                        //    temPrjAmount = DataSource;
                        //}
                        PrjAmount temPrjAmount = DataSource;

                        int version = ObjectHelper.GetDefaultInt32(DataSource.Version);
                        // client.updateAmountAndQty(DataSource.LstAmountDetail, LoginInfor.LoginName);
                        if (!temPrjAmount.Locked)
                        {
                            version = client.Commit(temPrjAmount.PrjamountNo, LoginInfor.LoginName);
                        }
                        WfRefNoName entity = new WfRefNoName();
                        entity.RefNo = temPrjAmount.PrjamountNo;
                        entity.RefName = temPrjAmount.PrjamountName;
                        GpClientWorkflowClient.CreateInstance().CompleteTask(LoginInfor.LoginName, (int)taskTable.Rows[0]["Id"], "", isAgree, optionNote, version);
                    },
                    (ex) =>
                    {
                        if (ex == null)
                        {
                            XtraMessageBox.Show("提交审批成功，需要重新读取数据。");
                            LoadData();
                        }
                    });
        }

        private void tlConstruction_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column == null)
                return;
            #region 设置可编辑单元格的背景颜色
            if (LoginRoleType == 1)
            {
                if (e.Column.FieldName == "PrjApplyQty")
                {
                    PrjAmountDetail detail = tlConstruction.GetDataRecordByNode(e.Node) as PrjAmountDetail;
                    if (detail != null)
                        if (!String.IsNullOrEmpty(detail.PrjamountdetailNo) && isEdit)
                        {
                            e.Appearance.BackColor = Color.Cornsilk;
                        }
                }
            }
            else if (LoginRoleType == 2)
            {
                if (e.Column.FieldName == "SupervisionQty")
                {
                    PrjAmountDetail detail = tlConstruction.GetDataRecordByNode(e.Node) as PrjAmountDetail;
                    if (detail != null)
                        if (!String.IsNullOrEmpty(detail.PrjamountdetailNo) && isEdit)
                        {
                            e.Appearance.BackColor = Color.Cornsilk;
                        }
                }
            }
            else if (LoginRoleType == 3)
            {
                if (e.Column.FieldName == "OwnerQty")
                {
                    PrjAmountDetail detail = tlConstruction.GetDataRecordByNode(e.Node) as PrjAmountDetail;
                    if (detail != null)
                        if (!String.IsNullOrEmpty(detail.PrjamountdetailNo) && isEdit)
                        {
                            e.Appearance.BackColor = Color.Cornsilk;
                        }
                }
            }
            //设置中间交工证书号——Style
            if (e.Column.FieldName == "MidCertifiNum")
            {
                if (e.Node.HasChildren)
                {
                    if (!String.IsNullOrEmpty((tlConstruction.GetDataRecordByNode(e.Node.Nodes[0]) as PrjAmountDetail).PrjamountdetailNo) && isEdit)
                    {
                        e.Appearance.BackColor = Color.Cornsilk;
                    }
                }
            }
            //监理、业主修改数据后，显示为红色
            if (LoginRoleType != 1)
            {
                PrjAmountDetail node = tlConstruction.GetDataRecordByNode(e.Node) as PrjAmountDetail;
                if (node != null)
                {
                    Nullable<decimal> Qty1 = node.PrjApplyQty;
                    Nullable<decimal> Qty2 = node.SupervisionQty;
                    if (LoginRoleType == 3)
                    {
                        Qty1 = node.SupervisionQty;
                        Qty2 = node.OwnerQty;
                    }
                    if (Qty1 != Qty2)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
            #endregion 设置可编辑单元格的背景颜色
        }

        /// <summary>
        /// 只查看已申报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barOnlyApplyed_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barOnlyApplyed.Checked)
                tlConstruction.ActiveFilterString = String.Format(@"PrjApplyQty is not null");
            else
            {
                tlConstruction.ActiveFilterString = "";
            }
        }

        /// <summary>
        /// 验证所填数据是否符合累计完成数量+本期申报 <= 合同数量
        /// </summary>
        private void repositoryItemCalcEdit_Validating(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarInport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Dictionary<String, PrjAmountDetail> dicDetail = new Dictionary<string, PrjAmountDetail>();
                Dictionary<String, PrjAmountDetail> dicWbs = new Dictionary<string, PrjAmountDetail>();
                //导入之前，先取出所有的清单项和WBS项
                DataSource.LstAmountDetail.ForEach(m =>
                {
                    if (!String.IsNullOrEmpty(m.PrjamountdetailNo))
                    {
                        if (!dicDetail.ContainsKey(m.WbsSysCode))
                        {
                            dicDetail.Add(m.WbsSysCode, m);
                        }
                    }
                    else
                    {
                        TreeListNode node = tlConstruction.FindNodeByFieldValue("WbsSysCode", m.WbsSysCode);
                        if (node != null)
                        {
                            if (node.HasChildren)
                            {
                                if (!String.IsNullOrEmpty((tlConstruction.GetDataRecordByNode(node.Nodes[0]) as PrjAmountDetail).PrjamountdetailNo))
                                {
                                    if(!dicWbs.ContainsKey(m.WbsSysCode))
                                    {
                                        dicWbs.Add(m.WbsSysCode, m);
                                    }
                                }
                            }
                        }
                    }
                });
                DataSet ds = ExeclOperation.ToDataTable(openFileDialog1.FileName);
                try
                {
                    PMExport.mainHandler = this;
                    PMExport.ImportWBS_Details(ds.Tables[0], dicDetail, dicWbs, LoginRoleType);
                    XtraMessageBox.Show("数据成功导入到程序中，保存后生效！");
                    tlConstruction.Refresh();
                }
                catch (BusinessException be)
                {
                    XtraMessageBox.Show(be.BusMessage);
                }
            }
        }

        public void Inport_CellValueChange(String sysCode, object val)
        {
            CellValueChangedEventArgs e1;
            if(LoginRoleType == 1)
                e1 = new CellValueChangedEventArgs(colApplyQty, tlConstruction.FindNodeByFieldValue("WbsSysCode", sysCode), val);
            else if (LoginRoleType == 2)
                e1 = new CellValueChangedEventArgs(SupervisionFinishQty, tlConstruction.FindNodeByFieldValue("WbsSysCode", sysCode), val);
            else
                e1 = new CellValueChangedEventArgs(OwnerFinishQty, tlConstruction.FindNodeByFieldValue("WbsSysCode", sysCode), val);

            this.tlConstruction_CellValueChanged(this, e1);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SupervisionFinishQty.Visible = true;
            SupervisionFinishAmount.Visible = true;
            OwnerFinishQty.Visible = true;
            OwnerFinishAmount.Visible = true;
            colWbsSysCode.Visible = true;
            colStartingApplyQty.Visible = false;
            colStartingApplyAmount.Visible = false;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存进度计量文件";
            saveFileDialog.FileName = DataSource.PrjamountName + " - 施工计量";
            saveFileDialog.Filter = "Excel文件|*.xls";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tlConstruction.ExportToXls(saveFileDialog.FileName);
                ExcelHelper.OpenFile(saveFileDialog.FileName);
            }
            //GridExportHelper.ExportExcel(tlConstruction, DataSource.PrjamountName + " - 施工计量");

            colWbsSysCode.Visible = false;
            colStartingApplyQty.Visible = true;
            colStartingApplyAmount.Visible = true; 
            switch (LoginRoleType)
            {
                case 1:
                    if (DataSource.ExecuteStat < 2)    //新增且未上报时，不显示后级
                    {
                        SupervisionFinishQty.Visible = false;
                        SupervisionFinishAmount.Visible = false;
                        OwnerFinishQty.Visible = false;
                        OwnerFinishAmount.Visible = false;
                    }
                    else if (DataSource.ExecuteStat == 2)
                    {
                        SupervisionFinishQty.Visible = true;
                        SupervisionFinishAmount.Visible = true;
                    }
                    else if (DataSource.ExecuteStat > 2)
                    {
                        SupervisionFinishQty.Visible = true;
                        SupervisionFinishAmount.Visible = true;
                        OwnerFinishQty.Visible = true;
                        OwnerFinishAmount.Visible = true;
                    }
                    break;
                case 2:
                    if (DataSource.ExecuteStat < 3)
                    {
                        OwnerFinishQty.Visible = false;
                        OwnerFinishAmount.Visible = false;
                    }
                    else
                    {
                        OwnerFinishQty.Visible = true;
                        OwnerFinishAmount.Visible = true;
                    }
                    break;
                case 3:
                    break;
            }
        }

        /// <summary>
        /// 计算合计值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlConstruction_GetCustomSummaryValue(object sender, GetCustomSummaryValueEventArgs e)
        {
            if (e.Column.FieldName == "PrjApplyAmount")
            {
                decimal value = 0;
                foreach(PrjAmountDetail detail in DataSource.LstAmountDetail)
                {
                    if(!String.IsNullOrEmpty(detail.PrjamountdetailNo))
                    {
                        value += Math.Round(detail.PrjApplyAmount ?? 0, 0, MidpointRounding.AwayFromZero);
                    }
                }
                e.CustomValue = Math.Round(value, 0, MidpointRounding.AwayFromZero).ToString("f0");
            }

            if (e.Column.FieldName == "SupervisionAmount")
            {
                decimal value = 0;
                foreach (PrjAmountDetail detail in DataSource.LstAmountDetail)
                {
                    if (!String.IsNullOrEmpty(detail.PrjamountdetailNo))
                    {
                        value += Math.Round(detail.SupervisionAmount ?? 0, 0, MidpointRounding.AwayFromZero);
                    }
                }
                e.CustomValue = Math.Round(value, 0, MidpointRounding.AwayFromZero).ToString("f0");
            }

            if (e.Column.FieldName == "OwnerAmount")
            {
                decimal value = 0;
                foreach (PrjAmountDetail detail in DataSource.LstAmountDetail)
                {
                    if (!String.IsNullOrEmpty(detail.PrjamountdetailNo))
                    {
                        value += Math.Round(detail.OwnerAmount ?? 0, 0, MidpointRounding.AwayFromZero);
                    }
                }
                e.CustomValue = Math.Round(value, 0, MidpointRounding.AwayFromZero).ToString("f0");
            }
            if (e.Column.FieldName == "StartingApplyAmount")
            {
                decimal value = 0;
                foreach (PrjAmountDetail detail in DataSource.LstAmountDetail)
                {
                    if (!String.IsNullOrEmpty(detail.PrjamountdetailNo))
                    {
                        value += Math.Round(detail.StartingApplyAmount ?? 0, 0, MidpointRounding.AwayFromZero);
                    }
                }
                e.CustomValue = Math.Round(value, 0, MidpointRounding.AwayFromZero).ToString("f0");
            }
        }
        
        /// <summary>
        /// 设置节点图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlConstruction_GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            if (e.Node == null)
                return;
            PrjAmountDetail detail = tlConstruction.GetDataRecordByNode(e.Node) as PrjAmountDetail;
            if (detail == null)
                return;
            if(String.IsNullOrEmpty(detail.PrjamountdetailNo))
            {
                e.NodeImageIndex = 0;
            }
            else
            {
                e.NodeImageIndex = 1;
            }
        }

        private void tlConstruction_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            PrjAmountDetail node = tlConstruction.GetDataRecordByNode(tlConstruction.FocusedNode) as PrjAmountDetail;
            if (node != null
                && (tlConstruction.FocusedColumn == colApplyQty || tlConstruction.FocusedColumn == SupervisionFinishQty || tlConstruction.FocusedColumn == OwnerFinishQty))
            {
                decimal value = 0;
                decimal.TryParse(e.Value != null ? e.Value.ToString() : "0", out value);

                if (value > node.CtrctQty)
                {
                    e.Valid = false;
                    e.ErrorText = "填写数值超过最大限额";
                }
            }
        }

        private void tlConstruction_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            PrjAmountDetail node = tlConstruction.GetDataRecordByNode(tlConstruction.FocusedNode) as PrjAmountDetail;
            if (node == null) return;
            if (e.CellValue == null)
                return;
            decimal value = -1;
            //if (e.Column.FieldName == "PrjApplyQty" || e.Column.FieldName == "SupervisionQty" || e.Column.FieldName == "OwnerQty" )
            //{
            //    e.CellText = node.WbsSysCode.StartsWith("01") ? String.Format("{0:" + (isEdit ? "#,0.########" : "#,0.###") + "}", e.CellValue)
            //        : String.Format("{0:" + (isEdit ? "#,0.###" : "#,0.###") + "}", e.CellValue);
            //}
            if (!isEdit && decimal.TryParse(e.CellValue.ToString(), out value) && value == 0)
            {
                e.CellText = string.Empty;
            }

        }
    }
}
