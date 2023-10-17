using Inventec.Common.Logging;
using MOS.UTILITY;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisIcd;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisIcdCFG
    {
        private const string NdsIcdCodeOtherCFG = "MOS.BHYT.NDS_ICD_CODE__OTHER";
        private const string NdsIcdCodeTeCFG = "MOS.BHYT.NDS_ICD_CODE__TE";

        private static List<HIS_ICD> data;
        public static List<HIS_ICD> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisIcdGet().Get(new HisIcdFilterQuery());
                }
                return data;
            }
        }

        private static List<HIS_ICD> dataByUnableForTreatment;
        public static List<HIS_ICD> DATA_BY_UNABLE_FOR_TREATMENT
        {
            get
            {
                if (dataByUnableForTreatment == null)
                {
                    dataByUnableForTreatment = DATA != null ? DATA.Where(o => o.UNABLE_FOR_TREATMENT.HasValue && o.UNABLE_FOR_TREATMENT.Value == Constant.IS_TRUE).ToList() : null;
                }
                return dataByUnableForTreatment;
            }
        }

        private static List<string> icdCodeNds;
        public static List<string> ListIcdCode_Nds
        {
            get
            {
                if (icdCodeNds == null)
                {
                    icdCodeNds = ConfigUtil.GetStrConfigs(NdsIcdCodeOtherCFG);
                }
                return icdCodeNds;
            }
        }

        private static List<string> icdCodeNdsTe;
        public static List<string> ListIcdCode_Nds_Te
        {
            get
            {
                if (icdCodeNdsTe == null)
                {
                    icdCodeNdsTe = ConfigUtil.GetStrConfigs(NdsIcdCodeTeCFG);
                }
                return icdCodeNdsTe;
            }
        }

        public static void Reload()
        {
            var tmp = new HisIcdGet().Get(new HisIcdFilterQuery());
            data = tmp;
            dataByUnableForTreatment = DATA != null ? DATA.Where(o => o.UNABLE_FOR_TREATMENT.HasValue && o.UNABLE_FOR_TREATMENT.Value == Constant.IS_TRUE).ToList() : null;
            icdCodeNds = ConfigUtil.GetStrConfigs(NdsIcdCodeOtherCFG);
            icdCodeNdsTe = ConfigUtil.GetStrConfigs(NdsIcdCodeTeCFG);
        }
    }
}
