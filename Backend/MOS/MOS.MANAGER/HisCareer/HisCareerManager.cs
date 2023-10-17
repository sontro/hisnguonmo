using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareer
{
    public class HisCareerManager : BusinessBase
    {
        public HisCareerManager()
            : base()
        {

        }
		
		public HisCareerManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_CAREER>> Get(HisCareerFilterQuery filter)
        {
            ApiResultObject<List<HIS_CAREER>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
				List<HIS_CAREER> resultData = null;
                if (valid)
                {
					resultData = new HisCareerGet(param).Get(filter);
                }
				result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_CAREER> Create(HIS_CAREER data)
        {
            ApiResultObject<HIS_CAREER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
				HIS_CAREER resultData = null;
                if (valid && new HisCareerCreate(param).Create(data))
                {
					resultData = data;
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
        public ApiResultObject<HIS_CAREER> Update(HIS_CAREER data)
        {
            ApiResultObject<HIS_CAREER> result = new ApiResultObject<HIS_CAREER>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
				HIS_CAREER resultData = null;
                if (valid && new HisCareerUpdate(param).Update(data))
                {
					resultData = data;
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
        public ApiResultObject<HIS_CAREER> ChangeLock(HIS_CAREER data)
        {
            ApiResultObject<HIS_CAREER> result = new ApiResultObject<HIS_CAREER>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
				HIS_CAREER resultData = null;
                if (valid && new HisCareerLock(param).ChangeLock(data))
                {
					resultData = data;
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
        public ApiResultObject<bool> Delete(HIS_CAREER data)
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
                    resultData = new HisCareerTruncate(param).Truncate(data);
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
    }
}
