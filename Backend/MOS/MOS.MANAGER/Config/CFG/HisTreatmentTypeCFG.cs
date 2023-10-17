using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatmentType;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisTreatmentTypeCFG
    {
        /// <summary>
        /// Id của các diện điều trị thuộc loại "điều trị" (điều trị nội trú, điều trị ngoại trú)
        /// </summary>
        public static List<long> TREATMENTs = new List<long>() {
            IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU,
            IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU,
            IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY
        };

        private static List<HIS_TREATMENT_TYPE> data;
        public static List<HIS_TREATMENT_TYPE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisTreatmentTypeGet().Get(new HisTreatmentTypeFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisTreatmentTypeGet().Get(new HisTreatmentTypeFilterQuery());
            data = tmp;
        }
    }
}
