using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceUnit
{
    public partial class HisServiceUnitManager : BusinessBase
    {
        public HisServiceUnitManager()
            : base()
        {

        }

        public HisServiceUnitManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_UNIT>> Get(HisServiceUnitFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_UNIT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_UNIT> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceUnitGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_UNIT> Create(HIS_SERVICE_UNIT data)
        {
            ApiResultObject<HIS_SERVICE_UNIT> result = new ApiResultObject<HIS_SERVICE_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_UNIT resultData = null;
                if (valid && new HisServiceUnitCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERVICE_UNIT> Update(HIS_SERVICE_UNIT data)
        {
            ApiResultObject<HIS_SERVICE_UNIT> result = new ApiResultObject<HIS_SERVICE_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_UNIT resultData = null;
                if (valid && new HisServiceUnitUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_UNIT> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_UNIT> result = new ApiResultObject<HIS_SERVICE_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_SERVICE_UNIT resultData = null;
                valid = valid && new HisServiceUnitLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceUnitTruncate(param).Truncate(id);
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
