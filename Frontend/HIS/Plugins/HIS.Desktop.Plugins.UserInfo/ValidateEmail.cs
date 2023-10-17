using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace HIS.Desktop.Plugins.UserInfo
{
    class ValidateEmail : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txt;
        // kiem tra su ton tai cua email
        public override bool Validate(System.Windows.Forms.Control control,Object value)
        {
            bool valid = false;
            try
            {
                if (txt.Text == null || txt.Text=="")
                {
                    valid = true;
                    return valid;
                } 
                if (!String.IsNullOrEmpty(txt.Text))
                {
                    try
                    {
                        MailAddress mail = new MailAddress(txt.Text);

                    }
                    catch (FormatException)
                    {
                        return valid;
                    }
                    return true;
                }
            }
            catch (Exception e)
            { 
                Inventec.Common.Logging.LogSystem.Error(e);
            }
            return valid;
        }

    }
}
