using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAssignBlood.ADO
{
    public class BloodADO : MOS.EFMODEL.DataModels.V_HIS_BLOOD
    {
        public decimal? AMOUNT { get; set; }
        public string SERVICE_CODE_HIDDEN { get; set; }
        public string SERVICE_NAME_HIDDEN { get; set; }
        public BloodADO() : base()
        {
            this.SERVICE_NAME_HIDDEN = convertToUnSign3(this.BLOOD_TYPE_NAME) + this.BLOOD_TYPE_NAME;
            this.SERVICE_CODE_HIDDEN = convertToUnSign3(this.BLOOD_TYPE_CODE) + this.BLOOD_TYPE_CODE;

        }

        public string convertToUnSign3(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return "";

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
