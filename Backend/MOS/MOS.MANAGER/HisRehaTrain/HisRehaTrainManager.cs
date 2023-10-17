using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrain
{
    public partial class HisRehaTrainManager : BusinessBase
    {
        public HisRehaTrainManager()
            : base()
        {

        }
        
        public HisRehaTrainManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_REHA_TRAIN>> Get(HisRehaTrainFilterQuery filter)
        {
            ApiResultObject<List<HIS_REHA_TRAIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REHA_TRAIN> resultData = null;
                if (valid)
                {
                    resultData = new HisRehaTrainGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_REHA_TRAIN>> GetView(HisRehaTrainViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_REHA_TRAIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_REHA_TRAIN> resultData = null;
                if (valid)
                {
                    resultData = new HisRehaTrainGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_REHA_TRAIN>> GetViewByRehaSumId(long rehaSumId)
        {
            ApiResultObject<List<V_HIS_REHA_TRAIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_REHA_TRAIN> resultData = null;
                if (valid)
                {
                    resultData = new HisRehaTrainGet(param).GetViewByRehaSumId(rehaSumId);
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
        public ApiResultObject<List<V_HIS_REHA_TRAIN>> GetViewByServiceReqId(long serviceReqId)
        {
            ApiResultObject<List<V_HIS_REHA_TRAIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_REHA_TRAIN> resultData = null;
                if (valid)
                {
                    resultData = new HisRehaTrainGet(param).GetViewByServiceReqId(serviceReqId);
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
        public ApiResultObject<HIS_REHA_TRAIN> Create(HIS_REHA_TRAIN data)
        {
            ApiResultObject<HIS_REHA_TRAIN> result = new ApiResultObject<HIS_REHA_TRAIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN resultData = null;
                if (valid && new HisRehaTrainCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_REHA_TRAIN>> CreateList(List<HIS_REHA_TRAIN> data)
        {
            ApiResultObject<List<HIS_REHA_TRAIN>> result = new ApiResultObject<List<HIS_REHA_TRAIN>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_REHA_TRAIN> resultData = null;
                if (valid && new HisRehaTrainCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_REHA_TRAIN> Update(HIS_REHA_TRAIN data)
        {
            ApiResultObject<HIS_REHA_TRAIN> result = new ApiResultObject<HIS_REHA_TRAIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN resultData = null;
                if (valid && new HisRehaTrainUpdate(param).Update(data))
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
        public ApiResultObject<HIS_REHA_TRAIN> ChangeLock(HIS_REHA_TRAIN data)
        {
            ApiResultObject<HIS_REHA_TRAIN> result = new ApiResultObject<HIS_REHA_TRAIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN resultData = null;
                if (valid && new HisRehaTrainLock(param).ChangeLock(data))
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
                    resultData = new HisRehaTrainTruncate(param).Truncate(id);
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
