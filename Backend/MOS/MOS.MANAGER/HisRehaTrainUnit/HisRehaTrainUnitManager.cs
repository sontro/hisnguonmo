using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrainUnit
{
    public partial class HisRehaTrainUnitManager : BusinessBase
    {
        public HisRehaTrainUnitManager()
            : base()
        {

        }
        
        public HisRehaTrainUnitManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_REHA_TRAIN_UNIT>> Get(HisRehaTrainUnitFilterQuery filter)
        {
            ApiResultObject<List<HIS_REHA_TRAIN_UNIT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REHA_TRAIN_UNIT> resultData = null;
                if (valid)
                {
                    resultData = new HisRehaTrainUnitGet(param).Get(filter);
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
        public ApiResultObject<HIS_REHA_TRAIN_UNIT> Create(HIS_REHA_TRAIN_UNIT data)
        {
            ApiResultObject<HIS_REHA_TRAIN_UNIT> result = new ApiResultObject<HIS_REHA_TRAIN_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN_UNIT resultData = null;
                if (valid && new HisRehaTrainUnitCreate(param).Create(data))
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
        public ApiResultObject<HIS_REHA_TRAIN_UNIT> Update(HIS_REHA_TRAIN_UNIT data)
        {
            ApiResultObject<HIS_REHA_TRAIN_UNIT> result = new ApiResultObject<HIS_REHA_TRAIN_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN_UNIT resultData = null;
                if (valid && new HisRehaTrainUnitUpdate(param).Update(data))
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
        public ApiResultObject<HIS_REHA_TRAIN_UNIT> ChangeLock(HIS_REHA_TRAIN_UNIT data)
        {
            ApiResultObject<HIS_REHA_TRAIN_UNIT> result = new ApiResultObject<HIS_REHA_TRAIN_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN_UNIT resultData = null;
                if (valid && new HisRehaTrainUnitLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_REHA_TRAIN_UNIT data)
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
                    resultData = new HisRehaTrainUnitTruncate(param).Truncate(data);
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
