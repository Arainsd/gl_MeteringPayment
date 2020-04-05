namespace HD.MeteringPayment.Module.Forms.MeteringRptMng
{
    partial class PrintingControlControl
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
            this.PrintContainer = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.PrintContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // PrintContainer
            // 
            this.PrintContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintContainer.Location = new System.Drawing.Point(0, 0);
            this.PrintContainer.Name = "PrintContainer";
            this.PrintContainer.Size = new System.Drawing.Size(915, 599);
            this.PrintContainer.TabIndex = 0;
            // 
            // PrintingControlControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 599);
            this.Controls.Add(this.PrintContainer);
            this.Name = "PrintingControlControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印";
            ((System.ComponentModel.ISupportInitialize)(this.PrintContainer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl PrintContainer;
    }
}