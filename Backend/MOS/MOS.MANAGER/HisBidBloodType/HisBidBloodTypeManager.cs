using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBidBloodType
{
    public partial class HisBidBloodTypeManager : BusinessBase
    {
        public HisBidBloodTypeManager()
            : base()
        {

        }
        
        public HisBidBloodTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BID_BLOOD_TYPE>> Get(HisBidBloodTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_BID_BLOOD_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BID_BLOOD_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisBidBloodTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_BID_BLOOD_TYPE> Create(HIS_BID_BLOOD_TYPE data)
        {
            ApiResultObject<HIS_BID_BLOOD_TYPE> result = new ApiResultObject<HIS_BID_BLOOD_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BID_BLOOD_TYPE resultData = null;
                if (valid && new HisBidBloodTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_BID_BLOOD_TYPE> Update(HIS_BID_BLOOD_TYPE data)
        {
            ApiResultObject<HIS_BID_BLOOD_TYPE> result = new ApiResultObject<HIS_BID_BLOOD_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BID_BLOOD_TYPE resultData = null;
                if (valid && new HisBidBloodTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BID_BLOOD_TYPE> ChangeLock(long id)
        {
            ApiResultObject<HIS_BID_BLOOD_TYPE> result = new ApiResultObject<HIS_BID_BLOOD_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BID_BLOOD_TYPE resultData = null;
                if (valid)
                {
                    new HisBidBloodTypeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BID_BLOOD_TYPE> Lock(long id)
        {
            ApiResultObject<HIS_BID_BLOOD_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BID_BLOOD_TYPE resultData = null;
                if (valid)
                {
                    new HisBidBloodTypeLock(param).Lock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_BID_BLOOD_TYPE> Unlock(long id)
        {
            ApiResultObject<HIS_BID_BLOOD_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BID_BLOOD_TYPE resultData = null;
                if (valid)
                {
                    new HisBidBloodTypeLock(param).Unlock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
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
                    resultData = new HisBidBloodTypeTruncate(param).Truncate(id);
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
