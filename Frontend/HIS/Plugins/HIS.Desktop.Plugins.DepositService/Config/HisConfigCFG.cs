using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DepositService.Config
{
    internal class HisConfigCFG
    {
        private const string His_Desktop_plugins_transactionTime_IsEditTransactionTime = "HIS.Desktop.Plugins.TransactionBill_Depo_Repa.IsEditTransactionTime";
        private const string CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT
        private const string CONFIG_KEY__PATIENT_TYPE_CODE__VP = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE";//Doi tuong VP
        private const string CONFIG_KEY__PAY_FORM__DEFAULT_OPTION = "HIS.HIS_PAY_FORM.DEFAULT_OPTION";
        private const string CONFIG_KEY__AUTOCHECK= "HIS.Desktop.Plugins.DepositService.HideServiceCheckbox.AutoCheckAndDisableOption";
        private const string CONFIG_KEY__SHOW_IN= "MOS.HIS_TRANSACTION.ALLOW_TO_DEPOSIT_INPATIENT_PRESCRIPTION";
        private const string CONFIG_KEY__CASHIER_ROOM_PAYMENT_OPTION = "MOS.EPAYMENT.CASHIER_ROOM_PAYMENT_OPTION";
        private const string HIS_Desktop_ShowServerTimeByDefault = "HIS.Desktop.ShowServerTimeByDefault";
        internal static string ShowServerTimeByDefault;
        internal static string CashierRoomPaymentOption;
        internal static string IsEditTransactionTimeCFG;

        internal static string PatientTypeCode__BHYT;
        internal static long PatientTypeId__BHYT;
        internal static string PatientTypeCode__VP;
        internal static long PatientTypeId__VP;
        internal static bool PayForm__DefaultOption = false;
        internal static bool AutoCheckAndDisableOption = false;
        internal static bool IsShowInPatientPrescriptionOption = false;
        internal static void LoadConfig()
        {
            try
            {
                CashierRoomPaymentOption = GetValue(CONFIG_KEY__CASHIER_ROOM_PAYMENT_OPTION);
                LogSystem.Debug("LoadConfig => 1");
                IsEditTransactionTimeCFG = GetValue(His_Desktop_plugins_transactionTime_IsEditTransactionTime);
                LogSystem.Debug("LoadConfig => 2");
                ShowServerTimeByDefault = GetValue(HIS_Desktop_ShowServerTimeByDefault);
                PatientTypeCode__BHYT = GetValue(CONFIG_KEY__PATIENT_TYPE_CODE__BHYT);
                PatientTypeId__BHYT = GetPatientTypeByCode(PatientTypeCode__BHYT).ID;
                PatientTypeCode__VP = GetValue(CONFIG_KEY__PATIENT_TYPE_CODE__VP);
                PatientTypeId__VP = GetPatientTypeByCode(PatientTypeCode__VP).ID;
                PayForm__DefaultOption = GetValue(CONFIG_KEY__PAY_FORM__DEFAULT_OPTION) == "1";
                AutoCheckAndDisableOption = GetValue(CONFIG_KEY__AUTOCHECK) == "1";
                IsShowInPatientPrescriptionOption = GetValue(CONFIG_KEY__SHOW_IN) == "1";
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        static MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeByCode(string code)
        {
            MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE result = new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE();
            try
            {
                result = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE == code);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result ?? new MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE();
        }

        private static string GetValue(string key)
        {
            try
            {
                return HisConfigs.Get<string>(key);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return "";
        }
    }
}
