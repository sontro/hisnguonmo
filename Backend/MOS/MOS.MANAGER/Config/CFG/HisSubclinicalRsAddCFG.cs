using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSubclinicalRsAdd;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisSubclinicalRsAddCFG
    {
        private static List<HIS_SUBCLINICAL_RS_ADD> data;
        public static List<HIS_SUBCLINICAL_RS_ADD> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisSubclinicalRsAddGet().Get(new HisSubclinicalRsAddFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisSubclinicalRsAddGet().Get(new HisSubclinicalRsAddFilterQuery());
            data = tmp;
        }
    }
}
