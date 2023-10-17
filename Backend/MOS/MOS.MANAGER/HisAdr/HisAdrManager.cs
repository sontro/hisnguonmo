using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisAdr.SDO.Update;
using MOS.MANAGER.HisAdr.SDO.Create;

namespace MOS.MANAGER.HisAdr
{
    public partial class HisAdrManager : BusinessBase
    {
        public HisAdrManager()
            : base()
        {

        }

        public HisAdrManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ADR>> Get(HisAdrFilterQuery filter)
        {
            ApiResultObject<List<HIS_ADR>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ADR> resultData = null;
                if (valid)
                {
                    resultData = new HisAdrGet(param).Get(filter);
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
        public ApiResultObject<HisAdrResultSDO> Create(HisAdrSDO data)
        {
            ApiResultObject<HisAdrResultSDO> result = new ApiResultObject<HisAdrResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisAdrResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAdrCreateSDO(param).Run(data, ref resultData);
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
        public ApiResultObject<HisAdrResultSDO> Update(HisAdrSDO data)
        {
            ApiResultObject<HisAdrResultSDO> result = new ApiResultObject<HisAdrResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisAdrResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAdrUpdateSDO(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_ADR> ChangeLock(long id)
        {
            ApiResultObject<HIS_ADR> result = new ApiResultObject<HIS_ADR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ADR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAdrLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ADR> Lock(long id)
        {
            ApiResultObject<HIS_ADR> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ADR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAdrLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ADR> Unlock(long id)
        {
            ApiResultObject<HIS_ADR> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ADR resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAdrLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAdrTruncate(param).Truncate(id);
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
