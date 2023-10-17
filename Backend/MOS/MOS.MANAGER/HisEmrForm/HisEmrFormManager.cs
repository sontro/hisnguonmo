using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrForm
{
    public partial class HisEmrFormManager : BusinessBase
    {
        public HisEmrFormManager()
            : base()
        {

        }

        public HisEmrFormManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EMR_FORM>> Get(HisEmrFormFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMR_FORM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EMR_FORM> resultData = null;
                if (valid)
                {
                    resultData = new HisEmrFormGet(param).Get(filter);
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
        public ApiResultObject<HIS_EMR_FORM> Create(HIS_EMR_FORM data)
        {
            ApiResultObject<HIS_EMR_FORM> result = new ApiResultObject<HIS_EMR_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMR_FORM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrFormCreate(param).Create(data);
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
        public ApiResultObject<HIS_EMR_FORM> Update(HIS_EMR_FORM data)
        {
            ApiResultObject<HIS_EMR_FORM> result = new ApiResultObject<HIS_EMR_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMR_FORM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrFormUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EMR_FORM> ChangeLock(long id)
        {
            ApiResultObject<HIS_EMR_FORM> result = new ApiResultObject<HIS_EMR_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMR_FORM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrFormLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EMR_FORM> Lock(long id)
        {
            ApiResultObject<HIS_EMR_FORM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMR_FORM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrFormLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EMR_FORM> Unlock(long id)
        {
            ApiResultObject<HIS_EMR_FORM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMR_FORM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrFormLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEmrFormTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> ChangeActive(List<HIS_EMR_FORM> listData)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrFormChangeActive(param).Run(listData);
                }
                result = this.PackSingleResult(isSuccess);
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
