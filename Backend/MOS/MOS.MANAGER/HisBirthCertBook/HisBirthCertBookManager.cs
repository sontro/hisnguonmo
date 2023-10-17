using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBirthCertBook
{
    public partial class HisBirthCertBookManager : BusinessBase
    {
        public HisBirthCertBookManager()
            : base()
        {

        }
        
        public HisBirthCertBookManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BIRTH_CERT_BOOK>> Get(HisBirthCertBookFilterQuery filter)
        {
            ApiResultObject<List<HIS_BIRTH_CERT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BIRTH_CERT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisBirthCertBookGet(param).Get(filter);
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
        public ApiResultObject<HIS_BIRTH_CERT_BOOK> Create(HIS_BIRTH_CERT_BOOK data)
        {
            ApiResultObject<HIS_BIRTH_CERT_BOOK> result = new ApiResultObject<HIS_BIRTH_CERT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BIRTH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBirthCertBookCreate(param).Create(data);
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
        public ApiResultObject<HIS_BIRTH_CERT_BOOK> Update(HIS_BIRTH_CERT_BOOK data)
        {
            ApiResultObject<HIS_BIRTH_CERT_BOOK> result = new ApiResultObject<HIS_BIRTH_CERT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BIRTH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBirthCertBookUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BIRTH_CERT_BOOK> ChangeLock(long id)
        {
            ApiResultObject<HIS_BIRTH_CERT_BOOK> result = new ApiResultObject<HIS_BIRTH_CERT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BIRTH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBirthCertBookLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BIRTH_CERT_BOOK> Lock(long id)
        {
            ApiResultObject<HIS_BIRTH_CERT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BIRTH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBirthCertBookLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BIRTH_CERT_BOOK> Unlock(long id)
        {
            ApiResultObject<HIS_BIRTH_CERT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BIRTH_CERT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBirthCertBookLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBirthCertBookTruncate(param).Truncate(id);
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
