using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestRoom
{
    public class HisMestRoomManager : BusinessBase
    {
        public HisMestRoomManager()
            : base()
        {

        }

        public HisMestRoomManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEST_ROOM>> Get(HisMestRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_ROOM>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisMestRoomGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MEST_ROOM>> GetView(HisMestRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_ROOM>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisMestRoomGet(param).GetView(filter);
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
        public ApiResultObject<HIS_MEST_ROOM> GetById(long id)
        {
            ApiResultObject<HIS_MEST_ROOM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_MEST_ROOM resultData = null;
                if (valid)
                {
                    HisMestRoomFilterQuery filter = new HisMestRoomFilterQuery();
                    resultData = new HisMestRoomGet(param).GetById(id, filter);
                }
                result = this.PackSingleResult(resultData);
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
        public ApiResultObject<V_HIS_MEST_ROOM> GetViewById(long id)
        {
            ApiResultObject<V_HIS_MEST_ROOM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                V_HIS_MEST_ROOM resultData = null;
                if (valid)
                {
                    HisMestRoomViewFilterQuery filter = new HisMestRoomViewFilterQuery();
                    resultData = new HisMestRoomGet(param).GetViewById(id, filter);
                }
                result = this.PackSingleResult(resultData);
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
        public ApiResultObject<HIS_MEST_ROOM> Create(HIS_MEST_ROOM data)
        {
            ApiResultObject<HIS_MEST_ROOM> result = new ApiResultObject<HIS_MEST_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_ROOM resultData = null;
                if (valid && new HisMestRoomCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_MEST_ROOM>> CreateList(List<HIS_MEST_ROOM> data)
        {
            ApiResultObject<List<HIS_MEST_ROOM>> result = new ApiResultObject<List<HIS_MEST_ROOM>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEST_ROOM> resultData = null;
                if (valid && new HisMestRoomCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_MEST_ROOM> Update(HIS_MEST_ROOM data)
        {
            ApiResultObject<HIS_MEST_ROOM> result = new ApiResultObject<HIS_MEST_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_ROOM resultData = null;
                if (valid && new HisMestRoomUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEST_ROOM> ChangeLock(HIS_MEST_ROOM data)
        {
            ApiResultObject<HIS_MEST_ROOM> result = new ApiResultObject<HIS_MEST_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_MEST_ROOM resultData = null;
                if (valid && new HisMestRoomLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEST_ROOM data)
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
                    resultData = new HisMestRoomTruncate(param).Truncate(data);
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
                    resultData = new HisMestRoomTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_MEST_ROOM>> CopyByMediStock(HisMestRoomCopyByMediStockSDO data)
        {
            ApiResultObject<List<HIS_MEST_ROOM>> result = new ApiResultObject<List<HIS_MEST_ROOM>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEST_ROOM> resultData = null;
                if (valid)
                {
                    new HisMestRoomCopyByMediStock(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_MEST_ROOM>> CopyByRoom(HisMestRoomCopyByRoomSDO data)
        {
            ApiResultObject<List<HIS_MEST_ROOM>> result = new ApiResultObject<List<HIS_MEST_ROOM>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEST_ROOM> resultData = null;
                if (valid)
                {
                    new HisMestRoomCopyByRoom(param).Run(data, ref resultData);
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
