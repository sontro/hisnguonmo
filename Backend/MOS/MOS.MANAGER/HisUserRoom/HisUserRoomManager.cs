using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserRoom
{
    public class HisUserRoomManager : BusinessBase
    {
        public HisUserRoomManager()
            : base()
        {

        }

        public HisUserRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_USER_ROOM>> Get(HisUserRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_USER_ROOM>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_USER_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisUserRoomGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_USER_ROOM>> GetView(HisUserRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_USER_ROOM>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_USER_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisUserRoomGet(param).GetView(filter);
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
        public ApiResultObject<HIS_USER_ROOM> Create(HIS_USER_ROOM data)
        {
            ApiResultObject<HIS_USER_ROOM> result = new ApiResultObject<HIS_USER_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_ROOM resultData = null;
                if (valid && new HisUserRoomCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_USER_ROOM>> CreateList(List<HIS_USER_ROOM> data)
        {
            ApiResultObject<List<HIS_USER_ROOM>> result = new ApiResultObject<List<HIS_USER_ROOM>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_USER_ROOM> resultData = null;
                if (valid && new HisUserRoomCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_USER_ROOM> Update(HIS_USER_ROOM data)
        {
            ApiResultObject<HIS_USER_ROOM> result = new ApiResultObject<HIS_USER_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_ROOM resultData = null;
                if (valid && new HisUserRoomUpdate(param).Update(data))
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
        public ApiResultObject<HIS_USER_ROOM> ChangeLock(HIS_USER_ROOM data)
        {
            ApiResultObject<HIS_USER_ROOM> result = new ApiResultObject<HIS_USER_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_USER_ROOM resultData = null;
                if (valid && new HisUserRoomLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_USER_ROOM data)
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
                    resultData = new HisUserRoomTruncate(param).Truncate(data);
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
                    resultData = new HisUserRoomTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_USER_ROOM>> CopyByLoginname(HisUserRoomCopyByLoginnameSDO data)
        {
            ApiResultObject<List<HIS_USER_ROOM>> result = new ApiResultObject<List<HIS_USER_ROOM>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_USER_ROOM> resultData = null;
                if (valid)
                {
                    new HisUserRoomCopyByLoginname(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_USER_ROOM>> CopyByRoom(HisUserRoomCopyByRoomSDO data)
        {
            ApiResultObject<List<HIS_USER_ROOM>> result = new ApiResultObject<List<HIS_USER_ROOM>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_USER_ROOM> resultData = null;
                if (valid)
                {
                    new HisUserRoomCopyByRoom(param).Run(data, ref resultData);
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
