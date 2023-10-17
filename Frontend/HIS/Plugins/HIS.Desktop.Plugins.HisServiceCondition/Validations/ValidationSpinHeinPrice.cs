using HIS.Desktop.Plugins.HisServiceCondition.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceCondition.Validations
{
    class ValidationSpinHeinPrice : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spnNumber;

        internal string Errtext = "";
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                string stringSpinValue = spnNumber.Value.ToString();
                if ((float)spnNumber.Value < 0)
                {
                    ErrorText = ResourceMessage.GiaTriPhaiLonHon0;
                    Errtext = ResourceMessage.GiaTriPhaiLonHon0;
                    return valid;

                }
                //if ((long)spnNumberLimitDemacialNo.Value < 0) return valid;
                //if ((long)spnNumberLimitDemacialNo.Value > 100) return valid;
                //if (string.IsNullOrEmpty(spnNumberLimitValue.Text)) return valid;
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
