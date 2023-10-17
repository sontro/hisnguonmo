using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class MemoTextValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.MemoEdit MemoEdit;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (MemoEdit == null) return valid;
                if (string.IsNullOrEmpty(MemoEdit.Text.Trim()))
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc); ;
                    return valid;
                }
                else if (MemoEdit.Text.Length > 3000)
                {
                    this.ErrorText = Inventec.Common.Resource.Get.Value("TruongThongTinCoDoDaiVuotQuaGioiHan", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                    return valid;
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
