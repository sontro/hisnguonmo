using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialTypeCreate.Validtion
{
    class HeinLimitRatioValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtHeinLimitRatio;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtHeinLimitRatio == null)
                    return valid;
                if (!String.IsNullOrEmpty(txtHeinLimitRatio.Text) && (CustomParse.ConvertCustom(txtHeinLimitRatio.Text) < 0 || CustomParse.ConvertCustom(txtHeinLimitRatio.Text) > 100))
                {
                    ErrorText = "Giá trị nằm trong khoảng 0 - 100";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                else
                {
                    valid = true;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
