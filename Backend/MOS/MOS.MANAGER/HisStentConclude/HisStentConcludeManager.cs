using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStentConclude
{
    public partial class HisStentConcludeManager : BusinessBase
    {
        public HisStentConcludeManager()
            : base()
        {

        }
        
        public HisStentConcludeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_STENT_CONCLUDE>> Get(HisStentConcludeFilterQuery filter)
        {
            ApiResultObject<List<HIS_STENT_CONCLUDE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_STENT_CONCLUDE> resultData = null;
                if (valid)
                {
                    resultData = new HisStentConcludeGet(param).Get(filter);
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
        public ApiResultObject<HIS_STENT_CONCLUDE> Create(HIS_STENT_CONCLUDE data)
        {
            ApiResultObject<HIS_STENT_CONCLUDE> result = new ApiResultObject<HIS_STENT_CONCLUDE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_STENT_CONCLUDE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisStentConcludeCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_STENT_CONCLUDE> Update(HIS_STENT_CONCLUDE data)
        {
            ApiResultObject<HIS_STENT_CONCLUDE> result = new ApiResultObject<HIS_STENT_CONCLUDE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_STENT_CONCLUDE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisStentConcludeUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_STENT_CONCLUDE>> CreateList(List<HIS_STENT_CONCLUDE> datas)
        {
            ApiResultObject<List<HIS_STENT_CONCLUDE>> result = new ApiResultObject<List<HIS_STENT_CONCLUDE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(datas);
                List<HIS_STENT_CONCLUDE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStentConcludeCreate(param).CreateList(datas);
                    resultData = isSuccess ? datas : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_STENT_CONCLUDE>> UpdateList(List<HIS_STENT_CONCLUDE> datas)
        {
            ApiResultObject<List<HIS_STENT_CONCLUDE>> result = new ApiResultObject<List<HIS_STENT_CONCLUDE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(datas);
                List<HIS_STENT_CONCLUDE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStentConcludeUpdate(param).UpdateList(datas);
                    resultData = isSuccess ? datas : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

		[Logger]
        public ApiResultObject<HIS_STENT_CONCLUDE> ChangeLock(long id)
        {
            ApiResultObject<HIS_STENT_CONCLUDE> result = new ApiResultObject<HIS_STENT_CONCLUDE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STENT_CONCLUDE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStentConcludeLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_STENT_CONCLUDE> Lock(long id)
        {
            ApiResultObject<HIS_STENT_CONCLUDE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STENT_CONCLUDE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStentConcludeLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_STENT_CONCLUDE> Unlock(long id)
        {
            ApiResultObject<HIS_STENT_CONCLUDE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STENT_CONCLUDE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStentConcludeLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
                    resultData = new HisStentConcludeTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> DeleteList(List<HIS_STENT_CONCLUDE> listData)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisStentConcludeTruncate(param).TruncateList(listData);
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
