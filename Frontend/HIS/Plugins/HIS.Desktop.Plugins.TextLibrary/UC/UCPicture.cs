using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;

namespace HIS.Desktop.Plugins.TextLibrary.UC
{
    public partial class UCPicture : UserControl
    {
        string pathImage = null;
        public UCPicture()
        {
            InitializeComponent();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Ảnh(*.jpg, *.Png, *.jpeg,)|*.jpg;*.png;*.jpeg";
                openFile.DefaultExt = ".jpg;.png;.jpeg";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    ptePicture.Image = System.Drawing.Image.FromFile(openFile.FileName);
                    this.pathImage = openFile.FileName;
                    ptePicture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public string ImageToBase64()
        {
            string base64String = null;
            if (pathImage != null)
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(this.pathImage))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            else
            {
                using (MemoryStream m = new MemoryStream())
                {
                    ImageConverter covert = new ImageConverter();
                    Image image = this.ptePicture.Image;
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                   
                }
            }

        }
        public PictureEdit Base64ToImage(string base64String)
        {

            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            ptePicture.Image = System.Drawing.Image.FromStream(ms, true);
            //ptePicture.Image=image;
            return ptePicture;
        }
        public PictureEdit clearImage()
        {
            if (ptePicture.Image != null)
            {
                ptePicture.Image = null;
            }
            return ptePicture;
        }
    }
}
