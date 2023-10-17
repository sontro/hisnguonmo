using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Resources;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.MessageBoxForm
{
    public partial class frmMessageBoxChooseVT : DevExpress.XtraEditors.XtraForm
    {
        ChonVTTrongKho chonVTTrongKho;
        public frmMessageBoxChooseVT(ChonVTTrongKho _chonThuocTrongKhoCungHoatChat)
        {
            InitializeComponent();
            this.chonVTTrongKho = _chonThuocTrongKhoCungHoatChat;
            SetCaptionByLanguageKey();
        }

        private void frmMessageBoxChooseMedicineTypeAcin_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.chonVTTrongKho == null)
                {
                    throw new ArgumentNullException("chonThuocTrongKhoCungHoatChat is null");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        /// <summary>
        ///Hàm xét ngôn ngữ cho giao diện frmMessageBoxChooseVT
        /// </summary>
        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseVT = new ResourceManager("HIS.Desktop.Plugins.AssignPrescriptionPK.Resources.Lang", typeof(frmMessageBoxChooseVT).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.lblDescription.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseVT.lblDescription.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseVT, LanguageManager.GetCulture());
                this.btnChonThuocNgoaiKho.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseVT.btnChonThuocNgoaiKho.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseVT, LanguageManager.GetCulture());
                this.btnCancel.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseVT.btnCancel.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseVT, LanguageManager.GetCulture());
                this.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseVT.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseVT, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }




        private void btnChonThuocNgoaiKho_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chonVTTrongKho != null)
                {
                    this.chonVTTrongKho(EnumOptionChonVatTuThayThe.VatTuNgoaiKho);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chonVTTrongKho != null)
                {
                    this.chonVTTrongKho(EnumOptionChonVatTuThayThe.None);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}