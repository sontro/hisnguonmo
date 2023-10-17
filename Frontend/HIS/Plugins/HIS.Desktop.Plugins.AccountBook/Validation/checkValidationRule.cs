using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAccountBookList.Validation
{
    class checkValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.CheckEdit chkForRepay;
        internal DevExpress.XtraEditors.CheckEdit chkForDeposit;
        internal DevExpress.XtraEditors.CheckEdit chkForBill;
        internal DevExpress.XtraEditors.TextEdit txtTemplateCode;
        internal DevExpress.XtraEditors.TextEdit txtSymbolCode;
        internal DevExpress.XtraEditors.CheckEdit chkForDebt;
        internal DevExpress.XtraEditors.CheckEdit chkForOtherSale;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                //if ((!String.IsNullOrEmpty(txtTemplateCode.Text) || !String.IsNullOrEmpty(txtSymbolCode.Text))&&chkForBill.Checked == false)
                //{
                //    return valid;
                //}
                if (chkForDeposit.Checked == false && chkForBill.Checked == false && chkForRepay.Checked == false && chkForDebt.Checked == false && chkForOtherSale.Checked ==false) return valid;
                //else if (chkForBill.Checked == false)
                //{
                //    return valid;
                //}
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
