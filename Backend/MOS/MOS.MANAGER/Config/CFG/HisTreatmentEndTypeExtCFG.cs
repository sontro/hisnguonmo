using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisServiceReqType;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisTreatmentEndType;
using MOS.MANAGER.HisTreatmentEndTypeExt;

namespace MOS.MANAGER.Config
{
    public class HisTreatmentEndTypeExtCFG
    {
        private static List<HIS_TREATMENT_END_TYPE_EXT> data;
        public static List<HIS_TREATMENT_END_TYPE_EXT> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisTreatmentEndTypeExtGet().Get(new HisTreatmentEndTypeExtFilterQuery());
                }
                return data;
            }
        }

        
        public static void Reload()
        {
            var tmp = new HisTreatmentEndTypeExtGet().Get(new HisTreatmentEndTypeExtFilterQuery());
            data = tmp;
        }
    }
}
