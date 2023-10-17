using HIS.Desktop.Plugins.ExamServiceReqExecute.Base;
using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute
{
    public partial class frmXemGiayChuyenTuyen : Form
    {
        System.IO.MemoryStream stream;
        public frmXemGiayChuyenTuyen(System.IO.MemoryStream _Stream)
        {
            InitializeComponent();
            try
            {
                this.stream = _Stream;
                SetIcon();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void SetIcon()
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon
                  (System.IO.Path.Combine(LocalStorage.Location.ApplicationStoreLocation.ApplicationDirectory, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void frmXemGiayChuyenTuyen_Load(object sender, EventArgs e)
        {
            try
            {
                pictureEdit1.Image = Image.FromStream(stream);
                SetCaptionByLanguageKey();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        ///Hàm xét ngôn ngữ cho giao diện frmXemGiayChuyenTuyen
        /// </summary>
        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                if (ResourceLangManager.LanguageUCExamServiceReqExecute == null)
                {
                    ResourceLangManager.InitResourceLanguageManager();
                }
                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmXemGiayChuyenTuyen.layoutControl1.Text", ResourceLangManager.LanguageUCExamServiceReqExecute, LanguageManager.GetCulture());
                this.Text = Inventec.Common.Resource.Get.Value("frmXemGiayChuyenTuyen.Text", ResourceLangManager.LanguageUCExamServiceReqExecute, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmXemGiayChuyenTuyen_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                stream = null;
                this.Load -= new System.EventHandler(this.frmXemGiayChuyenTuyen_Load);
                layoutControlItem1 = null;
                pictureEdit1 = null;
                layoutControlGroup1 = null;
                layoutControl1 = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
