using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHurtType
{
    public partial class HisAccidentHurtTypeManager : BusinessBase
    {
        public HisAccidentHurtTypeManager()
            : base()
        {

        }
        
        public HisAccidentHurtTypeManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_ACCIDENT_HURT_TYPE>> Get(HisAccidentHurtTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCIDENT_HURT_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCIDENT_HURT_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentHurtTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_ACCIDENT_HURT_TYPE> Create(HIS_ACCIDENT_HURT_TYPE data)
        {
            ApiResultObject<HIS_ACCIDENT_HURT_TYPE> result = new ApiResultObject<HIS_ACCIDENT_HURT_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT_TYPE resultData = null;
                if (valid && new HisAccidentHurtTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_ACCIDENT_HURT_TYPE> Update(HIS_ACCIDENT_HURT_TYPE data)
        {
            ApiResultObject<HIS_ACCIDENT_HURT_TYPE> result = new ApiResultObject<HIS_ACCIDENT_HURT_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT_TYPE resultData = null;
                if (valid && new HisAccidentHurtTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCIDENT_HURT_TYPE> ChangeLock(HIS_ACCIDENT_HURT_TYPE data)
        {
            ApiResultObject<HIS_ACCIDENT_HURT_TYPE> result = new ApiResultObject<HIS_ACCIDENT_HURT_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT_TYPE resultData = null;
                if (valid && new HisAccidentHurtTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACCIDENT_HURT_TYPE data)
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
                    resultData = new HisAccidentHurtTypeTruncate(param).Truncate(data);
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
