using HIS.Desktop.Plugins.TransactionBillTwoInOne.ADO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne.Validation
{
    class InvoicePayFormValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal List<VHisSereServADO> listData;
        internal DevExpress.XtraEditors.TextEdit txtInvoicePayFormCode;
        internal DevExpress.XtraEditors.TextEdit cboInvoicePayForm;
        internal DevExpress.XtraEditors.CheckEdit checkNotInvoice;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtInvoicePayFormCode == null || cboInvoicePayForm == null || checkNotInvoice == null) return valid;
                if (listData != null && listData.Count > 0 && !checkNotInvoice.Checked)
                {
                    if (String.IsNullOrEmpty(txtInvoicePayFormCode.Text) || cboInvoicePayForm.EditValue == null)
                    {
                        ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
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
