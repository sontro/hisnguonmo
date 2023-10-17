using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqStt
{
	public class HisServiceReqSttManager : BusinessBase
	{
		public HisServiceReqSttManager()
			: base()
		{

		}
		
		public HisServiceReqSttManager(CommonParam param)
			: base(param)
		{

		}

		[Logger]
		public ApiResultObject<List<HIS_SERVICE_REQ_STT>> Get(HisServiceReqSttFilterQuery filter)
		{
			ApiResultObject<List<HIS_SERVICE_REQ_STT>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_SERVICE_REQ_STT> resultData = null;
				if (valid)
				{
					resultData = new HisServiceReqSttGet(param).Get(filter);
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
		public ApiResultObject<HIS_SERVICE_REQ_STT> Create(HIS_SERVICE_REQ_STT data)
		{
			ApiResultObject<HIS_SERVICE_REQ_STT> result = new ApiResultObject<HIS_SERVICE_REQ_STT>(null);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERVICE_REQ_STT resultData = null;
				if (valid && new HisServiceReqSttCreate(param).Create(data))
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
		public ApiResultObject<HIS_SERVICE_REQ_STT> Update(HIS_SERVICE_REQ_STT data)
		{
			ApiResultObject<HIS_SERVICE_REQ_STT> result = new ApiResultObject<HIS_SERVICE_REQ_STT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERVICE_REQ_STT resultData = null;
				if (valid && new HisServiceReqSttUpdate(param).Update(data))
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
		public ApiResultObject<bool> ChangeLock(long data)
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
					resultData = new HisServiceReqSttLock(param).ChangeLock(data);
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
		public ApiResultObject<bool> Delete(HIS_SERVICE_REQ_STT data)
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
                    resultData = new HisServiceReqSttTruncate(param).Truncate(data);
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
