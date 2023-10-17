using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisMestInventory
{
    public partial class HisMestInventoryManager : BusinessBase
    {
        public HisMestInventoryManager()
            : base()
        {

        }

        public HisMestInventoryManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEST_INVENTORY>> Get(HisMestInventoryFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_INVENTORY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_INVENTORY> resultData = null;
                if (valid)
                {
                    resultData = new HisMestInventoryGet(param).Get(filter);
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
        public ApiResultObject<HisMestInventoryResultSDO> Create(HisMestInventorySDO data)
        {
            ApiResultObject<HisMestInventoryResultSDO> result = new ApiResultObject<HisMestInventoryResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisMestInventoryResultSDO resultData = null;
                if (valid)
                {
                    new HisMestInventoryCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_MEST_INVENTORY> Update(HIS_MEST_INVENTORY data)
        {
            ApiResultObject<HIS_MEST_INVENTORY> result = new ApiResultObject<HIS_MEST_INVENTORY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_INVENTORY resultData = null;
                if (valid && new HisMestInventoryUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEST_INVENTORY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEST_INVENTORY> result = new ApiResultObject<HIS_MEST_INVENTORY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_INVENTORY resultData = null;
                if (valid)
                {
                    new HisMestInventoryLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_INVENTORY> Lock(long id)
        {
            ApiResultObject<HIS_MEST_INVENTORY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_INVENTORY resultData = null;
                if (valid)
                {
                    new HisMestInventoryLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_INVENTORY> Unlock(long id)
        {
            ApiResultObject<HIS_MEST_INVENTORY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_INVENTORY resultData = null;
                if (valid)
                {
                    new HisMestInventoryLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMestInventoryTruncate(param).Truncate(id);
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
