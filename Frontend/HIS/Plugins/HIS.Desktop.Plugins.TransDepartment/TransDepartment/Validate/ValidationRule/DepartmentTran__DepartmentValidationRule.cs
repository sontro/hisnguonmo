using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TransDepartment.Validate.ValidationRule
{
    class DepartmentTran__BedRoomValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtDepartmentCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboDepartment;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtDepartmentCode == null || cboDepartment == null) return valid;

                if ((String.IsNullOrEmpty(txtDepartmentCode.Text) || cboDepartment.EditValue == null))
                    return valid;

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
