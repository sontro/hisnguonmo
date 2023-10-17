using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEmteMaterialType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
	public class HisEmteMaterialTypeController : BaseApiController
	{
		[HttpGet]
		[ApiParamFilter(typeof(ApiParam<HisEmteMaterialTypeFilterQuery>), "param")]
		[ActionName("Get")]
		public ApiResult Get(ApiParam<HisEmteMaterialTypeFilterQuery> param)
		{
			try
			{
				ApiResultObject<List<HIS_EMTE_MATERIAL_TYPE>> result = new ApiResultObject<List<HIS_EMTE_MATERIAL_TYPE>>(null);
				if (param != null)
				{
					HisEmteMaterialTypeManager mng = new HisEmteMaterialTypeManager(param.CommonParam);
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
		[ApiParamFilter(typeof(ApiParam<HisEmteMaterialTypeViewFilterQuery>), "param")]
		[ActionName("GetView")]
		public ApiResult GetView(ApiParam<HisEmteMaterialTypeViewFilterQuery> param)
		{
			try
			{
				ApiResultObject<List<V_HIS_EMTE_MATERIAL_TYPE>> result = new ApiResultObject<List<V_HIS_EMTE_MATERIAL_TYPE>>(null);
				if (param != null)
				{
					HisEmteMaterialTypeManager mng = new HisEmteMaterialTypeManager(param.CommonParam);
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
		
		[HttpPost]
		[ActionName("Create")]
		public ApiResult Create(ApiParam<HIS_EMTE_MATERIAL_TYPE> param)
		{
			try
			{
				ApiResultObject<HIS_EMTE_MATERIAL_TYPE> result = new ApiResultObject<HIS_EMTE_MATERIAL_TYPE>(null);
				if (param != null)
				{
					HisEmteMaterialTypeManager mng = new HisEmteMaterialTypeManager(param.CommonParam);
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
		public ApiResult Update(ApiParam<HIS_EMTE_MATERIAL_TYPE> param)
		{
			try
			{
				ApiResultObject<HIS_EMTE_MATERIAL_TYPE> result = new ApiResultObject<HIS_EMTE_MATERIAL_TYPE>(null);
				if (param != null)
				{
					HisEmteMaterialTypeManager mng = new HisEmteMaterialTypeManager(param.CommonParam);
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
		public ApiResult Delete(ApiParam<HIS_EMTE_MATERIAL_TYPE> param)
		{
			try
			{
				ApiResultObject<bool> result = new ApiResultObject<bool>(false);
				if (param != null)
				{
					HisEmteMaterialTypeManager mng = new HisEmteMaterialTypeManager(param.CommonParam);
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
		public ApiResult Lock(ApiParam<HIS_EMTE_MATERIAL_TYPE> param)
		{
			try
			{
				ApiResultObject<HIS_EMTE_MATERIAL_TYPE> result = new ApiResultObject<HIS_EMTE_MATERIAL_TYPE>(null);
				if (param != null && param.ApiData != null)
				{
					HisEmteMaterialTypeManager mng = new HisEmteMaterialTypeManager(param.CommonParam);
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
	}
}
