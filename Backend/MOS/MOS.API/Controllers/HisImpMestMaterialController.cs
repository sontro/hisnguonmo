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
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisImpMestMaterialController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestMaterialFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisImpMestMaterialFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_MATERIAL>> result = new ApiResultObject<List<HIS_IMP_MEST_MATERIAL>>();
                if (param != null)
                {
                    HisImpMestMaterialManager mng = new HisImpMestMaterialManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestMaterialViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisImpMestMaterialViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL>> result = new ApiResultObject<List<V_HIS_IMP_MEST_MATERIAL>>(null);
                if (param != null)
                {
                    HisImpMestMaterialManager mng = new HisImpMestMaterialManager(param.CommonParam);
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
