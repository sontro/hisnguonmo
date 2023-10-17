using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPayForm;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisPayFormController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPayFormFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPayFormFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PAY_FORM>> result = new ApiResultObject<List<HIS_PAY_FORM>>(null);
                if (param != null)
                {
                    HisPayFormManager mng = new HisPayFormManager(param.CommonParam);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_PAY_FORM> param)
        {
            try
            {
                ApiResultObject<HIS_PAY_FORM> result = new ApiResultObject<HIS_PAY_FORM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPayFormManager mng = new HisPayFormManager(param.CommonParam);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<HIS_PAY_FORM> param)
        {
            try
            {
                ApiResultObject<HIS_PAY_FORM> result = new ApiResultObject<HIS_PAY_FORM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPayFormManager mng = new HisPayFormManager(param.CommonParam);
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
