using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBloodRh;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisBloodRhController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBloodRhFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBloodRhFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BLOOD_RH>> result = new ApiResultObject<List<HIS_BLOOD_RH>>(null);
                if (param != null)
                {
                    HisBloodRhManager mng = new HisBloodRhManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_BLOOD_RH> param)
        {
            try
            {
                ApiResultObject<HIS_BLOOD_RH> result = new ApiResultObject<HIS_BLOOD_RH>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBloodRhManager mng = new HisBloodRhManager(param.CommonParam);
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
