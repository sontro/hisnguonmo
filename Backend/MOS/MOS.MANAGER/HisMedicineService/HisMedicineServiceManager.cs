using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineService
{
    public partial class HisMedicineServiceManager : BusinessBase
    {
        public HisMedicineServiceManager()
            : base()
        {

        }
        
        public HisMedicineServiceManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICINE_SERVICE>> Get(HisMedicineServiceFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineServiceGet(param).Get(filter);
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
        public ApiResultObject<List<HIS_MEDICINE_SERVICE>> Create(List<HIS_MEDICINE_SERVICE> datas)
        {
            ApiResultObject<List<HIS_MEDICINE_SERVICE>> result = new ApiResultObject<List<HIS_MEDICINE_SERVICE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(datas);
                List<HIS_MEDICINE_SERVICE> resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineServiceCreate(param).CreateList(datas);
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
        public ApiResultObject<List<HIS_MEDICINE_SERVICE>> Update(List<HIS_MEDICINE_SERVICE> datas)
        {
            ApiResultObject<List<HIS_MEDICINE_SERVICE>> result = new ApiResultObject<List<HIS_MEDICINE_SERVICE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(datas);
                List<HIS_MEDICINE_SERVICE> resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineServiceUpdate(param).UpdateList(datas);
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
        public ApiResultObject<List<HIS_MEDICINE_SERVICE>> CreateOrUpdate(List<HIS_MEDICINE_SERVICE> datas)
        {
            ApiResultObject<List<HIS_MEDICINE_SERVICE>> result = new ApiResultObject<List<HIS_MEDICINE_SERVICE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(datas);
                List<HIS_MEDICINE_SERVICE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineServiceCreateOrUpdate(param).Run(datas, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_SERVICE> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDICINE_SERVICE> result = new ApiResultObject<HIS_MEDICINE_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineServiceLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_SERVICE> Lock(long id)
        {
            ApiResultObject<HIS_MEDICINE_SERVICE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineServiceLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_SERVICE> Unlock(long id)
        {
            ApiResultObject<HIS_MEDICINE_SERVICE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineServiceLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMedicineServiceTruncate(param).Truncate(id);
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
