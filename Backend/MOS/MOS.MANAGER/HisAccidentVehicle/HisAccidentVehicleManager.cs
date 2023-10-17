using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentVehicle
{
    public partial class HisAccidentVehicleManager : BusinessBase
    {
        public HisAccidentVehicleManager()
            : base()
        {

        }
        
        public HisAccidentVehicleManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ACCIDENT_VEHICLE>> Get(HisAccidentVehicleFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCIDENT_VEHICLE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCIDENT_VEHICLE> resultData = null;
                if (valid)
                {
                    resultData = new HisAccidentVehicleGet(param).Get(filter);
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
        public ApiResultObject<HIS_ACCIDENT_VEHICLE> Create(HIS_ACCIDENT_VEHICLE data)
        {
            ApiResultObject<HIS_ACCIDENT_VEHICLE> result = new ApiResultObject<HIS_ACCIDENT_VEHICLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_VEHICLE resultData = null;
                if (valid && new HisAccidentVehicleCreate(param).Create(data))
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
        public ApiResultObject<HIS_ACCIDENT_VEHICLE> Update(HIS_ACCIDENT_VEHICLE data)
        {
            ApiResultObject<HIS_ACCIDENT_VEHICLE> result = new ApiResultObject<HIS_ACCIDENT_VEHICLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_VEHICLE resultData = null;
                if (valid && new HisAccidentVehicleUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCIDENT_VEHICLE> ChangeLock(HIS_ACCIDENT_VEHICLE data)
        {
            ApiResultObject<HIS_ACCIDENT_VEHICLE> result = new ApiResultObject<HIS_ACCIDENT_VEHICLE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_VEHICLE resultData = null;
                if (valid && new HisAccidentVehicleLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACCIDENT_VEHICLE data)
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
                    resultData = new HisAccidentVehicleTruncate(param).Truncate(data);
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
