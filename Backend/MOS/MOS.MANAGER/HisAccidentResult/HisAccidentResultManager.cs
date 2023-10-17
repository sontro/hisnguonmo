using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentResult
{
    public partial class HisAccidentResultManager : BusinessBase
    {
        public HisAccidentResultManager()
            : base()
        {

        }
        
        public HisAccidentResultManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ACCIDENT_RESULT>> Get(HisAccidentResultFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCIDENT_RESULT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCIDENT_RESULT> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentResultGet(param).Get(filter);
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
        public ApiResultObject<HIS_ACCIDENT_RESULT> Create(HIS_ACCIDENT_RESULT data)
        {
            ApiResultObject<HIS_ACCIDENT_RESULT> result = new ApiResultObject<HIS_ACCIDENT_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_RESULT resultData = null;
                if (valid && new HisAccidentResultCreate(param).Create(data))
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
        public ApiResultObject<HIS_ACCIDENT_RESULT> Update(HIS_ACCIDENT_RESULT data)
        {
            ApiResultObject<HIS_ACCIDENT_RESULT> result = new ApiResultObject<HIS_ACCIDENT_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_RESULT resultData = null;
                if (valid && new HisAccidentResultUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCIDENT_RESULT> ChangeLock(HIS_ACCIDENT_RESULT data)
        {
            ApiResultObject<HIS_ACCIDENT_RESULT> result = new ApiResultObject<HIS_ACCIDENT_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_RESULT resultData = null;
                if (valid && new HisAccidentResultLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACCIDENT_RESULT data)
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
                    resultData = new HisAccidentResultTruncate(param).Truncate(data);
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
