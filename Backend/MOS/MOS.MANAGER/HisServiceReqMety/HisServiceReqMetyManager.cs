using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMety
{
    public partial class HisServiceReqMetyManager : BusinessBase
    {
        public HisServiceReqMetyManager()
            : base()
        {

        }

        public HisServiceReqMetyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_REQ_METY>> Get(HisServiceReqMetyFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_REQ_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_REQ_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqMetyGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_REQ_METY> Create(HIS_SERVICE_REQ_METY data)
        {
            ApiResultObject<HIS_SERVICE_REQ_METY> result = new ApiResultObject<HIS_SERVICE_REQ_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_METY resultData = null;
                if (valid && new HisServiceReqMetyCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERVICE_REQ_METY> Update(HIS_SERVICE_REQ_METY data)
        {
            ApiResultObject<HIS_SERVICE_REQ_METY> result = new ApiResultObject<HIS_SERVICE_REQ_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_METY resultData = null;
                if (valid && new HisServiceReqMetyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_REQ_METY> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ_METY> result = new ApiResultObject<HIS_SERVICE_REQ_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ_METY resultData = null;
                if (valid)
                {
                    new HisServiceReqMetyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ_METY> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ_METY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ_METY resultData = null;
                if (valid)
                {
                    new HisServiceReqMetyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_REQ_METY> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_REQ_METY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_REQ_METY resultData = null;
                if (valid)
                {
                    new HisServiceReqMetyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisServiceReqMetyTruncate(param).Truncate(id);
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
        public ApiResultObject<HIS_SERVICE_REQ_METY> UpdateCommonInfo(HIS_SERVICE_REQ_METY data)
        {
            ApiResultObject<HIS_SERVICE_REQ_METY> result = new ApiResultObject<HIS_SERVICE_REQ_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_REQ_METY resultData = null;
                if (valid)
                {
                    new HisServiceReqMetyUpdateCommonInfo(param).Run(data, ref resultData);
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
