using HIS.Desktop.Common;
using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.AssignPrescription
{
    public partial class frmEditDayNum : Form
    {
        DelegateSelectData ReloadData;

        public frmEditDayNum(HIS.Desktop.Common.DelegateSelectData _reloadData)
        {
            InitializeComponent();
            try
            {
                this.ReloadData = _reloadData;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }

        private void barButtonItemCtrlS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (btnSave.Enabled)
                {
                    btnSave_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (spinDayNum.EditValue == null || spinDayNum.Value <= 0)
                { 
                    MessageBox.Show("Số ngày phải lớn hơn 0",HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao),MessageBoxButtons.OK,
MessageBoxIcon.Warning );
                    return;
                }

                if (this.ReloadData != null)
                    this.ReloadData(spinDayNum.Value);

                this.Close();

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }

        private void SetIcon()
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(LocalStorage.Location.ApplicationStoreLocation.ApplicationDirectory, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmEditDayNum_Load(object sender, EventArgs e)
        {
            try
            {
                SetIcon();
                SetCaptionByLanguageKey();
                spinDayNum.EditValue = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


        /// <summary>
        ///Hàm xét ngôn ngữ cho giao diện frmEditDayNum
        /// </summary>
        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguagefrmEditDayNum = new ResourceManager("HIS.Desktop.Plugins.AssignPrescriptionPK.Resources.Lang", typeof(frmEditDayNum).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmEditDayNum.layoutControl1.Text", Resources.ResourceLanguageManager.LanguagefrmEditDayNum, LanguageManager.GetCulture());
                this.btnSave.Text = Inventec.Common.Resource.Get.Value("frmEditDayNum.btnSave.Text", Resources.ResourceLanguageManager.LanguagefrmEditDayNum, LanguageManager.GetCulture());
                this.layoutControlItem1.Text = Inventec.Common.Resource.Get.Value("frmEditDayNum.layoutControlItem1.Text", Resources.ResourceLanguageManager.LanguagefrmEditDayNum, LanguageManager.GetCulture());
                this.bar1.Text = Inventec.Common.Resource.Get.Value("frmEditDayNum.bar1.Text", Resources.ResourceLanguageManager.LanguagefrmEditDayNum, LanguageManager.GetCulture());
                this.barButtonItemCtrlS.Caption = Inventec.Common.Resource.Get.Value("frmEditDayNum.barButtonItemCtrlS.Caption", Resources.ResourceLanguageManager.LanguagefrmEditDayNum, LanguageManager.GetCulture());
                this.Text = Inventec.Common.Resource.Get.Value("frmEditDayNum.Text", Resources.ResourceLanguageManager.LanguagefrmEditDayNum, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


        private void spinDayNum_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSave_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }
    }
}
