using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediReactType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMediReactTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediReactTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMediReactTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_REACT_TYPE>> result = new ApiResultObject<List<HIS_MEDI_REACT_TYPE>>(null);
                if (param != null)
                {
                    HisMediReactTypeManager mng = new HisMediReactTypeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEDI_REACT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT_TYPE> result = new ApiResultObject<HIS_MEDI_REACT_TYPE>(null);
                if (param != null)
                {
                    HisMediReactTypeManager mng = new HisMediReactTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEDI_REACT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT_TYPE> result = new ApiResultObject<HIS_MEDI_REACT_TYPE>(null);
                if (param != null)
                {
                    HisMediReactTypeManager mng = new HisMediReactTypeManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MEDI_REACT_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediReactTypeManager mng = new HisMediReactTypeManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_MEDI_REACT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_REACT_TYPE> result = new ApiResultObject<HIS_MEDI_REACT_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMediReactTypeManager mng = new HisMediReactTypeManager(param.CommonParam);
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
