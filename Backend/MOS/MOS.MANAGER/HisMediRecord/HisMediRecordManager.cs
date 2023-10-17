using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.MANAGER.HisMediRecord.Store;
using MOS.SDO;

namespace MOS.MANAGER.HisMediRecord
{
    public partial class HisMediRecordManager : BusinessBase
    {
        public HisMediRecordManager()
            : base()
        {

        }
        
        public HisMediRecordManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDI_RECORD>> Get(HisMediRecordFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_RECORD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_RECORD> resultData = null;
                if (valid)
                {
                    resultData = new HisMediRecordGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_RECORD> Create(HIS_MEDI_RECORD data)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = new ApiResultObject<HIS_MEDI_RECORD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_RECORD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediRecordCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDI_RECORD> Update(HIS_MEDI_RECORD data)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = new ApiResultObject<HIS_MEDI_RECORD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_RECORD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMediRecordUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDI_RECORD> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = new ApiResultObject<HIS_MEDI_RECORD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_RECORD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_RECORD> Lock(long id)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_RECORD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_RECORD> Unlock(long id)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_RECORD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_RECORD> Store(HisMediRecordStoreSDO sdo)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_RECORD resultData = null;
                bool isSuccess = false;
                if (valid)
                {

                    isSuccess = new HisMediRecordStore(param).Store(sdo, ref resultData);
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
        public ApiResultObject<List<HIS_MEDI_RECORD>> Store(List<HisMediRecordStoreSDO> sdos)
        {
            ApiResultObject<List<HIS_MEDI_RECORD>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_MEDI_RECORD> resultData = null;
                bool isSuccess = false;
                if (valid)
                {

                    isSuccess = new HisMediRecordStore(param).Store(sdos, ref resultData);
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
        public ApiResultObject<HIS_MEDI_RECORD> UnStore(long id)
        {
            ApiResultObject<HIS_MEDI_RECORD> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_RECORD resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMediRecordStore(param).UnStore(id, ref resultData);
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
                    resultData = new HisMediRecordTruncate(param).Truncate(id);
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
