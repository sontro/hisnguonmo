using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Validation
{
    class ValidateWorkPlace : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtWorkPlace;
        internal DevExpress.XtraEditors.GridLookUpEdit cboWorkPlace;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (!String.IsNullOrEmpty(txtWorkPlace.Text) || cboWorkPlace.EditValue != null)
                    return true;
                else if (!String.IsNullOrEmpty(txtWorkPlace.Text) && Encoding.UTF8.GetByteCount(txtWorkPlace.Text) > 100)
                    this.ErrorText = "Vượt quá độ dài cho phép (" + 100 + ")";
                else
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
