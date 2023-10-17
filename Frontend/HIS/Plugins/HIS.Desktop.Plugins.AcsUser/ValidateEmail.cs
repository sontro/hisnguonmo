using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace HIS.Desktop.Plugins.AcsUser
{
    class ValidateEmail : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txt;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txt == null) return valid;
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    try
                    {
                        MailAddress m = new MailAddress(txt.Text);
                    }
                    catch (FormatException)
                    {
                        return valid;
                    }
                    return true;
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
