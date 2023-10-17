using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeciality
{
    public partial class HisSpecialityManager : BusinessBase
    {
        public HisSpecialityManager()
            : base()
        {

        }
        
        public HisSpecialityManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SPECIALITY>> Get(HisSpecialityFilterQuery filter)
        {
            ApiResultObject<List<HIS_SPECIALITY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SPECIALITY> resultData = null;
                if (valid)
                {
                    resultData = new HisSpecialityGet(param).Get(filter);
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
        public ApiResultObject<HIS_SPECIALITY> Create(HIS_SPECIALITY data)
        {
            ApiResultObject<HIS_SPECIALITY> result = new ApiResultObject<HIS_SPECIALITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SPECIALITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSpecialityCreate(param).Create(data);
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
        public ApiResultObject<HIS_SPECIALITY> Update(HIS_SPECIALITY data)
        {
            ApiResultObject<HIS_SPECIALITY> result = new ApiResultObject<HIS_SPECIALITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SPECIALITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSpecialityUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SPECIALITY> ChangeLock(long id)
        {
            ApiResultObject<HIS_SPECIALITY> result = new ApiResultObject<HIS_SPECIALITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SPECIALITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSpecialityLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SPECIALITY> Lock(long id)
        {
            ApiResultObject<HIS_SPECIALITY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SPECIALITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSpecialityLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SPECIALITY> Unlock(long id)
        {
            ApiResultObject<HIS_SPECIALITY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SPECIALITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSpecialityLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisSpecialityTruncate(param).Truncate(id);
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
