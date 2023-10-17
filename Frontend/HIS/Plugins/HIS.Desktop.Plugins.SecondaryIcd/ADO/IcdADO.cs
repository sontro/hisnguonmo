using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SecondaryIcd.ADO
{
    public class IcdADO : MOS.EFMODEL.DataModels.HIS_ICD
    {
        public bool IsChecked { get; set; }

        public IcdADO(MOS.EFMODEL.DataModels.HIS_ICD icd, string icdCodes)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<IcdADO>(this, icd);
                if (!String.IsNullOrEmpty(icdCodes) && icdCodes.Contains(SecondaryIcdUtil.AddSeperateToKey(this.ICD_CODE)))
                {
                    this.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public IcdADO(MOS.EFMODEL.DataModels.HIS_ICD_CM icd, string icdCodes)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<IcdADO>(this, icd);
                this.ICD_CODE = icd.ICD_CM_CODE;
                this.ICD_NAME = icd.ICD_CM_NAME;
                this.CHAPTER_CODE = icd.ICD_CM_CHAPTER_CODE;
                this.CHAPTER_NAME = icd.ICD_CM_CHAPTER_NAME;
                this.GROUP_CODE = icd.ICD_CM_GROUP_CODE;
                //this.GROUP_NAME = icd.ICD_CM_GROUP_NAME;

                if (!String.IsNullOrEmpty(icdCodes) && icdCodes.Contains(SecondaryIcdUtil.AddSeperateToKey(this.ICD_CODE)))
                {
                    this.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
