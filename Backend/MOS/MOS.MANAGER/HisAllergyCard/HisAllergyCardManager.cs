using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisAllergyCard.SDO.Create;
using MOS.MANAGER.HisAllergyCard.SDO.Update;

namespace MOS.MANAGER.HisAllergyCard
{
    public partial class HisAllergyCardManager : BusinessBase
    {
        public HisAllergyCardManager()
            : base()
        {

        }

        public HisAllergyCardManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ALLERGY_CARD>> Get(HisAllergyCardFilterQuery filter)
        {
            ApiResultObject<List<HIS_ALLERGY_CARD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ALLERGY_CARD> resultData = null;
                if (valid)
                {
                    resultData = new HisAllergyCardGet(param).Get(filter);
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
        public ApiResultObject<HisAllergyCardResultSDO> Create(HisAllergyCardSDO data)
        {
            ApiResultObject<HisAllergyCardResultSDO> result = new ApiResultObject<HisAllergyCardResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisAllergyCardResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAllergyCardCreateSDO(param).Run(data, ref resultData);
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
        public ApiResultObject<HisAllergyCardResultSDO> Update(HisAllergyCardSDO data)
        {
            ApiResultObject<HisAllergyCardResultSDO> result = new ApiResultObject<HisAllergyCardResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisAllergyCardResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAllergyCardUpdateSDO(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_ALLERGY_CARD> ChangeLock(long id)
        {
            ApiResultObject<HIS_ALLERGY_CARD> result = new ApiResultObject<HIS_ALLERGY_CARD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ALLERGY_CARD resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAllergyCardLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ALLERGY_CARD> Lock(long id)
        {
            ApiResultObject<HIS_ALLERGY_CARD> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ALLERGY_CARD resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAllergyCardLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ALLERGY_CARD> Unlock(long id)
        {
            ApiResultObject<HIS_ALLERGY_CARD> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ALLERGY_CARD resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAllergyCardLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAllergyCardTruncate(param).Truncate(id);
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
