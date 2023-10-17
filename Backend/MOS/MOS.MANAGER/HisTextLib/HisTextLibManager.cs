using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTextLib
{
	public partial class HisTextLibManager : BusinessBase
	{
		public HisTextLibManager()
			: base()
		{

		}
		
		public HisTextLibManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_TEXT_LIB>> Get(HisTextLibFilterQuery filter)
		{
			ApiResultObject<List<HIS_TEXT_LIB>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_TEXT_LIB> resultData = null;
				if (valid)
				{
					resultData = new HisTextLibGet(param).Get(filter);
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
		public ApiResultObject<HIS_TEXT_LIB> Create(HIS_TEXT_LIB data)
		{
			ApiResultObject<HIS_TEXT_LIB> result = new ApiResultObject<HIS_TEXT_LIB>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_TEXT_LIB resultData = null;
				if (valid && new HisTextLibCreate(param).Create(data))
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
		public ApiResultObject<HIS_TEXT_LIB> Update(HIS_TEXT_LIB data)
		{
			ApiResultObject<HIS_TEXT_LIB> result = new ApiResultObject<HIS_TEXT_LIB>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_TEXT_LIB resultData = null;
				if (valid && new HisTextLibUpdate(param).Update(data))
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
		public ApiResultObject<HIS_TEXT_LIB> ChangeLock(HIS_TEXT_LIB data)
		{
			ApiResultObject<HIS_TEXT_LIB> result = new ApiResultObject<HIS_TEXT_LIB>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_TEXT_LIB resultData = null;
				if (valid && new HisTextLibLock(param).ChangeLock(data))
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
		public ApiResultObject<bool> Delete(HIS_TEXT_LIB data)
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
                    resultData = new HisTextLibTruncate(param).Truncate(data);
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
