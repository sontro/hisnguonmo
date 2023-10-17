using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexRange
{
    public class HisTestIndexRangeManager : BusinessBase
    {
        public HisTestIndexRangeManager()
			: base()
		{

		}
		
		public HisTestIndexRangeManager(CommonParam param)
			: base(param)
		{

		}

		[Logger]
		public ApiResultObject<List<HIS_TEST_INDEX_RANGE>> Get(HisTestIndexRangeFilterQuery filter)
		{
			ApiResultObject<List<HIS_TEST_INDEX_RANGE>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_TEST_INDEX_RANGE> resultData = null;
				if (valid)
				{
					resultData = new HisTestIndexRangeGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_TEST_INDEX_RANGE>> GetView(HisTestIndexRangeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TEST_INDEX_RANGE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TEST_INDEX_RANGE> resultData = null;
                if (valid)
                {
                    resultData = new HisTestIndexRangeGet(param).GetView(filter);
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
		public ApiResultObject<HIS_TEST_INDEX_RANGE> Create(HIS_TEST_INDEX_RANGE data)
		{
			ApiResultObject<HIS_TEST_INDEX_RANGE> result = new ApiResultObject<HIS_TEST_INDEX_RANGE>(null);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_TEST_INDEX_RANGE resultData = null;
				if (valid && new HisTestIndexRangeCreate(param).Create(data))
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
		public ApiResultObject<HIS_TEST_INDEX_RANGE> Update(HIS_TEST_INDEX_RANGE data)
		{
			ApiResultObject<HIS_TEST_INDEX_RANGE> result = new ApiResultObject<HIS_TEST_INDEX_RANGE>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_TEST_INDEX_RANGE resultData = null;
				if (valid && new HisTestIndexRangeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_TEST_INDEX_RANGE> ChangeLock(HIS_TEST_INDEX_RANGE data)
		{
            ApiResultObject<HIS_TEST_INDEX_RANGE> result = new ApiResultObject<HIS_TEST_INDEX_RANGE>(null);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
                HIS_TEST_INDEX_RANGE resultData = null;
				if (valid && new HisTestIndexRangeLock(param).ChangeLock(data))
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
		public ApiResultObject<bool> Delete(HIS_TEST_INDEX_RANGE data)
		{
			ApiResultObject<bool> result = new ApiResultObject<bool>(false);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				bool resultData = false;
				if (valid)
				{
                    resultData = new HisTestIndexRangeTruncate(param).Truncate(data);
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
        public ApiResultObject<List<HIS_TEST_INDEX_RANGE>> CreateList(List<HIS_TEST_INDEX_RANGE> listData)
        {
            ApiResultObject<List<HIS_TEST_INDEX_RANGE>> result = new ApiResultObject<List<HIS_TEST_INDEX_RANGE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_TEST_INDEX_RANGE> resultData = null;
                if (valid && new HisTestIndexRangeCreate(param).CreateList(listData))
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
