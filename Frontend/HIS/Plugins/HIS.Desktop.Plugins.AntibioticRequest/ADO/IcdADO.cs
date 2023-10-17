using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AntibioticRequest
{
    public class IcdADO : MOS.EFMODEL.DataModels.HIS_ICD
    {
        public IcdADO(MOS.EFMODEL.DataModels.HIS_ICD icd, string[] icdCodes)
        {
            try
            {
                if (icd != null)
                {
                    this.ICD_CODE = icd.ICD_CODE;
                    this.ICD_CHAPTER_ID = icd.ICD_CHAPTER_ID;
                    this.ICD_GROUP_ID = icd.ICD_GROUP_ID;
                    this.ICD_NAME = icd.ICD_NAME;
                    this.ICD_NAME_COMMON = icd.ICD_NAME_COMMON;
                    this.ICD_NAME_EN = icd.ICD_NAME_EN;
                    this.ID = icd.ID;
                    this.IS_HEIN_NDS = icd.IS_HEIN_NDS;
                    if (icdCodes != null && icdCodes.Count() > 0 && icdCodes.Contains(this.ICD_CODE))
                    {
                        this.IsChecked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public bool IsChecked { get; set; }
    }
}
