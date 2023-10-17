using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServMaty
{
    public partial class HisSereServMatyManager : BusinessBase
    {
        public HisSereServMatyManager()
            : base()
        {

        }

        public HisSereServMatyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERE_SERV_MATY>> Get(HisSereServMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServMatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERE_SERV_MATY> Create(HIS_SERE_SERV_MATY data)
        {
            ApiResultObject<HIS_SERE_SERV_MATY> result = new ApiResultObject<HIS_SERE_SERV_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServMatyCreate(param).Create(data);
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
        public ApiResultObject<HIS_SERE_SERV_MATY> Update(HIS_SERE_SERV_MATY data)
        {
            ApiResultObject<HIS_SERE_SERV_MATY> result = new ApiResultObject<HIS_SERE_SERV_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServMatyUpdate(param).Update(data);
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
        public ApiResultObject<List<HIS_SERE_SERV_MATY>> CreateList(List<HIS_SERE_SERV_MATY> listData)
        {
            ApiResultObject<List<HIS_SERE_SERV_MATY>> result = new ApiResultObject<List<HIS_SERE_SERV_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_SERE_SERV_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServMatyCreate(param).CreateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<List<HIS_SERE_SERV_MATY>> UpdateList(List<HIS_SERE_SERV_MATY> listData)
        {
            ApiResultObject<List<HIS_SERE_SERV_MATY>> result = new ApiResultObject<List<HIS_SERE_SERV_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_SERE_SERV_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServMatyUpdate(param).UpdateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<HIS_SERE_SERV_MATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_MATY> result = new ApiResultObject<HIS_SERE_SERV_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServMatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_MATY> Lock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_MATY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServMatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_MATY> Unlock(long id)
        {
            ApiResultObject<HIS_SERE_SERV_MATY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERE_SERV_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServMatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSereServMatyTruncate(param).Truncate(id);
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
