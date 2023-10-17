using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisSereServTein;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisSereServTeinController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServTeinFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSereServTeinFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV_TEIN>> result = new ApiResultObject<List<HIS_SERE_SERV_TEIN>>();
                if (param != null)
                {
                    HisSereServTeinManager mng = new HisSereServTeinManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisSereServTeinViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisSereServTeinViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_TEIN>> result = new ApiResultObject<List<V_HIS_SERE_SERV_TEIN>>();
                if (param != null)
                {
                    HisSereServTeinManager mng = new HisSereServTeinManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisSereServTeinView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisSereServTeinView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_TEIN_1>> result = new ApiResultObject<List<V_HIS_SERE_SERV_TEIN_1>>();
                if (param != null)
                {
                    HisSereServTeinManager mng = new HisSereServTeinManager(param.CommonParam);
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
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServTeinAmountByNormationFilter>), "param")]
        [ActionName("GetMaterialAmountByNormation")]
        public ApiResult GetMaterialAmountByNormation(ApiParam<HisSereServTeinAmountByNormationFilter> param)
        {
            try
            {
                ApiResultObject<TestMaterialByNormationCollectionSDO> result = new ApiResultObject<TestMaterialByNormationCollectionSDO>();
                if (param != null)
                {
                    HisSereServTeinManager mng = new HisSereServTeinManager(param.CommonParam);
                    result = mng.GetMaterialAmountByNormation(param.ApiData);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_SERE_SERV_TEIN> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_TEIN> result = new ApiResultObject<HIS_SERE_SERV_TEIN>();
                if (param != null)
                {
                    HisSereServTeinManager mng = new HisSereServTeinManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HIS_SERE_SERV_TEIN> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSereServTeinManager mng = new HisSereServTeinManager(param.CommonParam);
                    result = mng.Delete(param.ApiData);
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
 