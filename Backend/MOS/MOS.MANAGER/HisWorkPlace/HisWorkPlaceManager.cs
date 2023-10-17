using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkPlace
{
    public partial class HisWorkPlaceManager : BusinessBase
    {
        public HisWorkPlaceManager()
            : base()
        {

        }
        
        public HisWorkPlaceManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_WORK_PLACE>> Get(HisWorkPlaceFilterQuery filter)
        {
            ApiResultObject<List<HIS_WORK_PLACE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_WORK_PLACE> resultData = null;
                if (valid)
                {
                    resultData = new HisWorkPlaceGet(param).Get(filter);
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
        public ApiResultObject<HIS_WORK_PLACE> Create(HIS_WORK_PLACE data)
        {
            ApiResultObject<HIS_WORK_PLACE> result = new ApiResultObject<HIS_WORK_PLACE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_WORK_PLACE resultData = null;
                if (valid && new HisWorkPlaceCreate(param).Create(data))
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
        public ApiResultObject<HIS_WORK_PLACE> Update(HIS_WORK_PLACE data)
        {
            ApiResultObject<HIS_WORK_PLACE> result = new ApiResultObject<HIS_WORK_PLACE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_WORK_PLACE resultData = null;
                if (valid && new HisWorkPlaceUpdate(param).Update(data))
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
        public ApiResultObject<HIS_WORK_PLACE> ChangeLock(HIS_WORK_PLACE data)
        {
            ApiResultObject<HIS_WORK_PLACE> result = new ApiResultObject<HIS_WORK_PLACE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_WORK_PLACE resultData = null;
                if (valid && new HisWorkPlaceLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_WORK_PLACE data)
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
                    resultData = new HisWorkPlaceTruncate(param).Truncate(data);
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
