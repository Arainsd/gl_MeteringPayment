namespace HD.MeteringPayment.Module.Forms.WBSBoqMng
{
    partial class frmPrjBoqExport
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
            this.gcExport = new DevExpress.XtraGrid.GridControl();
            this.gvExport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btExport = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcExport
            // 
            this.gcExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcExport.Location = new System.Drawing.Point(0, 39);
            this.gcExport.MainView = this.gvExport;
            this.gcExport.Name = "gcExport";
            this.gcExport.Size = new System.Drawing.Size(1165, 544);
            this.gcExport.TabIndex = 0;
            this.gcExport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExport});
            // 
            // gvExport
            // 
            this.gvExport.GridControl = this.gcExport;
            this.gvExport.Name = "gvExport";
            this.gvExport.OptionsView.ColumnAutoWidth = false;
            this.gvExport.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btExport);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1165, 39);
            this.panelControl1.TabIndex = 1;
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(13, 10);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(75, 23);
            this.btExport.TabIndex = 0;
            this.btExport.Text = "导出";
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // frmPrjBoqExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 583);
            this.Controls.Add(this.gcExport);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmPrjBoqExport";
            this.Text = "导出模板";
            this.Shown += new System.EventHandler(this.frmPrjBoqExport_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gcExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcExport;
        private DevExpress.XtraGrid.Views.Grid.GridView gvExport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btExport;
    }
}