using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestPropose;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
	public partial class HisImpMestProposeController : BaseApiController
	{
		[HttpGet]
		[ApiParamFilter(typeof(ApiParam<HisImpMestProposeFilterQuery>), "param")]
		[ActionName("Get")]
		public ApiResult Get(ApiParam<HisImpMestProposeFilterQuery> param)
		{
			try
			{
				ApiResultObject<List<HIS_IMP_MEST_PROPOSE>> result = new ApiResultObject<List<HIS_IMP_MEST_PROPOSE>>(null);
				if (param != null)
				{
					HisImpMestProposeManager mng = new HisImpMestProposeManager(param.CommonParam);
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
		
		[HttpPost]
		[ActionName("Create")]
		public ApiResult Create(ApiParam<HisImpMestProposeSDO> param)
		{
			try
			{
                ApiResultObject<HisImpMestProposeResultSDO> result = new ApiResultObject<HisImpMestProposeResultSDO>(null);
				if (param != null)
				{
					HisImpMestProposeManager mng = new HisImpMestProposeManager(param.CommonParam);
					result = mng.Create(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

		[HttpPost]
		[ActionName("Update")]
        public ApiResult Update(ApiParam<HisImpMestProposeSDO> param)
		{
			try
			{
                ApiResultObject<HisImpMestProposeResultSDO> result = new ApiResultObject<HisImpMestProposeResultSDO>(null);
				if (param != null)
				{
					HisImpMestProposeManager mng = new HisImpMestProposeManager(param.CommonParam);
					result = mng.Update(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

		[HttpPost]
		[ActionName("Delete")]
		public ApiResult Delete(ApiParam<HisImpMestProposeDeleteSDO> param)
		{
			try
			{
				ApiResultObject<bool> result = new ApiResultObject<bool>(false);
				if (param != null)
				{
					HisImpMestProposeManager mng = new HisImpMestProposeManager(param.CommonParam);
					result = mng.Delete(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}
		
		[HttpPost]
		[ActionName("ChangeLock")]
		public ApiResult ChangeLock(ApiParam<long> param)
		{
			try
			{
				ApiResultObject<HIS_IMP_MEST_PROPOSE> result = new ApiResultObject<HIS_IMP_MEST_PROPOSE>(null);
				if (param != null && param.ApiData != null)
				{
					HisImpMestProposeManager mng = new HisImpMestProposeManager(param.CommonParam);
					result = mng.ChangeLock(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}
		
		[HttpPost]
		[ActionName("Lock")]
		public ApiResult Lock(ApiParam<long> param)
		{
			ApiResultObject<HIS_IMP_MEST_PROPOSE> result = null;
			if (param != null && param.ApiData != null)
			{
				HisImpMestProposeManager mng = new HisImpMestProposeManager(param.CommonParam);
				result = mng.Lock(param.ApiData);
			}
			return new ApiResult(result, this.ActionContext);
		}
	}
}
