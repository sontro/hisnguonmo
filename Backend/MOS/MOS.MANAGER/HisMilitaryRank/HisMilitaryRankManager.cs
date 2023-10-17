using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMilitaryRank
{
    public partial class HisMilitaryRankManager : BusinessBase
    {
        public HisMilitaryRankManager()
            : base()
        {

        }
        
        public HisMilitaryRankManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_MILITARY_RANK>> Get(HisMilitaryRankFilterQuery filter)
        {
            ApiResultObject<List<HIS_MILITARY_RANK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MILITARY_RANK> resultData = null;
                if (valid)
                {
                    resultData = new HisMilitaryRankGet(param).Get(filter);
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
        public ApiResultObject<HIS_MILITARY_RANK> Create(HIS_MILITARY_RANK data)
        {
            ApiResultObject<HIS_MILITARY_RANK> result = new ApiResultObject<HIS_MILITARY_RANK>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MILITARY_RANK resultData = null;
                if (valid && new HisMilitaryRankCreate(param).Create(data))
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
        public ApiResultObject<HIS_MILITARY_RANK> Update(HIS_MILITARY_RANK data)
        {
            ApiResultObject<HIS_MILITARY_RANK> result = new ApiResultObject<HIS_MILITARY_RANK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MILITARY_RANK resultData = null;
                if (valid && new HisMilitaryRankUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MILITARY_RANK> ChangeLock(HIS_MILITARY_RANK data)
        {
            ApiResultObject<HIS_MILITARY_RANK> result = new ApiResultObject<HIS_MILITARY_RANK>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MILITARY_RANK resultData = null;
                if (valid && new HisMilitaryRankLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MILITARY_RANK data)
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
                    resultData = new HisMilitaryRankTruncate(param).Truncate(data);
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
