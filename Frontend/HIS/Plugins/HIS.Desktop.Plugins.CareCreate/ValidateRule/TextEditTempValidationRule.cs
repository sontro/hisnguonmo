using HIS.Desktop.Plugins.CareCreate.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CareCreate.ValidateRule
{
    class TextEditTempValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
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
                        this.ErrorText = ResourceMessage.ThieuTruongDuLieuBatBuoc;
                        return valid;
                    }
                }

                if (maxLenght > 0)
                {
                    if (Inventec.Common.String.CountVi.Count(txtEdit.Text) > maxLenght)
                    {
                        this.ErrorText = ResourceMessage.DoDaiVuotQua + maxLenght;
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
