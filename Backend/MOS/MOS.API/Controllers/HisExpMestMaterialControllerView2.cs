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
        [ApiParamFilter(typeof(ApiParam<HisExpMestMaterialView2FilterQuery>), "param")]
        [ActionName("GetView2")]
        public ApiResult GetView2(ApiParam<HisExpMestMaterialView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL_2>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL_2>>(null);
                if (param != null)
                {
                    HisExpMestMaterialManager mng = new HisExpMestMaterialManager(param.CommonParam);
                    result = mng.GetView2(param.ApiData);
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
