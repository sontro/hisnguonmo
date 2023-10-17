using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqType
{
    public partial class HisServiceReqTypeManager : BusinessBase
    {
        public HisServiceReqTypeManager()
            : base()
        {

        }
        
        public HisServiceReqTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_REQ_TYPE>> Get(HisServiceReqTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_REQ_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_REQ_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_REQ_TYPE> Create(HIS_SERVICE_REQ_TYPE data)
        {
            ApiResultObject<HIS_SERVICE_REQ_TYPE> result = new ApiResultObject<HIS_SERVICE_REQ_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_TYPE resultData = null;
                if (valid && new HisServiceReqTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERVICE_REQ_TYPE> Update(HIS_SERVICE_REQ_TYPE data)
        {
            ApiResultObject<HIS_SERVICE_REQ_TYPE> result = new ApiResultObject<HIS_SERVICE_REQ_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_TYPE resultData = null;
                if (valid && new HisServiceReqTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_REQ_TYPE> ChangeLock(HIS_SERVICE_REQ_TYPE data)
        {
            ApiResultObject<HIS_SERVICE_REQ_TYPE> result = new ApiResultObject<HIS_SERVICE_REQ_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_TYPE resultData = null;
                if (valid && new HisServiceReqTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SERVICE_REQ_TYPE data)
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
                    resultData = new HisServiceReqTypeTruncate(param).Truncate(data);
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
