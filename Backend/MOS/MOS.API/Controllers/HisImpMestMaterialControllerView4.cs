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
        [ApiParamFilter(typeof(ApiParam<HisImpMestMaterialView4FilterQuery>), "param")]
        [ActionName("GetView4")]
        public ApiResult GetView4(ApiParam<HisImpMestMaterialView4FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL_4>> result = new ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL_4>>(null);
                if (param != null)
                {
                    HisImpMestMaterialManager mng = new HisImpMestMaterialManager(param.CommonParam);
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
