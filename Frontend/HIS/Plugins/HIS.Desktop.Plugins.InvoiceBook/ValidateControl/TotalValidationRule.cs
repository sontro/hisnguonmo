using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFS.APP.Modules.InvoiceBook.ValidationControls
{
    class TotalValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinTotal;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinTotal == null) return valid;
                if (String.IsNullOrEmpty(spinTotal.Text)) return valid;
                if (spinTotal.Value < 1 || spinTotal.Text.Length > 19)
                {
                    this.ErrorText = HIS.Desktop.Plugins.InvoiceBook.Resources.ResourceMessage.GiaTriLonHon0;
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
