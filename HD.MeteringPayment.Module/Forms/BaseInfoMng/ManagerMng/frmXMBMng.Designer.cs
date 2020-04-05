using Erp.SharedLib.Presentation.CommonDataForm;
namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    partial class frmXMBMng
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXMBMng));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.XMBMngSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.XMBMngCancle = new DevExpress.XtraBars.BarButtonItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.GpUserSelector = new Erp.SharedLib.Presentation.CommonDataForm.GridViewPopupEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ProjectSelector = new Erp.SharedLib.Presentation.CommonDataForm.GridViewPopupEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GpUserSelector.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectSelector.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.XMBMngSave,
            this.XMBMngCancle});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(316, 157);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.XMBMngSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // XMBMngSave
            // 
            this.XMBMngSave.Caption = "保存";
            this.XMBMngSave.Glyph = ((System.Drawing.Image)(resources.GetObject("XMBMngSave.Glyph")));
            this.XMBMngSave.Id = 0;
            this.XMBMngSave.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("XMBMngSave.LargeGlyph")));
            this.XMBMngSave.Name = "XMBMngSave";
            this.XMBMngSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.XMBMngSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(381, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 103);
            this.barDockControlBottom.Size = new System.Drawing.Size(381, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 72);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(381, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 72);
            // 
            // XMBMngCancle
            // 
            this.XMBMngCancle.Caption = "取消";
            this.XMBMngCancle.Id = 1;
            this.XMBMngCancle.Name = "XMBMngCancle";
            this.XMBMngCancle.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.GpUserSelector);
            this.layoutControl1.Controls.Add(this.ProjectSelector);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl1.OptionsPrint.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControl1.OptionsPrint.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutControl1.OptionsPrint.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(381, 72);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // GpUserSelector
            // 
            this.GpUserSelector.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource1, "LoginName", true));
            this.GpUserSelector.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "UserName", true));
            this.GpUserSelector.DisplayField = null;
            this.GpUserSelector.GetDataSource = null;
            this.GpUserSelector.KeyField = null;
            this.GpUserSelector.Location = new System.Drawing.Point(39, 36);
            this.GpUserSelector.MenuManager = this.barManager1;
            this.GpUserSelector.Name = "GpUserSelector";
            this.GpUserSelector.PopMaxHeight = 300;
            this.GpUserSelector.PopMaxWidth = 400;
            this.GpUserSelector.PopMiniHeight = 400;
            this.GpUserSelector.PopMiniWidth = 400;
            this.GpUserSelector.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GpUserSelector.RefDataSource = null;
            this.GpUserSelector.ShowAddButton = false;
            this.GpUserSelector.ShowClearButton = false;
            this.GpUserSelector.Size = new System.Drawing.Size(330, 20);
            this.GpUserSelector.StyleController = this.layoutControl1;
            this.GpUserSelector.TabIndex = 9;
            this.GpUserSelector.Value = null;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(HD.MeteringPayment.Domain.Entity.BaseInfoEntity.Manager);
            // 
            // ProjectSelector
            // 
            this.ProjectSelector.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource1, "ProjectNo", true));
            this.ProjectSelector.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ProjectName", true));
            this.ProjectSelector.DisplayField = null;
            this.ProjectSelector.GetDataSource = null;
            this.ProjectSelector.KeyField = null;
            this.ProjectSelector.Location = new System.Drawing.Point(39, 12);
            this.ProjectSelector.MenuManager = this.barManager1;
            this.ProjectSelector.Name = "ProjectSelector";
            this.ProjectSelector.PopMaxHeight = 300;
            this.ProjectSelector.PopMaxWidth = 400;
            this.ProjectSelector.PopMiniHeight = 400;
            this.ProjectSelector.PopMiniWidth = 400;
            this.ProjectSelector.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ProjectSelector.RefDataSource = null;
            this.ProjectSelector.ShowAddButton = false;
            this.ProjectSelector.ShowClearButton = false;
            this.ProjectSelector.Size = new System.Drawing.Size(330, 20);
            this.ProjectSelector.StyleController = this.layoutControl1;
            this.ProjectSelector.TabIndex = 8;
            this.ProjectSelector.Value = null;
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(381, 72);
            this.Root.Text = "Root";
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ProjectSelector;
            this.layoutControlItem1.CustomizationFormText = "项目";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(361, 24);
            this.layoutControlItem1.Text = "标段";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.GpUserSelector;
            this.layoutControlItem2.CustomizationFormText = "用户";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(361, 28);
            this.layoutControlItem2.Text = "用户";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(24, 14);
            // 
            // frmXMBMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 103);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmXMBMng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标段管理员";
            this.Load += new System.EventHandler(this.frmXMBMng_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GpUserSelector.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectSelector.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem XMBMngSave;
        private DevExpress.XtraBars.BarButtonItem XMBMngCancle;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.BindingSource bindingSource1;
        private GridViewPopupEdit ProjectSelector;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private GridViewPopupEdit GpUserSelector;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;

    }
}