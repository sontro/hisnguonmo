using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccountBook
{
    public partial class HisAccountBookManager : BusinessBase
    {
        public HisAccountBookManager()
            : base()
        {

        }

        public HisAccountBookManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ACCOUNT_BOOK>> Get(HisAccountBookFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACCOUNT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACCOUNT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisAccountBookGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_ACCOUNT_BOOK>> GetView(HisAccountBookViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ACCOUNT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ACCOUNT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisAccountBookGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_ACCOUNT_BOOK> Create(HIS_ACCOUNT_BOOK data)
        {
            ApiResultObject<HIS_ACCOUNT_BOOK> result = new ApiResultObject<HIS_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCOUNT_BOOK resultData = null;
                if (valid && new HisAccountBookCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_ACCOUNT_BOOK>> CreateList(List<HIS_ACCOUNT_BOOK> data)
        {
            ApiResultObject<List<HIS_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_ACCOUNT_BOOK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ACCOUNT_BOOK> resultData = null;
                if (valid && new HisAccountBookCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_ACCOUNT_BOOK> Update(HIS_ACCOUNT_BOOK data)
        {
            ApiResultObject<HIS_ACCOUNT_BOOK> result = new ApiResultObject<HIS_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACCOUNT_BOOK resultData = null;
                if (valid && new HisAccountBookUpdate(param).Update(data))
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
        public ApiResultObject<HIS_ACCOUNT_BOOK> ChangeLock(HIS_ACCOUNT_BOOK data)
        {
            ApiResultObject<HIS_ACCOUNT_BOOK> result = new ApiResultObject<HIS_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_ACCOUNT_BOOK resultData = null;
                if (valid && new HisAccountBookLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_ACCOUNT_BOOK data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    result = this.PackSingleResult(new HisAccountBookTruncate(param).Truncate(data));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<HisAccountBookGeneralInfoSDO>> GetGeneralInfo(HisAccountBookGeneralInfoFilter filter)
        {
            ApiResultObject<List<HisAccountBookGeneralInfoSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisAccountBookGeneralInfoSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisAccountBookGetSql(param).GetGeneralInfo(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

    }
}
