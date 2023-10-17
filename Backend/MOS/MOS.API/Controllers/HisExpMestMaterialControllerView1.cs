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
        [ApiParamFilter(typeof(ApiParam<HisExpMestMaterialView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisExpMestMaterialView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL_1>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL_1>>(null);
                if (param != null)
                {
                    HisExpMestMaterialManager mng = new HisExpMestMaterialManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
