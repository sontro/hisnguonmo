
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HIS.Desktop.Plugins.AggrExpMestPrintFilter
{
    internal class AppConfigKeys
    {
        #region Public key

        internal const string CONFIG_KEY__HIS_DESKTOP__CHE_DO_IN_GOP_PHIEU_LINH = "CONFIG_KEY__CHE_DO_IN_GOP_PHIEU_LINH";
        internal const string CONFIG_KEY__HIS_DESKTOP__IN_GOP_GAY_NGHIEN_HUONG_THAN = "CONFIG_KEY__HIS_DESKTOP__IN_GOP_GAY_NGHIEN_HUONG_THAN";

        internal const string CONFIG_KEY__HIS_PHIEU_TRA_DOI_THUOC_COLUMN_SIZE = "HIS.PHIEU_TRA_DOI_THUOC.COLUMN_SIZE";

        #endregion

        internal static long PatientTypeId__BHYT
        {
            get
            {
                return GetPatientTypeBhyt();
            }
        }

        internal static bool IsmergePrint
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HIS.Desktop.Plugins.OptionMergePrint.Ismerge") == "1";
            }
        }

        private static long GetPatientTypeBhyt()
        {
            long result = 0;
            try
            {
                string code = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT");
                if (!String.IsNullOrWhiteSpace(code))
                {
                    var patientType = BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE.ToLower() == code.ToLower().Trim());
                    if (patientType != null)
                    {
                        result = patientType.ID;
                    }
                }
            }
            catch (Exception ex)
            {
                result = 0;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        internal static List<string> ListParentMedicine
        {
            get
            {
                return ProcessListParentConfig();
            }
        }

        private static List<string> ProcessListParentConfig()
        {
            List<string> result = null;
            try
            {
                string code = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HIS.Desktop.Plugins.AggrExpMestPrint.ParentMety");
                if (!String.IsNullOrWhiteSpace(code))
                {
                    result = code.Split(',').ToList();
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
