using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineMaterial;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMedicineMaterialController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineMaterialFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicineMaterialFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_MATERIAL>> result = new ApiResultObject<List<HIS_MEDICINE_MATERIAL>>(null);
                if (param != null)
                {
                    HisMedicineMaterialManager mng = new HisMedicineMaterialManager(param.CommonParam);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_MATERIAL> result = new ApiResultObject<HIS_MEDICINE_MATERIAL>(null);
                if (param != null)
                {
                    HisMedicineMaterialManager mng = new HisMedicineMaterialManager(param.CommonParam);
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
            ApiResultObject<HIS_MEDICINE_MATERIAL> result = null;
            if (param != null)
            {
                HisMedicineMaterialManager mng = new HisMedicineMaterialManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
