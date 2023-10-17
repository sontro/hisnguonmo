using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrainType
{
    public partial class HisRehaTrainTypeManager : BusinessBase
    {
        public HisRehaTrainTypeManager()
            : base()
        {

        }
        
        public HisRehaTrainTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_REHA_TRAIN_TYPE>> Get(HisRehaTrainTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_REHA_TRAIN_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REHA_TRAIN_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisRehaTrainTypeGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_REHA_TRAIN_TYPE>> GetView(HisRehaTrainTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_REHA_TRAIN_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_REHA_TRAIN_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisRehaTrainTypeGet(param).GetView(filter);
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
        public ApiResultObject<HIS_REHA_TRAIN_TYPE> Create(HIS_REHA_TRAIN_TYPE data)
        {
            ApiResultObject<HIS_REHA_TRAIN_TYPE> result = new ApiResultObject<HIS_REHA_TRAIN_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN_TYPE resultData = null;
                if (valid && new HisRehaTrainTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_REHA_TRAIN_TYPE> Update(HIS_REHA_TRAIN_TYPE data)
        {
            ApiResultObject<HIS_REHA_TRAIN_TYPE> result = new ApiResultObject<HIS_REHA_TRAIN_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN_TYPE resultData = null;
                if (valid && new HisRehaTrainTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_REHA_TRAIN_TYPE> ChangeLock(HIS_REHA_TRAIN_TYPE data)
        {
            ApiResultObject<HIS_REHA_TRAIN_TYPE> result = new ApiResultObject<HIS_REHA_TRAIN_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN_TYPE resultData = null;
                if (valid && new HisRehaTrainTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_REHA_TRAIN_TYPE data)
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
                    resultData = new HisRehaTrainTypeTruncate(param).Truncate(data);
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
