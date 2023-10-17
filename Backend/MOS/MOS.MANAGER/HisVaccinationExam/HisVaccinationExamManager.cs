using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisVaccinationExam.Register;
using MOS.MANAGER.HisVaccinationExam.Appointment;

namespace MOS.MANAGER.HisVaccinationExam
{
    public partial class HisVaccinationExamManager : BusinessBase
    {
        public HisVaccinationExamManager()
            : base()
        {

        }

        public HisVaccinationExamManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_VACCINATION_EXAM>> Get(HisVaccinationExamFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACCINATION_EXAM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACCINATION_EXAM> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationExamGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_VACCINATION_EXAM> Finish(long id)
        {
            ApiResultObject<HIS_VACCINATION_EXAM> result = new ApiResultObject<HIS_VACCINATION_EXAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_EXAM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationExamFinish(param).Finish(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_VACCINATION_EXAM> CancelFinish(long id)
        {
            ApiResultObject<HIS_VACCINATION_EXAM> result = new ApiResultObject<HIS_VACCINATION_EXAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_EXAM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationExamFinish(param).CancelFinish(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_VACCINATION_EXAM> UpdateSdo(HisVaccinationExamSDO data)
        {
            ApiResultObject<HIS_VACCINATION_EXAM> result = new ApiResultObject<HIS_VACCINATION_EXAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_EXAM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationExamUpdateSdo(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Treat(HisVaccinationExamTreatSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationExamTreat(param).Run(data);
                }
                result = this.PackResult(isSuccess, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_VACCINATION_EXAM> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACCINATION_EXAM> result = new ApiResultObject<HIS_VACCINATION_EXAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_EXAM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationExamLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_VACCINATION_EXAM> Lock(long id)
        {
            ApiResultObject<HIS_VACCINATION_EXAM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_EXAM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationExamLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_VACCINATION_EXAM> Unlock(long id)
        {
            ApiResultObject<HIS_VACCINATION_EXAM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_EXAM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationExamLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HisVaccinationExamDeleteSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationExamTruncate(param).Truncate(data);
                }
                result = this.PackSingleResult(isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<VaccinationRegisterResultSDO> Register(HisPatientVaccinationSDO data)
        {
            ApiResultObject<VaccinationRegisterResultSDO> result = new ApiResultObject<VaccinationRegisterResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                VaccinationRegisterResultSDO resultData = null;
                if (valid)
                {
                    new HisVaccinationExamRegister(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_VACC_APPOINTMENT>> Appointment(HisVaccinationAppointmentSDO data)
        {
            ApiResultObject<List<V_HIS_VACC_APPOINTMENT>> result = new ApiResultObject<List<V_HIS_VACC_APPOINTMENT>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<V_HIS_VACC_APPOINTMENT> resultData = null;
                if (valid)
                {
                    new HisVaccinationExamAppointment(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
