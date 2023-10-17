using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTracking
{
    public partial class HisTrackingManager : BusinessBase
    {
        public HisTrackingManager()
            : base()
        {

        }
        
        public HisTrackingManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TRACKING>> Get(HisTrackingFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRACKING>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRACKING> resultData = null;
                if (valid)
                {
                    resultData = new HisTrackingGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_TRACKING>> GetView(HisTrackingViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TRACKING>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TRACKING> resultData = null;
                if (valid)
                {
                    resultData = new HisTrackingGet(param).GetView(filter);
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
        public ApiResultObject<HIS_TRACKING> Create(HisTrackingSDO data)
        {
            ApiResultObject<HIS_TRACKING> result = new ApiResultObject<HIS_TRACKING>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRACKING resultData = null;
                if (valid)
                {
                    new HisTrackingCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_TRACKING> Update(HisTrackingSDO data)
        {
            ApiResultObject<HIS_TRACKING> result = new ApiResultObject<HIS_TRACKING>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRACKING resultData = null;
                if (valid)
                {
                    new HisTrackingUpdate(param).Update(data, ref resultData);
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
        public ApiResultObject<HIS_TRACKING> ChangeLock(HIS_TRACKING data)
        {
            ApiResultObject<HIS_TRACKING> result = new ApiResultObject<HIS_TRACKING>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRACKING resultData = null;
                if (valid && new HisTrackingLock(param).ChangeLock(data))
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
                    resultData = new HisTrackingTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HisTrackingTDO>> GetForEmr(HisTrackingForEmrFilter filter)
        {
            ApiResultObject<List<HisTrackingTDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisTrackingTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisTrackingGetSql(param).GetForEmr(filter);
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
        public ApiResultObject<HisTrackingDataSDO> GetData(TrackingDataInputSDO data)
        {
            ApiResultObject<HisTrackingDataSDO> result = new ApiResultObject<HisTrackingDataSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisTrackingDataSDO resultData = null;
                if (valid)
                {
                    new HisTrakingGetData(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
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
