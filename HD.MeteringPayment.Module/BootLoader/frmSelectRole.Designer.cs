using Erp.SharedLib.Presentation.CommonDataForm;
namespace HD.MeteringPayment.Module.BootLoader
{
    partial class frmSelectRole
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
            this.cbLoginRoles = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.cbProject = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbLoginOrg = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOrg = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.cbLoginRoles.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLoginOrg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOrg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciProject)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLoginRoles
            // 
            this.cbLoginRoles.Location = new System.Drawing.Point(73, 22);
            this.cbLoginRoles.Name = "cbLoginRoles";
            this.cbLoginRoles.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLoginRoles.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbLoginRoles.Size = new System.Drawing.Size(271, 20);
            this.cbLoginRoles.StyleController = this.dataLayoutControl1;
            this.cbLoginRoles.TabIndex = 2;
            this.cbLoginRoles.SelectedValueChanged += new System.EventHandler(this.cbLoginRoles_SelectedValueChanged);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.cbProject);
            this.dataLayoutControl1.Controls.Add(this.cbLoginOrg);
            this.dataLayoutControl1.Controls.Add(this.cbLoginRoles);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.dataLayoutControl1.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.dataLayoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.dataLayoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.dataLayoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.dataLayoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dataLayoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dataLayoutControl1.OptionsPrint.AppearanceItemCaption.Options.UseTextOptions = true;
            this.dataLayoutControl1.OptionsPrint.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.dataLayoutControl1.OptionsPrint.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(366, 87);
            this.dataLayoutControl1.TabIndex = 9;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // cbProject
            // 
            this.cbProject.Location = new System.Drawing.Point(236, 54);
            this.cbProject.Name = "cbProject";
            this.cbProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbProject.Size = new System.Drawing.Size(108, 20);
            this.cbProject.StyleController = this.dataLayoutControl1;
            this.cbProject.TabIndex = 9;
            // 
            // cbLoginOrg
            // 
            this.cbLoginOrg.Location = new System.Drawing.Point(73, 54);
            this.cbLoginOrg.Name = "cbLoginOrg";
            this.cbLoginOrg.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLoginOrg.Properties.ReadOnly = true;
            this.cbLoginOrg.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbLoginOrg.Size = new System.Drawing.Size(108, 20);
            this.cbLoginOrg.StyleController = this.dataLayoutControl1;
            this.cbLoginOrg.TabIndex = 8;
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lciOrg,
            this.lciProject});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 20, 20, 10);
            this.Root.Size = new System.Drawing.Size(366, 87);
            this.Root.Text = "Root";
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbLoginRoles;
            this.layoutControlItem1.CustomizationFormText = "登录角色";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 10);
            this.layoutControlItem1.Size = new System.Drawing.Size(326, 32);
            this.layoutControlItem1.Text = "登录角色";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // lciOrg
            // 
            this.lciOrg.Control = this.cbLoginOrg;
            this.lciOrg.CustomizationFormText = "所属部门";
            this.lciOrg.Location = new System.Drawing.Point(0, 32);
            this.lciOrg.Name = "lciOrg";
            this.lciOrg.Size = new System.Drawing.Size(163, 25);
            this.lciOrg.Text = "所属部门";
            this.lciOrg.TextSize = new System.Drawing.Size(48, 14);
            // 
            // lciProject
            // 
            this.lciProject.Control = this.cbProject;
            this.lciProject.CustomizationFormText = "选择项目";
            this.lciProject.Location = new System.Drawing.Point(163, 32);
            this.lciProject.Name = "lciProject";
            this.lciProject.Size = new System.Drawing.Size(163, 25);
            this.lciProject.Text = "选择项目";
            this.lciProject.TextSize = new System.Drawing.Size(48, 14);
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Location = new System.Drawing.Point(304, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(42, 24);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "退出";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.Location = new System.Drawing.Point(222, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "进入系统";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 87);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panel1.Size = new System.Drawing.Size(366, 24);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(290, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(14, 24);
            this.panel2.TabIndex = 4;
            // 
            // frmSelectRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 121);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectRole";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录角色";
            this.Load += new System.EventHandler(this.frmSelectRole_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbLoginRoles.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLoginOrg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOrg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciProject)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbLoginRoles;
        private DevExpress.XtraEditors.ComboBoxEdit cbLoginOrg;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbProject;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lciOrg;
        private DevExpress.XtraLayout.LayoutControlItem lciProject;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}