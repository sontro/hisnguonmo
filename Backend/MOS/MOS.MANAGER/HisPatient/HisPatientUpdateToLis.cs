using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using LIS.SDO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient
{
    class HisPatientUpdateToLis : BusinessBase
    {
        internal HisPatientUpdateToLis()
            : base()
        {

        }

        internal HisPatientUpdateToLis(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(long id)
        {
            bool result = false;
            try
            {
                if (Config.LisCFG.LIS_INTEGRATE_OPTION != (int)Config.LisCFG.LisIntegrateOption.LIS)
                {
                    return false;
                }
                if (Config.LisCFG.LIS_ADDRESSES == null || Config.LisCFG.LIS_ADDRESSES.Count <= 0)
                {
                    return false;
                }

                V_HIS_PATIENT patient = new HisPatientGet().GetViewById(id);
                if (patient == null)
                {
                    throw new ArgumentNullException("patient is null");
                }
                UpdateSampleByPatientCodeSDO updateSDO = new UpdateSampleByPatientCodeSDO();
                updateSDO.Dob = patient.DOB;
                updateSDO.FirstName = patient.FIRST_NAME;
                updateSDO.GenderCode = patient.GENDER_CODE;
                updateSDO.LastName = patient.LAST_NAME;
                updateSDO.PatientCode = patient.PATIENT_CODE;
                List<string> lisUris = Config.LisCFG.LIS_ADDRESSES.Select(s => s.Url).Distinct().ToList();
                foreach (string baseUri in lisUris)
                {
                    try
                    {
                        if (!String.IsNullOrWhiteSpace(baseUri))
                        {
                            ApiConsumer serviceConsumer = new ApiConsumer(baseUri, "", MOS.UTILITY.Constant.APPLICATION_CODE);

                            var ro = serviceConsumer.Post<Inventec.Core.ApiResultObject<bool>>("/api/LisSample/UpdateByPatientCode", null, updateSDO);
                            if (ro == null || !ro.Success)
                            {
                                LogSystem.Warn("Khong update duoc thong tin Benh nhan sang LIS Address: " + baseUri + LogUtil.TraceData("\n result", ro));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Warn("Co exception khi update thong tin Benh nhan sang LIS Address: " + baseUri);
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
