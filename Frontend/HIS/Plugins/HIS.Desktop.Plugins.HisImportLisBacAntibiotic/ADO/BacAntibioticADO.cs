using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HIS.Desktop.Plugins.HisImportLisBacAntibiotic.ADO
{
    public class BacAntibioticADO : LIS_BAC_ANTIBIOTIC
    {
        public string MIC_STR { get; set; }
        public string ERROR { get; set; }
        public string ANTIBIOTIC_CODE { get; set; }
        public string ANTIBIOTIC_NAME { get; set; }
        public string BACTERIUM_CODE { get; set; }
        public string BACTERIUM_NAME { get; set; }

        public BacAntibioticADO()
        {
        }

        public BacAntibioticADO(LIS_BAC_ANTIBIOTIC data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<BacAntibioticADO>(this, data);
        }
    }
}
