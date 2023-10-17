using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisVaccination.Assign;
using MOS.MANAGER.HisVaccination.Assign.Create;
using MOS.MANAGER.HisVaccination.Assign.Update;
using MOS.MANAGER.HisVaccination.Process;
using MOS.MANAGER.HisVaccination.ChangeMedicine;
using MOS.MANAGER.HisVaccination.React;
using MOS.MANAGER.HisVaccination.React.UnReact;

namespace MOS.MANAGER.HisVaccination
{
    public partial class HisVaccinationManager : BusinessBase
    {
        public HisVaccinationManager()
            : base()
        {

        }

        public HisVaccinationManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_VACCINATION>> Get(HisVaccinationFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACCINATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACCINATION> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationGet(param).Get(filter);
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
        public ApiResultObject<VaccinationResultSDO> AssignCreate(HisVaccinationAssignSDO sdo)
        {
            ApiResultObject<VaccinationResultSDO> result = new ApiResultObject<VaccinationResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                VaccinationResultSDO resultData = null;
                if (valid)
                {
                    isSuccess = new HisVaccinationAssignCreate(param).Run(sdo, ref resultData);
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
        public ApiResultObject<VaccinationResultSDO> AssignUpdate(HisVaccinationAssignSDO sdo)
        {
            ApiResultObject<VaccinationResultSDO> result = new ApiResultObject<VaccinationResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                VaccinationResultSDO resultData = null;
                if (valid)
                {
                    isSuccess = new HisVaccinationAssignUpdate(param).Run(sdo, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_MEDICINE> ChangeMedicine(HisVaccinationChangeMedicineSDO sdo)
        {
            ApiResultObject<HIS_EXP_MEST_MEDICINE> result = new ApiResultObject<HIS_EXP_MEST_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                HIS_EXP_MEST_MEDICINE resultData = null;
                if (valid)
                {
                    isSuccess = new HisVaccinationChangeMedicine(param).Run(sdo, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION> Process(HisVaccinationProcessSDO sdo)
        {
            ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                HIS_VACCINATION resultData = null;
                if (valid)
                {
                    isSuccess = new HisVaccinationProcess(param).Run(sdo, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION> Update(HIS_VACCINATION data)
        {
            ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
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
        public ApiResultObject<HIS_VACCINATION> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION> Lock(long id)
        {
            ApiResultObject<HIS_VACCINATION> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION> Unlock(long id)
        {
            ApiResultObject<HIS_VACCINATION> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(HisVaccinationSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisVaccinationTruncate(param).Truncate(data);
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
        public ApiResultObject<HIS_VACCINATION> UpdateReactInfo(HisVaccReactInfoSDO data)
        {
            ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationUpdateReactInfo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION> UnReact(HIS_VACCINATION data)
        {
            ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationUnReact(param).Run(data, ref resultData);
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

    }
}
