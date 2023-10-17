namespace HIS.Desktop.Plugins.ServiceExecute.UcWords
{
    partial class UcTelerik
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
            this.radRichTextEditorRuler1 = new Telerik.WinControls.UI.RadRichTextEditorRuler();
            this.radRichTextEditor1 = new Telerik.WinControls.UI.RadRichTextEditor();
            ((System.ComponentModel.ISupportInitialize)(this.radRichTextEditorRuler1)).BeginInit();
            this.radRichTextEditorRuler1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radRichTextEditor1)).BeginInit();
            this.SuspendLayout();
            // 
            // radRichTextEditorRuler1
            // 
            this.radRichTextEditorRuler1.AssociatedRichTextBox = this.radRichTextEditor1;
            this.radRichTextEditorRuler1.Controls.Add(this.radRichTextEditor1);
            this.radRichTextEditorRuler1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radRichTextEditorRuler1.Location = new System.Drawing.Point(0, 0);
            this.radRichTextEditorRuler1.Name = "radRichTextEditorRuler1";
            this.radRichTextEditorRuler1.Size = new System.Drawing.Size(700, 490);
            this.radRichTextEditorRuler1.TabIndex = 0;
            // 
            // radRichTextEditor1
            // 
            this.radRichTextEditor1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            this.radRichTextEditor1.LayoutMode = Telerik.WinForms.Documents.Model.DocumentLayoutMode.Paged;
            this.radRichTextEditor1.Location = new System.Drawing.Point(29, 29);
            this.radRichTextEditor1.Name = "radRichTextEditor1";
            this.radRichTextEditor1.SelectionFill = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(78)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.radRichTextEditor1.Size = new System.Drawing.Size(670, 460);
            this.radRichTextEditor1.TabIndex = 0;
            this.radRichTextEditor1.ScaleFactorChanged += new System.EventHandler(this.radRichTextEditor1_ScaleFactorChanged);
            // 
            // UcTelerik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radRichTextEditorRuler1);
            this.Name = "UcTelerik";
            this.Size = new System.Drawing.Size(700, 490);
            ((System.ComponentModel.ISupportInitialize)(this.radRichTextEditorRuler1)).EndInit();
            this.radRichTextEditorRuler1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radRichTextEditor1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadRichTextEditorRuler radRichTextEditorRuler1;
        internal Telerik.WinControls.UI.RadRichTextEditor radRichTextEditor1;
    }
}
