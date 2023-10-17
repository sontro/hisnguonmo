using Inventec.Common.Logging;
using System;
using System.Linq;

namespace MOS.MANAGER.Config
{
    class HisMediRecordCFG
    {
        /// <summary>
        /// Tu dong "nhap kho" khi tao benh an ngoai tru
        /// </summary>
        private const string AUTO_STORED_WITH_OUTPATIENT_MEDI_RECORD_CFG = "MOS.HIS_MEDI_RECORD.AUTO_STORED_WITH_OUTPATIENT_MEDI_RECORD";

        private const string RESET_DATE_WITH_OUTPATIENT_CFG = "MOS.HIS_MEDI_RECORD.RESET_DATE_WITH_OUTPATIENT";


        private static bool? autoStoredWithOutpatientMediRecord;
        public static bool AUTO_STORED_WITH_OUTPATIENT_MEDI_RECORD
        {
            get
            {
                if (!autoStoredWithOutpatientMediRecord.HasValue)
                {
                    autoStoredWithOutpatientMediRecord = ConfigUtil.GetIntConfig(AUTO_STORED_WITH_OUTPATIENT_MEDI_RECORD_CFG) == 1;
                }
                return autoStoredWithOutpatientMediRecord.Value;
            }
        }

        private static string resetDateWithOutPatient;
        internal static string RESET_DATE_WITH_OUTPATIENT
        {
            get
            {
                if (String.IsNullOrWhiteSpace(resetDateWithOutPatient))
                {
                    resetDateWithOutPatient = ConfigUtil.GetStrConfig(RESET_DATE_WITH_OUTPATIENT_CFG);
                }
                return resetDateWithOutPatient;
            }
        }



        public static void Reload()
        {
            autoStoredWithOutpatientMediRecord = ConfigUtil.GetIntConfig(AUTO_STORED_WITH_OUTPATIENT_MEDI_RECORD_CFG) == 1;
            resetDateWithOutPatient = ConfigUtil.GetStrConfig(RESET_DATE_WITH_OUTPATIENT_CFG);
        }
    }
}
