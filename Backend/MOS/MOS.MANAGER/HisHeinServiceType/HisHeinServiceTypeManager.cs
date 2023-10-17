using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHeinServiceType
{
    public partial class HisHeinServiceTypeManager : BusinessBase
    {
        public HisHeinServiceTypeManager()
            : base()
        {

        }
        
        public HisHeinServiceTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_HEIN_SERVICE_TYPE>> Get(HisHeinServiceTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_HEIN_SERVICE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HEIN_SERVICE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisHeinServiceTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_HEIN_SERVICE_TYPE> Create(HIS_HEIN_SERVICE_TYPE data)
        {
            ApiResultObject<HIS_HEIN_SERVICE_TYPE> result = new ApiResultObject<HIS_HEIN_SERVICE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HEIN_SERVICE_TYPE resultData = null;
                if (valid && new HisHeinServiceTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_HEIN_SERVICE_TYPE> Update(HIS_HEIN_SERVICE_TYPE data)
        {
            ApiResultObject<HIS_HEIN_SERVICE_TYPE> result = new ApiResultObject<HIS_HEIN_SERVICE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HEIN_SERVICE_TYPE resultData = null;
                if (valid && new HisHeinServiceTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_HEIN_SERVICE_TYPE> ChangeLock(HIS_HEIN_SERVICE_TYPE data)
        {
            ApiResultObject<HIS_HEIN_SERVICE_TYPE> result = new ApiResultObject<HIS_HEIN_SERVICE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HEIN_SERVICE_TYPE resultData = null;
                if (valid && new HisHeinServiceTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_HEIN_SERVICE_TYPE data)
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
                    resultData = new HisHeinServiceTypeTruncate(param).Truncate(data);
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
