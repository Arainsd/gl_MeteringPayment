namespace HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng
{
    partial class frmFGSManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFGSManager));
            this.gcManager = new DevExpress.XtraGrid.GridControl();
            this.gvManager = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.FGSMngAdd = new DevExpress.XtraBars.BarButtonItem();
            this.FGSMngDel = new DevExpress.XtraBars.BarButtonItem();
            this.FGSMngRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.BarExpand = new DevExpress.XtraBars.BarButtonItem();
            this.BarShrink = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcManager
            // 
            this.gcManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcManager.Location = new System.Drawing.Point(0, 28);
            this.gcManager.MainView = this.gvManager;
            this.gcManager.Name = "gcManager";
            this.gcManager.Size = new System.Drawing.Size(586, 420);
            this.gcManager.TabIndex = 0;
            this.gcManager.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvManager});
            this.gcManager.Click += new System.EventHandler(this.gcManager_Click);
            // 
            // gvManager
            // 
            this.gvManager.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn3});
            this.gvManager.GridControl = this.gcManager;
            this.gvManager.GroupFormat = "{1} ";
            this.gvManager.Name = "gvManager";
            this.gvManager.OptionsView.AllowCellMerge = true;
            this.gvManager.OptionsView.ColumnAutoWidth = false;
            this.gvManager.OptionsView.ShowGroupPanel = false;
            this.gvManager.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.gvManager_CellMerge);
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "用户";
            this.gridColumn2.FieldName = "UserName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "账号";
            this.gridColumn4.FieldName = "LoginName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "标段";
            this.gridColumn3.FieldName = "OrgName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 250;
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
            this.FGSMngAdd,
            this.barButtonItem2,
            this.FGSMngRefresh,
            this.FGSMngDel,
            this.BarExpand,
            this.BarShrink});
            this.barManager1.MaxItemId = 6;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.FGSMngAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.FGSMngDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.FGSMngRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BarExpand, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BarShrink, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // FGSMngAdd
            // 
            this.FGSMngAdd.Caption = "新增";
            this.FGSMngAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("FGSMngAdd.Glyph")));
            this.FGSMngAdd.Id = 0;
            this.FGSMngAdd.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("FGSMngAdd.LargeGlyph")));
            this.FGSMngAdd.Name = "FGSMngAdd";
            this.FGSMngAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FGSMngAdd_ItemClick);
            // 
            // FGSMngDel
            // 
            this.FGSMngDel.Caption = "删除";
            this.FGSMngDel.Glyph = global::HD.MeteringPayment.Module.Properties.Resources.deleteitem_16x16;
            this.FGSMngDel.Id = 3;
            this.FGSMngDel.Name = "FGSMngDel";
            this.FGSMngDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FGSMngDel_ItemClick);
            // 
            // FGSMngRefresh
            // 
            this.FGSMngRefresh.Caption = "刷新";
            this.FGSMngRefresh.Glyph = global::HD.MeteringPayment.Module.Properties.Resources.refresh_16x16;
            this.FGSMngRefresh.Id = 2;
            this.FGSMngRefresh.LargeGlyph = global::HD.MeteringPayment.Module.Properties.Resources.refresh;
            this.FGSMngRefresh.Name = "FGSMngRefresh";
            this.FGSMngRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FGSMngRefresh_ItemClick);
            // 
            // BarExpand
            // 
            this.BarExpand.Caption = "展开";
            this.BarExpand.Glyph = ((System.Drawing.Image)(resources.GetObject("BarExpand.Glyph")));
            this.BarExpand.Id = 4;
            this.BarExpand.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("BarExpand.LargeGlyph")));
            this.BarExpand.Name = "BarExpand";
            this.BarExpand.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarExpand_ItemClick);
            // 
            // BarShrink
            // 
            this.BarShrink.Caption = "折叠";
            this.BarShrink.Glyph = ((System.Drawing.Image)(resources.GetObject("BarShrink.Glyph")));
            this.BarShrink.Id = 5;
            this.BarShrink.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("BarShrink.LargeGlyph")));
            this.BarShrink.Name = "BarShrink";
            this.BarShrink.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarShrink_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(586, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 448);
            this.barDockControlBottom.Size = new System.Drawing.Size(586, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 420);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(586, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 420);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "删除";
            this.barButtonItem2.Glyph = global::HD.MeteringPayment.Module.Properties.Resources.delete;
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.LargeGlyph = global::HD.MeteringPayment.Module.Properties.Resources.delete;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // frmFGSManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcManager);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmFGSManager";
            this.Size = new System.Drawing.Size(586, 448);
            this.Load += new System.EventHandler(this.frmFGSManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcManager;
        private DevExpress.XtraGrid.Views.Grid.GridView gvManager;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem FGSMngAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem FGSMngRefresh;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem FGSMngDel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraBars.BarButtonItem BarExpand;
        private DevExpress.XtraBars.BarButtonItem BarShrink;
    }
}
