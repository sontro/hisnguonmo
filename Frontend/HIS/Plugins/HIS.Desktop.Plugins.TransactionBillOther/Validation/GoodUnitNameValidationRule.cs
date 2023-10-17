using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.TransactionBillOther.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillOther.Validation
{
    class GoodUnitNameValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtGoodUnitName;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtGoodUnitName == null)
                    return valid;

                if (!String.IsNullOrEmpty(txtGoodUnitName.Text) && txtGoodUnitName.Text.Length >= 100)
                {
                    ErrorText = String.Format(ResourceMessageManager.DoDaiKhongDuocVuotQua, ResourceMessageManager.DonViTinh, 100);
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
