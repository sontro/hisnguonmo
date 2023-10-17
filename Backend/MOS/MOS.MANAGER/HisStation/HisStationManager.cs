using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisStation
{
    public partial class HisStationManager : BusinessBase
    {
        public HisStationManager()
            : base()
        {

        }
        
        public HisStationManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_STATION>> Get(HisStationFilterQuery filter)
        {
            ApiResultObject<List<HIS_STATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_STATION> resultData = null;
                if (valid)
                {
                    resultData = new HisStationGet(param).Get(filter);
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
        public ApiResultObject<HisStationSDO> Create(HisStationSDO data)
        {
            ApiResultObject<HisStationSDO> result = new ApiResultObject<HisStationSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisStationSDO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisStationCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
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
        public ApiResultObject<HisStationSDO> Update(HisStationSDO data)
        {
            ApiResultObject<HisStationSDO> result = new ApiResultObject<HisStationSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisStationSDO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisStationUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
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
        public ApiResultObject<HIS_STATION> ChangeLock(long id)
        {
            ApiResultObject<HIS_STATION> result = new ApiResultObject<HIS_STATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStationLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_STATION> Lock(long id)
        {
            ApiResultObject<HIS_STATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStationLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_STATION> Unlock(long id)
        {
            ApiResultObject<HIS_STATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStationLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
                    resultData = new HisStationTruncate(param).Truncate(id);
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
