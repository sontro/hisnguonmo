using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentPoison
{
    public partial class HisAccidentPoisonManager : BusinessBase
    {
        public HisAccidentPoisonManager()
            : base()
        {

        }
        
        public HisAccidentPoisonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ACCIDENT_POISON>> Get(HisAccidentPoisonFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCIDENT_POISON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCIDENT_POISON> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentPoisonGet(param).Get(filter);
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
        public ApiResultObject<HIS_ACCIDENT_POISON> Create(HIS_ACCIDENT_POISON data)
        {
            ApiResultObject<HIS_ACCIDENT_POISON> result = new ApiResultObject<HIS_ACCIDENT_POISON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_POISON resultData = null;
                if (valid && new HisAccidentPoisonCreate(param).Create(data))
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
        public ApiResultObject<HIS_ACCIDENT_POISON> Update(HIS_ACCIDENT_POISON data)
        {
            ApiResultObject<HIS_ACCIDENT_POISON> result = new ApiResultObject<HIS_ACCIDENT_POISON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_POISON resultData = null;
                if (valid && new HisAccidentPoisonUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCIDENT_POISON> ChangeLock(HIS_ACCIDENT_POISON data)
        {
            ApiResultObject<HIS_ACCIDENT_POISON> result = new ApiResultObject<HIS_ACCIDENT_POISON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_POISON resultData = null;
                if (valid && new HisAccidentPoisonLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACCIDENT_POISON data)
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
                    resultData = new HisAccidentPoisonTruncate(param).Truncate(data);
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
