using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisCoTreatment.Receive;

namespace MOS.MANAGER.HisCoTreatment
{
    public partial class HisCoTreatmentManager : BusinessBase
    {
        public HisCoTreatmentManager()
            : base()
        {

        }

        public HisCoTreatmentManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_CO_TREATMENT>> Get(HisCoTreatmentFilterQuery filter)
        {
            ApiResultObject<List<HIS_CO_TREATMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CO_TREATMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisCoTreatmentGet(param).Get(filter);
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
        public ApiResultObject<HIS_CO_TREATMENT> Create(HisCoTreatmentSDO data)
        {
            ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CO_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCoTreatmentCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_CO_TREATMENT> Update(HIS_CO_TREATMENT data)
        {
            ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CO_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCoTreatmentUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CO_TREATMENT> ChangeLock(long id)
        {
            ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CO_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCoTreatmentLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CO_TREATMENT> Lock(long id)
        {
            ApiResultObject<HIS_CO_TREATMENT> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CO_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCoTreatmentLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CO_TREATMENT> Unlock(long id)
        {
            ApiResultObject<HIS_CO_TREATMENT> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CO_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCoTreatmentLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCoTreatmentTruncate(param).Truncate(id);
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
        public ApiResultObject<HIS_CO_TREATMENT> Receive(HisCoTreatmentReceiveSDO data)
        {
            ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CO_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCoTreatmentReceive(param).Receive(data, ref resultData);
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
        public ApiResultObject<HIS_CO_TREATMENT> Finish(HisCoTreatmentFinishSDO data)
        {
            ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CO_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCoTreatmentFinish(param).Finish(data, ref resultData);
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

    }
}
