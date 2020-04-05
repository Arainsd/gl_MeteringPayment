namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    partial class frmGSManager
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGSManager));
            this.gcGSManager = new DevExpress.XtraGrid.GridControl();
            this.gvGSManager = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.GSMngAdd = new DevExpress.XtraBars.BarButtonItem();
            this.GSMngDel = new DevExpress.XtraBars.BarButtonItem();
            this.GSMngCancle = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcGSManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGSManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcGSManager
            // 
            this.gcGSManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcGSManager.Location = new System.Drawing.Point(0, 28);
            this.gcGSManager.MainView = this.gvGSManager;
            this.gcGSManager.Name = "gcGSManager";
            this.gcGSManager.Size = new System.Drawing.Size(561, 400);
            this.gcGSManager.TabIndex = 0;
            this.gcGSManager.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGSManager});
            this.gcGSManager.Click += new System.EventHandler(this.gcGSManager_Click);
            // 
            // gvGSManager
            // 
            this.gvGSManager.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvGSManager.GridControl = this.gcGSManager;
            this.gvGSManager.Name = "gvGSManager";
            this.gvGSManager.OptionsView.ColumnAutoWidth = false;
            this.gvGSManager.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "用户";
            this.gridColumn1.FieldName = "UserName";
            this.gridColumn1.MinWidth = 150;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 150;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "登录名";
            this.gridColumn2.FieldName = "LoginName";
            this.gridColumn2.MinWidth = 150;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 150;
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
            this.GSMngAdd,
            this.GSMngDel,
            this.GSMngCancle});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.GSMngAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.GSMngDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.GSMngCancle, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // GSMngAdd
            // 
            this.GSMngAdd.Caption = "新增";
            this.GSMngAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("GSMngAdd.Glyph")));
            this.GSMngAdd.Id = 0;
            this.GSMngAdd.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("GSMngAdd.LargeGlyph")));
            this.GSMngAdd.Name = "GSMngAdd";
            this.GSMngAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.GSMngAdd_ItemClick);
            // 
            // GSMngDel
            // 
            this.GSMngDel.Caption = "删除";
            this.GSMngDel.Glyph = global::HD.MeteringPayment.Module.Properties.Resources.deleteitem_16x16;
            this.GSMngDel.Id = 1;
            this.GSMngDel.LargeGlyph = global::HD.MeteringPayment.Module.Properties.Resources.delete;
            this.GSMngDel.Name = "GSMngDel";
            this.GSMngDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.GSMngDel_ItemClick);
            // 
            // GSMngCancle
            // 
            this.GSMngCancle.Caption = "刷新";
            this.GSMngCancle.Glyph = global::HD.MeteringPayment.Module.Properties.Resources.refresh_16x16;
            this.GSMngCancle.Id = 2;
            this.GSMngCancle.LargeGlyph = global::HD.MeteringPayment.Module.Properties.Resources.refresh;
            this.GSMngCancle.Name = "GSMngCancle";
            this.GSMngCancle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.GSMngCancle_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(561, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 428);
            this.barDockControlBottom.Size = new System.Drawing.Size(561, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 400);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(561, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 400);
            // 
            // frmGSManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcGSManager);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmGSManager";
            this.Size = new System.Drawing.Size(561, 428);
            this.Load += new System.EventHandler(this.frmGSManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcGSManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGSManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcGSManager;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGSManager;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem GSMngAdd;
        private DevExpress.XtraBars.BarButtonItem GSMngDel;
        private DevExpress.XtraBars.BarButtonItem GSMngCancle;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
