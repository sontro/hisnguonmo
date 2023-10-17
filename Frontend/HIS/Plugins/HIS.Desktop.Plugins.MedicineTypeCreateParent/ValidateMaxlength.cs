using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeCreateParent
{
    class ValidateMaxlength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtEdit;
        internal bool required;
        internal long maxLenght;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtEdit == null) return valid;

                if (required)
                {
                    if (string.IsNullOrEmpty(txtEdit.Text))
                    {
                        this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                        return valid;
                    }
                }

                if (maxLenght > 0)
                {
                    if (Inventec.Common.String.CountVi.Count(txtEdit.Text) > maxLenght)
                    {
                        this.ErrorText = "Độ dài ký tự vượt quá " + maxLenght;
                        return valid;
                    }
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }
    }
}

