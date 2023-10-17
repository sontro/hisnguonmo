using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisExroRoom
{
    public partial class HisExroRoomManager : BusinessBase
    {
        public HisExroRoomManager()
            : base()
        {

        }

        public HisExroRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EXRO_ROOM>> Get(HisExroRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXRO_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXRO_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisExroRoomGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXRO_ROOM> Create(HIS_EXRO_ROOM data)
        {
            ApiResultObject<HIS_EXRO_ROOM> result = new ApiResultObject<HIS_EXRO_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXRO_ROOM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomCreate(param).Create(data);
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
        public ApiResultObject<HIS_EXRO_ROOM> Update(HIS_EXRO_ROOM data)
        {
            ApiResultObject<HIS_EXRO_ROOM> result = new ApiResultObject<HIS_EXRO_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXRO_ROOM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EXRO_ROOM> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXRO_ROOM> result = new ApiResultObject<HIS_EXRO_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXRO_ROOM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXRO_ROOM> Lock(long id)
        {
            ApiResultObject<HIS_EXRO_ROOM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXRO_ROOM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXRO_ROOM> Unlock(long id)
        {
            ApiResultObject<HIS_EXRO_ROOM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXRO_ROOM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExroRoomTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_EXRO_ROOM>> CreateList(List<HIS_EXRO_ROOM> listData)
        {
            ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_EXRO_ROOM> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomCreate(param).CreateList(listData);
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
        public ApiResultObject<List<HIS_EXRO_ROOM>> UpdateList(List<HIS_EXRO_ROOM> listData)
        {
            ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_EXRO_ROOM> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomUpdate(param).UpdateList(listData);
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
                    resultData = new HisExroRoomTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_EXRO_ROOM>> CopyByExecuteRoom(HisExroRoomCopyByExroSDO data)
        {
            ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_EXRO_ROOM> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomCopyByExro(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_EXRO_ROOM>> CopyByRoom(HisExroRoomCopyByRoomSDO data)
        {
            ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_EXRO_ROOM> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExroRoomCopyByRoom(param).Run(data, ref resultData);
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

    }
}
