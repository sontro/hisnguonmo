using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBed
{
    public partial class HisBedManager : BusinessBase
    {
        public HisBedManager()
            : base()
        {

        }

        public HisBedManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_BED>> Get(HisBedFilterQuery filter)
        {
            ApiResultObject<List<HIS_BED>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BED> resultData = null;
                if (valid)
                {
                    resultData = new HisBedGet(param).Get(filter);
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
        public ApiResultObject<HIS_BED> Create(HIS_BED data)
        {
            ApiResultObject<HIS_BED> result = new ApiResultObject<HIS_BED>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BED resultData = null;
                if (valid && new HisBedCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_BED> Update(HIS_BED data)
        {
            ApiResultObject<HIS_BED> result = new ApiResultObject<HIS_BED>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BED resultData = null;
                if (valid && new HisBedUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_BED> ChangeLock(HIS_BED data)
        {
            ApiResultObject<HIS_BED> result = new ApiResultObject<HIS_BED>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BED resultData = null;
                if (valid && new HisBedLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_BED> Lock(long id)
        {
            ApiResultObject<HIS_BED> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BED resultData = null;
                if (valid)
                {
                    new HisBedLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BED> Unlock(long id)
        {
            ApiResultObject<HIS_BED> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BED resultData = null;
                if (valid)
                {
                    new HisBedLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBedTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_BED>> CreateList(List<HIS_BED> listData)
        {
            ApiResultObject<List<HIS_BED>> result = new ApiResultObject<List<HIS_BED>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_BED> resultData = null;
                if (valid && new HisBedCreate(param).CreateList(listData))
                {
                    resultData = listData;
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
        public ApiResultObject<List<HIS_BED>> UpdateMap(List<HIS_BED> listData)
        {
            ApiResultObject<List<HIS_BED>> result = new ApiResultObject<List<HIS_BED>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_BED> resultData = null;
                if (valid && new HisBedUpdateMap(param).Run(listData))
                {
                    resultData = listData;
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
