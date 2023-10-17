using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierRoom
{
    public class HisCashierRoomManager : BusinessBase
    {
        public HisCashierRoomManager()
            : base()
        {

        }
        
        public HisCashierRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_CASHIER_ROOM>> Get(HisCashierRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_CASHIER_ROOM>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CASHIER_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisCashierRoomGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_CASHIER_ROOM>> GetView(HisCashierRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CASHIER_ROOM>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CASHIER_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisCashierRoomGet(param).GetView(filter);
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
        public ApiResultObject<HisCashierRoomSDO> Create(HisCashierRoomSDO data)
        {
            ApiResultObject<HisCashierRoomSDO> result = new ApiResultObject<HisCashierRoomSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisCashierRoomSDO resultData = null;
                if (valid && new HisCashierRoomCreate(param).Create(data))
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
        public ApiResultObject<HisCashierRoomSDO> Update(HisCashierRoomSDO data)
        {
            ApiResultObject<HisCashierRoomSDO> result = new ApiResultObject<HisCashierRoomSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisCashierRoomSDO resultData = null;
                if (valid && new HisCashierRoomUpdate(param).Update(data))
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
        public ApiResultObject<HIS_CASHIER_ROOM> ChangeLock(HIS_CASHIER_ROOM data)
        {
            ApiResultObject<HIS_CASHIER_ROOM> result = new ApiResultObject<HIS_CASHIER_ROOM>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_CASHIER_ROOM resultData = null;
                if (valid && new HisCashierRoomLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_CASHIER_ROOM data)
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
                    resultData = new HisCashierRoomTruncate(param).Truncate(data);
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
