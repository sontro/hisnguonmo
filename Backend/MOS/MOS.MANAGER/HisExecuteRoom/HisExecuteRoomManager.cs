using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRoom
{
    public partial class HisExecuteRoomManager : BusinessBase
    {
        public HisExecuteRoomManager()
            : base()
        {

        }

        public HisExecuteRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EXECUTE_ROOM>> Get(HisExecuteRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXECUTE_ROOM>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXECUTE_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteRoomGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_EXECUTE_ROOM>> GetView(HisExecuteRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXECUTE_ROOM>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXECUTE_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteRoomGet(param).GetView(filter);
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
        public ApiResultObject<HisExecuteRoomSDO> Create(HisExecuteRoomSDO data)
        {
            ApiResultObject<HisExecuteRoomSDO> result = new ApiResultObject<HisExecuteRoomSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisExecuteRoomSDO resultData = null;
                if (valid && new HisExecuteRoomCreate(param).Create(data))
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
        public ApiResultObject<HisExecuteRoomSDO> Update(HisExecuteRoomSDO data)
        {
            ApiResultObject<HisExecuteRoomSDO> result = new ApiResultObject<HisExecuteRoomSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisExecuteRoomSDO resultData = null;
                if (valid && new HisExecuteRoomUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXECUTE_ROOM> ChangeLock(HIS_EXECUTE_ROOM data)
        {
            ApiResultObject<HIS_EXECUTE_ROOM> result = new ApiResultObject<HIS_EXECUTE_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_EXECUTE_ROOM resultData = null;
                if (valid && new HisExecuteRoomLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_EXECUTE_ROOM data)
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
                    resultData = new HisExecuteRoomTruncate(param).Truncate(data);
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
        public ApiResultObject<List<HisExecuteRoomSDO>> CreateList(List<HisExecuteRoomSDO> listData)
        {
            ApiResultObject<List<HisExecuteRoomSDO>> result = new ApiResultObject<List<HisExecuteRoomSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HisExecuteRoomSDO> resultData = null;
                if (valid && new HisExecuteRoomCreate(param).CreateList(listData))
                {
                    resultData = listData;
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
        public ApiResultObject<List<HisExecuteRoomAppointedSDO>> GetCountAppointed(HisExecuteRoomAppointedFilter filter)
        {
            ApiResultObject<List<HisExecuteRoomAppointedSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisExecuteRoomAppointedSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisExecuteRoomGetSql(param).GetCountAppointed(filter);
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

    }
}
