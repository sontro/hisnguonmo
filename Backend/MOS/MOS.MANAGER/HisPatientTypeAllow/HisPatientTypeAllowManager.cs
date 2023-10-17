using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeAllow
{
    public partial class HisPatientTypeAllowManager : BusinessBase
    {
        public HisPatientTypeAllowManager()
            : base()
        {

        }
        
        public HisPatientTypeAllowManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PATIENT_TYPE_ALLOW>> Get(HisPatientTypeAllowFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT_TYPE_ALLOW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT_TYPE_ALLOW> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeAllowGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_PATIENT_TYPE_ALLOW>> GetView(HisPatientTypeAllowViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT_TYPE_ALLOW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT_TYPE_ALLOW> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeAllowGet(param).GetView(filter);
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
        public ApiResultObject<HIS_PATIENT_TYPE_ALLOW> Create(HIS_PATIENT_TYPE_ALLOW data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ALLOW> result = new ApiResultObject<HIS_PATIENT_TYPE_ALLOW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_ALLOW resultData = null;
                if (valid && new HisPatientTypeAllowCreate(param).Create(data))
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
        public ApiResultObject<HIS_PATIENT_TYPE_ALLOW> Update(HIS_PATIENT_TYPE_ALLOW data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ALLOW> result = new ApiResultObject<HIS_PATIENT_TYPE_ALLOW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_ALLOW resultData = null;
                if (valid && new HisPatientTypeAllowUpdate(param).Update(data))
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
        public ApiResultObject<HIS_PATIENT_TYPE_ALLOW> ChangeLock(HIS_PATIENT_TYPE_ALLOW data)
        {
            ApiResultObject<HIS_PATIENT_TYPE_ALLOW> result = new ApiResultObject<HIS_PATIENT_TYPE_ALLOW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_ALLOW resultData = null;
                if (valid && new HisPatientTypeAllowLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_PATIENT_TYPE_ALLOW data)
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
                    resultData = new HisPatientTypeAllowTruncate(param).Truncate(data);
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
