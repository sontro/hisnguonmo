using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk.Config
{
    internal class HisConfigCFG
    {
        private const string CONFIG_KEY__TIME_NO_EXECUTE_KIOS = "CONFIG_KEY__HIS_DESKTOP__TIME_NO_EXECUTE_KIOS";
        private const string CONFIG_KEY__DEFAULT_BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";
        private const string CONFIG_KEY__IS_SET_PRIMARY_PATIENT_TYPE = "MOS.HIS_SERE_SERV.IS_SET_PRIMARY_PATIENT_TYPE";
        public const string CONFIG_KEY__NotDisplayedRouteTypeOver = "HIS.Desktop.Plugins.Register.NotDisplayedRouteTypeOver";
        private const string CONFIG_KEY__HOSPITAL_FEE = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE";
        private const string CHECK_PREVIOUS_DEBT_OPTION_CFG = "MOS.HIS_TREATMENT.CHECK_PREVIOUS_DEBT_OPTION";
        public const string CONFIG_KEY__DoNotAllowToEditDefaultRouteType = "HIS.Desktop.Plugins.RegisterExamKiosk.DoNotAllowToEditDefaultRouteType";
        public const string HIS_CAREER_CODE__BASE = "EXE.HIS_CAREER_CODE__BASE";
        private static string checkPreviousDebtOption;
        public static string CHECK_PREVIOUS_DEBT_OPTION
        {
            get
            {
                if (checkPreviousDebtOption == null)
                {
                    checkPreviousDebtOption = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CHECK_PREVIOUS_DEBT_OPTION_CFG);
                }
                return checkPreviousDebtOption;
            }
            set
            {
                checkPreviousDebtOption = value;
            }
        }

        private static long patientTypeIdIsFee;
        public static long PATIENT_TYPE_ID__IS_FEE
        {
            get
            {
                if (patientTypeIdIsFee == 0)
                {
                    patientTypeIdIsFee = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__HOSPITAL_FEE));
                }
                return patientTypeIdIsFee;
            }
            set
            {
                patientTypeIdIsFee = value;
            }
        }

        private static long patientTypeIdIsHein;
        public static long PATIENT_TYPE_ID__BHYT
        {
            get
            {
                if (patientTypeIdIsHein == 0)
                {
                    patientTypeIdIsHein = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__DEFAULT_BHYT));
                }
                return patientTypeIdIsHein;
            }
            set
            {
                patientTypeIdIsHein = value;
            }
        }

        public static long PrimaryPatientType
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<long>(CONFIG_KEY__IS_SET_PRIMARY_PATIENT_TYPE);
            }
        }

        internal static int timeWaitingMilisecond
        {
            get
            {
                int waitTime = ConfigApplicationWorker.Get<int>(CONFIG_KEY__TIME_NO_EXECUTE_KIOS);
                if (waitTime == 0)
                    return 300000;
                else
                    return waitTime;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                var data = BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE == code);
                if (!(data != null && data.ID > 0)) throw new ArgumentNullException(code + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code));
                result = data.ID;
            }
            catch (Exception ex)
            {
                LogSystem.Debug(ex);
                result = 0;
            }
            return result;
        }
    }
}
