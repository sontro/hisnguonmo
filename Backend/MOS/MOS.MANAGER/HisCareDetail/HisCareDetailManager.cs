using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareDetail
{
    public partial class HisCareDetailManager : BusinessBase
    {
        public HisCareDetailManager()
            : base()
        {

        }
        
        public HisCareDetailManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_CARE_DETAIL>> Get(HisCareDetailFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARE_DETAIL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARE_DETAIL> resultData = null;
                if (valid)
                {
                    resultData = new HisCareDetailGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_CARE_DETAIL>> GetView(HisCareDetailViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CARE_DETAIL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CARE_DETAIL> resultData = null;
                if (valid)
                {
                    resultData = new HisCareDetailGet(param).GetView(filter);
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
        public ApiResultObject<HIS_CARE_DETAIL> Create(HIS_CARE_DETAIL data)
        {
            ApiResultObject<HIS_CARE_DETAIL> result = new ApiResultObject<HIS_CARE_DETAIL>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_DETAIL resultData = null;
                if (valid && new HisCareDetailCreate(param).Create(data))
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
        public ApiResultObject<HIS_CARE_DETAIL> Update(HIS_CARE_DETAIL data)
        {
            ApiResultObject<HIS_CARE_DETAIL> result = new ApiResultObject<HIS_CARE_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_DETAIL resultData = null;
                if (valid && new HisCareDetailUpdate(param).Update(data))
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
        public ApiResultObject<HIS_CARE_DETAIL> ChangeLock(HIS_CARE_DETAIL data)
        {
            ApiResultObject<HIS_CARE_DETAIL> result = new ApiResultObject<HIS_CARE_DETAIL>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_DETAIL resultData = null;
                if (valid && new HisCareDetailLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_CARE_DETAIL data)
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
                    resultData = new HisCareDetailTruncate(param).Truncate(data);
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
