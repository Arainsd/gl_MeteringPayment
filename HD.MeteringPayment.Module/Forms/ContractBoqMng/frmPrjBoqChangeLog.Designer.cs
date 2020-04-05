namespace HD.MeteringPayment.Module.Forms.ContractBoqMng
{
    partial class frmPrjBoqChangeLog
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiExport = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colIItemCoe = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colItemCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colItemName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUom = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBefPrjPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBefQty = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.amountEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colBefAmount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colAfPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAfQty = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAfAmount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colChangeQty = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colChangeDetailNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colChangePrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colChangeAmount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amountEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiExport});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExport)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbiExport
            // 
            this.bbiExport.Caption = "导出";
            this.bbiExport.Id = 0;
            this.bbiExport.Name = "bbiExport";
            this.bbiExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExport_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1222, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 459);
            this.barDockControlBottom.Size = new System.Drawing.Size(1222, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 435);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1222, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 435);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 24);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.amountEdit});
            this.gridControl1.Size = new System.Drawing.Size(1222, 435);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand4,
            this.gridBand5,
            this.gridBand6,
            this.gridBand7,
            this.gridBand3,
            this.gridBand2});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colAfAmount,
            this.colAfPrice,
            this.colAfQty,
            this.colBefAmount,
            this.colBefPrjPrice,
            this.colBefQty,
            this.colChangeAmount,
            this.colChangeDetailNo,
            this.colChangePrice,
            this.colChangeQty,
            this.colItemName,
            this.colIItemCoe,
            this.colItemCode,
            this.colType,
            this.colUom,
            this.bandedGridColumn1,
            this.bandedGridColumn2});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.Editable = false;
            this.bandedGridView1.OptionsPrint.AllowMultilineHeaders = true;
            this.bandedGridView1.OptionsView.ColumnAutoWidth = false;
            this.bandedGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.bandedGridView1.OptionsView.ShowFooter = true;
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "修改分类";
            this.gridBand4.Columns.Add(this.bandedGridColumn1);
            this.gridBand4.Columns.Add(this.bandedGridColumn2);
            this.gridBand4.Columns.Add(this.colType);
            this.gridBand4.Columns.Add(this.colIItemCoe);
            this.gridBand4.Columns.Add(this.colItemCode);
            this.gridBand4.Columns.Add(this.colItemName);
            this.gridBand4.Columns.Add(this.colUom);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.OptionsBand.ShowCaption = false;
            this.gridBand4.VisibleIndex = 0;
            this.gridBand4.Width = 525;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "修改时间";
            this.bandedGridColumn1.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.bandedGridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.bandedGridColumn1.FieldName = "AdjustDate";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "修改人";
            this.bandedGridColumn2.FieldName = "AdjustBy";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            // 
            // colType
            // 
            this.colType.AppearanceHeader.Options.UseTextOptions = true;
            this.colType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colType.Caption = "修改类型";
            this.colType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colType.FieldName = "TypeShow";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("新增", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("修改单价", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("修改数量", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("修改数量单价", 4, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("修改其它", 5, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("删除", 6, -1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // colIItemCoe
            // 
            this.colIItemCoe.AppearanceHeader.Options.UseTextOptions = true;
            this.colIItemCoe.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIItemCoe.Caption = "清单编号";
            this.colIItemCoe.FieldName = "IItemCoe";
            this.colIItemCoe.Name = "colIItemCoe";
            this.colIItemCoe.Visible = true;
            // 
            // colItemCode
            // 
            this.colItemCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colItemCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colItemCode.Caption = "系统编号";
            this.colItemCode.FieldName = "ItemCode";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.Visible = true;
            // 
            // colItemName
            // 
            this.colItemName.AppearanceHeader.Options.UseTextOptions = true;
            this.colItemName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colItemName.Caption = "清单项";
            this.colItemName.FieldName = "ItemName";
            this.colItemName.Name = "colItemName";
            this.colItemName.Visible = true;
            // 
            // colUom
            // 
            this.colUom.AppearanceHeader.Options.UseTextOptions = true;
            this.colUom.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUom.Caption = "单位";
            this.colUom.FieldName = "Uom";
            this.colUom.Name = "colUom";
            this.colUom.Visible = true;
            // 
            // gridBand5
            // 
            this.gridBand5.Caption = "清单编号";
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.OptionsBand.ShowCaption = false;
            this.gridBand5.Visible = false;
            this.gridBand5.VisibleIndex = -1;
            this.gridBand5.Width = 75;
            // 
            // gridBand6
            // 
            this.gridBand6.Caption = "清单名称";
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.OptionsBand.ShowCaption = false;
            this.gridBand6.Visible = false;
            this.gridBand6.VisibleIndex = -1;
            this.gridBand6.Width = 75;
            // 
            // gridBand7
            // 
            this.gridBand7.Caption = "单位";
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.OptionsBand.ShowCaption = false;
            this.gridBand7.Visible = false;
            this.gridBand7.VisibleIndex = -1;
            this.gridBand7.Width = 75;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "修改前";
            this.gridBand3.Columns.Add(this.colBefPrjPrice);
            this.gridBand3.Columns.Add(this.colBefQty);
            this.gridBand3.Columns.Add(this.colBefAmount);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 1;
            this.gridBand3.Width = 225;
            // 
            // colBefPrjPrice
            // 
            this.colBefPrjPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colBefPrjPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBefPrjPrice.Caption = "单价";
            this.colBefPrjPrice.DisplayFormat.FormatString = "#,0.######";
            this.colBefPrjPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBefPrjPrice.FieldName = "BefPrjPrice";
            this.colBefPrjPrice.Name = "colBefPrjPrice";
            this.colBefPrjPrice.Visible = true;
            // 
            // colBefQty
            // 
            this.colBefQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colBefQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBefQty.Caption = "数量";
            this.colBefQty.ColumnEdit = this.amountEdit;
            this.colBefQty.FieldName = "BefQty";
            this.colBefQty.Name = "colBefQty";
            this.colBefQty.Visible = true;
            // 
            // amountEdit
            // 
            this.amountEdit.AutoHeight = false;
            this.amountEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.amountEdit.DisplayFormat.FormatString = "f2";
            this.amountEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.amountEdit.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.amountEdit.Mask.EditMask = "f2";
            this.amountEdit.Name = "amountEdit";
            // 
            // colBefAmount
            // 
            this.colBefAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colBefAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBefAmount.Caption = "金额";
            this.colBefAmount.ColumnEdit = this.amountEdit;
            this.colBefAmount.FieldName = "BefAmount";
            this.colBefAmount.Name = "colBefAmount";
            this.colBefAmount.Visible = true;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "修改后";
            this.gridBand2.Columns.Add(this.colAfPrice);
            this.gridBand2.Columns.Add(this.colAfQty);
            this.gridBand2.Columns.Add(this.colAfAmount);
            this.gridBand2.Columns.Add(this.colChangeQty);
            this.gridBand2.Columns.Add(this.colChangeDetailNo);
            this.gridBand2.Columns.Add(this.colChangePrice);
            this.gridBand2.Columns.Add(this.colChangeAmount);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 2;
            this.gridBand2.Width = 450;
            // 
            // colAfPrice
            // 
            this.colAfPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colAfPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAfPrice.Caption = "单价";
            this.colAfPrice.ColumnEdit = this.amountEdit;
            this.colAfPrice.FieldName = "AfPrice";
            this.colAfPrice.Name = "colAfPrice";
            this.colAfPrice.Visible = true;
            // 
            // colAfQty
            // 
            this.colAfQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colAfQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAfQty.Caption = "数量";
            this.colAfQty.ColumnEdit = this.amountEdit;
            this.colAfQty.FieldName = "AfQty";
            this.colAfQty.Name = "colAfQty";
            this.colAfQty.Visible = true;
            // 
            // colAfAmount
            // 
            this.colAfAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAfAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAfAmount.Caption = "金额";
            this.colAfAmount.DisplayFormat.FormatString = "#,0.##";
            this.colAfAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAfAmount.FieldName = "AfAmount";
            this.colAfAmount.Name = "colAfAmount";
            this.colAfAmount.Visible = true;
            // 
            // colChangeQty
            // 
            this.colChangeQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colChangeQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChangeQty.Caption = "变更数量";
            this.colChangeQty.ColumnEdit = this.amountEdit;
            this.colChangeQty.FieldName = "ChangeQty";
            this.colChangeQty.Name = "colChangeQty";
            this.colChangeQty.Visible = true;
            // 
            // colChangeDetailNo
            // 
            this.colChangeDetailNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colChangeDetailNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChangeDetailNo.FieldName = "ChangeDetailNo";
            this.colChangeDetailNo.Name = "colChangeDetailNo";
            // 
            // colChangePrice
            // 
            this.colChangePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colChangePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChangePrice.Caption = "变更单价";
            this.colChangePrice.ColumnEdit = this.amountEdit;
            this.colChangePrice.FieldName = "ChangePrice";
            this.colChangePrice.Name = "colChangePrice";
            this.colChangePrice.Visible = true;
            // 
            // colChangeAmount
            // 
            this.colChangeAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colChangeAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChangeAmount.Caption = "变更金额";
            this.colChangeAmount.ColumnEdit = this.amountEdit;
            this.colChangeAmount.FieldName = "ChangeAmount";
            this.colChangeAmount.Name = "colChangeAmount";
            this.colChangeAmount.Visible = true;
            // 
            // frmPrjBoqChangeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 482);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmPrjBoqChangeLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变更记录";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amountEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbiExport;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIItemCoe;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colItemCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colItemName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUom;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBefPrjPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBefQty;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBefAmount;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAfPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAfQty;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAfAmount;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colChangeQty;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colChangeDetailNo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colChangePrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colChangeAmount;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit amountEdit;
    }
}