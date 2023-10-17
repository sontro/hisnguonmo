using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
class TinhtrangValidationRul : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {

    internal DevExpress.XtraEditors.GridLookUpEdit cbotinhtrang;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cbotinhtrang == null) return valid;
                //if (String.cbotinhtrang.EditValue == null) return valid;
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }


    //class TextEditValidationRule1 : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    //{
    //    internal DevExpress.XtraEditors.TextEdit txtTextEdit;
    //    int max = 9;
    //    public TextEditValidationRule1()
    //    {

    //    }

    //    public override bool Validate(Control control, object value)
    //    {
    //        bool valid = false;
    //        try
    //        {
    //            if (String.IsNullOrEmpty(txtTextEdit.Text))
    //                return valid;
    //            int number = 0;
    //            int.TryParse(txtTextEdit.Text, out number);
    //            if (number > max) return false;
    //            valid = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            Inventec.Common.Logging.LogSystem.Error(ex);
    //        }
    //        return valid;
    //    }
    //}
