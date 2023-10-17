using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BloodList.Validate
{
     class ValidCmndCccdHc : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txt;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (!String.IsNullOrEmpty(txt.Text.Trim()) && IsNumber(txt.Text.Trim()) && txt.Text.Trim().Length == 9)
                {
                    //La CMND
                    return true;
                }
                else if (!String.IsNullOrEmpty(txt.Text.Trim()) && IsNumber(txt.Text.Trim()) && txt.Text.Trim().Length == 12)
                {
                    //La CCCD
                    return true;
                }
                else if (!String.IsNullOrEmpty(txt.Text.Trim()) && IsValid(txt.Text.Trim()) && txt.Text.Trim().Length < 10)
                {
                    //La passport
                    return true;
                }
                else if (!String.IsNullOrEmpty(txt.Text.Trim()))
                {
                    this.ErrorText = "CMND/CCCD/Hộ chiếu không đúng định dạng";
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return valid;
        }
        private bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(pText);
        }

        private bool IsValid(string txtCMND)
        {
            bool valid = false;
            var txt = txtCMND;
            int countNumber = 0;
            int total = txt.Length;
            for (int i = 0; i < txt.Length; i++)
            {
                if (IsNumber(txt[i].ToString()))
                {
                    countNumber++;
                }
            }
            if (countNumber == 0)
            {
                valid = false;
            }
            else if (countNumber != 0 && countNumber < total)
            {
                valid = true;
            }
            return valid;
        }
    }
}
