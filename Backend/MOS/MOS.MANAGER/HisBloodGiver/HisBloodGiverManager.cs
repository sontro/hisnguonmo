using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodGiver
{
    public partial class HisBloodGiverManager : BusinessBase
    {
        public HisBloodGiverManager()
            : base()
        {

        }
        
        public HisBloodGiverManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BLOOD_GIVER>> Get(HisBloodGiverFilterQuery filter)
        {
            ApiResultObject<List<HIS_BLOOD_GIVER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BLOOD_GIVER> resultData = null;
                if (valid)
                {
                    resultData = new HisBloodGiverGet(param).Get(filter);
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
        public ApiResultObject<HIS_BLOOD_GIVER> Create(HIS_BLOOD_GIVER data)
        {
            ApiResultObject<HIS_BLOOD_GIVER> result = new ApiResultObject<HIS_BLOOD_GIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_GIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBloodGiverCreate(param).Create(data);
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
        public ApiResultObject<HIS_BLOOD_GIVER> Update(HIS_BLOOD_GIVER data)
        {
            ApiResultObject<HIS_BLOOD_GIVER> result = new ApiResultObject<HIS_BLOOD_GIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_GIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBloodGiverUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BLOOD_GIVER> ChangeLock(long id)
        {
            ApiResultObject<HIS_BLOOD_GIVER> result = new ApiResultObject<HIS_BLOOD_GIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLOOD_GIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBloodGiverLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BLOOD_GIVER> Lock(long id)
        {
            ApiResultObject<HIS_BLOOD_GIVER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLOOD_GIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBloodGiverLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BLOOD_GIVER> Unlock(long id)
        {
            ApiResultObject<HIS_BLOOD_GIVER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLOOD_GIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBloodGiverLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBloodGiverTruncate(param).Truncate(id);
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
