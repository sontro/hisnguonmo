using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RationSchedule.Validtion
{
    class GridLookupWithTextValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit gridLookUpEdit;
        internal DevExpress.XtraEditors.TextEdit txt;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (gridLookUpEdit == null || txt == null)
                    return valid;
                if (gridLookUpEdit.EditValue == null || string.IsNullOrEmpty(txt.Text.Trim()))
                {
                    ErrorText = "Trường dữ liệu bắt buộc";
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
