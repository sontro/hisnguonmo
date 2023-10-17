using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineInteractive
{
    public partial class HisMedicineInteractiveManager : BusinessBase
    {
        public HisMedicineInteractiveManager()
            : base()
        {

        }
        
        public HisMedicineInteractiveManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICINE_INTERACTIVE>> Get(HisMedicineInteractiveFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_INTERACTIVE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_INTERACTIVE> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineInteractiveGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDICINE_INTERACTIVE> Create(HIS_MEDICINE_INTERACTIVE data)
        {
            ApiResultObject<HIS_MEDICINE_INTERACTIVE> result = new ApiResultObject<HIS_MEDICINE_INTERACTIVE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_INTERACTIVE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicineInteractiveCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDICINE_INTERACTIVE> Update(HIS_MEDICINE_INTERACTIVE data)
        {
            ApiResultObject<HIS_MEDICINE_INTERACTIVE> result = new ApiResultObject<HIS_MEDICINE_INTERACTIVE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_INTERACTIVE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicineInteractiveUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDICINE_INTERACTIVE> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDICINE_INTERACTIVE> result = new ApiResultObject<HIS_MEDICINE_INTERACTIVE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_INTERACTIVE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineInteractiveLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_INTERACTIVE> Lock(long id)
        {
            ApiResultObject<HIS_MEDICINE_INTERACTIVE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_INTERACTIVE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineInteractiveLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_INTERACTIVE> Unlock(long id)
        {
            ApiResultObject<HIS_MEDICINE_INTERACTIVE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_INTERACTIVE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineInteractiveLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMedicineInteractiveTruncate(param).Truncate(id);
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
