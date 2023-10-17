using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using System;
using System.Linq;
using System.Collections.Generic;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.Config
{
    public class HisPatientTypeCFG
    {
        private const string SDA_CONFIG__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT
        private const string SDA_CONFIG__PATIENT_TYPE_CODE__HOSPITAL_FEE = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE";//Doi tuong vien phi
        private const string SDA_CONFIG__PATIENT_TYPE_CODE__IS_FREE = "MRS.HIS_RS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.IS_FREE";//Doi tuong mien phi

        private static long patientTypeIdIsHein;
        public static long PATIENT_TYPE_ID__BHYT
        {
            get
            {
                if (patientTypeIdIsHein == 0)
                {
                    patientTypeIdIsHein = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SDA_CONFIG__PATIENT_TYPE_CODE__BHYT));
                }
                return patientTypeIdIsHein;
            }
            set
            {
                patientTypeIdIsHein = value;
            }
        }

        private static long patientTypeIdIsFree;
        public static long PATIENT_TYPE_ID__IS_FREE
        {
            get
            {
                if (patientTypeIdIsFree == 0)
                {
                    patientTypeIdIsFree = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SDA_CONFIG__PATIENT_TYPE_CODE__IS_FREE));
                }
                return patientTypeIdIsFree;
            }
            set
            {
                patientTypeIdIsFree = value;
            }
        }

        private static long patientTypeIdHospitalFee;
        public static long PATIENT_TYPE_ID__HOSPITAL_FEE
        {
            get
            {
                if (patientTypeIdHospitalFee == 0)
                {
                    patientTypeIdHospitalFee = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SDA_CONFIG__PATIENT_TYPE_CODE__HOSPITAL_FEE));
                }
                return patientTypeIdHospitalFee;
            }
            set
            {
                patientTypeIdHospitalFee = value;
            }
        }

        //Code
        private static string patientTypeCodeIsHein;
        public static string PATIENT_TYPE_CODE__BHYT
        {
            get
            {
                if (String.IsNullOrEmpty(patientTypeCodeIsHein))
                {
                    patientTypeCodeIsHein = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SDA_CONFIG__PATIENT_TYPE_CODE__BHYT);
                }
                return patientTypeCodeIsHein;
            }
            set
            {
                patientTypeCodeIsHein = value;
            }
        }

        private static string patientTypeCodeIsFree;
        public static string PATIENT_TYPE_CODE__IS_FREE
        {
            get
            {
                if (String.IsNullOrEmpty(patientTypeCodeIsFree))
                {
                    patientTypeCodeIsFree = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SDA_CONFIG__PATIENT_TYPE_CODE__IS_FREE);
                }
                return patientTypeCodeIsFree;
            }
            set
            {
                patientTypeCodeIsFree = value;
            }
        }

        private static string patientTypeCodeHospitalFee;
        public static string PATIENT_TYPE_CODE__HOSPITAL_FEE
        {
            get
            {
                if (String.IsNullOrEmpty(patientTypeCodeHospitalFee))
                {
                    patientTypeCodeHospitalFee = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(SDA_CONFIG__PATIENT_TYPE_CODE__HOSPITAL_FEE);
                }
                return patientTypeCodeHospitalFee;
            }
            set
            {
                patientTypeCodeHospitalFee = value;
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
