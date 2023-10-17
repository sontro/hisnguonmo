using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSuimSetySuin
{
    public partial class HisSuimSetySuinManager : BusinessBase
    {
        public HisSuimSetySuinManager()
            : base()
        {

        }
        
        public HisSuimSetySuinManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SUIM_SETY_SUIN>> Get(HisSuimSetySuinFilterQuery filter)
        {
            ApiResultObject<List<HIS_SUIM_SETY_SUIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SUIM_SETY_SUIN> resultData = null;
                if (valid)
                {
                    resultData = new HisSuimSetySuinGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SUIM_SETY_SUIN>> GetView(HisSuimSetySuinViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SUIM_SETY_SUIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SUIM_SETY_SUIN> resultData = null;
                if (valid)
                {
                    resultData = new HisSuimSetySuinGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SUIM_SETY_SUIN> Create(HIS_SUIM_SETY_SUIN data)
        {
            ApiResultObject<HIS_SUIM_SETY_SUIN> result = new ApiResultObject<HIS_SUIM_SETY_SUIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SUIM_SETY_SUIN resultData = null;
                if (valid && new HisSuimSetySuinCreate(param).Create(data))
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
        public ApiResultObject<HIS_SUIM_SETY_SUIN> Update(HIS_SUIM_SETY_SUIN data)
        {
            ApiResultObject<HIS_SUIM_SETY_SUIN> result = new ApiResultObject<HIS_SUIM_SETY_SUIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SUIM_SETY_SUIN resultData = null;
                if (valid && new HisSuimSetySuinUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SUIM_SETY_SUIN> ChangeLock(HIS_SUIM_SETY_SUIN data)
        {
            ApiResultObject<HIS_SUIM_SETY_SUIN> result = new ApiResultObject<HIS_SUIM_SETY_SUIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SUIM_SETY_SUIN resultData = null;
                if (valid && new HisSuimSetySuinLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SUIM_SETY_SUIN data)
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
                    resultData = new HisSuimSetySuinTruncate(param).Truncate(data);
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
