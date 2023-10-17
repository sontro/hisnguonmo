using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSampleRoom
{
    public class HisSampleRoomManager : BusinessBase
    {
        public HisSampleRoomManager()
            : base()
        {

        }
        
        public HisSampleRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SAMPLE_ROOM>> Get(HisSampleRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_SAMPLE_ROOM>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SAMPLE_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisSampleRoomGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_SAMPLE_ROOM>> GetView(HisSampleRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SAMPLE_ROOM>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SAMPLE_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisSampleRoomGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisSampleRoomSDO> Create(HisSampleRoomSDO data)
        {
            ApiResultObject<HisSampleRoomSDO> result = new ApiResultObject<HisSampleRoomSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisSampleRoomSDO resultData = null;
                if (valid && new HisSampleRoomCreate(param).Create(data))
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
        public ApiResultObject<HisSampleRoomSDO> Update(HisSampleRoomSDO data)
        {
            ApiResultObject<HisSampleRoomSDO> result = new ApiResultObject<HisSampleRoomSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisSampleRoomSDO resultData = null;
                if (valid && new HisSampleRoomUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SAMPLE_ROOM> ChangeLock(HIS_SAMPLE_ROOM data)
        {
            ApiResultObject<HIS_SAMPLE_ROOM> result = new ApiResultObject<HIS_SAMPLE_ROOM>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_SAMPLE_ROOM resultData = null;
                if (valid && new HisSampleRoomLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SAMPLE_ROOM data)
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
                    resultData = new HisSampleRoomTruncate(param).Truncate(data);
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
    }
}
