using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PharmacyCashier.Validation
{
    class InvoiceAccountBookServiceValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal frmPharmacyCashier frmMain;
        internal DevExpress.XtraEditors.TextEdit txtInvoiceAccountBookService;
        internal DevExpress.XtraEditors.LookUpEdit cboInvoiceAccountBookService;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (frmMain == null || txtInvoiceAccountBookService == null || cboInvoiceAccountBookService == null) return valid;
                if ((frmMain.listSereServAdo != null && frmMain.listSereServAdo.Any(a => a.IsInvoiced))
                    && (String.IsNullOrEmpty(txtInvoiceAccountBookService.Text) || cboInvoiceAccountBookService.EditValue == null))
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
