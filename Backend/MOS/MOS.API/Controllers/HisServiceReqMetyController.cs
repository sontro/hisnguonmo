using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReqMety;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
	public partial class HisServiceReqMetyController : BaseApiController
	{
		[HttpGet]
		[ApiParamFilter(typeof(ApiParam<HisServiceReqMetyFilterQuery>), "param")]
		[ActionName("Get")]
		public ApiResult Get(ApiParam<HisServiceReqMetyFilterQuery> param)
		{
			try
			{
				ApiResultObject<List<HIS_SERVICE_REQ_METY>> result = new ApiResultObject<List<HIS_SERVICE_REQ_METY>>(null);
				if (param != null)
				{
					HisServiceReqMetyManager mng = new HisServiceReqMetyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceReqMetyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServiceReqMetyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_REQ_METY>> result = new ApiResultObject<List<V_HIS_SERVICE_REQ_METY>>(null);
                if (param != null)
                {
                    HisServiceReqMetyManager mng = new HisServiceReqMetyManager(param.CommonParam);
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
		public ApiResult Create(ApiParam<HIS_SERVICE_REQ_METY> param)
		{
			try
			{
				ApiResultObject<HIS_SERVICE_REQ_METY> result = new ApiResultObject<HIS_SERVICE_REQ_METY>(null);
				if (param != null)
				{
					HisServiceReqMetyManager mng = new HisServiceReqMetyManager(param.CommonParam);
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
		public ApiResult Update(ApiParam<HIS_SERVICE_REQ_METY> param)
		{
			try
			{
				ApiResultObject<HIS_SERVICE_REQ_METY> result = new ApiResultObject<HIS_SERVICE_REQ_METY>(null);
				if (param != null)
				{
					HisServiceReqMetyManager mng = new HisServiceReqMetyManager(param.CommonParam);
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
		public ApiResult Delete(ApiParam<long> param)
		{
			try
			{
				ApiResultObject<bool> result = new ApiResultObject<bool>(false);
				if (param != null)
				{
					HisServiceReqMetyManager mng = new HisServiceReqMetyManager(param.CommonParam);
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
				ApiResultObject<HIS_SERVICE_REQ_METY> result = new ApiResultObject<HIS_SERVICE_REQ_METY>(null);
				if (param != null && param.ApiData != null)
				{
					HisServiceReqMetyManager mng = new HisServiceReqMetyManager(param.CommonParam);
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
			ApiResultObject<HIS_SERVICE_REQ_METY> result = null;
			if (param != null && param.ApiData != null)
			{
				HisServiceReqMetyManager mng = new HisServiceReqMetyManager(param.CommonParam);
				result = mng.Lock(param.ApiData);
			}
			return new ApiResult(result, this.ActionContext);
		}

		[HttpPost]
		[ActionName("UpdateCommonInfo")]
		public ApiResult UpdateCommonInfo(ApiParam<HIS_SERVICE_REQ_METY> param)
		{
			try
			{
				ApiResultObject<HIS_SERVICE_REQ_METY> result = new ApiResultObject<HIS_SERVICE_REQ_METY>(null);
				if (param != null)
				{
					HisServiceReqMetyManager mng = new HisServiceReqMetyManager(param.CommonParam);
					result = mng.UpdateCommonInfo(param.ApiData);
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
