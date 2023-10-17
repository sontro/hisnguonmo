namespace HIS.Desktop.Plugins.ServiceExecute
{
    partial class UcWord
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDescription = new DevExpress.XtraRichEdit.RichEditControl();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(0, 0);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(707, 491);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.ZoomChanged += new System.EventHandler(this.txtDescription_ZoomChanged);
            this.txtDescription.SizeChanged += new System.EventHandler(this.txtDescription_SizeChanged);
            // 
            // UcWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDescription);
            this.Name = "UcWord";
            this.Size = new System.Drawing.Size(707, 491);
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraRichEdit.RichEditControl txtDescription;
    }
}
