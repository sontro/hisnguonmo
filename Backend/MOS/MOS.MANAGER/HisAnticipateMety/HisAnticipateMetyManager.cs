using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateMety
{
    public partial class HisAnticipateMetyManager : BusinessBase
    {
        public HisAnticipateMetyManager()
            : base()
        {

        }
        
        public HisAnticipateMetyManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_ANTICIPATE_METY>> Get(HisAnticipateMetyFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTICIPATE_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTICIPATE_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisAnticipateMetyGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_ANTICIPATE_METY>> GetView(HisAnticipateMetyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ANTICIPATE_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ANTICIPATE_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisAnticipateMetyGet(param).GetView(filter);
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
        public ApiResultObject<HIS_ANTICIPATE_METY> Create(HIS_ANTICIPATE_METY data)
        {
            ApiResultObject<HIS_ANTICIPATE_METY> result = new ApiResultObject<HIS_ANTICIPATE_METY>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_METY resultData = null;
                if (valid && new HisAnticipateMetyCreate(param).Create(data))
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
        public ApiResultObject<HIS_ANTICIPATE_METY> Update(HIS_ANTICIPATE_METY data)
        {
            ApiResultObject<HIS_ANTICIPATE_METY> result = new ApiResultObject<HIS_ANTICIPATE_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_METY resultData = null;
                if (valid && new HisAnticipateMetyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ANTICIPATE_METY> ChangeLock(HIS_ANTICIPATE_METY data)
        {
            ApiResultObject<HIS_ANTICIPATE_METY> result = new ApiResultObject<HIS_ANTICIPATE_METY>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_METY resultData = null;
                if (valid && new HisAnticipateMetyLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ANTICIPATE_METY data)
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
                    resultData = new HisAnticipateMetyTruncate(param).Truncate(data);
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
