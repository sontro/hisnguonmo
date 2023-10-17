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
using HIS.Desktop.Plugins.AssignPrescriptionPK.Config;
using Inventec.Desktop.Common.LanguageManager;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK.MessageBoxForm
{
    public partial class frmMessageBoxChooseThuocExceAmout : DevExpress.XtraEditors.XtraForm
    {
        ChonThuocTrongKhoCungHoatChat chonThuocTrongKhoCungHoatChat;
        string medicineTypeAcin__SameAcinBhyt = "";
        bool IsChooseOption = false;
        public frmMessageBoxChooseThuocExceAmout(ChonThuocTrongKhoCungHoatChat _chonThuocTrongKhoCungHoatChat, string _medicineTypeAcin__SameAcinBhyt)
        {
            InitializeComponent();

            try
            {
                this.chonThuocTrongKhoCungHoatChat = _chonThuocTrongKhoCungHoatChat;
                this.medicineTypeAcin__SameAcinBhyt = _medicineTypeAcin__SameAcinBhyt;
                SetCaptionByLanguageKey();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmMessageBoxChooseThuocExceAmout_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.chonThuocTrongKhoCungHoatChat == null)
                {
                    throw new ArgumentNullException("chonThuocTrongKhoCungHoatChat is null");
                }

                if (!String.IsNullOrEmpty(this.medicineTypeAcin__SameAcinBhyt))
                {
                    btnChonThuocCungHoatChat.Visible = true;
                }
                else
                {
                    btnChonThuocCungHoatChat.Visible = false;
                }

                if (!HisConfigCFG.IsAutoCreateSaleExpMest && !HisConfigCFG.IsExceedAvailableOutStock)
                {
                    btnGiuNguyen.Visible = true;
                }
                else
                {
                    btnGiuNguyen.Visible = false;
                }


                if (btnChonThuocCungHoatChat.Visible)
                {
                    btnChonThuocCungHoatChat.Focus();
                }
                else if (btnGiuNguyen.Visible)
                {
                    btnGiuNguyen.Focus();
                }
                else
                {
                    btnSuaSoluong.Focus();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


        /// <summary>
        ///Hàm xét ngôn ngữ cho giao diện frmMessageBoxChooseThuocExceAmout
        /// </summary>
        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseThuocExceAmout = new ResourceManager("HIS.Desktop.Plugins.AssignPrescriptionPK.Resources.Lang", typeof(frmMessageBoxChooseThuocExceAmout).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.lblDescription.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseThuocExceAmout.lblDescription.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseThuocExceAmout, LanguageManager.GetCulture());
                this.btnChonThuocCungHoatChat.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseThuocExceAmout.btnChonThuocCungHoatChat.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseThuocExceAmout, LanguageManager.GetCulture());
                this.btnGiuNguyen.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseThuocExceAmout.btnGiuNguyen.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseThuocExceAmout, LanguageManager.GetCulture());
                this.btnSuaSoluong.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseThuocExceAmout.btnSuaSoluong.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseThuocExceAmout, LanguageManager.GetCulture());
                this.Text = Inventec.Common.Resource.Get.Value("frmMessageBoxChooseThuocExceAmout.Text", Resources.ResourceLanguageManager.LanguageResourcefrmMessageBoxChooseThuocExceAmout, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnChonThuocCungHoatChat_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chonThuocTrongKhoCungHoatChat != null)
                {
                    this.chonThuocTrongKhoCungHoatChat(OptionChonThuocThayThe.ThuocCungHoatChat);
                    this.IsChooseOption = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnGiuNguyen_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chonThuocTrongKhoCungHoatChat != null)
                {
                    this.chonThuocTrongKhoCungHoatChat(OptionChonThuocThayThe.None);
                    this.IsChooseOption = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnSuaSoluong_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chonThuocTrongKhoCungHoatChat != null)
                {
                    this.chonThuocTrongKhoCungHoatChat(OptionChonThuocThayThe.SuaSoLuong);
                    this.IsChooseOption = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmMessageBoxChooseThuocExceAmout_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if(!IsChooseOption)
                    this.chonThuocTrongKhoCungHoatChat(OptionChonThuocThayThe.NoOption);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
