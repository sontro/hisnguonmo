using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatySub
{
    public partial class HisMestPatySubManager : BusinessBase
    {
        public HisMestPatySubManager()
            : base()
        {

        }
        
        public HisMestPatySubManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEST_PATY_SUB>> Get(HisMestPatySubFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_PATY_SUB>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_PATY_SUB> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPatySubGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEST_PATY_SUB> Create(HIS_MEST_PATY_SUB data)
        {
            ApiResultObject<HIS_MEST_PATY_SUB> result = new ApiResultObject<HIS_MEST_PATY_SUB>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATY_SUB resultData = null;
                if (valid && new HisMestPatySubCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_MEST_PATY_SUB>> CreateList(List<HIS_MEST_PATY_SUB> data)
        {
            ApiResultObject<List<HIS_MEST_PATY_SUB>> result = new ApiResultObject<List<HIS_MEST_PATY_SUB>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEST_PATY_SUB> resultData = null;
                if (valid && new HisMestPatySubCreate(param).CreateList(data))
                {
                    resultData = data;
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

		[Logger]
        public ApiResultObject<HIS_MEST_PATY_SUB> Update(HIS_MEST_PATY_SUB data)
        {
            ApiResultObject<HIS_MEST_PATY_SUB> result = new ApiResultObject<HIS_MEST_PATY_SUB>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATY_SUB resultData = null;
                if (valid && new HisMestPatySubUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEST_PATY_SUB> ChangeLock(HIS_MEST_PATY_SUB data)
        {
            ApiResultObject<HIS_MEST_PATY_SUB> result = new ApiResultObject<HIS_MEST_PATY_SUB>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATY_SUB resultData = null;
                if (valid && new HisMestPatySubLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEST_PATY_SUB data)
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
                    resultData = new HisMestPatySubTruncate(param).Truncate(data);
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
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMestPatySubTruncate(param).TruncateList(ids);
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
