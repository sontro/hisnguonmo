using Inventec.Common.Logging;
using Inventec.Core;
using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.SdaCustomizeButton;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaCustomizeButtonController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SdaCustomizeButtonFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SdaCustomizeButtonFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> result = new ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>>(null);
                if (param != null)
                {
                    SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<SDA_CUSTOMIZE_BUTTON> param)
        {
            try
            {
                ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = new ApiResultObject<SDA_CUSTOMIZE_BUTTON>(null);
                if (param != null)
                {
                    SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<SDA_CUSTOMIZE_BUTTON> param)
        {
            try
            {
                ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = new ApiResultObject<SDA_CUSTOMIZE_BUTTON>(null);
                if (param != null)
                {
                    SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
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
                    SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
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
                ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = new ApiResultObject<SDA_CUSTOMIZE_BUTTON>(null);
                if (param != null && param.ApiData != null)
                {
                    SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
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
            ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = null;
            if (param != null && param.ApiData != null)
            {
                SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
