using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomSaro
{
    public partial class HisRoomSaroManager : BusinessBase
    {
        public HisRoomSaroManager()
            : base()
        {

        }

        public HisRoomSaroManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ROOM_SARO>> Get(HisRoomSaroFilterQuery filter)
        {
            ApiResultObject<List<HIS_ROOM_SARO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ROOM_SARO> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomSaroGet(param).Get(filter);
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
        public ApiResultObject<HIS_ROOM_SARO> Create(HIS_ROOM_SARO data)
        {
            ApiResultObject<HIS_ROOM_SARO> result = new ApiResultObject<HIS_ROOM_SARO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_SARO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomSaroCreate(param).Create(data);
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
        public ApiResultObject<HIS_ROOM_SARO> Update(HIS_ROOM_SARO data)
        {
            ApiResultObject<HIS_ROOM_SARO> result = new ApiResultObject<HIS_ROOM_SARO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_SARO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomSaroUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ROOM_SARO> ChangeLock(long id)
        {
            ApiResultObject<HIS_ROOM_SARO> result = new ApiResultObject<HIS_ROOM_SARO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_SARO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomSaroLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ROOM_SARO> Lock(long id)
        {
            ApiResultObject<HIS_ROOM_SARO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_SARO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomSaroLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ROOM_SARO> Unlock(long id)
        {
            ApiResultObject<HIS_ROOM_SARO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_SARO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomSaroLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRoomSaroTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_ROOM_SARO>> CreateList(List<HIS_ROOM_SARO> listData)
        {
            ApiResultObject<List<HIS_ROOM_SARO>> result = new ApiResultObject<List<HIS_ROOM_SARO>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_ROOM_SARO> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomSaroCreate(param).CreateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<List<HIS_ROOM_SARO>> UpdateList(List<HIS_ROOM_SARO> listData)
        {
            ApiResultObject<List<HIS_ROOM_SARO>> result = new ApiResultObject<List<HIS_ROOM_SARO>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_ROOM_SARO> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomSaroUpdate(param).UpdateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(ids);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRoomSaroTruncate(param).TruncateList(ids);
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
