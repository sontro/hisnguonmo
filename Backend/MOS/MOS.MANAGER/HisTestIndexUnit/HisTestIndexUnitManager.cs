using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexUnit
{
	public class HisTestIndexUnitManager : BusinessBase
	{
		public HisTestIndexUnitManager()
			: base()
		{

		}
		
		public HisTestIndexUnitManager(CommonParam param)
			: base(param)
		{

		}

		[Logger]
		public ApiResultObject<List<HIS_TEST_INDEX_UNIT>> Get(HisTestIndexUnitFilterQuery filter)
		{
			ApiResultObject<List<HIS_TEST_INDEX_UNIT>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_TEST_INDEX_UNIT> resultData = null;
				if (valid)
				{
					resultData = new HisTestIndexUnitGet(param).Get(filter);
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
		public ApiResultObject<HIS_TEST_INDEX_UNIT> Create(HIS_TEST_INDEX_UNIT data)
		{
			ApiResultObject<HIS_TEST_INDEX_UNIT> result = new ApiResultObject<HIS_TEST_INDEX_UNIT>(null);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_TEST_INDEX_UNIT resultData = null;
				if (valid && new HisTestIndexUnitCreate(param).Create(data))
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
		public ApiResultObject<HIS_TEST_INDEX_UNIT> Update(HIS_TEST_INDEX_UNIT data)
		{
			ApiResultObject<HIS_TEST_INDEX_UNIT> result = new ApiResultObject<HIS_TEST_INDEX_UNIT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_TEST_INDEX_UNIT resultData = null;
				if (valid && new HisTestIndexUnitUpdate(param).Update(data))
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
		public ApiResultObject<HIS_TEST_INDEX_UNIT> ChangeLock(HIS_TEST_INDEX_UNIT data)
		{
			ApiResultObject<HIS_TEST_INDEX_UNIT> result = new ApiResultObject<HIS_TEST_INDEX_UNIT>(null);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_TEST_INDEX_UNIT resultData = null;
				if (valid && new HisTestIndexUnitLock(param).ChangeLock(data))
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
		public ApiResultObject<bool> Delete(HIS_TEST_INDEX_UNIT data)
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
                    resultData = new HisTestIndexUnitTruncate(param).Truncate(data);
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
