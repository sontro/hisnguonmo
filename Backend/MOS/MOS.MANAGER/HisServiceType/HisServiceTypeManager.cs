using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceType
{
    public partial class HisServiceTypeManager : BusinessBase
    {
        public HisServiceTypeManager()
            : base()
        {

        }
        
        public HisServiceTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_TYPE>> Get(HisServiceTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_TYPE> Create(HIS_SERVICE_TYPE data)
        {
            ApiResultObject<HIS_SERVICE_TYPE> result = new ApiResultObject<HIS_SERVICE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_TYPE resultData = null;
                if (valid && new HisServiceTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERVICE_TYPE> Update(HIS_SERVICE_TYPE data)
        {
            ApiResultObject<HIS_SERVICE_TYPE> result = new ApiResultObject<HIS_SERVICE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_TYPE resultData = null;
                if (valid && new HisServiceTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_TYPE> ChangeLock(HIS_SERVICE_TYPE data)
        {
            ApiResultObject<HIS_SERVICE_TYPE> result = new ApiResultObject<HIS_SERVICE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_TYPE resultData = null;
                if (valid && new HisServiceTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SERVICE_TYPE data)
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
                    resultData = new HisServiceTypeTruncate(param).Truncate(data);
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
        public ApiResultObject<HIS_SERVICE_TYPE> UpdateSdo(ServiceTypeUpdateSDO data)
        {
            ApiResultObject<HIS_SERVICE_TYPE> result = new ApiResultObject<HIS_SERVICE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_TYPE resultData = null;
                if (valid && new HisServiceTypeUpdate(param).UpdateSdo(data, ref resultData))
                {
                    result = this.PackSingleResult(resultData);
                }
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
