using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisRegisterReq
{
    public partial class HisRegisterReqManager : BusinessBase
    {
        public HisRegisterReqManager()
            : base()
        {

        }

        public HisRegisterReqManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_REGISTER_REQ>> Get(HisRegisterReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_REGISTER_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REGISTER_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisRegisterReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_REGISTER_REQ> Create(HIS_REGISTER_REQ data)
        {
            ApiResultObject<HIS_REGISTER_REQ> result = new ApiResultObject<HIS_REGISTER_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REGISTER_REQ resultData = null;
                if (valid && new HisRegisterReqCreate(param).Create(data))
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
        public ApiResultObject<HIS_REGISTER_REQ> Update(HIS_REGISTER_REQ data)
        {
            ApiResultObject<HIS_REGISTER_REQ> result = new ApiResultObject<HIS_REGISTER_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REGISTER_REQ resultData = null;
                if (valid && new HisRegisterReqUpdate(param).Update(data))
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
        public ApiResultObject<HIS_REGISTER_REQ> ChangeLock(long id)
        {
            ApiResultObject<HIS_REGISTER_REQ> result = new ApiResultObject<HIS_REGISTER_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGISTER_REQ resultData = null;
                if (valid)
                {
                    new HisRegisterReqLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_REGISTER_REQ> Lock(long id)
        {
            ApiResultObject<HIS_REGISTER_REQ> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGISTER_REQ resultData = null;
                if (valid)
                {
                    new HisRegisterReqLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_REGISTER_REQ> Unlock(long id)
        {
            ApiResultObject<HIS_REGISTER_REQ> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGISTER_REQ resultData = null;
                if (valid)
                {
                    new HisRegisterReqLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRegisterReqTruncate(param).Truncate(id);
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
        public ApiResultObject<V_HIS_REGISTER_REQ> CreateSdo(HisRegisterReqSDO data)
        {
            ApiResultObject<V_HIS_REGISTER_REQ> result = new ApiResultObject<V_HIS_REGISTER_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_REGISTER_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRegisterReqCreateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> CallPatient(CallPatientSDO data)
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
                    resultData = new HisRegisterReqCallPatient(param).Run(data);
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
