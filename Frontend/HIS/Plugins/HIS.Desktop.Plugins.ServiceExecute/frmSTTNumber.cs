using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    public partial class frmSTTNumber : Form
    {
        ADO.ImageADO currentData;
        Action<decimal> actSTTChanged;
        List<ADO.ImageADO> listImage;

        public frmSTTNumber(ADO.ImageADO _currentData, List<ADO.ImageADO> _listImage, Action<decimal> _actSTTChanged)
        {
            InitializeComponent();
            this.currentData = _currentData;
            this.actSTTChanged = _actSTTChanged;
            this.listImage = _listImage;
        }

        private void frmSTTNumber_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetCaptionByLanguageKey();
                if (this.currentData != null)
                {
                    spinSTT.EditValue = this.currentData.STTImage;
                }
                spinSTT.Focus();
                spinSTT.SelectAll();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {                                
                if (this.actSTTChanged != null)
                {
                    this.actSTTChanged(spinSTT.Value);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void spinSTT_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.actSTTChanged != null)
                    {
                        this.actSTTChanged(spinSTT.Value);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        ///Hàm xét ngôn ngữ cho giao diện frmSTTNumber
        /// </summary>
        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResourcefrmSTTNumber = new ResourceManager("HIS.Desktop.Plugins.ServiceExecute.Resources.Lang", typeof(frmSTTNumber).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.btnOK.Text = Inventec.Common.Resource.Get.Value("frmSTTNumber.btnOK.Text", Resources.ResourceLanguageManager.LanguageResourcefrmSTTNumber, LanguageManager.GetCulture());
                this.Text = Inventec.Common.Resource.Get.Value("frmSTTNumber.Text", Resources.ResourceLanguageManager.LanguageResourcefrmSTTNumber, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
