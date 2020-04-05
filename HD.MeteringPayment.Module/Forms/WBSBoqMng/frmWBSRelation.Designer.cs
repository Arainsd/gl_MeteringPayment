namespace HD.MeteringPayment.Module.Forms.WBSBoqMng
{
    partial class frmWBSRelation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWBSRelation));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BarOK = new DevExpress.XtraBars.BarButtonItem();
            this.BarCancel = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tlDetail = new DevExpress.XtraTreeList.TreeList();
            this.treeListBand1 = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.colIItemCoe = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colItemName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUom = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCtrctPrjPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colCtrctQty = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCtrctAmount = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListBand2 = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colItemCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
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
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BarOK,
            this.BarCancel});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BarOK, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BarCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Custom 2";
            // 
            // BarOK
            // 
            this.BarOK.Caption = "确认";
            this.BarOK.Glyph = ((System.Drawing.Image)(resources.GetObject("BarOK.Glyph")));
            this.BarOK.Id = 0;
            this.BarOK.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("BarOK.LargeGlyph")));
            this.BarOK.Name = "BarOK";
            this.BarOK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarOK_ItemClick);
            // 
            // BarCancel
            // 
            this.BarCancel.Caption = "取消";
            this.BarCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("BarCancel.Glyph")));
            this.BarCancel.Id = 1;
            this.BarCancel.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("BarCancel.LargeGlyph")));
            this.BarCancel.Name = "BarCancel";
            this.BarCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarCancel_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1250, 28);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1250, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 519);
            this.barDockControlBottom.Size = new System.Drawing.Size(1250, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 519);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1250, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 519);
            // 
            // tlDetail
            // 
            this.tlDetail.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.treeListBand1,
            this.treeListBand2});
            this.tlDetail.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colIItemCoe,
            this.colItemName,
            this.treeListColumn6,
            this.colUom,
            this.colCtrctQty,
            this.colCtrctPrjPrice,
            this.colCtrctAmount,
            this.treeListColumn8,
            this.treeListColumn4});
            this.tlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDetail.KeyFieldName = "ItemCode";
            this.tlDetail.Location = new System.Drawing.Point(0, 28);
            this.tlDetail.Name = "tlDetail";
            this.tlDetail.OptionsSelection.MultiSelect = true;
            this.tlDetail.OptionsView.AutoWidth = false;
            this.tlDetail.OptionsView.ShowCheckBoxes = true;
            this.tlDetail.ParentFieldName = "ParentCode";
            this.tlDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit1});
            this.tlDetail.Size = new System.Drawing.Size(1250, 491);
            this.tlDetail.TabIndex = 5;
            this.tlDetail.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.tlDetail_BeforeCheckNode);
            // 
            // treeListBand1
            // 
            this.treeListBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListBand1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListBand1.Caption = "合同清单原始值";
            this.treeListBand1.Columns.Add(this.colIItemCoe);
            this.treeListBand1.Columns.Add(this.colItemName);
            this.treeListBand1.Columns.Add(this.treeListColumn6);
            this.treeListBand1.Columns.Add(this.colUom);
            this.treeListBand1.Columns.Add(this.colCtrctPrjPrice);
            this.treeListBand1.Columns.Add(this.colCtrctQty);
            this.treeListBand1.Columns.Add(this.colCtrctAmount);
            this.treeListBand1.MinWidth = 34;
            this.treeListBand1.Name = "treeListBand1";
            this.treeListBand1.Width = 525;
            // 
            // colIItemCoe
            // 
            this.colIItemCoe.AppearanceCell.Options.UseTextOptions = true;
            this.colIItemCoe.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIItemCoe.AppearanceHeader.Options.UseTextOptions = true;
            this.colIItemCoe.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIItemCoe.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIItemCoe.Caption = "清单编号";
            this.colIItemCoe.FieldName = "IItemCoe";
            this.colIItemCoe.MinWidth = 32;
            this.colIItemCoe.Name = "colIItemCoe";
            this.colIItemCoe.OptionsColumn.AllowEdit = false;
            this.colIItemCoe.Visible = true;
            this.colIItemCoe.VisibleIndex = 0;
            this.colIItemCoe.Width = 90;
            // 
            // colItemName
            // 
            this.colItemName.AppearanceCell.Options.UseTextOptions = true;
            this.colItemName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colItemName.AppearanceHeader.Options.UseTextOptions = true;
            this.colItemName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colItemName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colItemName.Caption = "清单名称";
            this.colItemName.FieldName = "ItemName";
            this.colItemName.Name = "colItemName";
            this.colItemName.OptionsColumn.AllowEdit = false;
            this.colItemName.Visible = true;
            this.colItemName.VisibleIndex = 1;
            this.colItemName.Width = 300;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListColumn6.Caption = "系统编号";
            this.treeListColumn6.FieldName = "ItemCode";
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.OptionsColumn.AllowEdit = false;
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 2;
            this.treeListColumn6.Width = 90;
            // 
            // colUom
            // 
            this.colUom.AppearanceCell.Options.UseTextOptions = true;
            this.colUom.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colUom.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUom.AppearanceHeader.Options.UseTextOptions = true;
            this.colUom.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUom.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUom.Caption = "单位";
            this.colUom.FieldName = "Uom";
            this.colUom.Name = "colUom";
            this.colUom.OptionsColumn.AllowEdit = false;
            this.colUom.Visible = true;
            this.colUom.VisibleIndex = 3;
            this.colUom.Width = 90;
            // 
            // colCtrctPrjPrice
            // 
            this.colCtrctPrjPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colCtrctPrjPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCtrctPrjPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCtrctPrjPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colCtrctPrjPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCtrctPrjPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCtrctPrjPrice.Caption = "单价";
            this.colCtrctPrjPrice.ColumnEdit = this.repositoryItemCalcEdit1;
            this.colCtrctPrjPrice.FieldName = "CtrctPrjPrice";
            this.colCtrctPrjPrice.Name = "colCtrctPrjPrice";
            this.colCtrctPrjPrice.OptionsColumn.AllowEdit = false;
            this.colCtrctPrjPrice.Visible = true;
            this.colCtrctPrjPrice.VisibleIndex = 4;
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.DisplayFormat.FormatString = "f2";
            this.repositoryItemCalcEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCalcEdit1.Mask.EditMask = "f2";
            this.repositoryItemCalcEdit1.Mask.SaveLiteral = false;
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // colCtrctQty
            // 
            this.colCtrctQty.AppearanceCell.Options.UseTextOptions = true;
            this.colCtrctQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCtrctQty.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCtrctQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colCtrctQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCtrctQty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCtrctQty.Caption = "数量";
            this.colCtrctQty.FieldName = "CtrctQty";
            this.colCtrctQty.Name = "colCtrctQty";
            this.colCtrctQty.OptionsColumn.AllowEdit = false;
            // 
            // colCtrctAmount
            // 
            this.colCtrctAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colCtrctAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCtrctAmount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCtrctAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colCtrctAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCtrctAmount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCtrctAmount.Caption = "金额";
            this.colCtrctAmount.FieldName = "CtrctAmount";
            this.colCtrctAmount.Name = "colCtrctAmount";
            this.colCtrctAmount.OptionsColumn.AllowEdit = false;
            this.colCtrctAmount.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            // 
            // treeListBand2
            // 
            this.treeListBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListBand2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListBand2.Caption = "未关联合同清单";
            this.treeListBand2.Columns.Add(this.treeListColumn8);
            this.treeListBand2.Columns.Add(this.treeListColumn4);
            this.treeListBand2.Name = "treeListBand2";
            this.treeListBand2.Width = 100;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn8.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListColumn8.Caption = "未关联数量";
            this.treeListColumn8.ColumnEdit = this.repositoryItemCalcEdit1;
            this.treeListColumn8.FieldName = "CtrctQty";
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.OptionsColumn.AllowEdit = false;
            this.treeListColumn8.Visible = true;
            this.treeListColumn8.VisibleIndex = 5;
            this.treeListColumn8.Width = 100;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.treeListColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListColumn4.Caption = "未关联金额";
            this.treeListColumn4.ColumnEdit = this.repositoryItemCalcEdit1;
            this.treeListColumn4.FieldName = "CtrctAmount";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.AllowEdit = false;
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 6;
            this.treeListColumn4.Width = 100;
            // 
            // colItemCode
            // 
            this.colItemCode.Caption = "系统编号";
            this.colItemCode.FieldName = "ItemCode";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.Visible = true;
            this.colItemCode.VisibleIndex = 2;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "系统编号";
            this.treeListColumn1.FieldName = "ItemCode";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 2;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "系统编号";
            this.treeListColumn2.FieldName = "ItemCode";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 2;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "系统编号";
            this.treeListColumn3.FieldName = "ItemCode";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 2;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "系统编号";
            this.treeListColumn5.FieldName = "ItemCode";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 2;
            // 
            // frmWBSRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 519);
            this.Controls.Add(this.tlDetail);
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmWBSRelation";
            this.Text = "添加合同清单关联";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem BarOK;
        private DevExpress.XtraBars.BarButtonItem BarCancel;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraTreeList.TreeList tlDetail;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIItemCoe;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colItemName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colItemCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUom;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCtrctPrjPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCtrctQty;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCtrctAmount;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListBand treeListBand1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListBand treeListBand2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
    }
}