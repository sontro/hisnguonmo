using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMaty
{
    public partial class HisServiceReqMatyManager : BusinessBase
    {
        public HisServiceReqMatyManager()
            : base()
        {

        }
        
        public HisServiceReqMatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_REQ_MATY>> Get(HisServiceReqMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_REQ_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_REQ_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqMatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_REQ_MATY> Create(HIS_SERVICE_REQ_MATY data)
        {
            ApiResultObject<HIS_SERVICE_REQ_MATY> result = new ApiResultObject<HIS_SERVICE_REQ_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_MATY resultData = null;
                if (valid && new HisServiceReqMatyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_SERVICE_REQ_MATY>> CreateList(List<HIS_SERVICE_REQ_MATY> data)
        {
            ApiResultObject<List<HIS_SERVICE_REQ_MATY>> result = new ApiResultObject<List<HIS_SERVICE_REQ_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_REQ_MATY> resultData = null;
                if (valid && new HisServiceReqMatyCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_SERVICE_REQ_MATY> Update(HIS_SERVICE_REQ_MATY data)
        {
            ApiResultObject<HIS_SERVICE_REQ_MATY> result = new ApiResultObject<HIS_SERVICE_REQ_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_MATY resultData = null;
                if (valid && new HisServiceReqMatyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_REQ_MATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ_MATY> result = new ApiResultObject<HIS_SERVICE_REQ_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ_MATY resultData = null;
                if (valid)
                {
                    new HisServiceReqMatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ_MATY> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ_MATY resultData = null;
                if (valid)
                {
                    new HisServiceReqMatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ_MATY> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ_MATY resultData = null;
                if (valid)
                {
                    new HisServiceReqMatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisServiceReqMatyTruncate(param).Truncate(id);
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
