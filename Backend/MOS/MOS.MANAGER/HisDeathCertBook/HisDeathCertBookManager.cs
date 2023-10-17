using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathCertBook
{
    public partial class HisDeathCertBookManager : BusinessBase
    {
        public HisDeathCertBookManager()
            : base()
        {

        }
        
        public HisDeathCertBookManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEATH_CERT_BOOK>> Get(HisDeathCertBookFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEATH_CERT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEATH_CERT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisDeathCertBookGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEATH_CERT_BOOK> Create(HIS_DEATH_CERT_BOOK data)
        {
            ApiResultObject<HIS_DEATH_CERT_BOOK> result = new ApiResultObject<HIS_DEATH_CERT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEATH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDeathCertBookCreate(param).Create(data);
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
        public ApiResultObject<HIS_DEATH_CERT_BOOK> Update(HIS_DEATH_CERT_BOOK data)
        {
            ApiResultObject<HIS_DEATH_CERT_BOOK> result = new ApiResultObject<HIS_DEATH_CERT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEATH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDeathCertBookUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DEATH_CERT_BOOK> ChangeLock(long id)
        {
            ApiResultObject<HIS_DEATH_CERT_BOOK> result = new ApiResultObject<HIS_DEATH_CERT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEATH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDeathCertBookLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DEATH_CERT_BOOK> Lock(long id)
        {
            ApiResultObject<HIS_DEATH_CERT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEATH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDeathCertBookLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DEATH_CERT_BOOK> Unlock(long id)
        {
            ApiResultObject<HIS_DEATH_CERT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEATH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDeathCertBookLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDeathCertBookTruncate(param).Truncate(id);
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
