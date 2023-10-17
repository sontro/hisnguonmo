namespace HIS.Desktop.Plugins.MaterialType.MaterialTypeList
{
    partial class UCMaterialTypeList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMaterialTypeList));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("edit_16x16.png", "office2013/edit/edit_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/edit/edit_16x16.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "edit_16x16.png");
            this.imageCollection1.InsertGalleryImage("close_16x16.png", "devav/actions/close_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("devav/actions/close_16x16.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "close_16x16.png");
            this.imageCollection1.Images.SetKeyName(2, "hmenu-unlock.gif");
            this.imageCollection1.Images.SetKeyName(3, "hmenu-lock.png");
            // 
            // UCMaterialTypeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCMaterialTypeList";
            this.Size = new System.Drawing.Size(1320, 550);
            this.Load += new System.EventHandler(this.UCMaterialTypeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
