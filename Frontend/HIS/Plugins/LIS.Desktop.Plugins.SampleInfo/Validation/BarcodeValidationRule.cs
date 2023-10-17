using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using LIS.Desktop.Plugins.SampleInfo.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.SampleInfo.Validation
{
    class BarcodeValidationRule : ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBarcode;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtBarcode == null) return valid;
                if (LisConfigCFG.IS_AUTO_CREATE_BARCODE != "1" && String.IsNullOrWhiteSpace(txtBarcode.Text))
                {
                    ErrorText = MessageUtil.GetMessage(Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = ErrorType.Warning;
                    return valid;
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
