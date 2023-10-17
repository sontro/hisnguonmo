using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSuimIndexUnit;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisSuimIndexUnitController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSuimIndexUnitFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSuimIndexUnitFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SUIM_INDEX_UNIT>> result = new ApiResultObject<List<HIS_SUIM_INDEX_UNIT>>(null);
                if (param != null)
                {
                    HisSuimIndexUnitManager mng = new HisSuimIndexUnitManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SUIM_INDEX_UNIT> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_INDEX_UNIT> result = new ApiResultObject<HIS_SUIM_INDEX_UNIT>(null);
                if (param != null)
                {
                    HisSuimIndexUnitManager mng = new HisSuimIndexUnitManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SUIM_INDEX_UNIT> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_INDEX_UNIT> result = new ApiResultObject<HIS_SUIM_INDEX_UNIT>(null);
                if (param != null)
                {
                    HisSuimIndexUnitManager mng = new HisSuimIndexUnitManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SUIM_INDEX_UNIT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSuimIndexUnitManager mng = new HisSuimIndexUnitManager(param.CommonParam);
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
        
        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<HIS_SUIM_INDEX_UNIT> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_INDEX_UNIT> result = new ApiResultObject<HIS_SUIM_INDEX_UNIT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSuimIndexUnitManager mng = new HisSuimIndexUnitManager(param.CommonParam);
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
    }
}
