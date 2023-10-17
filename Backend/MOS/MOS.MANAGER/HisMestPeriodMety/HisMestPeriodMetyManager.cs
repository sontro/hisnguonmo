using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMety
{
	public class HisMestPeriodMetyManager : BusinessBase
	{
		public HisMestPeriodMetyManager()
			: base()
		{

		}
		
		public HisMestPeriodMetyManager(CommonParam param)
			: base(param)
		{

		}

		[Logger]
		public ApiResultObject<List<HIS_MEST_PERIOD_METY>> Get(HisMestPeriodMetyFilterQuery filter)
		{
			ApiResultObject<List<HIS_MEST_PERIOD_METY>> result = null;
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_MEST_PERIOD_METY> resultData = null;
				if (valid)
				{
					resultData = new HisMestPeriodMetyGet(param).Get(filter);
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
		public ApiResultObject<List<V_HIS_MEST_PERIOD_METY>> GetView(HisMestPeriodMetyViewFilterQuery filter)
		{
			ApiResultObject<List<V_HIS_MEST_PERIOD_METY>> result = null;
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<V_HIS_MEST_PERIOD_METY> resultData = null;
				if (valid)
				{
					resultData = new HisMestPeriodMetyGet(param).GetView(filter);
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
		public ApiResultObject<HIS_MEST_PERIOD_METY> GetById(long id)
		{
			ApiResultObject<HIS_MEST_PERIOD_METY> result = null;
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsGreaterThanZero(id);
				HIS_MEST_PERIOD_METY resultData = null;
				if (valid)
				{
					HisMestPeriodMetyFilterQuery filter = new HisMestPeriodMetyFilterQuery();
					resultData = new HisMestPeriodMetyGet(param).GetById(id, filter);
				}
				result = this.PackSingleResult(resultData);
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
		public ApiResultObject<V_HIS_MEST_PERIOD_METY> GetViewById(long id)
		{
			ApiResultObject<V_HIS_MEST_PERIOD_METY> result = null;
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsGreaterThanZero(id);
				V_HIS_MEST_PERIOD_METY resultData = null;
				if (valid)
				{
					HisMestPeriodMetyViewFilterQuery filter = new HisMestPeriodMetyViewFilterQuery();
					resultData = new HisMestPeriodMetyGet(param).GetViewById(id, filter);
				}
				result = this.PackSingleResult(resultData);
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
		public ApiResultObject<HIS_MEST_PERIOD_METY> Create(HIS_MEST_PERIOD_METY data)
		{
			ApiResultObject<HIS_MEST_PERIOD_METY> result = new ApiResultObject<HIS_MEST_PERIOD_METY>(null);
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_MEST_PERIOD_METY resultData = null;
				if (valid && new HisMestPeriodMetyCreate(param).Create(data))
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
		public ApiResultObject<HIS_MEST_PERIOD_METY> Update(HIS_MEST_PERIOD_METY data)
		{
			ApiResultObject<HIS_MEST_PERIOD_METY> result = new ApiResultObject<HIS_MEST_PERIOD_METY>(null);
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_MEST_PERIOD_METY resultData = null;
				if (valid && new HisMestPeriodMetyUpdate(param).Update(data))
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
		public ApiResultObject<HIS_MEST_PERIOD_METY> ChangeLock(HIS_MEST_PERIOD_METY data)
		{
			ApiResultObject<HIS_MEST_PERIOD_METY> result = new ApiResultObject<HIS_MEST_PERIOD_METY>(null);
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsGreaterThanZero(data.ID);
				HIS_MEST_PERIOD_METY resultData = null;
				if (valid && new HisMestPeriodMetyLock(param).ChangeLock(data))
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
	}
}
