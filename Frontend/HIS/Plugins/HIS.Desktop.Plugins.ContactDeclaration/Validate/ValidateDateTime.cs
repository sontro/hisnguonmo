using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.ContactDeclaration.Validate
{
    class ValidateDateTime : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public DevExpress.XtraEditors.DateEdit dateEdit;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dateEdit == null) return valid;
                if (dateEdit == null || dateEdit.EditValue == null)
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }
                //TimeSpan diff_ = (System.DateTime.Now - dateEdit.DateTime);

                //Inventec.Common.Logging.LogSystem.Warn("diff_.Milliseconds: " + diff_.Milliseconds);
                //if (diff_.Milliseconds < 0)

                if (dateEdit.DateTime > DateTime.Now)
                {
                    this.ErrorText = "Ngày tiếp xúc không được lớn hơn ngày hiện tại";
                    this.ErrorType = ErrorType.Warning;
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
