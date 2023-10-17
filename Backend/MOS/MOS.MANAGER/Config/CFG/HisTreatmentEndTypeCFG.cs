using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisServiceReqType;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisTreatmentEndType;

namespace MOS.MANAGER.Config
{
    public class HisTreatmentEndTypeCFG
    {
        private static List<HIS_TREATMENT_END_TYPE> data;
        public static List<HIS_TREATMENT_END_TYPE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisTreatmentEndTypeGet().Get(new HisTreatmentEndTypeFilterQuery());
                }
                return data;
            }
        }

        
        public static void Reload()
        {
            var tmp = new HisTreatmentEndTypeGet().Get(new HisTreatmentEndTypeFilterQuery());
            data = tmp;
        }
    }
}
