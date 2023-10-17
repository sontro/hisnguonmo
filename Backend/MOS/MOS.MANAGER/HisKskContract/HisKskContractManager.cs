using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisKskContract.Import;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskContract
{
    public partial class HisKskContractManager : BusinessBase
    {
        public HisKskContractManager()
            : base()
        {

        }

        public HisKskContractManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_KSK_CONTRACT>> Get(HisKskContractFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_CONTRACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_CONTRACT> resultData = null;
                if (valid)
                {
                    resultData = new HisKskContractGet(param).Get(filter);
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
        public ApiResultObject<List<HisKskContractTDO>> GetTdo(long? fromTime, long? toTime)
        {
            ApiResultObject<List<HisKskContractTDO>> result = null;

            try
            {
                bool valid = true;
                //valid = valid && IsNotNull(param);
                List<HisKskContractTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisKskContractGetTDO(param).GetTdo(fromTime, toTime);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_KSK_CONTRACT> Create(KsKContractSDO data)
        {
            ApiResultObject<HIS_KSK_CONTRACT> result = new ApiResultObject<HIS_KSK_CONTRACT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.KskContract);
                HIS_KSK_CONTRACT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskContractCreate(param).Create(data, ref resultData);
                }
                result = this.PackResult<HIS_KSK_CONTRACT>(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_KSK_CONTRACT> Update(KsKContractSDO data)
        {
            ApiResultObject<HIS_KSK_CONTRACT> result = new ApiResultObject<HIS_KSK_CONTRACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.KskContract);
                HIS_KSK_CONTRACT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                   isSuccess = new HisKskContractUpdate(param).Update(data, ref resultData);
                }
                result = this.PackResult<HIS_KSK_CONTRACT>(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_KSK_CONTRACT> ChangeLock(HIS_KSK_CONTRACT data)
        {
            ApiResultObject<HIS_KSK_CONTRACT> result = new ApiResultObject<HIS_KSK_CONTRACT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_CONTRACT resultData = null;
                if (valid && new HisKskContractLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_KSK_CONTRACT data)
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
                    resultData = new HisKskContractTruncate(param).Truncate(data);
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
        public ApiResultObject<HisKskContractSDO> Import(HisKskContractSDO data)
        {
            ApiResultObject<HisKskContractSDO> result = new ApiResultObject<HisKskContractSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisKskContractSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskContractImport(param).Run(data);
                    resultData = data;
                }
                result = this.PackResult<HisKskContractSDO>(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

    }
}
