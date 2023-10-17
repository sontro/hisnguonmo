using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ServiceExecute.ViewImage
{
    public partial class FormViewImage : Form
    {
        private ADO.ImageADO ImagePreview;
        private List<ADO.ImageADO> ListImage;

        public FormViewImage()
        {
            InitializeComponent();
        }

        public FormViewImage(List<ADO.ImageADO> listImage, ADO.ImageADO imagePreview)
            : this()
        {
            try
            {
                ImagePreview = imagePreview;
                ListImage = listImage;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void FormViewImage_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessListDataImage();

                ProcessImagePreview();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessListDataImage()
        {
            try
            {
                if (ListImage != null && ListImage.Count > 0)
                {
                    foreach (var image in ListImage)
                    {
                        TileItem tileNew = new TileItem();
                        tileNew.Image = image.IMAGE_DISPLAY;
                        tileNew.ImageScaleMode = TileItemImageScaleMode.Stretch;
                        tileNew.Name = image.FileName;
                        tileNew.ItemClick += TileItemClick;
                        if (ImagePreview != null && image == ImagePreview)
                        {
                            tileNew.Checked = true;
                        }
                        tileGroup2.Items.Add(tileNew);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void TileItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                foreach (var item in tileGroup2.Items)
                {
                    item.Checked = false;
                }
                PictureEdit.Image = e.Item.Image;
                PictureEdit.Properties.ZoomPercent = 100;
                e.Item.Checked = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessImagePreview()
        {
            try
            {
                if (ImagePreview != null)
                {
                    PictureEdit.Image = ImagePreview.IMAGE_DISPLAY;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
