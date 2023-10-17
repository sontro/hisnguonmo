using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisBedBsty
{
    public partial class HisBedBstyManager : BusinessBase
    {
        public HisBedBstyManager()
            : base()
        {

        }

        public HisBedBstyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_BED_BSTY>> Get(HisBedBstyFilterQuery filter)
        {
            ApiResultObject<List<HIS_BED_BSTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BED_BSTY> resultData = null;
                if (valid)
                {
                    resultData = new HisBedBstyGet(param).Get(filter);
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
        public ApiResultObject<HIS_BED_BSTY> Create(HIS_BED_BSTY data)
        {
            ApiResultObject<HIS_BED_BSTY> result = new ApiResultObject<HIS_BED_BSTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BED_BSTY resultData = null;
                if (valid && new HisBedBstyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_BED_BSTY>> CreateList(List<HIS_BED_BSTY> data)
        {
            ApiResultObject<List<HIS_BED_BSTY>> result = new ApiResultObject<List<HIS_BED_BSTY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_BED_BSTY> resultData = null;
                if (valid && new HisBedBstyCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_BED_BSTY> Update(HIS_BED_BSTY data)
        {
            ApiResultObject<HIS_BED_BSTY> result = new ApiResultObject<HIS_BED_BSTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BED_BSTY resultData = null;
                if (valid && new HisBedBstyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BED_BSTY> ChangeLock(long data)
        {
            ApiResultObject<HIS_BED_BSTY> result = new ApiResultObject<HIS_BED_BSTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BED_BSTY resultData = null;
                if (valid)
                {
                    new HisBedBstyLock(param).ChangeLock(data, ref resultData);
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
        public ApiResultObject<HIS_BED_BSTY> Lock(long id)
        {
            ApiResultObject<HIS_BED_BSTY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BED_BSTY resultData = null;
                if (valid)
                {
                    new HisBedBstyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BED_BSTY> Unlock(long id)
        {
            ApiResultObject<HIS_BED_BSTY> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BED_BSTY resultData = null;
                if (valid)
                {
                    new HisBedBstyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBedBstyTruncate(param).Truncate(id);
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
                    resultData = new HisBedBstyTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_BED_BSTY>> CopyByBed(HisBedBstyCopyByBedSDO data)
        {
            ApiResultObject<List<HIS_BED_BSTY>> result = new ApiResultObject<List<HIS_BED_BSTY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_BED_BSTY> resultData = null;
                if (valid)
                {
                    new HisBedBstyCopyByBed(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_BED_BSTY>> CopyByBsty(HisBedBstyCopyByBstySDO data)
        {
            ApiResultObject<List<HIS_BED_BSTY>> result = new ApiResultObject<List<HIS_BED_BSTY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_BED_BSTY> resultData = null;
                if (valid)
                {
                    new HisBedBstyCopyByBsty(param).Run(data, ref resultData);
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
