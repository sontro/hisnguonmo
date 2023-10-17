using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMachineServMaty
{
    public partial class HisMachineServMatyManager : BusinessBase
    {
        public HisMachineServMatyManager()
            : base()
        {

        }
        
        public HisMachineServMatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MACHINE_SERV_MATY>> Get(HisMachineServMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MACHINE_SERV_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MACHINE_SERV_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMachineServMatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_MACHINE_SERV_MATY> Create(HIS_MACHINE_SERV_MATY data)
        {
            ApiResultObject<HIS_MACHINE_SERV_MATY> result = new ApiResultObject<HIS_MACHINE_SERV_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MACHINE_SERV_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMachineServMatyCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_MACHINE_SERV_MATY>> CreateList(List<HIS_MACHINE_SERV_MATY> data)
        {
            ApiResultObject<List<HIS_MACHINE_SERV_MATY>> result = new ApiResultObject<List<HIS_MACHINE_SERV_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MACHINE_SERV_MATY> resultData = null;
                if (valid && new HisMachineServMatyCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_MACHINE_SERV_MATY> Update(HIS_MACHINE_SERV_MATY data)
        {
            ApiResultObject<HIS_MACHINE_SERV_MATY> result = new ApiResultObject<HIS_MACHINE_SERV_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MACHINE_SERV_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMachineServMatyUpdate(param).Update(data);
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
        public ApiResultObject<List<HIS_MACHINE_SERV_MATY>> UpdateList(List<HIS_MACHINE_SERV_MATY> data)
        {
            ApiResultObject<List<HIS_MACHINE_SERV_MATY>> result = new ApiResultObject<List<HIS_MACHINE_SERV_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MACHINE_SERV_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineServMatyUpdate(param).UpdateList(data);
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
        public ApiResultObject<HIS_MACHINE_SERV_MATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MACHINE_SERV_MATY> result = new ApiResultObject<HIS_MACHINE_SERV_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MACHINE_SERV_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineServMatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MACHINE_SERV_MATY> Lock(long id)
        {
            ApiResultObject<HIS_MACHINE_SERV_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MACHINE_SERV_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineServMatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MACHINE_SERV_MATY> Unlock(long id)
        {
            ApiResultObject<HIS_MACHINE_SERV_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MACHINE_SERV_MATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMachineServMatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMachineServMatyTruncate(param).Truncate(id);
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
                    resultData = new HisMachineServMatyTruncate(param).TruncateList(ids);
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
