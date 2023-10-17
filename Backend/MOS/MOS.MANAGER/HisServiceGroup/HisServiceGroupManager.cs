using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceGroup
{
    public partial class HisServiceGroupManager : BusinessBase
    {
        public HisServiceGroupManager()
            : base()
        {

        }

        public HisServiceGroupManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_GROUP>> Get(HisServiceGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceGroupGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_GROUP> Create(HIS_SERVICE_GROUP data)
        {
            ApiResultObject<HIS_SERVICE_GROUP> result = new ApiResultObject<HIS_SERVICE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_GROUP resultData = null;
                if (valid && new HisServiceGroupCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERVICE_GROUP> Update(HIS_SERVICE_GROUP data)
        {
            ApiResultObject<HIS_SERVICE_GROUP> result = new ApiResultObject<HIS_SERVICE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_GROUP resultData = null;
                if (valid && new HisServiceGroupUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_GROUP> ChangeLock(HIS_SERVICE_GROUP data)
        {
            ApiResultObject<HIS_SERVICE_GROUP> result = new ApiResultObject<HIS_SERVICE_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_GROUP resultData = null;
                if (valid && new HisServiceGroupLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SERVICE_GROUP data)
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
                    resultData = new HisServiceGroupTruncate(param).Truncate(data);
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
