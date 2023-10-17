using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.MANAGER.HisContactPoint.Save;
using MOS.MANAGER.HisContactPoint.AddContact;
using MOS.SDO;
using MOS.MANAGER.HisContactPoint.SetContactLevel;

namespace MOS.MANAGER.HisContactPoint
{
    public partial class HisContactPointManager : BusinessBase
    {
        public HisContactPointManager()
            : base()
        {

        }
        
        public HisContactPointManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CONTACT_POINT>> Get(HisContactPointFilterQuery filter)
        {
            ApiResultObject<List<HIS_CONTACT_POINT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CONTACT_POINT> resultData = null;
                if (valid)
                {
                    resultData = new HisContactPointGet(param).Get(filter);
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
        public ApiResultObject<HIS_CONTACT_POINT> Create(HIS_CONTACT_POINT data)
        {
            ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONTACT_POINT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisContactPointCreate(param).Create(data);
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
        public ApiResultObject<HIS_CONTACT_POINT> Save(HIS_CONTACT_POINT data)
        {
            ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONTACT_POINT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactPointSave(param).Run(data);
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
        public ApiResultObject<HisContactResultSDO> AddContactInfo(HisContactSDO data)
        {
            ApiResultObject<HisContactResultSDO> result = new ApiResultObject<HisContactResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisContactResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactPointAddContact(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_CONTACT_POINT> SetContactLevel(HisContactLevelSDO data)
        {
            ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONTACT_POINT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactPointSetContactLevel(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_CONTACT_POINT> Update(HIS_CONTACT_POINT data)
        {
            ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONTACT_POINT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisContactPointUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CONTACT_POINT> ChangeLock(long id)
        {
            ApiResultObject<HIS_CONTACT_POINT> result = new ApiResultObject<HIS_CONTACT_POINT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTACT_POINT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactPointLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CONTACT_POINT> Lock(long id)
        {
            ApiResultObject<HIS_CONTACT_POINT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTACT_POINT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactPointLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CONTACT_POINT> Unlock(long id)
        {
            ApiResultObject<HIS_CONTACT_POINT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTACT_POINT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactPointLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisContactPointTruncate(param).Truncate(id);
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
