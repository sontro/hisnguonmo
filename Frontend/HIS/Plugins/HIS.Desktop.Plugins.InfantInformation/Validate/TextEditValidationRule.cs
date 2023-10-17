using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
    class TextEditValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spin;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spin == null)
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

    //class TextEditValidationRule1 : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    //{
    //    internal DevExpress.XtraEditors.TextEdit txtPatientsothang;
    //    int max = 9;
    //    public TextEditValidationRule1()
    //    {
    //    }
    //    private void textEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
    //    {
    //        if (e.NewValue != null)
    //            if (e.NewValue.ToString().Length > 50)
    //                e.Cancel = true;
    //    }
    //}



    //    public override bool Validate(Control control, object value)
    //    {
    //        bool valid = false;
    //        try
    //        {
    //            if (String.IsNullOrEmpty(txtPatientsothang.Text))
    //                return valid;
    //            int number = 0;
    //            int.TryParse(txtPatientsothang.Text, out number);
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
