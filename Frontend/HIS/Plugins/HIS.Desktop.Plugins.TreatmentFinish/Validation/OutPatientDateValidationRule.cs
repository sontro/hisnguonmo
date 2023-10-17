using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class OutPatientDateValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dateEditForValidation;
        internal DevExpress.XtraEditors.DateEdit dateEditRequired;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dateEditRequired == null) return true;
                if (dateEditRequired.EditValue == null)
                {
                    valid = true;
                }
                else
                {
                    if (dateEditForValidation.EditValue != null)
                        valid = true;
                    else
                        valid = false;
                }
            }
            catch (Exception ex)
            {
                valid = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
