using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentLocation
{
    public partial class HisAccidentLocationManager : BusinessBase
    {
        public HisAccidentLocationManager()
            : base()
        {

        }
        
        public HisAccidentLocationManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ACCIDENT_LOCATION>> Get(HisAccidentLocationFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCIDENT_LOCATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCIDENT_LOCATION> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentLocationGet(param).Get(filter);
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
        public ApiResultObject<HIS_ACCIDENT_LOCATION> Create(HIS_ACCIDENT_LOCATION data)
        {
            ApiResultObject<HIS_ACCIDENT_LOCATION> result = new ApiResultObject<HIS_ACCIDENT_LOCATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_LOCATION resultData = null;
                if (valid && new HisAccidentLocationCreate(param).Create(data))
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
        public ApiResultObject<HIS_ACCIDENT_LOCATION> Update(HIS_ACCIDENT_LOCATION data)
        {
            ApiResultObject<HIS_ACCIDENT_LOCATION> result = new ApiResultObject<HIS_ACCIDENT_LOCATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_LOCATION resultData = null;
                if (valid && new HisAccidentLocationUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCIDENT_LOCATION> ChangeLock(HIS_ACCIDENT_LOCATION data)
        {
            ApiResultObject<HIS_ACCIDENT_LOCATION> result = new ApiResultObject<HIS_ACCIDENT_LOCATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_LOCATION resultData = null;
                if (valid && new HisAccidentLocationLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACCIDENT_LOCATION data)
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
                    resultData = new HisAccidentLocationTruncate(param).Truncate(data);
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
