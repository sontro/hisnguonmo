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
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisExpMestMaterialController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestMaterialFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExpMestMaterialFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST_MATERIAL>> result = new ApiResultObject<List<HIS_EXP_MEST_MATERIAL>>(null);
                if (param != null)
                {
                    HisExpMestMaterialManager mng = new HisExpMestMaterialManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestMaterialViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisExpMestMaterialViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL>>(null);
                if (param != null)
                {
                    HisExpMestMaterialManager mng = new HisExpMestMaterialManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewByTreatmentId")]
        public ApiResult GetViewByTreatmentId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MATERIAL>>(null);
                if (param != null)
                {
                    HisExpMestMaterialManager mng = new HisExpMestMaterialManager(param.CommonParam);
                    result = mng.GetViewByTreatmentId(param.ApiData);
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
        [ActionName("Used")]
        public ApiResult Used(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST_MATERIAL> result = new ApiResultObject<HIS_EXP_MEST_MATERIAL>(null);
                if (param != null)
                {
                    HisExpMestMaterialManager mng = new HisExpMestMaterialManager(param.CommonParam);
                    result = mng.Used(param.ApiData);
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
        [ActionName("Unused")]
        public ApiResult Unused(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST_MATERIAL> result = new ApiResultObject<HIS_EXP_MEST_MATERIAL>(null);
                if (param != null)
                {
                    HisExpMestMaterialManager mng = new HisExpMestMaterialManager(param.CommonParam);
                    result = mng.Unused(param.ApiData);
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
