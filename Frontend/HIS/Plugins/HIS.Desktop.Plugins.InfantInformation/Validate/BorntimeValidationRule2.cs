using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
class BorntimeValidationRule2 : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTextEdit;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (String.IsNullOrEmpty(txtTextEdit.Text)) return true;
                // return valid;
                if (Int64.Parse(txtTextEdit.Text.Substring(0, 2)) > 23 || Int64.Parse(txtTextEdit.Text.Substring(0, 2)) < 0
                    || Int64.Parse(txtTextEdit.Text.Substring(2, 2)) > 59 || Int64.Parse(txtTextEdit.Text.Substring(2, 2)) < 0)
                {
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
