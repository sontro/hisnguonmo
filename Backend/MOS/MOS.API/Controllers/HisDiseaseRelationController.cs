using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDiseaseRelation;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisDiseaseRelationController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDiseaseRelationFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDiseaseRelationFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DISEASE_RELATION>> result = new ApiResultObject<List<HIS_DISEASE_RELATION>>(null);
                if (param != null)
                {
                    HisDiseaseRelationManager mng = new HisDiseaseRelationManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_DISEASE_RELATION> param)
        {
            try
            {
                ApiResultObject<HIS_DISEASE_RELATION> result = new ApiResultObject<HIS_DISEASE_RELATION>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDiseaseRelationManager mng = new HisDiseaseRelationManager(param.CommonParam);
                    result = mng.Create(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_DISEASE_RELATION> param)
        {
            try
            {
                ApiResultObject<HIS_DISEASE_RELATION> result = new ApiResultObject<HIS_DISEASE_RELATION>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDiseaseRelationManager mng = new HisDiseaseRelationManager(param.CommonParam);
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
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<HIS_DISEASE_RELATION> param)
        {
            try
            {
                ApiResultObject<HIS_DISEASE_RELATION> result = new ApiResultObject<HIS_DISEASE_RELATION>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDiseaseRelationManager mng = new HisDiseaseRelationManager(param.CommonParam);
                    result = mng.ChangeLock(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_DISEASE_RELATION> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null && param.ApiData != null)
                {
                    HisDiseaseRelationManager mng = new HisDiseaseRelationManager(param.CommonParam);
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
