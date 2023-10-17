using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportRegisterByXml.Base
{
    class Config
    {
        private const string CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT

        private static long patientTypeId;
        internal static long PatientTypeId_BHYT
        {
            get
            {
                if (patientTypeId <= 0)
                {
                    var PatientTypeCode__BHYT = HisConfigs.Get<string>(CONFIG_KEY__PATIENT_TYPE_CODE__BHYT);
                    patientTypeId = GetPatientTypeByCode(PatientTypeCode__BHYT).ID;
                }
                return patientTypeId;
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

        private static Dictionary<string, string> dicRoomEmployee;
        internal static Dictionary<string, string> DicRoomEmployee
        {
            get
            {
                return dicRoomEmployee;
            }
            set
            {
                dicRoomEmployee = value;
            }
        }

        private static List<string> listHeinServiceTypeExam;
        internal static List<string> ListHeinServiceTypeExam
        {
            get
            {
                if (listHeinServiceTypeExam == null || listHeinServiceTypeExam.Count <= 0)
                {
                    var examService = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_SERVICE>().Where(o => o.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH);
                    listHeinServiceTypeExam = examService != null ? examService.Select(o => o.HEIN_SERVICE_BHYT_CODE ?? "").Distinct().ToList() : new List<string>();
                }
                return listHeinServiceTypeExam;
            }
        }

        internal class CommuneCFG
        {
            public static List<SDA.EFMODEL.DataModels.SDA_COMMUNE> SDA_COMMUNEs
            {
                get
                {
                    return HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<SDA.EFMODEL.DataModels.SDA_COMMUNE>();
                }
            }

            private static List<string> initialName;
            public static List<string> INITIAL_NAMEs
            {
                get
                {
                    if (initialName == null || initialName.Count <= 0)
                    {
                        initialName = SDA_COMMUNEs.Select(s => s.INITIAL_NAME).Distinct().ToList();
                    }
                    return initialName;
                }
            }
        }

        internal class DistrictCFG
        {
            public static List<SDA.EFMODEL.DataModels.SDA_DISTRICT> SDA_DISTRICTs
            {
                get
                {
                    return HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<SDA.EFMODEL.DataModels.SDA_DISTRICT>();
                }
            }

            private static List<string> initialName;
            public static List<string> INITIAL_NAMEs
            {
                get
                {
                    if (initialName == null || initialName.Count <= 0)
                    {
                        initialName = SDA_DISTRICTs.Select(s => s.INITIAL_NAME).Distinct().ToList();
                    }
                    return initialName;
                }
            }
        }

        internal class ProvinceCFG
        {
            public static List<SDA.EFMODEL.DataModels.SDA_PROVINCE> SDA_PROVINCEs
            {
                get
                {
                    return HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<SDA.EFMODEL.DataModels.SDA_PROVINCE>();
                }
            }
        }
    }
}
