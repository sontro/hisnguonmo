using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestMaterial;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestMaterialController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestMaterialView4FilterQuery>), "param")]
        [ActionName("GetView4")]
        public ApiResult GetView4(ApiParam<HisExpMestMaterialView4FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL_4>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL_4>>(null);
                if (param != null)
                {
                    HisExpMestMaterialManager mng = new HisExpMestMaterialManager(param.CommonParam);
                    result = mng.GetView4(param.ApiData);
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
