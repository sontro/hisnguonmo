using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTreatmentEndType
{
    class ValidateCheckEdit : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.CheckEdit check1;
        internal DevExpress.XtraEditors.CheckEdit check2;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (check1 == null || check2 == null) return valid;
                if (!check1.Checked && !check2.Checked)
                {
                    this.ErrorText = "Bắt buộc chọn khám hoặc nhập viện";
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
