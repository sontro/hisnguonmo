using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisReportTypeCat;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisReportTypeCatController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisReportTypeCatFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisReportTypeCatFilterQuery> param)
        {
            try
            {
				ApiResultObject<List<HIS_REPORT_TYPE_CAT>> result = new ApiResultObject<List<HIS_REPORT_TYPE_CAT>>(null);
				if (param != null)
				{
					HisReportTypeCatManager mng = new HisReportTypeCatManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_REPORT_TYPE_CAT> param)
        {
            try
            {
                ApiResultObject<HIS_REPORT_TYPE_CAT> result = new ApiResultObject<HIS_REPORT_TYPE_CAT>(null);
                if (param != null)
                {
					HisReportTypeCatManager mng = new HisReportTypeCatManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_REPORT_TYPE_CAT> param)
        {
            try
            {
                ApiResultObject<HIS_REPORT_TYPE_CAT> result = new ApiResultObject<HIS_REPORT_TYPE_CAT>(null);
                if (param != null)
                {
					HisReportTypeCatManager mng = new HisReportTypeCatManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_REPORT_TYPE_CAT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
					HisReportTypeCatManager mng = new HisReportTypeCatManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_REPORT_TYPE_CAT> param)
        {
			try
            {
				ApiResultObject<HIS_REPORT_TYPE_CAT> result = new ApiResultObject<HIS_REPORT_TYPE_CAT>(null);
				if (param != null && param.ApiData != null)
				{
					HisReportTypeCatManager mng = new HisReportTypeCatManager(param.CommonParam);
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
