using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmergencyWtime
{
    public partial class HisEmergencyWtimeManager : BusinessBase
    {
        public HisEmergencyWtimeManager()
            : base()
        {

        }
        
        public HisEmergencyWtimeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EMERGENCY_WTIME>> Get(HisEmergencyWtimeFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMERGENCY_WTIME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EMERGENCY_WTIME> resultData = null;
                if (valid)
                {
                    resultData = new HisEmergencyWtimeGet(param).Get(filter);
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
        public ApiResultObject<HIS_EMERGENCY_WTIME> Create(HIS_EMERGENCY_WTIME data)
        {
            ApiResultObject<HIS_EMERGENCY_WTIME> result = new ApiResultObject<HIS_EMERGENCY_WTIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMERGENCY_WTIME resultData = null;
                if (valid && new HisEmergencyWtimeCreate(param).Create(data))
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
        public ApiResultObject<HIS_EMERGENCY_WTIME> Update(HIS_EMERGENCY_WTIME data)
        {
            ApiResultObject<HIS_EMERGENCY_WTIME> result = new ApiResultObject<HIS_EMERGENCY_WTIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMERGENCY_WTIME resultData = null;
                if (valid && new HisEmergencyWtimeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EMERGENCY_WTIME> ChangeLock(HIS_EMERGENCY_WTIME data)
        {
            ApiResultObject<HIS_EMERGENCY_WTIME> result = new ApiResultObject<HIS_EMERGENCY_WTIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMERGENCY_WTIME resultData = null;
                if (valid && new HisEmergencyWtimeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_EMERGENCY_WTIME data)
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
                    resultData = new HisEmergencyWtimeTruncate(param).Truncate(data);
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
