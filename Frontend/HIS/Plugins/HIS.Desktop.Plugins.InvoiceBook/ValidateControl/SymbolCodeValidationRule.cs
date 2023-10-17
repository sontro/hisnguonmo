using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFS.APP.Modules.InvoiceBook.ValidationControls
{
    class SymbolCodeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtName;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtName == null) return valid;
                if (String.IsNullOrEmpty(txtName.Text)) return valid;
                if (txtName.Text.Length > 8)
                {
                    this.ErrorText = HIS.Desktop.Plugins.InvoiceBook.Resources.ResourceMessage.GiaTriLoaiSoTrongKhoangName;
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
