using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedRoom
{
    public class HisBedRoomManager : BusinessBase
    {
        public HisBedRoomManager()
            : base()
        {

        }
        
        public HisBedRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_BED_ROOM>> Get(HisBedRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_BED_ROOM>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BED_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisBedRoomGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_BED_ROOM>> GetView(HisBedRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BED_ROOM>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BED_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisBedRoomGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_BED_ROOM_1>> GetView1(HisBedRoomView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BED_ROOM_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BED_ROOM_1> resultData = null;
                if (valid)
                {
                    resultData = new HisBedRoomGet(param).GetView1(filter);
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
        public ApiResultObject<HisBedRoomSDO> Create(HisBedRoomSDO data)
        {
            ApiResultObject<HisBedRoomSDO> result = new ApiResultObject<HisBedRoomSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisBedRoomSDO resultData = null;
                if (valid && new HisBedRoomCreate(param).Create(data))
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
        public ApiResultObject<HisBedRoomSDO> Update(HisBedRoomSDO data)
        {
            ApiResultObject<HisBedRoomSDO> result = new ApiResultObject<HisBedRoomSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisBedRoomSDO resultData = null;
                if (valid && new HisBedRoomUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BED_ROOM> ChangeLock(HIS_BED_ROOM data)
        {
            ApiResultObject<HIS_BED_ROOM> result = new ApiResultObject<HIS_BED_ROOM>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_BED_ROOM resultData = null;
                if (valid && new HisBedRoomLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_BED_ROOM data)
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
                    resultData = new HisBedRoomTruncate(param).Truncate(data);
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
        public ApiResultObject<List<HisBedRoomSDO>> CreateList(List<HisBedRoomSDO> listData)
        {
            ApiResultObject<List<HisBedRoomSDO>> result = new ApiResultObject<List<HisBedRoomSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HisBedRoomSDO> resultData = null;
                if (valid && new HisBedRoomCreate(param).CreateList(listData))
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

    }
}
