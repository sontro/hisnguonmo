using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InfantInformation.Validate
{
    class MonthValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spInfantMonth;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                ////if (String.IsNullOrEmpty(spInfantMonth.Text))
                //    return true;
                if (spInfantMonth == null) return valid;
                if (spInfantMonth.Value <= 0) return valid;

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
    //}internal DevExpress.XtraEditors.SpinEdit spinAmount;

    //    public override bool Validate(System.Windows.Forms.Control control, object value)
    //    {
    //        bool valid = false;
    //        try
    //        {
    //            if (spinAmount == null) return valid;
    //            if (spinAmount.Value < 0) return valid;

    //            valid = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            Inventec.Common.Logging.LogSystem.Error(ex);
    //        }
    //        return valid;
    
    

