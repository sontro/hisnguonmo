using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDA.Desktop.Plugins.SdaExecuteSql
{
    class ValidateRequired : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal UserControl uc;
        internal string str;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc";
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
