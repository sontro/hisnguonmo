using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHelmet
{
    public partial class HisAccidentHelmetManager : BusinessBase
    {
        public HisAccidentHelmetManager()
            : base()
        {

        }
        
        public HisAccidentHelmetManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ACCIDENT_HELMET>> Get(HisAccidentHelmetFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCIDENT_HELMET>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCIDENT_HELMET> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentHelmetGet(param).Get(filter);
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
        public ApiResultObject<HIS_ACCIDENT_HELMET> Create(HIS_ACCIDENT_HELMET data)
        {
            ApiResultObject<HIS_ACCIDENT_HELMET> result = new ApiResultObject<HIS_ACCIDENT_HELMET>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HELMET resultData = null;
                if (valid && new HisAccidentHelmetCreate(param).Create(data))
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
        public ApiResultObject<HIS_ACCIDENT_HELMET> Update(HIS_ACCIDENT_HELMET data)
        {
            ApiResultObject<HIS_ACCIDENT_HELMET> result = new ApiResultObject<HIS_ACCIDENT_HELMET>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HELMET resultData = null;
                if (valid && new HisAccidentHelmetUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCIDENT_HELMET> ChangeLock(HIS_ACCIDENT_HELMET data)
        {
            ApiResultObject<HIS_ACCIDENT_HELMET> result = new ApiResultObject<HIS_ACCIDENT_HELMET>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HELMET resultData = null;
                if (valid && new HisAccidentHelmetLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACCIDENT_HELMET data)
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
                    resultData = new HisAccidentHelmetTruncate(param).Truncate(data);
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
