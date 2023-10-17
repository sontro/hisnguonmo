using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisMestMatyDepa
{
    public partial class HisMestMatyDepaManager : BusinessBase
    {
        public HisMestMatyDepaManager()
            : base()
        {

        }

        public HisMestMatyDepaManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEST_MATY_DEPA>> Get(HisMestMatyDepaFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_MATY_DEPA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_MATY_DEPA> resultData = null;
                if (valid)
                {
                    resultData = new HisMestMatyDepaGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEST_MATY_DEPA> Create(HIS_MEST_MATY_DEPA data)
        {
            ApiResultObject<HIS_MEST_MATY_DEPA> result = new ApiResultObject<HIS_MEST_MATY_DEPA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_MATY_DEPA resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestMatyDepaCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEST_MATY_DEPA> Update(HIS_MEST_MATY_DEPA data)
        {
            ApiResultObject<HIS_MEST_MATY_DEPA> result = new ApiResultObject<HIS_MEST_MATY_DEPA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_MATY_DEPA resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestMatyDepaUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEST_MATY_DEPA> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEST_MATY_DEPA> result = new ApiResultObject<HIS_MEST_MATY_DEPA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_MATY_DEPA resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestMatyDepaLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_MATY_DEPA> Lock(long id)
        {
            ApiResultObject<HIS_MEST_MATY_DEPA> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_MATY_DEPA resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestMatyDepaLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_MATY_DEPA> Unlock(long id)
        {
            ApiResultObject<HIS_MEST_MATY_DEPA> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_MATY_DEPA resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestMatyDepaLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMestMatyDepaTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> CreateByMaterial(HisMestMatyDepaByMaterialSDO data)
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
                    resultData = new HisMestMatyDepaCreateByMaterial(param).Run(data);
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
