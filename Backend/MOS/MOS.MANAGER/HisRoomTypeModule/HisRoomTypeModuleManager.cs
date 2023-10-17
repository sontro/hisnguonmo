using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisRoomTypeModule
{
    public partial class HisRoomTypeModuleManager : BusinessBase
    {
        public HisRoomTypeModuleManager()
            : base()
        {

        }

        public HisRoomTypeModuleManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> Get(HisRoomTypeModuleFilterQuery filter)
        {
            ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ROOM_TYPE_MODULE> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomTypeModuleGet(param).Get(filter);
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
        public ApiResultObject<HIS_ROOM_TYPE_MODULE> Create(HIS_ROOM_TYPE_MODULE data)
        {
            ApiResultObject<HIS_ROOM_TYPE_MODULE> result = new ApiResultObject<HIS_ROOM_TYPE_MODULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_TYPE_MODULE resultData = null;
                if (valid && new HisRoomTypeModuleCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> CreateList(List<HIS_ROOM_TYPE_MODULE> data)
        {
            ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> result = new ApiResultObject<List<HIS_ROOM_TYPE_MODULE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ROOM_TYPE_MODULE> resultData = null;
                if (valid && new HisRoomTypeModuleCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_ROOM_TYPE_MODULE> Update(HIS_ROOM_TYPE_MODULE data)
        {
            ApiResultObject<HIS_ROOM_TYPE_MODULE> result = new ApiResultObject<HIS_ROOM_TYPE_MODULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_TYPE_MODULE resultData = null;
                if (valid && new HisRoomTypeModuleUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ROOM_TYPE_MODULE> ChangeLock(long id)
        {
            ApiResultObject<HIS_ROOM_TYPE_MODULE> result = new ApiResultObject<HIS_ROOM_TYPE_MODULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_TYPE_MODULE resultData = null;
                if (valid)
                {
                    new HisRoomTypeModuleLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ROOM_TYPE_MODULE> Lock(long id)
        {
            ApiResultObject<HIS_ROOM_TYPE_MODULE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_TYPE_MODULE resultData = null;
                if (valid)
                {
                    new HisRoomTypeModuleLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ROOM_TYPE_MODULE> Unlock(long id)
        {
            ApiResultObject<HIS_ROOM_TYPE_MODULE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ROOM_TYPE_MODULE resultData = null;
                if (valid)
                {
                    new HisRoomTypeModuleLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRoomTypeModuleTruncate(param).Truncate(id);
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
                    resultData = new HisRoomTypeModuleTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> CopyByModule(HisRotyModuleCopyByModuleSDO data)
        {
            ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> result = new ApiResultObject<List<HIS_ROOM_TYPE_MODULE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ROOM_TYPE_MODULE> resultData = null;
                if (valid)
                {
                    new HisRoomTypeModuleCopyByModule(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> CopyByRoomType(HisRotyModuleCopyByRoomTypeSDO data)
        {
            ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> result = new ApiResultObject<List<HIS_ROOM_TYPE_MODULE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ROOM_TYPE_MODULE> resultData = null;
                if (valid)
                {
                    new HisRoomTypeModuleCopyByRoomType(param).Run(data, ref resultData);
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
