namespace HIS.Desktop.Plugins.ServiceExecute
{
    partial class frmSTTNumber
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
            this.spinSTT = new DevExpress.XtraEditors.SpinEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.spinSTT.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // spinSTT
            // 
            this.spinSTT.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinSTT.Location = new System.Drawing.Point(12, 24);
            this.spinSTT.Name = "spinSTT";
            this.spinSTT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSTT.Properties.IsFloatValue = false;
            this.spinSTT.Properties.Mask.EditMask = "N00";
            this.spinSTT.Properties.MaxValue = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.spinSTT.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinSTT.Size = new System.Drawing.Size(100, 20);
            this.spinSTT.TabIndex = 0;
            this.spinSTT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.spinSTT_PreviewKeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(142, 22);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Lưu";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmSTTNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 72);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.spinSTT);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSTTNumber";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa số thứ tự sắp xếp";
            this.Load += new System.EventHandler(this.frmSTTNumber_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spinSTT.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit spinSTT;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}