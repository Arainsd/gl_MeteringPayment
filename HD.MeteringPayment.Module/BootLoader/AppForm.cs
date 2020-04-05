using Erp.SharedLib.Presentation.FormBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTabbedMdi;
using Erp.CommonData;
using Erp.CommonData.Interface;
using Erp.CommonData.Entity;

using DevExpress.XtraEditors;
using System.Diagnostics;

using DevExpress.XtraBars.Docking2010.Views;
using Erp.SharedLib.Presentation.ControlBases;
using DevExpress.XtraNavBar;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.BootLoader.Menu;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using HD.MeteringPayment.Module.Forms.ProjectInfoMng;
using Erp.CommonData.Entity.Business;
using HD.MeteringPayment.Domain.Client;

namespace HD.MeteringPayment.Module.BootLoader
{
    public partial class AppForm : GpFormBase, IMainForm, IGpModule
    {
        public AppForm(IMainModuleForm ff)
            : base(null)
        {
            InitializeComponent();
            this.FormClosing += AppForm_FormClosing;
            CurrentForm = this;
            AppConfig.mmForm = ff;
        }
        //允许关闭标识
        private Boolean _AllowClose = false;
        /// <summary>
        /// 系统编号
        /// </summary>
        public static String MngSystemNo
        {
            get
            {
                return "A0118";
            }
        }

        #region 模块信息
        string IGpModule.Description
        {
            get { return "计量支付管理"; }
        }

        string IGpModule.DisplayName
        {
            get { return "计量支付管理"; }
        }

        bool IGpModule.Enable
        {
            get
            {
                return true;
            }
        }

        string IGpModule.Group
        {
            get { return "业务系统"; }
        }

        Image IGpModule.Icon
        {
            get { return Properties.Resources.产值入库图标; }
        }

        System.Windows.Forms.Form IGpModule.MainForm
        {
            get { return this; }
        }
        #endregion
        public static AppForm CurrentForm;
        void AppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_AllowClose)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show("确认退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    _AllowClose = true;
                    e.Cancel = false;
                    this.Hide();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (!isEntry)
            {
                isEntry = true;
                this.Close();
            }
        }
        private bool isEntry = false;
        public bool AllowClose
        {
            get { return _AllowClose; }
        }
        private string _roleInfo;

        public string RoleInfo
        {
            get { return _roleInfo; }
            set
            {
                _roleInfo = value;
                lbRoleInfo.Caption = value;
            }
        }
        #region 显示窗体
        public void ShowChildForm(System.Windows.Forms.Form _fb)
        {
            _fb.MdiParent = this;
            _fb.Show();
            _fb.Activate();
        }

