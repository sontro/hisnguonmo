using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndex
{
    public class HisTestIndexManager : MOS.MANAGER.Base.BusinessBase
    {
        public HisTestIndexManager()
            : base()
        {

        }

        public HisTestIndexManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_TEST_INDEX>> Get(HisTestIndexFilterQuery filter)
        {
            ApiResultObject<List<HIS_TEST_INDEX>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TEST_INDEX> resultData = null;
                if (valid)
                {
                    resultData = new HisTestIndexGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_TEST_INDEX>> GetView(HisTestIndexViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TEST_INDEX>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TEST_INDEX> resultData = null;
                if (valid)
                {
                    resultData = new HisTestIndexGet(param).GetView(filter);
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
        public ApiResultObject<List<HisTestIndexTDO>> GetTDO()
        {
            ApiResultObject<List<HisTestIndexTDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisTestIndexTDO> resultData = null;
                if (valid)
                {
                    resultData = new HisTestIndexGet(param).GetTDO();
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
        public ApiResultObject<HIS_TEST_INDEX> Create(HIS_TEST_INDEX data)
        {
            ApiResultObject<HIS_TEST_INDEX> result = new ApiResultObject<HIS_TEST_INDEX>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_INDEX resultData = null;
                if (valid && new HisTestIndexCreate(param).Create(data))
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
        public ApiResultObject<HIS_TEST_INDEX> Update(HIS_TEST_INDEX data)
        {
            ApiResultObject<HIS_TEST_INDEX> result = new ApiResultObject<HIS_TEST_INDEX>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_INDEX resultData = null;
                if (valid && new HisTestIndexUpdate(param).Update(data))
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
        public ApiResultObject<HIS_TEST_INDEX> ChangeLock(HIS_TEST_INDEX data)
        {
            ApiResultObject<HIS_TEST_INDEX> result = new ApiResultObject<HIS_TEST_INDEX>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_INDEX resultData = null;
                if (valid && new HisTestIndexLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_TEST_INDEX data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    HisTestIndexTruncate deleteConcrete = new HisTestIndexTruncate(param);
                    result = deleteConcrete.PackSingleResult(deleteConcrete.Truncate(data));
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
        public ApiResultObject<List<HIS_TEST_INDEX>> CreateList(List<HIS_TEST_INDEX> listData)
        {
            ApiResultObject<List<HIS_TEST_INDEX>> result = new ApiResultObject<List<HIS_TEST_INDEX>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_TEST_INDEX> resultData = null;
                if (valid && new HisTestIndexCreate(param).CreateList(listData))
                {
                    resultData = listData;
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
