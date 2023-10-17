using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ValidationRules
{
    class ValidationPatient : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtPatientCode;
        internal DevExpress.XtraEditors.LookUpEdit cboPatient;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtPatientCode == null || cboPatient == null) return valid;
                if (txtPatientCode.Enabled && txtPatientCode.Visible && (string.IsNullOrEmpty(txtPatientCode.Text) || string.IsNullOrEmpty(cboPatient.Text))) return valid;

                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }
    }
}
