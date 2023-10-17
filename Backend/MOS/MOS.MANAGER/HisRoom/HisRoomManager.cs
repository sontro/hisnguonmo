using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom.UpdateResponsibleUser;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoom
{
    public partial class HisRoomManager : BusinessBase
    {
        public HisRoomManager()
            : base()
        {

        }

        public HisRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ROOM>> Get(HisRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_ROOM>> GetView(HisRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_ROOM_COUNTER>> GetCounterView(HisRoomCounterViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ROOM_COUNTER>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ROOM_COUNTER> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomGet(param).GetCounterView(filter);
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
        public ApiResultObject<List<V_HIS_ROOM_COUNTER_1>> GetCounter1View(HisRoomCounter1ViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ROOM_COUNTER_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ROOM_COUNTER_1> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomGet(param).GetCounter1View(filter);
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
        public ApiResultObject<List<L_HIS_ROOM_COUNTER>> GetCounterLView(HisRoomCounterLViewFilterQuery filter)
        {
            ApiResultObject<List<L_HIS_ROOM_COUNTER>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_ROOM_COUNTER> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomGet(param).GetCounterLView(filter);
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
        public ApiResultObject<HIS_ROOM> Create(HIS_ROOM data)
        {
            ApiResultObject<HIS_ROOM> result = new ApiResultObject<HIS_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM resultData = null;
                if (valid && new HisRoomCreate(param).Create(data))
                {
                    resultData = data;
                    Config.HisRoomCFG.Reload();
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
        public ApiResultObject<HIS_ROOM> Update(HIS_ROOM data)
        {
            ApiResultObject<HIS_ROOM> result = new ApiResultObject<HIS_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM resultData = null;
                if (valid && new HisRoomUpdate(param).Update(data))
                {
                    resultData = data;
                    Config.HisRoomCFG.Reload();
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
        public ApiResultObject<List<HIS_ROOM>> UpdateResponsibleUser(List<UpdateResponsibleUserSDO> data)
        {
            ApiResultObject<List<HIS_ROOM>> result = new ApiResultObject<List<HIS_ROOM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ROOM> resultData = null;
                if (valid)
                {
                    new HisRoomUpdateResponsibleUser(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_ROOM> ChangeLock(HIS_ROOM data)
        {
            ApiResultObject<HIS_ROOM> result = new ApiResultObject<HIS_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM resultData = null;
                if (valid && new HisRoomLock(param).ChangeLock(data.ID))
                {
                    resultData = data;
                    Config.HisRoomCFG.Reload();
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
        public ApiResultObject<bool> Delete(HIS_ROOM data)
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
                    resultData = new HisRoomTruncate(param).Truncate(data);
                    if (resultData) Config.HisRoomCFG.Reload();
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
        public ApiResultObject<List<L_HIS_ROOM_COUNTER_1>> GetCounterLView1(HisRoomCounterLView1FilterQuery filter)
        {
            ApiResultObject<List<L_HIS_ROOM_COUNTER_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_ROOM_COUNTER_1> resultData = null;
                if (valid)
                {
                    resultData = new HisRoomGet(param).GetCounterLView1(filter);
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
        public ApiResultObject<HIS_ROOM> UpdateJsonPrintId(HisRoomSDO data)
        {
            ApiResultObject<HIS_ROOM> result = new ApiResultObject<HIS_ROOM>(null);
            try
            {

                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool isSuccess = false;
                HIS_ROOM resultData = null;
                if (valid)
                {
                    isSuccess = new HisRoomUpdateJsonPrintId(param).Run(data, ref resultData);
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
