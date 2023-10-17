using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeSub
{
    public partial class HisPatientTypeSubManager : BusinessBase
    {
        public HisPatientTypeSubManager()
            : base()
        {

        }
        
        public HisPatientTypeSubManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PATIENT_TYPE_SUB>> Get(HisPatientTypeSubFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT_TYPE_SUB>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT_TYPE_SUB> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeSubGet(param).Get(filter);
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
        public ApiResultObject<HIS_PATIENT_TYPE_SUB> Create(HIS_PATIENT_TYPE_SUB data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_SUB> result = new ApiResultObject<HIS_PATIENT_TYPE_SUB>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_SUB resultData = null;
                if (valid && new HisPatientTypeSubCreate(param).Create(data))
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
        public ApiResultObject<HIS_PATIENT_TYPE_SUB> Update(HIS_PATIENT_TYPE_SUB data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_SUB> result = new ApiResultObject<HIS_PATIENT_TYPE_SUB>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_SUB resultData = null;
                if (valid && new HisPatientTypeSubUpdate(param).Update(data))
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
        public ApiResultObject<HIS_PATIENT_TYPE_SUB> ChangeLock(HIS_PATIENT_TYPE_SUB data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_SUB> result = new ApiResultObject<HIS_PATIENT_TYPE_SUB>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_SUB resultData = null;
                if (valid && new HisPatientTypeSubLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_PATIENT_TYPE_SUB data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisPatientTypeSubTruncate(param).Truncate(data);
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
