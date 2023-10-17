using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodVolume
{
    public partial class HisBloodVolumeManager : BusinessBase
    {
        public HisBloodVolumeManager()
            : base()
        {

        }
        
        public HisBloodVolumeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BLOOD_VOLUME>> Get(HisBloodVolumeFilterQuery filter)
        {
            ApiResultObject<List<HIS_BLOOD_VOLUME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BLOOD_VOLUME> resultData = null;
                if (valid)
                {
                    resultData = new HisBloodVolumeGet(param).Get(filter);
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
        public ApiResultObject<HIS_BLOOD_VOLUME> Create(HIS_BLOOD_VOLUME data)
        {
            ApiResultObject<HIS_BLOOD_VOLUME> result = new ApiResultObject<HIS_BLOOD_VOLUME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_VOLUME resultData = null;
                if (valid && new HisBloodVolumeCreate(param).Create(data))
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
        public ApiResultObject<HIS_BLOOD_VOLUME> Update(HIS_BLOOD_VOLUME data)
        {
            ApiResultObject<HIS_BLOOD_VOLUME> result = new ApiResultObject<HIS_BLOOD_VOLUME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_VOLUME resultData = null;
                if (valid && new HisBloodVolumeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BLOOD_VOLUME> ChangeLock(HIS_BLOOD_VOLUME data)
        {
            ApiResultObject<HIS_BLOOD_VOLUME> result = new ApiResultObject<HIS_BLOOD_VOLUME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_VOLUME resultData = null;
                if (valid && new HisBloodVolumeLock(param).ChangeLock(data))
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
        public ApiResultObject<HIS_BLOOD_VOLUME> Lock(long id)
        {
            ApiResultObject<HIS_BLOOD_VOLUME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLOOD_VOLUME resultData = null;
                if (valid)
                {
                    new HisBloodVolumeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BLOOD_VOLUME> Unlock(long id)
        {
            ApiResultObject<HIS_BLOOD_VOLUME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BLOOD_VOLUME resultData = null;
                if (valid)
                {
                    new HisBloodVolumeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBloodVolumeTruncate(param).Truncate(id);
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