        public bool ShowChildForm(Guid FormId, params object[] obj)
        {

            foreach (BaseDocument xt in tbbdvwContent.Documents)
            {
                IChildGuidForm instance = xt.Control as IChildGuidForm;
                if (instance == null) continue;
                if (instance.FormId == FormId)
                {
                    bool exist = false;
                    if (instance.Parameter != null && obj != null && instance.Parameter.Length == obj.Length)
                    {
                        int i = 0;
                        for (i = 0; i < instance.Parameter.Length; i++)
                        {
                            exist = true;
                            if (!Object.Equals(instance.Parameter[i], obj[i]))
                            {
                                exist = false;
                                break;
                            }
                        }
                    }
                    if (instance.Parameter == null && obj == null)
                        exist = true;
                    if (exist)
                    {
                        instance.LoadParameter(obj);
                        tbbdvwContent.ActivateDocument(xt.Control);
                        return exist;
                    }

                }
            }
            return false;

        }
        public bool ShowChildForm(Guid FormId)
        {
            return ShowChildForm(FormId, null);
        }
        public void LoadMenu(GpNavBarMenu menu)
        {
            nvbrcntrlLeft.Groups.Clear();
            nvbrcntrlLeft.Items.Clear();
            for (int i = 0; i < menu.Groups.Count; i++)
            {
                NavBarGroup group = new NavBarGroup(menu.Groups[i].Caption);
                nvbrcntrlLeft.Groups.Add(group);
                if (menu.Groups[i].Icon != null)
                    group.SmallImage = menu.Groups[i].Icon;
                for (int j = 0; j < menu.Groups[i].Items.Count; j++)
                {
                    GpNavBarItem gpItem = menu.Groups[i].Items[j];
                    NavBarItem item = new NavBarItem(gpItem.Caption);
                    nvbrcntrlLeft.Items.Add(item);
                    group.ItemLinks.Add(item);
                    if (gpItem.Icon != null)
                        item.SmallImage = gpItem.Icon;
                    item.Tag = gpItem.ItemTag;
                    item.Enabled = gpItem.Enable;
                    item.LinkClicked += delegate(Object sender, NavBarLinkEventArgs e)
                    {
                        if (gpItem.MenuClick != null)
                            gpItem.MenuClick(sender, e, item.Tag);
                    };
                }
            }

        }
        public void LoadTreeMenu(GpNavBarTreeMenu menu)
        {
            nvbrcntrlLeft.Groups.Clear();
            nvbrcntrlLeft.Items.Clear();

            NavBarGroup grpHeader = nvbrcntrlLeft.Groups.Add();
            grpHeader.Caption = menu.Caption;
            grpHeader.GroupStyle = NavBarGroupStyle.ControlContainer;
            TreeList tree = new TreeList();
            tree.RowHeight = 30;
            NavBarGroupControlContainer container = new NavBarGroupControlContainer();
            container.Controls.Add(tree);
            grpHeader.ControlContainer = container;
            container.Padding = new System.Windows.Forms.Padding(4);
            tree.Dock = DockStyle.Fill;
            tree.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            tree.MouseClick += new MouseEventHandler(tree_MouseClick);
            tree.Appearance.SelectedRow.BackColor = Color.FromArgb(53, 153, 255);
            tree.OptionsView.ShowHorzLines = false;
            tree.OptionsView.ShowVertLines = false;
            tree.OptionsBehavior.Editable = false;
            tree.OptionsView.ShowRoot = false;
            tree.OptionsView.ShowColumns = false;
            tree.OptionsView.ShowIndicator = false;
            tree.SelectImageList = imageCollection1;
            TreeListColumn col = tree.Columns.Add();
            col.Visible = true;
            col.Width = 150;
            col.FieldName = "test";
            col.Caption = "test";
            for (int i = 0; i < menu.Items.Count; i++)
            {
                GpNavBarTreeItem item = menu.Items[i];
                TreeListNode childNode = tree.AppendNode(new Object[] { item.Caption }, -1);
                childNode.Tag = item;
                if (item.IsFolder)
                {
                    for (int j = 0; j < item.Items.Count; j++)
                    {
                        LoadItem(childNode, item.Items[j]);
                    }
                }
                if (tree.SelectImageList != null)
                {
                    childNode.ImageIndex = item.IsFolder ? 0 : 1;
                    childNode.SelectImageIndex = item.IsFolder ? 0 : 1;
                }
            }
            tree.ExpandAll();
        }
        void tree_MouseClick(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            TreeListHitInfo hitInfo = tree.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo != null && hitInfo.Node != null)
            {
                GpNavBarTreeItem item = hitInfo.Node.Tag as GpNavBarTreeItem;
                RefreshNodeClick(item);

            }
        }
        void RefreshNodeClick(GpNavBarTreeItem item)
        {
            if (item != null && item.MenuClick != null)
            {
                item.MenuClick(item, "", item.ItemTag);
            }

        }
        private void LoadItem(TreeListNode node, GpNavBarTreeItem item)
        {
            TreeListNode childNode = node.Nodes.Add(new Object[] { item.Caption });
            childNode.Tag = item;
            if (item.IsFolder)
            {
                for (int i = 0; i < item.Items.Count; i++)
                {
                    LoadItem(childNode, item.Items[i]);
                }
            }
            if (childNode.TreeList.SelectImageList != null)
            {
                childNode.ImageIndex = item.IsFolder ? 0 : 1;
                childNode.SelectImageIndex = item.IsFolder ? 0 : 1;
            }

        }

        public void CloseAllTab()
        {
            if (tbbdvwContent.Controller != null)
                tbbdvwContent.Controller.CloseAll();
        }
        public object GetChildForm(Guid FormId, params Object[] arg)
        {
            foreach (BaseDocument xt in tbbdvwContent.Documents)
            {
                IChildGuidForm instance = xt.Control as IChildGuidForm;
                if (instance == null) continue;
                if (instance.FormId == FormId)
                {
                    bool exist = false;
                    if (instance.Parameter != null && arg != null && instance.Parameter.Length == arg.Length)
                    {
                        int i = 0;
                        for (i = 0; i < instance.Parameter.Length; i++)
                        {
                            exist = true;
                            if (!Object.Equals(instance.Parameter[i], arg[i]))
                            {
                                exist = false;
                                break;
                            }
                        }
                    }
                    if (instance.Parameter == null && arg == null)
                        exist = true;
                    if (exist)
                        return xt.Control;
                }
            }
            return null;
        }
        private void AppForm_Shown(object sender, EventArgs e)
        {
            pctrbxMax_Click(sender, e);
            InitUI();
            ResetButtonColor();
        }
        void InitUI()
        {
            //btCompany.Visible = WorkConfig.riCompany.Count > 0 || WorkConfig.riGroup.Count > 0 || PlanConfig.riCompany.Count > 0 || PlanConfig.riGroup.Count > 0;
            //btGroup.Visible = WorkConfig.riGroup.Count > 0 || PlanConfig.riGroup.Count > 0;
            //btBaseInfor.Visible = WorkConfig.riGroup.Count > 0 || PlanConfig.riGroup.Count > 0;
        }
        #endregion
        public void ChangeForm(String caption, GpControlBase ctrl)
        {
            ChangeForm(caption, ctrl, null);
        }
        public void CloseTabPage(Guid FormId)
        {
            foreach (BaseDocument xt in tbbdvwContent.Documents)
            {
                IChildGuidForm instance = xt.Control as IChildGuidForm;
                if (instance == null) continue;
                if (instance.FormId == FormId)
                {
                    tbbdvwContent.Controller.Close(xt);
                    break;
                }
            }
        }
        /// <summary>
        /// 加载带参数tab页或者模块
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="args"></param>
        public void ChangeForm(String caption, GpControlBase ctrl, params Object[] args)
        {
            GpChildForm form = GetChildForm(ctrl.FormId, args) as GpChildForm;
            if (form != null)
            {
                ShowChildForm(ctrl.FormId, args);
            }
            else
            {
                ctrl.FInfor.GForm.FormId = ctrl.FormId;
                ctrl.FInfor.GForm.Parameter = args;
                ctrl.FInfor.GForm.Controls.Add(ctrl);
                ctrl.Dock = DockStyle.Fill;
                ctrl.FInfor.GForm.MdiParent = AppForm.CurrentForm;
                ctrl.FInfor.GForm.Text = caption;
                ctrl.FInfor.GForm.Show();
                ctrl.FInfor.GForm.LoadParameter(args);
            }
        }
        #region 窗体移动、最大化、最小化
        private void pctrbxCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pctrbxCtrl = sender as PictureBox;
            pctrbxCtrl.BackColor = Color.LightGray;
        }

        private void pctrbxCtrl_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pctrbxCtrl = sender as PictureBox;
            pctrbxCtrl.BackColor = Color.Transparent;
        }

        private void pctrbxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pctrbxMax_Click(object sender, EventArgs e)
        {

            if (this.Size == Screen.PrimaryScreen.WorkingArea.Size)
            {
                this.Size = clientSize;
                this.Location = location;
            }
            else
            {
                clientSize = this.Size;
                location = this.Location;
                this.Location = new Point(0, 0);
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            }

        }
        private Size clientSize;
        private Point location;
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        SimpleButton btSelected = null;//选中按钮
        private void pnlcntrlTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void pnlcntrlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void pnlcntrlTop_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        private void pctrbxMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion
        private void btGroup_Click(object sender, EventArgs e)
        {
            btSelected = sender as SimpleButton;
            ResetButtonColor();
            LoadTreeMenu(new GSMeteringPaymentMenu());
        }

        private void btXMB_Click(object sender, MouseEventArgs e)
        {
            btSelected = sender as SimpleButton;
            ResetButtonColor();
            LoadTreeMenu(new XMBMeteringPaymentMenu());
        }

        private void btCompany_Click(object sender, EventArgs e)
        {
            btSelected = sender as SimpleButton;
            ResetButtonColor();
            LoadTreeMenu(new FGSMeteringPaymentMenu());
        }


        private void btContractDraft_Click(object sender, EventArgs e)
        {
            btSelected = sender as SimpleButton;
            ResetButtonColor();
            LoadTreeMenu(new XMBMeteringPaymentMenu());
            //LoadMenu(new projectmenu());
        }
        /// <summary>
        /// 根据按钮状态设置颜色
        /// </summary>
        private void ResetButtonColor()
        {

            btGroup.Appearance.BackColor = Color.FromArgb(128, 128, 128);
            btPrjDepartment.Appearance.BackColor = Color.FromArgb(128, 128, 128);
            btBaseInfor.Appearance.BackColor = Color.FromArgb(128, 128, 128);
            btBaseInfor.Appearance.BackColor = Color.FromArgb(128, 128, 128);

            btCompany.Appearance.BackColor = Color.FromArgb(128, 128, 128);
            if (btSelected != null)
                btSelected.Appearance.BackColor = Color.FromArgb(51, 122, 183);
        }

        private void btBaseInfor_Click(object sender, EventArgs e)
        {
            btSelected = sender as SimpleButton;
            ResetButtonColor();
            LoadTreeMenu(new BaseDataMngMenu());
        }




        public void ForceClose()
        {
            _AllowClose = true;
            this.Close();
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            RoleSelect();

            //如果是系统管理员，则显示管理员设置按钮
            if(Erp.GpServiceClient.GpClient.CreateInstance().IsAdmin(AppConfig.SYSTEM_NO, LoginInfor.LoginName))
            {
                btBaseInfor.Left = 333;
                btBaseInfor.Visible = true;
            }
            else
            {
                btBaseInfor.Visible = false;
            }
        }
        private void RoleSelect()
        {
            LoginUser tempUser = null;
            if (AppConfig.CurrentLoginUser != null)
            {
                tempUser = ObjectHelper.Clone(AppConfig.CurrentLoginUser);
                tempUser.LoginOrg = AppConfig.CurrentLoginUser.LoginOrg;  //缓存用户的登录机构
            }
            AppConfig.CurrentLoginUser = new LoginUser(LoginInfor.LoginName);
            if (AppConfig.CurrentLoginUser.RoleCount > 1)
            {
                frmSelectRole selectrole = new frmSelectRole(this);
                if (selectrole.ShowDialog() == DialogResult.Cancel)
                {
                    if (!IsChangeRoles)
                        ForceClose();
                    else
                    {
                        AppConfig.CurrentLoginUser = tempUser;  //还原用户登录机构
                        return;
                    }
                }
                LoadRoleInfo();
            }
            else if (AppConfig.CurrentLoginUser.RoleCount == 1)
            {
                if (AppConfig.CurrentLoginUser.LstFGSOrgs.Count > 1 || AppConfig.CurrentLoginUser.LstProject.Count > 1)
                {
                    frmSelectRole selectrole = new frmSelectRole(this);
                    if (selectrole.ShowDialog() == DialogResult.Cancel)
                    {
                        if (!IsChangeRoles)
                            ForceClose();
                        else
                        {
                            AppConfig.CurrentLoginUser = tempUser;  //还原用户登录机构
                            return;
                        }
                    }
                }
                else
                {
                    string roleStr = "";
                    switch (AppConfig.CurrentLoginUser.LoginRole)
                    {
                        case AppConfig.ManagerRole.GrpMngr:
                            AppConfig.CurrentLoginUser.LoginOrg = AppConfig.CurrentLoginUser.LstGrpOrgs[0];
                            roleStr = "业主";
                            break;
                        case AppConfig.ManagerRole.FGSMngr:
                            AppConfig.CurrentLoginUser.LoginOrg = AppConfig.CurrentLoginUser.LstFGSOrgs[0];
                            roleStr = "监理";
                            break;
                        case AppConfig.ManagerRole.XMBMngr:
                            CacheOrg project = new CacheOrg();
                            roleStr = "标段管理员";
                            break;
                    }
                    if (AppConfig.CurrentLoginUser.LoginRole == AppConfig.ManagerRole.XMBMngr)
                    {
                        AppConfig.SelectProject = new MeteringPaymentClient().GetIProjectInfoService().GetCurrencyAndExchangeRate(AppConfig.CurrentLoginUser.LstProject[0]);
                        RoleInfo = "当前登录角色:" + AppConfig.SelectProject.ProjectName + "-" + roleStr;
                    }
                    else
                    {
                        RoleInfo = "当前登录角色:" + AppConfig.CurrentLoginUser.LoginOrg.BidName + "-" + roleStr;
                    }
                }
                LoadRoleInfo();
            }
            else if (AppConfig.CurrentLoginUser.RoleCount == 0)
            {
                XtraMessageBox.Show("无系统权限,请联系管理员");
                ForceClose();
            }
        }
        private void LoadRoleInfo()
        {
            switch (AppConfig.CurrentLoginUser.LoginRole)
            {
                case AppConfig.ManagerRole.GrpMngr:
                    btSelected = btGroup;
                    btGroup.Visible = true;
                    btPrjDepartment.Visible = false;
                    btCompany.Visible = false;
                    btGroup.Left = 213;
                    LoadTreeMenu(new GSMeteringPaymentMenu());
                    break;
                case AppConfig.ManagerRole.FGSMngr:
                    btSelected = btCompany;
                    btCompany.Visible = true;
                    btGroup.Visible = false;
                    btPrjDepartment.Visible = false;
                    btCompany.Left = 213;
                    LoadTreeMenu(new FGSMeteringPaymentMenu());
                    break;
                case AppConfig.ManagerRole.XMBMngr:
                    btSelected = btPrjDepartment;
                    btPrjDepartment.Visible = true;
                    btGroup.Visible = false;
                    btCompany.Visible = false;
                    btPrjDepartment.Left = 213;
                    LoadTreeMenu(new XMBMeteringPaymentMenu());
                    break;
            }
            ResetButtonColor();
            CloseAllTab();
        }
        bool IsChangeRoles = false;
        private void btnChangeRoles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IsChangeRoles = true;
            RoleSelect();
        }

        
    }
}
