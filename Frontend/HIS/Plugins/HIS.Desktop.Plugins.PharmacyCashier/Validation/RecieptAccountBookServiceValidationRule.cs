using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PharmacyCashier.Validation
{
    class RecieptAccountBookServiceValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal frmPharmacyCashier frmMain;
        internal DevExpress.XtraEditors.TextEdit txtRecieptAccountBookService;
        internal DevExpress.XtraEditors.LookUpEdit cboRecieptAccountBookService;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (frmMain == null || txtRecieptAccountBookService == null || cboRecieptAccountBookService == null) return valid;
                if ((frmMain.listSereServAdo != null && frmMain.listSereServAdo.Any(a => a.IsReciepted))
                    && (String.IsNullOrEmpty(txtRecieptAccountBookService.Text) || cboRecieptAccountBookService.EditValue == null))
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
