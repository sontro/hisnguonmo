using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.HisServiceCondition.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceCondition.Validations
{
    class ValidationSpinDemacialNo : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spnNumberLimitDemacialNo;

        internal string Errtext = "";
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                string stringSpinValue = spnNumberLimitDemacialNo.Value.ToString();
                int index = stringSpinValue.IndexOf(',');
                if (index > 0 && stringSpinValue.Length > index + 3)
                {
                    ErrorText = ResourceMessage.ChiChoPhep2ChuSoSauDauThapPhan;
                    Errtext = ResourceMessage.ChiChoPhep2ChuSoSauDauThapPhan;
                    return valid;
                    
                }
                if ((float)spnNumberLimitDemacialNo.Value < 0 || (float)(spnNumberLimitDemacialNo.Value) > 100)
                {
                    ErrorText = ResourceMessage.GiaTriNamNgoaiKhoangChoPhep;
                    Errtext = ResourceMessage.GiaTriNamNgoaiKhoangChoPhep;
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
