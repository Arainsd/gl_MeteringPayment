namespace HD.MeteringPayment.Module.Forms.ProjectContractInfoMng.XMBProjectInfoMng
{
    partial class AddPrjManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPrjManager));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel9 = new System.Windows.Forms.Panel();
            this.gcSelectedGpUser = new DevExpress.XtraGrid.GridControl();
            this.gvSelectedGpUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.isSelectedColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.orgNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnDelSelected = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.gcGpUser = new DevExpress.XtraGrid.GridControl();
            this.gvGpUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.loginUserColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.userNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.teFilterCondition = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.tlOrgs = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panel10 = new System.Windows.Forms.Panel();
            this.gcSelectUsers = new DevExpress.XtraGrid.GridControl();
            this.gvSelectUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSelectedGpUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectedGpUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGpUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGpUser)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teFilterCondition.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlOrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSelectUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 458);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(791, 39);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(358, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSend
            // 
            this.btnSend.Image = global::HD.MeteringPayment.Module.Properties.Resources.apply_16x16;
            this.btnSend.Location = new System.Drawing.Point(259, 9);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "确定";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // gridView2
            // 
            this.gridView2.Name = "gridView2";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.gcSelectedGpUser);
            this.panel9.Controls.Add(this.panelControl2);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(410, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(381, 458);
            this.panel9.TabIndex = 3;
            // 
            // gcSelectedGpUser
            // 
            this.gcSelectedGpUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSelectedGpUser.Location = new System.Drawing.Point(0, 34);
            this.gcSelectedGpUser.MainView = this.gvSelectedGpUser;
            this.gcSelectedGpUser.Name = "gcSelectedGpUser";
            this.gcSelectedGpUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcSelectedGpUser.Size = new System.Drawing.Size(381, 424);
            this.gcSelectedGpUser.TabIndex = 4;
            this.gcSelectedGpUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSelectedGpUser});
            // 
            // gvSelectedGpUser
            // 
            this.gvSelectedGpUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.isSelectedColumn,
            this.gridColumn1,
            this.gridColumn2,
            this.orgNameColumn});
            this.gvSelectedGpUser.GridControl = this.gcSelectedGpUser;
            this.gvSelectedGpUser.Name = "gvSelectedGpUser";
            this.gvSelectedGpUser.OptionsView.ColumnAutoWidth = false;
            this.gvSelectedGpUser.OptionsView.ShowGroupPanel = false;
            // 
            // isSelectedColumn
            // 
            this.isSelectedColumn.Caption = " ";
            this.isSelectedColumn.ColumnEdit = this.repositoryItemCheckEdit1;
            this.isSelectedColumn.FieldName = "isSelected";
            this.isSelectedColumn.MinWidth = 30;
            this.isSelectedColumn.Name = "isSelectedColumn";
            this.isSelectedColumn.Visible = true;
            this.isSelectedColumn.VisibleIndex = 0;
            this.isSelectedColumn.Width = 30;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "登录名";
            this.gridColumn1.FieldName = "LoginName";
            this.gridColumn1.MinWidth = 100;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 100;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "姓名";
            this.gridColumn2.FieldName = "UserName";
            this.gridColumn2.MinWidth = 100;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 100;
            // 
            // orgNameColumn
            // 
            this.orgNameColumn.Caption = "所属部门";
            this.orgNameColumn.FieldName = "OrgName";
            this.orgNameColumn.MinWidth = 200;
            this.orgNameColumn.Name = "orgNameColumn";
            this.orgNameColumn.OptionsColumn.AllowEdit = false;
            this.orgNameColumn.Visible = true;
            this.orgNameColumn.VisibleIndex = 3;
            this.orgNameColumn.Width = 200;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnDelSelected);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(381, 34);
            this.panelControl2.TabIndex = 3;
            // 
            // btnDelSelected
            // 
            this.btnDelSelected.Image = ((System.Drawing.Image)(resources.GetObject("btnDelSelected.Image")));
            this.btnDelSelected.Location = new System.Drawing.Point(5, 6);
            this.btnDelSelected.Name = "btnDelSelected";
            this.btnDelSelected.Size = new System.Drawing.Size(80, 23);
            this.btnDelSelected.TabIndex = 3;
            this.btnDelSelected.Text = "删除所选";
            this.btnDelSelected.Click += new System.EventHandler(this.btnDelSelected_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(410, 458);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            this.xtraTabControl1.TabIndexChanged += new System.EventHandler(this.xtraTabControl1_TabIndexChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.panel1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(408, 432);
            this.xtraTabPage1.Text = "账号查找";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 432);
            this.panel1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.gcGpUser);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(408, 432);
            this.panel5.TabIndex = 1;
            // 
            // gcGpUser
            // 
            this.gcGpUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcGpUser.Location = new System.Drawing.Point(0, 36);
            this.gcGpUser.MainView = this.gvGpUser;
            this.gcGpUser.Name = "gcGpUser";
            this.gcGpUser.Size = new System.Drawing.Size(408, 396);
            this.gcGpUser.TabIndex = 3;
            this.gcGpUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGpUser});
            this.gcGpUser.DoubleClick += new System.EventHandler(this.gvGpUser_DoubleClick);
            // 
            // gvGpUser
            // 
            this.gvGpUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.loginUserColumn,
            this.userNameColumn});
            this.gvGpUser.GridControl = this.gcGpUser;
            this.gvGpUser.Name = "gvGpUser";
            this.gvGpUser.OptionsView.ShowGroupPanel = false;
            this.gvGpUser.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.loginUserColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // loginUserColumn
            // 
            this.loginUserColumn.Caption = "登录名";
            this.loginUserColumn.FieldName = "LoginName";
            this.loginUserColumn.MinWidth = 120;
            this.loginUserColumn.Name = "loginUserColumn";
            this.loginUserColumn.OptionsColumn.AllowEdit = false;
            this.loginUserColumn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.loginUserColumn.OptionsColumn.FixedWidth = true;
            this.loginUserColumn.Visible = true;
            this.loginUserColumn.VisibleIndex = 0;
            this.loginUserColumn.Width = 120;
            // 
            // userNameColumn
            // 
            this.userNameColumn.Caption = "姓名";
            this.userNameColumn.FieldName = "UserName";
            this.userNameColumn.Name = "userNameColumn";
            this.userNameColumn.OptionsColumn.AllowEdit = false;
            this.userNameColumn.Visible = true;
            this.userNameColumn.VisibleIndex = 1;
            this.userNameColumn.Width = 382;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel6.Controls.Add(this.btnSearch);
            this.panel6.Controls.Add(this.teFilterCondition);
            this.panel6.Controls.Add(this.labelControl1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(408, 36);
            this.panel6.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(290, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(52, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查找";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // teFilterCondition
            // 
            this.teFilterCondition.Location = new System.Drawing.Point(81, 7);
            this.teFilterCondition.Name = "teFilterCondition";
            this.teFilterCondition.Size = new System.Drawing.Size(202, 20);
            this.teFilterCondition.TabIndex = 1;
            this.teFilterCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teFilterCondition_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "账号/姓名";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.tlOrgs);
            this.xtraTabPage2.Controls.Add(this.panel10);
            this.xtraTabPage2.Controls.Add(this.gcSelectUsers);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(408, 432);
            this.xtraTabPage2.Text = "部门查找";
            // 
            // tlOrgs
            // 
            this.tlOrgs.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tlOrgs.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.tlOrgs.Appearance.FocusedRow.Options.UseBackColor = true;
            this.tlOrgs.Appearance.FocusedRow.Options.UseForeColor = true;
            this.tlOrgs.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tlOrgs.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.tlOrgs.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.tlOrgs.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.tlOrgs.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tlOrgs.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.tlOrgs.Appearance.SelectedRow.Options.UseBackColor = true;
            this.tlOrgs.Appearance.SelectedRow.Options.UseForeColor = true;
            this.tlOrgs.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn4});
            this.tlOrgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlOrgs.KeyFieldName = "OrgNo";
            this.tlOrgs.Location = new System.Drawing.Point(0, 0);
            this.tlOrgs.Name = "tlOrgs";
            this.tlOrgs.OptionsBehavior.AllowExpandOnDblClick = false;
            this.tlOrgs.OptionsBehavior.EnableFiltering = true;
            this.tlOrgs.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            this.tlOrgs.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.Always;
            this.tlOrgs.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tlOrgs.OptionsView.ShowAutoFilterRow = true;
            this.tlOrgs.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
            this.tlOrgs.ParentFieldName = "ParentOrgNo";
            this.tlOrgs.Size = new System.Drawing.Size(211, 432);
            this.tlOrgs.TabIndex = 21;
            this.tlOrgs.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tlOrgs_FocusedNodeChanged);
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "组织机构";
            this.treeListColumn4.FieldName = "OrgName";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.AllowEdit = false;
            this.treeListColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains;
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 0;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.LightGray;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(211, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(4, 432);
            this.panel10.TabIndex = 1;
            // 
            // gcSelectUsers
            // 
            this.gcSelectUsers.Dock = System.Windows.Forms.DockStyle.Right;
            this.gcSelectUsers.Location = new System.Drawing.Point(215, 0);
            this.gcSelectUsers.MainView = this.gvSelectUsers;
            this.gcSelectUsers.Name = "gcSelectUsers";
            this.gcSelectUsers.Size = new System.Drawing.Size(193, 432);
            this.gcSelectUsers.TabIndex = 5;
            this.gcSelectUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSelectUsers});
            // 
            // gvSelectUsers
            // 
            this.gvSelectUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4});
            this.gvSelectUsers.GridControl = this.gcSelectUsers;
            this.gvSelectUsers.Name = "gvSelectUsers";
            this.gvSelectUsers.OptionsView.ShowGroupPanel = false;
            this.gvSelectUsers.DoubleClick += new System.EventHandler(this.gvSelectUsers_DoubleClick);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "登录名";
            this.gridColumn3.FieldName = "LoginName";
            this.gridColumn3.MinWidth = 70;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 70;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "姓名";
            this.gridColumn4.FieldName = "UserName";
            this.gridColumn4.MinWidth = 100;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 105;
            // 
            // AddPrjManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 497);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPrjManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "人员查找";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSelectedGpUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectedGpUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcGpUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGpUser)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teFilterCondition.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlOrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSelectUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraGrid.GridControl gcGpUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGpUser;
        private DevExpress.XtraGrid.Columns.GridColumn loginUserColumn;
        private DevExpress.XtraGrid.Columns.GridColumn userNameColumn;
        private System.Windows.Forms.Panel panel6;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.TextEdit teFilterCondition;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Panel panel9;
        private DevExpress.XtraGrid.GridControl gcSelectedGpUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSelectedGpUser;
        private DevExpress.XtraGrid.Columns.GridColumn isSelectedColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn orgNameColumn;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDelSelected;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl gcSelectUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSelectUsers;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.Panel panel10;
        private DevExpress.XtraTreeList.TreeList tlOrgs;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
    }
}