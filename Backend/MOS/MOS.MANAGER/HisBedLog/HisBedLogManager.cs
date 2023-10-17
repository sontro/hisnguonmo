using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisBedLog.Update;

namespace MOS.MANAGER.HisBedLog
{
    public partial class HisBedLogManager : BusinessBase
    {
        public HisBedLogManager()
            : base()
        {

        }
        
        public HisBedLogManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BED_LOG>> Get(HisBedLogFilterQuery filter)
        {
            ApiResultObject<List<HIS_BED_LOG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BED_LOG> resultData = null;
                if (valid)
                {
                    resultData = new HisBedLogGet(param).Get(filter);
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
        public ApiResultObject<HIS_BED_LOG> Create(HisBedLogSDO data)
        {
            ApiResultObject<HIS_BED_LOG> result = new ApiResultObject<HIS_BED_LOG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BED_LOG resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBedLogCreate(param).Create(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_BED_LOG> Update(HisBedLogSDO data)
        {
            ApiResultObject<HIS_BED_LOG> result = new ApiResultObject<HIS_BED_LOG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BED_LOG resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBedLogUpdate(param).Update(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_BED_LOG> ChangeLock(HIS_BED_LOG data)
        {
            ApiResultObject<HIS_BED_LOG> result = new ApiResultObject<HIS_BED_LOG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BED_LOG resultData = null;
                if (valid && new HisBedLogLock(param).ChangeLock(data))
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
        public ApiResultObject<HIS_BED_LOG> Lock(long id)
        {
            ApiResultObject<HIS_BED_LOG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BED_LOG resultData = null;
                if (valid)
                {
                    new HisBedLogLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BED_LOG> Unlock(long id)
        {
            ApiResultObject<HIS_BED_LOG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BED_LOG resultData = null;
                if (valid)
                {
                    new HisBedLogLock(param).Unlock(id, ref resultData);
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
                    HIS_BED_LOG bedLog = new HIS_BED_LOG();
                    bedLog.ID = id;
                    resultData = new HisBedLogDelete(param).Delete(bedLog);
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
