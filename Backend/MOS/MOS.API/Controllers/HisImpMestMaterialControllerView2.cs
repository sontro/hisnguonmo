using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpMestMaterialController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestMaterialView2FilterQuery>), "param")]
        [ActionName("GetView2")]
        public ApiResult GetView2(ApiParam<HisImpMestMaterialView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL_2>> result = new ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL_2>>(null);
                if (param != null)
                {
                    HisImpMestMaterialManager mng = new HisImpMestMaterialManager(param.CommonParam);
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
