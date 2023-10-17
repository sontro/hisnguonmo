using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BloodTransfusion.Validate
{
    class ValidateTransfusionVolume : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spin;
        internal decimal? bloodVolume;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spin.EditValue != null && bloodVolume != null && spin.Value > bloodVolume)
                {
                    this.ErrorText = "Dung tích truyền không được lớn hơn dung tích túi máu";
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
