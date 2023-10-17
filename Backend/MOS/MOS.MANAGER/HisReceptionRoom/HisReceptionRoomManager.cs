using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisReceptionRoom
{
    public class HisReceptionRoomManager : BusinessBase
    {
        public HisReceptionRoomManager()
            : base()
        {

        }
        
        public HisReceptionRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_RECEPTION_ROOM>> Get(HisReceptionRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_RECEPTION_ROOM>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_RECEPTION_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisReceptionRoomGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_RECEPTION_ROOM>> GetView(HisReceptionRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_RECEPTION_ROOM>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_RECEPTION_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisReceptionRoomGet(param).GetView(filter);
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
        public ApiResultObject<HisReceptionRoomSDO> Create(HisReceptionRoomSDO data)
        {
            ApiResultObject<HisReceptionRoomSDO> result = new ApiResultObject<HisReceptionRoomSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisReceptionRoomSDO resultData = null;
                if (valid && new HisReceptionRoomCreate(param).Create(data))
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
        public ApiResultObject<HisReceptionRoomSDO> Update(HisReceptionRoomSDO data)
        {
            ApiResultObject<HisReceptionRoomSDO> result = new ApiResultObject<HisReceptionRoomSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisReceptionRoomSDO resultData = null;
                if (valid && new HisReceptionRoomUpdate(param).Update(data))
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
        public ApiResultObject<HIS_RECEPTION_ROOM> ChangeLock(HIS_RECEPTION_ROOM data)
        {
            ApiResultObject<HIS_RECEPTION_ROOM> result = new ApiResultObject<HIS_RECEPTION_ROOM>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_RECEPTION_ROOM resultData = null;
                if (valid && new HisReceptionRoomLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_RECEPTION_ROOM data)
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
                    resultData = new HisReceptionRoomTruncate(param).Truncate(data);
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
