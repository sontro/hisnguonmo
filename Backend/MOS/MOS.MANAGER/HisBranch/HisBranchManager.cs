using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBranch
{
    public partial class HisBranchManager : BusinessBase
    {
        public HisBranchManager()
            : base()
        {

        }
        
        public HisBranchManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BRANCH>> Get(HisBranchFilterQuery filter)
        {
            ApiResultObject<List<HIS_BRANCH>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BRANCH> resultData = null;
                if (valid)
                {
                    resultData = new HisBranchGet(param).Get(filter);
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
        public ApiResultObject<HIS_BRANCH> Create(HisBranchSDO data)
        {
            ApiResultObject<HIS_BRANCH> result = new ApiResultObject<HIS_BRANCH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BRANCH resultData = null;
                if (valid && new HisBranchCreate(param).Create(data))
                {
                    resultData = data.Branch;
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
        public ApiResultObject<HIS_BRANCH> Update(HisBranchSDO data)
        {
            ApiResultObject<HIS_BRANCH> result = new ApiResultObject<HIS_BRANCH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BRANCH resultData = null;
                if (valid && new HisBranchUpdate(param).Update(data))
                {
                    resultData = data.Branch;
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
        public ApiResultObject<HIS_BRANCH> ChangeLock(long data)
        {
            ApiResultObject<HIS_BRANCH> result = new ApiResultObject<HIS_BRANCH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BRANCH resultData = null;
                if (valid)
                {
                    new HisBranchLock(param).ChangeLock(data, ref resultData);
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
                    resultData = new HisBranchTruncate(param).Truncate(id);
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
        public ApiResultObject<HIS_BRANCH> CreateWeb(HisBranchWebSDO data)
        {
            ApiResultObject<HIS_BRANCH> result = new ApiResultObject<HIS_BRANCH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BRANCH resultData = null;
                if (valid && new HisBranchWebCreate(param).Create(data))
                {
                    resultData = data.Branch;
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
