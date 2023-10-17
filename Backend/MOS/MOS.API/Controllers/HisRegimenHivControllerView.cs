using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRegimenHiv;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisRegimenHivController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRegimenHivViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisRegimenHivViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_REGIMEN_HIV>> result = new ApiResultObject<List<V_HIS_REGIMEN_HIV>>(null);
                if (param != null)
                {
                    HisRegimenHivManager mng = new HisRegimenHivManager(param.CommonParam);
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
