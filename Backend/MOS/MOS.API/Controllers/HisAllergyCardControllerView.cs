using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAllergyCard;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAllergyCardController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAllergyCardViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAllergyCardViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ALLERGY_CARD>> result = new ApiResultObject<List<V_HIS_ALLERGY_CARD>>(null);
                if (param != null)
                {
                    HisAllergyCardManager mng = new HisAllergyCardManager(param.CommonParam);
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
