using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPayForm
{
	public partial class HisPayFormManager : BusinessBase
	{
		public HisPayFormManager()
			: base()
		{

		}
		
		public HisPayFormManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_PAY_FORM>> Get(HisPayFormFilterQuery filter)
		{
			ApiResultObject<List<HIS_PAY_FORM>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_PAY_FORM> resultData = null;
				if (valid)
				{
					resultData = new HisPayFormGet(param).Get(filter);
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
		public ApiResultObject<HIS_PAY_FORM> Create(HIS_PAY_FORM data)
		{
			ApiResultObject<HIS_PAY_FORM> result = new ApiResultObject<HIS_PAY_FORM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_PAY_FORM resultData = null;
				if (valid && new HisPayFormCreate(param).Create(data))
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
		public ApiResultObject<HIS_PAY_FORM> Update(HIS_PAY_FORM data)
		{
			ApiResultObject<HIS_PAY_FORM> result = new ApiResultObject<HIS_PAY_FORM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_PAY_FORM resultData = null;
				if (valid && new HisPayFormUpdate(param).Update(data))
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
		public ApiResultObject<HIS_PAY_FORM> ChangeLock(HIS_PAY_FORM data)
		{
			ApiResultObject<HIS_PAY_FORM> result = new ApiResultObject<HIS_PAY_FORM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_PAY_FORM resultData = null;
				if (valid && new HisPayFormLock(param).ChangeLock(data.ID))
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
		public ApiResultObject<bool> Delete(HIS_PAY_FORM data)
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
					resultData = new HisPayFormTruncate(param).Truncate(data);
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
