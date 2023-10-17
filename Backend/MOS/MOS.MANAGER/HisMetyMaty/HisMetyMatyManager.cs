using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisMetyMaty
{
    public partial class HisMetyMatyManager : BusinessBase
    {
        public HisMetyMatyManager()
            : base()
        {

        }

        public HisMetyMatyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_METY_MATY>> Get(HisMetyMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_METY_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_METY_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMetyMatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_METY_MATY> Create(HIS_METY_MATY data)
        {
            ApiResultObject<HIS_METY_MATY> result = new ApiResultObject<HIS_METY_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_METY_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_METY_MATY>> CreateList(List<HIS_METY_MATY> listData)
        {
            ApiResultObject<List<HIS_METY_MATY>> result = new ApiResultObject<List<HIS_METY_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_METY_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyCreate(param).CreateList(listData);
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
        public ApiResultObject<HIS_METY_MATY> Update(HIS_METY_MATY data)
        {
            ApiResultObject<HIS_METY_MATY> result = new ApiResultObject<HIS_METY_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_METY_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyUpdate(param).Update(data);
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
        public ApiResultObject<List<HIS_METY_MATY>> UpdateList(List<HIS_METY_MATY> listData)
        {
            ApiResultObject<List<HIS_METY_MATY>> result = new ApiResultObject<List<HIS_METY_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_METY_MATY> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyUpdate(param).UpdateList(listData);
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
        public ApiResultObject<HIS_METY_MATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_METY_MATY> result = new ApiResultObject<HIS_METY_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_METY_MATY> Lock(long id)
        {
            ApiResultObject<HIS_METY_MATY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_METY_MATY> Unlock(long id)
        {
            ApiResultObject<HIS_METY_MATY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_MATY resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyMatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMetyMatyTruncate(param).Truncate(id);
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
                    resultData = new HisMetyMatyTruncate(param).TruncateList(ids);
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
