using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomGroup
{
    public partial class HisRoomGroupManager : BusinessBase
    {
        public HisRoomGroupManager()
            : base()
        {

        }
        
        public HisRoomGroupManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ROOM_GROUP>> Get(HisRoomGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_ROOM_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ROOM_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomGroupGet(param).Get(filter);
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
        public ApiResultObject<HIS_ROOM_GROUP> Create(HIS_ROOM_GROUP data)
        {
            ApiResultObject<HIS_ROOM_GROUP> result = new ApiResultObject<HIS_ROOM_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRoomGroupCreate(param).Create(data);
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
        public ApiResultObject<HIS_ROOM_GROUP> Update(HIS_ROOM_GROUP data)
        {
            ApiResultObject<HIS_ROOM_GROUP> result = new ApiResultObject<HIS_ROOM_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRoomGroupUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ROOM_GROUP> ChangeLock(long id)
        {
            ApiResultObject<HIS_ROOM_GROUP> result = new ApiResultObject<HIS_ROOM_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomGroupLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ROOM_GROUP> Lock(long id)
        {
            ApiResultObject<HIS_ROOM_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomGroupLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ROOM_GROUP> Unlock(long id)
        {
            ApiResultObject<HIS_ROOM_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomGroupLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRoomGroupTruncate(param).Truncate(id);
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
