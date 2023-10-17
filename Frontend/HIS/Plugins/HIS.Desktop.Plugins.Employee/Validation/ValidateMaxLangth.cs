using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.Employee.Validation
{
    class ValidateMaxLangth : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit textedit;
        internal int? maxlength;
        public override bool Validate(Control control, object value)
        {
            bool validate = false;
            try
            {
                if (textedit == null) return validate;
                if (!String.IsNullOrEmpty(textedit.Text) && Inventec.Common.String.CountVi.Count(textedit.Text) > maxlength)
                {
                    this.ErrorText = "Vượt quá độ dài cho phép (" + maxlength + ")";
                    this.ErrorType = ErrorType.Warning;
                    return validate;
                }
                validate = true;
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return validate;
        }
    }
}
