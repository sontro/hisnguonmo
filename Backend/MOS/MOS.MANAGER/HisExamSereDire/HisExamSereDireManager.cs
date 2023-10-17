using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSereDire
{
	public partial class HisExamSereDireManager : BusinessBase
	{
		public HisExamSereDireManager()
			: base()
		{

		}
		
		public HisExamSereDireManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_EXAM_SERE_DIRE>> Get(HisExamSereDireFilterQuery filter)
		{
			ApiResultObject<List<HIS_EXAM_SERE_DIRE>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_EXAM_SERE_DIRE> resultData = null;
				if (valid)
				{
					resultData = new HisExamSereDireGet(param).Get(filter);
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
		public ApiResultObject<List<V_HIS_EXAM_SERE_DIRE>> GetView(HisExamSereDireViewFilterQuery filter)
		{
			ApiResultObject<List<V_HIS_EXAM_SERE_DIRE>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<V_HIS_EXAM_SERE_DIRE> resultData = null;
				if (valid)
				{
					resultData = new HisExamSereDireGet(param).GetView(filter);
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
		public ApiResultObject<HIS_EXAM_SERE_DIRE> Create(HIS_EXAM_SERE_DIRE data)
		{
			ApiResultObject<HIS_EXAM_SERE_DIRE> result = new ApiResultObject<HIS_EXAM_SERE_DIRE>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_EXAM_SERE_DIRE resultData = null;
				if (valid && new HisExamSereDireCreate(param).Create(data))
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

		public ApiResultObject<HIS_EXAM_SERE_DIRE> Update(HIS_EXAM_SERE_DIRE data)
		{
			ApiResultObject<HIS_EXAM_SERE_DIRE> result = new ApiResultObject<HIS_EXAM_SERE_DIRE>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_EXAM_SERE_DIRE resultData = null;
				if (valid && new HisExamSereDireUpdate(param).Update(data))
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

		public ApiResultObject<HIS_EXAM_SERE_DIRE> ChangeLock(HIS_EXAM_SERE_DIRE data)
		{
			ApiResultObject<HIS_EXAM_SERE_DIRE> result = new ApiResultObject<HIS_EXAM_SERE_DIRE>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_EXAM_SERE_DIRE resultData = null;
				if (valid && new HisExamSereDireLock(param).ChangeLock(data))
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

		public ApiResultObject<bool> Delete(HIS_EXAM_SERE_DIRE data)
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
                    resultData = new HisExamSereDireTruncate(param).Truncate(data);
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
