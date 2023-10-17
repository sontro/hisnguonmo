using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFund
{
	public partial class HisFundManager : BusinessBase
	{
		public HisFundManager()
			: base()
		{

		}
		
		public HisFundManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_FUND>> Get(HisFundFilterQuery filter)
		{
			ApiResultObject<List<HIS_FUND>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_FUND> resultData = null;
				if (valid)
				{
					resultData = new HisFundGet(param).Get(filter);
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
		public ApiResultObject<HIS_FUND> Create(HIS_FUND data)
		{
			ApiResultObject<HIS_FUND> result = new ApiResultObject<HIS_FUND>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_FUND resultData = null;
				if (valid && new HisFundCreate(param).Create(data))
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
		public ApiResultObject<HIS_FUND> Update(HIS_FUND data)
		{
			ApiResultObject<HIS_FUND> result = new ApiResultObject<HIS_FUND>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_FUND resultData = null;
				if (valid && new HisFundUpdate(param).Update(data))
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
		public ApiResultObject<HIS_FUND> ChangeLock(HIS_FUND data)
		{
			ApiResultObject<HIS_FUND> result = new ApiResultObject<HIS_FUND>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_FUND resultData = null;
				if (valid && new HisFundLock(param).ChangeLock(data))
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
					resultData = new HisFundTruncate(param).Truncate(id);
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
