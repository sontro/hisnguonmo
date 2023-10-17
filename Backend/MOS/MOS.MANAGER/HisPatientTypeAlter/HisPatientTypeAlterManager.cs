using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeAlter
{
    public partial class HisPatientTypeAlterManager : BusinessBase
    {
        public HisPatientTypeAlterManager()
            : base()
        {

        }
        
        public HisPatientTypeAlterManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_PATIENT_TYPE_ALTER>> Get(HisPatientTypeAlterFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT_TYPE_ALTER>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT_TYPE_ALTER> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeAlterGet(param).Get(filter);
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
        public ApiResultObject<V_HIS_PATIENT_TYPE_ALTER> GetApplied(HisPatientTypeAlterViewAppliedFilter filter)
        {
            ApiResultObject<V_HIS_PATIENT_TYPE_ALTER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                V_HIS_PATIENT_TYPE_ALTER resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeAlterGet(param).GetViewApplied(filter.TreatmentId, filter.InstructionTime);
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
        public ApiResultObject<List<V_HIS_PATIENT_TYPE_ALTER>> GetView(HisPatientTypeAlterViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT_TYPE_ALTER>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT_TYPE_ALTER> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeAlterGet(param).GetView(filter);
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
        public ApiResultObject<HIS_PATIENT_TYPE_ALTER> GetLastByTreatmentId(long treatmentId)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ALTER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_TYPE_ALTER resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeAlterGet(param).GetLastByTreatmentId(treatmentId);
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
        public ApiResultObject<V_HIS_PATIENT_TYPE_ALTER> GetViewLastByTreatmentId(long treatmentId)
        {
            ApiResultObject<V_HIS_PATIENT_TYPE_ALTER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                V_HIS_PATIENT_TYPE_ALTER resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeAlterGet(param).GetViewLastByTreatmentId(treatmentId);
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
        public ApiResultObject<List<HIS_PATIENT_TYPE_ALTER>> GetDistinct(long treatmentId)
        {
            ApiResultObject<List<HIS_PATIENT_TYPE_ALTER>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_PATIENT_TYPE_ALTER> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeAlterGet(param).GetDistinct(treatmentId);
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
        public ApiResultObject<HisPatientTypeAlterAndTranPatiSDO> Create(HisPatientTypeAlterAndTranPatiSDO data)
        {
            ApiResultObject<HisPatientTypeAlterAndTranPatiSDO> result = new ApiResultObject<HisPatientTypeAlterAndTranPatiSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPatientTypeAlterAndTranPatiSDO resultData = null;
                if (valid)
                {
                    new HisPatientTypeAlterCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisPatientTypeAlterAndTranPatiSDO> Update(HisPatientTypeAlterAndTranPatiSDO data)
        {
            ApiResultObject<HisPatientTypeAlterAndTranPatiSDO> result = new ApiResultObject<HisPatientTypeAlterAndTranPatiSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPatientTypeAlterAndTranPatiSDO resultData = null;
                if (valid)
                {
                    new HisPatientTypeAlterUpdate(param).Update(data, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_TYPE_ALTER> ChangeLock(HIS_PATIENT_TYPE_ALTER data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ALTER> result = new ApiResultObject<HIS_PATIENT_TYPE_ALTER>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_PATIENT_TYPE_ALTER resultData = null;
                if (valid && new HisPatientTypeAlterLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<bool> Delete(DeletePatientTypeAlterSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisPatientTypeAlterTruncate(param).Truncate(data);
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
