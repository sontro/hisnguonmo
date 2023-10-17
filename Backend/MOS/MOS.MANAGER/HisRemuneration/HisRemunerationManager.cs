using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRemuneration
{
    public partial class HisRemunerationManager : BusinessBase
    {
        public HisRemunerationManager()
            : base()
        {

        }
        
        public HisRemunerationManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_REMUNERATION>> Get(HisRemunerationFilterQuery filter)
        {
            ApiResultObject<List<HIS_REMUNERATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REMUNERATION> resultData = null;
                if (valid)
                {
                    resultData = new HisRemunerationGet(param).Get(filter);
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
        public ApiResultObject<HIS_REMUNERATION> Create(HIS_REMUNERATION data)
        {
            ApiResultObject<HIS_REMUNERATION> result = new ApiResultObject<HIS_REMUNERATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REMUNERATION resultData = null;
                if (valid && new HisRemunerationCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_REMUNERATION>> CreateList(List<HIS_REMUNERATION> data)
        {
            ApiResultObject<List<HIS_REMUNERATION>> result = new ApiResultObject<List<HIS_REMUNERATION>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_REMUNERATION> resultData = null;
                if (valid && new HisRemunerationCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_REMUNERATION> Update(HIS_REMUNERATION data)
        {
            ApiResultObject<HIS_REMUNERATION> result = new ApiResultObject<HIS_REMUNERATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REMUNERATION resultData = null;
                if (valid && new HisRemunerationUpdate(param).Update(data))
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
        public ApiResultObject<List<HIS_REMUNERATION>> UpdateList(List<HIS_REMUNERATION> data)
        {
            ApiResultObject<List<HIS_REMUNERATION>> result = new ApiResultObject<List<HIS_REMUNERATION>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_REMUNERATION> resultData = null;
                if (valid && new HisRemunerationUpdate(param).UpdateList(data))
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
        public ApiResultObject<HIS_REMUNERATION> ChangeLock(long id)
        {
            ApiResultObject<HIS_REMUNERATION> result = new ApiResultObject<HIS_REMUNERATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REMUNERATION resultData = null;
                if (valid)
                {
                    new HisRemunerationLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_REMUNERATION> Lock(long id)
        {
            ApiResultObject<HIS_REMUNERATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REMUNERATION resultData = null;
                if (valid)
                {
                    new HisRemunerationLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_REMUNERATION> Unlock(long id)
        {
            ApiResultObject<HIS_REMUNERATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REMUNERATION resultData = null;
                if (valid)
                {
                    new HisRemunerationLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRemunerationTruncate(param).Truncate(id);
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
                    resultData = new HisRemunerationTruncate(param).TruncateList(ids);
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
