using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServicePaty
{
    public partial class HisServicePatyManager : BusinessBase
    {
        public HisServicePatyManager()
            : base()
        {

        }
        
        public HisServicePatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_PATY>> Get(HisServicePatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_PATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisServicePatyGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERVICE_PATY>> GetView(HisServicePatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_PATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisServicePatyGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_SERVICE_PATY>> GetAppliedView(long serviceId, long? treatmentTime)
        {
            ApiResultObject<List<V_HIS_SERVICE_PATY>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(serviceId);
                List<V_HIS_SERVICE_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisServicePatyGet(param).GetAppliedView(serviceId, treatmentTime);
                }
                return this.PackSingleResult(resultData);
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
        public ApiResultObject<HIS_SERVICE_PATY> Create(HIS_SERVICE_PATY data)
        {
            ApiResultObject<HIS_SERVICE_PATY> result = new ApiResultObject<HIS_SERVICE_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_PATY resultData = null;
                if (valid && new HisServicePatyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_SERVICE_PATY>> CreateList(List<HIS_SERVICE_PATY> data)
        {
            ApiResultObject<List<HIS_SERVICE_PATY>> result = new ApiResultObject<List<HIS_SERVICE_PATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_PATY> resultData = null;
                if (valid && new HisServicePatyCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_SERVICE_PATY> Update(HIS_SERVICE_PATY data)
        {
            ApiResultObject<HIS_SERVICE_PATY> result = new ApiResultObject<HIS_SERVICE_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_PATY resultData = null;
                if (valid && new HisServicePatyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_PATY> ChangeLock(HIS_SERVICE_PATY data)
        {
            ApiResultObject<HIS_SERVICE_PATY> result = new ApiResultObject<HIS_SERVICE_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_PATY resultData = null;
                if (valid && new HisServicePatyLock(param).ChangeLock(data.ID))
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
        public ApiResultObject<List<HIS_SERVICE_PATY>> UpdateList(List<HIS_SERVICE_PATY> data)
        {
            ApiResultObject<List<HIS_SERVICE_PATY>> result = new ApiResultObject<List<HIS_SERVICE_PATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_PATY> resultData = null;
                if (valid && new HisServicePatyUpdate(param).UpdateList(data))
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
        public ApiResultObject<bool> Delete(HIS_SERVICE_PATY data)
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
                    resultData = new HisServicePatyTruncate(param).Truncate(data);
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
