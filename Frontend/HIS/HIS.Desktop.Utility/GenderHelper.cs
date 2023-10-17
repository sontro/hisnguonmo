using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Utility
{
    public class GenderHelper
    {
        public static long GenerateGenderByPatientName(string patientName)
        {
            long genderId = 0;
            try
            {
                int idx = patientName.LastIndexOf(" ");
                string LAST_NAME = (idx > -1 ? patientName.Substring(0, idx).Trim() : "");
                if (!String.IsNullOrEmpty(LAST_NAME))
                {
                    var nameArr = LAST_NAME.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (nameArr != null && nameArr.Length > 1)
                    {
                        if (nameArr[1].ToLower() == "văn")
                        {
                            genderId = IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE;
                        }
                        else if (nameArr[1].ToLower() == "thị")
                        {
                            genderId = IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return genderId;
        }
    }
}
