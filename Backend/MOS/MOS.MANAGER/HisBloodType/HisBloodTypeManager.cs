using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodType
{
    public partial class HisBloodTypeManager : BusinessBase
    {
        public HisBloodTypeManager()
            : base()
        {

        }
        
        public HisBloodTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BLOOD_TYPE>> Get(HisBloodTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_BLOOD_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BLOOD_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisBloodTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_BLOOD_TYPE> Create(HIS_BLOOD_TYPE data)
        {
            ApiResultObject<HIS_BLOOD_TYPE> result = new ApiResultObject<HIS_BLOOD_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_TYPE resultData = null;
                if (valid && new HisBloodTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_BLOOD_TYPE> Update(HIS_BLOOD_TYPE data)
        {
            ApiResultObject<HIS_BLOOD_TYPE> result = new ApiResultObject<HIS_BLOOD_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_TYPE resultData = null;
                if (valid && new HisBloodTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BLOOD_TYPE> ChangeLock(long id)
        {
            ApiResultObject<HIS_BLOOD_TYPE> result = new ApiResultObject<HIS_BLOOD_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLOOD_TYPE resultData = null;
                if (valid && new HisBloodTypeLock(param).ChangeLock(id, ref resultData))
                {
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
        public ApiResultObject<HIS_BLOOD_TYPE> Lock(long id)
        {
            ApiResultObject<HIS_BLOOD_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLOOD_TYPE resultData = null;
                if (valid)
                {
                    new HisBloodTypeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BLOOD_TYPE> Unlock(long id)
        {
            ApiResultObject<HIS_BLOOD_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLOOD_TYPE resultData = null;
                if (valid)
                {
                    new HisBloodTypeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBloodTypeTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_BLOOD_TYPE>> CreateList(List<HIS_BLOOD_TYPE> data)
        {
            ApiResultObject<List<HIS_BLOOD_TYPE>> result = new ApiResultObject<List<HIS_BLOOD_TYPE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_BLOOD_TYPE> resultData = null;
                if (valid && new HisBloodTypeCreate(param).CreateList(data))
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
    }
}
