using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSuimIndex;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisSuimIndexController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSuimIndexFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSuimIndexFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SUIM_INDEX>> result = new ApiResultObject<List<HIS_SUIM_INDEX>>(null);
                if (param != null)
                {
                    HisSuimIndexManager mng = new HisSuimIndexManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisSuimIndexViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisSuimIndexViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SUIM_INDEX>> result = new ApiResultObject<List<V_HIS_SUIM_INDEX>>(null);
                if (param != null)
                {
                    HisSuimIndexManager mng = new HisSuimIndexManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_SUIM_INDEX> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_INDEX> result = new ApiResultObject<HIS_SUIM_INDEX>(null);
                if (param != null)
                {
                    HisSuimIndexManager mng = new HisSuimIndexManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SUIM_INDEX> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_INDEX> result = new ApiResultObject<HIS_SUIM_INDEX>(null);
                if (param != null)
                {
                    HisSuimIndexManager mng = new HisSuimIndexManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SUIM_INDEX> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSuimIndexManager mng = new HisSuimIndexManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_SUIM_INDEX> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_INDEX> result = new ApiResultObject<HIS_SUIM_INDEX>(null);
                if (param != null)
                {
                    HisSuimIndexManager mng = new HisSuimIndexManager(param.CommonParam);
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
