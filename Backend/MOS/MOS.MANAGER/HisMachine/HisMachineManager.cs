using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.Filter;

namespace MOS.MANAGER.HisMachine
{
    public partial class HisMachineManager : BusinessBase
    {
        public HisMachineManager()
            : base()
        {

        }

        public HisMachineManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MACHINE>> Get(HisMachineFilterQuery filter)
        {
            ApiResultObject<List<HIS_MACHINE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MACHINE> resultData = null;
                if (valid)
                {
                    resultData = new HisMachineGet(param).Get(filter);
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
        public ApiResultObject<HIS_MACHINE> Create(HIS_MACHINE data)
        {
            ApiResultObject<HIS_MACHINE> result = new ApiResultObject<HIS_MACHINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_MACHINE>> CreateList(List<HIS_MACHINE> listData)
        {
            ApiResultObject<List<HIS_MACHINE>> result = new ApiResultObject<List<HIS_MACHINE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_MACHINE> resultData = null;
                if (valid && new HisMachineCreate(param).CreateList(listData))
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
        public ApiResultObject<HIS_MACHINE> Update(HIS_MACHINE data)
        {
            ApiResultObject<HIS_MACHINE> result = new ApiResultObject<HIS_MACHINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MACHINE> ChangeLock(long id)
        {
            ApiResultObject<HIS_MACHINE> result = new ApiResultObject<HIS_MACHINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MACHINE> Lock(long id)
        {
            ApiResultObject<HIS_MACHINE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MACHINE> Unlock(long id)
        {
            ApiResultObject<HIS_MACHINE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMachineTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HisMachineCounterSDO>> GetCounter(HisMachineCounterFilter filter)
        {
            ApiResultObject<List<HisMachineCounterSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMachineCounterSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMachineGetSql(param).GetCounter(filter);
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

    }
}
