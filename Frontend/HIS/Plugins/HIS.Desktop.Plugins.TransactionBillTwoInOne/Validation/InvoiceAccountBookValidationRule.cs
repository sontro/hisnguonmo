using HIS.Desktop.Plugins.TransactionBillTwoInOne.ADO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne.Validation
{
    class InvoiceAccountBookValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal List<VHisSereServADO> listData = new List<VHisSereServADO>();
        internal DevExpress.XtraEditors.TextEdit txtInvoiceAccountBookCode;
        internal DevExpress.XtraEditors.TextEdit cboInvoiceAccountBook;
        internal DevExpress.XtraEditors.CheckEdit checkNotInvoice;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtInvoiceAccountBookCode == null || cboInvoiceAccountBook == null || checkNotInvoice == null) return valid;

                if (listData != null && listData.Count > 0 && (!checkNotInvoice.Checked))
                {
                    if (String.IsNullOrEmpty(txtInvoiceAccountBookCode.Text) || cboInvoiceAccountBook.EditValue == null)
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
