using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Print;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MPS.Processor.Mps000091.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RequestDeposit
{
    class PrintGlobalStore
    {
        public static PatientADO getPatient(long treatmentId)
        {
            PatientADO currentPatientADO = new PatientADO();
            MOS.EFMODEL.DataModels.V_HIS_PATIENT patient = new V_HIS_PATIENT();
            MOS.EFMODEL.DataModels.V_HIS_TREATMENT currentHisTreatment = new MOS.EFMODEL.DataModels.V_HIS_TREATMENT();
            try
            {
                MOS.Filter.HisTreatmentViewFilter hisTreatmentFilter = new MOS.Filter.HisTreatmentViewFilter();
                hisTreatmentFilter.ID = treatmentId;

                CommonParam param = new CommonParam();
                var treatments = new BackendAdapter(param).Get<List<V_HIS_TREATMENT>>(HIS.Desktop.ApiConsumer.HisRequestUriStore.HIS_TREATMENT_GETVIEW, ApiConsumers.MosConsumer, hisTreatmentFilter, param);
                if (treatments != null && treatments.Count > 0)
                {
                    currentHisTreatment = treatments.FirstOrDefault();

                    MOS.Filter.HisPatientViewFilter patientViewFilter = new MOS.Filter.HisPatientViewFilter();
                    patientViewFilter.ID = currentHisTreatment.PATIENT_ID;
                    patient = new BackendAdapter(param).Get<List<V_HIS_PATIENT>>(HIS.Desktop.ApiConsumer.HisRequestUriStore.HIS_PATIENT_GETVIEW, ApiConsumers.MosConsumer, patientViewFilter, param).SingleOrDefault();

                    AutoMapper.Mapper.CreateMap<MOS.EFMODEL.DataModels.V_HIS_PATIENT, PatientADO>();
                    currentPatientADO = AutoMapper.Mapper.Map<MOS.EFMODEL.DataModels.V_HIS_PATIENT, PatientADO>(patient);
                    currentPatientADO.DOB_STR = Inventec.Common.DateTime.Convert.TimeNumberToDateString(currentPatientADO.DOB);
                    currentPatientADO.AGE = AgeHelper.CalculateAgeFromYear(currentPatientADO.DOB);
                    if (currentPatientADO != null && currentPatientADO.DOB > 0)
                    {
                        currentPatientADO.DOB_YEAR = currentPatientADO.DOB.ToString().Substring(0, 4);
                    }
                    if (currentPatientADO.GENDER_CODE == Inventec.Common.LocalStorage.SdaConfig.SdaConfigs.Get<string>(Inventec.Common.LocalStorage.SdaConfig.ConfigKeys.DBCODE__HIS_RS__HIS_GENDER__GENDER_CODE__FEMALE))
                    {
                        currentPatientADO.GENDER_MALE = "";
                        currentPatientADO.GENDER_FEMALE = "X";
                    }
                    else
                    {
                        currentPatientADO.GENDER_MALE = "X";
                        currentPatientADO.GENDER_FEMALE = "";
                    }
                }
            }
            catch (Exception ex)
            {
                currentPatientADO = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return currentPatientADO;
        }
    }
}
