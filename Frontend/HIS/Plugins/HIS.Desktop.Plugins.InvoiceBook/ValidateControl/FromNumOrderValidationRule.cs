using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFS.APP.Modules.InvoiceBook.ValidationControls
{
    class FromNumOrderValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinFromNumOrder;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinFromNumOrder == null) return valid;
                if (String.IsNullOrEmpty(spinFromNumOrder.Text)) return valid;
                if (spinFromNumOrder.Value < 1 || spinFromNumOrder.Text.Length > 19)
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
