using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPeriodMaty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
	public class HisMestPeriodMatyController : BaseApiController
	{
		[HttpGet]
		[ApiParamFilter(typeof(ApiParam<HisMestPeriodMatyFilterQuery>), "param")]
		[ActionName("Get")]
		public ApiResult Get(ApiParam<HisMestPeriodMatyFilterQuery> param)
		{
			try
			{
				ApiResultObject<List<HIS_MEST_PERIOD_MATY>> result = new ApiResultObject<List<HIS_MEST_PERIOD_MATY>>(null);
				if (param != null)
				{
					HisMestPeriodMatyManager mng = new HisMestPeriodMatyManager(param.CommonParam);
					result = mng.Get(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

		[HttpGet]
		[ApiParamFilter(typeof(ApiParam<HisMestPeriodMatyViewFilterQuery>), "param")]
		[ActionName("GetView")]
		public ApiResult GetView(ApiParam<HisMestPeriodMatyViewFilterQuery> param)
		{
			try
			{
				ApiResultObject<List<V_HIS_MEST_PERIOD_MATY>> result = new ApiResultObject<List<V_HIS_MEST_PERIOD_MATY>>(null);
				if (param != null)
				{
					HisMestPeriodMatyManager mng = new HisMestPeriodMatyManager(param.CommonParam);
					result = mng.GetView(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}
		
	}
}
