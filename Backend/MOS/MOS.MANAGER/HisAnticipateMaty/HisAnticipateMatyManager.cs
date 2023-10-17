using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateMaty
{
    public partial class HisAnticipateMatyManager : BusinessBase
    {
        public HisAnticipateMatyManager()
            : base()
        {

        }
        
        public HisAnticipateMatyManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_ANTICIPATE_MATY>> Get(HisAnticipateMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTICIPATE_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTICIPATE_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisAnticipateMatyGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_ANTICIPATE_MATY>> GetView(HisAnticipateMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ANTICIPATE_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ANTICIPATE_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisAnticipateMatyGet(param).GetView(filter);
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
        public ApiResultObject<HIS_ANTICIPATE_MATY> Create(HIS_ANTICIPATE_MATY data)
        {
            ApiResultObject<HIS_ANTICIPATE_MATY> result = new ApiResultObject<HIS_ANTICIPATE_MATY>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_MATY resultData = null;
                if (valid && new HisAnticipateMatyCreate(param).Create(data))
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
        public ApiResultObject<HIS_ANTICIPATE_MATY> Update(HIS_ANTICIPATE_MATY data)
        {
            ApiResultObject<HIS_ANTICIPATE_MATY> result = new ApiResultObject<HIS_ANTICIPATE_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_MATY resultData = null;
                if (valid && new HisAnticipateMatyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ANTICIPATE_MATY> ChangeLock(HIS_ANTICIPATE_MATY data)
        {
            ApiResultObject<HIS_ANTICIPATE_MATY> result = new ApiResultObject<HIS_ANTICIPATE_MATY>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_MATY resultData = null;
                if (valid && new HisAnticipateMatyLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ANTICIPATE_MATY data)
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
                    resultData = new HisAnticipateMatyTruncate(param).Truncate(data);
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
