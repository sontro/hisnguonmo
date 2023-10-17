using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomTime
{
    public partial class HisRoomTimeManager : BusinessBase
    {
        public HisRoomTimeManager()
            : base()
        {

        }

        public HisRoomTimeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ROOM_TIME>> Get(HisRoomTimeFilterQuery filter)
        {
            ApiResultObject<List<HIS_ROOM_TIME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ROOM_TIME> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomTimeGet(param).Get(filter);
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
        public ApiResultObject<HIS_ROOM_TIME> Create(HIS_ROOM_TIME data)
        {
            ApiResultObject<HIS_ROOM_TIME> result = new ApiResultObject<HIS_ROOM_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_TIME resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomTimeCreate(param).Create(data);
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
        public ApiResultObject<HIS_ROOM_TIME> Update(HIS_ROOM_TIME data)
        {
            ApiResultObject<HIS_ROOM_TIME> result = new ApiResultObject<HIS_ROOM_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_TIME resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomTimeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ROOM_TIME> ChangeLock(long id)
        {
            ApiResultObject<HIS_ROOM_TIME> result = new ApiResultObject<HIS_ROOM_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_TIME resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomTimeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ROOM_TIME> Lock(long id)
        {
            ApiResultObject<HIS_ROOM_TIME> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_TIME resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomTimeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ROOM_TIME> Unlock(long id)
        {
            ApiResultObject<HIS_ROOM_TIME> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_TIME resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomTimeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRoomTimeTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_ROOM_TIME>> CreateList(List<HIS_ROOM_TIME> listData)
        {
            ApiResultObject<List<HIS_ROOM_TIME>> result = new ApiResultObject<List<HIS_ROOM_TIME>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_ROOM_TIME> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomTimeCreate(param).CreateList(listData);
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
        public ApiResultObject<List<HIS_ROOM_TIME>> UpdateList(List<HIS_ROOM_TIME> listData)
        {
            ApiResultObject<List<HIS_ROOM_TIME>> result = new ApiResultObject<List<HIS_ROOM_TIME>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_ROOM_TIME> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRoomTimeUpdate(param).UpdateList(listData);
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
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRoomTimeTruncate(param).TruncateList(ids);
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
