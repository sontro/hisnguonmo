using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyVolume
{
    public partial class HisBltyVolumeManager : BusinessBase
    {
        public HisBltyVolumeManager()
            : base()
        {

        }
        
        public HisBltyVolumeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BLTY_VOLUME>> Get(HisBltyVolumeFilterQuery filter)
        {
            ApiResultObject<List<HIS_BLTY_VOLUME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BLTY_VOLUME> resultData = null;
                if (valid)
                {
                    resultData = new HisBltyVolumeGet(param).Get(filter);
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
        public ApiResultObject<HIS_BLTY_VOLUME> Create(HIS_BLTY_VOLUME data)
        {
            ApiResultObject<HIS_BLTY_VOLUME> result = new ApiResultObject<HIS_BLTY_VOLUME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLTY_VOLUME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBltyVolumeCreate(param).Create(data);
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
        public ApiResultObject<HIS_BLTY_VOLUME> Update(HIS_BLTY_VOLUME data)
        {
            ApiResultObject<HIS_BLTY_VOLUME> result = new ApiResultObject<HIS_BLTY_VOLUME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLTY_VOLUME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBltyVolumeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BLTY_VOLUME> ChangeLock(long id)
        {
            ApiResultObject<HIS_BLTY_VOLUME> result = new ApiResultObject<HIS_BLTY_VOLUME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLTY_VOLUME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBltyVolumeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BLTY_VOLUME> Lock(long id)
        {
            ApiResultObject<HIS_BLTY_VOLUME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLTY_VOLUME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBltyVolumeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BLTY_VOLUME> Unlock(long id)
        {
            ApiResultObject<HIS_BLTY_VOLUME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLTY_VOLUME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBltyVolumeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBltyVolumeTruncate(param).Truncate(id);
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
