using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInfusion
{
    public partial class HisInfusionManager : BusinessBase
    {
        public HisInfusionManager()
            : base()
        {

        }
        
        public HisInfusionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_INFUSION>> Get(HisInfusionFilterQuery filter)
        {
            ApiResultObject<List<HIS_INFUSION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_INFUSION> resultData = null;
                if (valid)
                {
                    resultData = new HisInfusionGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_INFUSION>> GetView(HisInfusionViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_INFUSION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_INFUSION> resultData = null;
                if (valid)
                {
                    resultData = new HisInfusionGet(param).GetView(filter);
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
        public ApiResultObject<HisInfusionSDO> Create(HisInfusionSDO data)
        {
            ApiResultObject<HisInfusionSDO> result = new ApiResultObject<HisInfusionSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisInfusion);
                HisInfusionSDO resultData = null;
                if (valid && new HisInfusionCreate(param).Create(data))
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
        public ApiResultObject<HIS_INFUSION> Finish(HIS_INFUSION data)
        {
            ApiResultObject<HIS_INFUSION> result = new ApiResultObject<HIS_INFUSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INFUSION resultData = null;
                if (valid && new HisInfusionUpdate(param).Finish(data))
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
        public ApiResultObject<HIS_INFUSION> Unfinish(HIS_INFUSION data)
        {
            ApiResultObject<HIS_INFUSION> result = new ApiResultObject<HIS_INFUSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INFUSION resultData = null;
                if (valid && new HisInfusionUpdate(param).Unfinish(data))
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
        public ApiResultObject<HisInfusionSDO> Update(HisInfusionSDO data)
        {
            ApiResultObject<HisInfusionSDO> result = new ApiResultObject<HisInfusionSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisInfusion);
                HisInfusionSDO resultData = null;
                if (valid && new HisInfusionUpdate(param).Update(data))
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
        public ApiResultObject<HIS_INFUSION> ChangeLock(HIS_INFUSION data)
        {
            ApiResultObject<HIS_INFUSION> result = new ApiResultObject<HIS_INFUSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INFUSION resultData = null;
                if (valid && new HisInfusionLock(param).ChangeLock(data))
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
                    resultData = new HisInfusionTruncate(param).Truncate(id);
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
