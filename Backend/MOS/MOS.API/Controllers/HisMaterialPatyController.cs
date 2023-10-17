using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMaterialPaty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMaterialPatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMaterialPatyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMaterialPatyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MATERIAL_PATY>> result = new ApiResultObject<List<HIS_MATERIAL_PATY>>(null);
                if (param != null)
                {
                    HisMaterialPatyManager mng = new HisMaterialPatyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialPatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMaterialPatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MATERIAL_PATY>> result = new ApiResultObject<List<V_HIS_MATERIAL_PATY>>(null);
                if (param != null)
                {
                    HisMaterialPatyManager mng = new HisMaterialPatyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MATERIAL_PATY> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_PATY> result = new ApiResultObject<HIS_MATERIAL_PATY>(null);
                if (param != null)
                {
                    HisMaterialPatyManager mng = new HisMaterialPatyManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_MATERIAL_PATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MATERIAL_PATY>> result = new ApiResultObject<List<HIS_MATERIAL_PATY>>(null);
                if (param != null)
                {
                    HisMaterialPatyManager mng = new HisMaterialPatyManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        [ActionName("GetOfLast")]
        public ApiResult GetOfLast(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<HIS_MATERIAL_PATY>> result = new ApiResultObject<List<HIS_MATERIAL_PATY>>(null);
                if (param != null)
                {
                    HisMaterialPatyManager mng = new HisMaterialPatyManager(param.CommonParam);
                    result = mng.GetOfLast(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_MATERIAL_PATY> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_PATY> result = new ApiResultObject<HIS_MATERIAL_PATY>(null);
                if (param != null)
                {
                    HisMaterialPatyManager mng = new HisMaterialPatyManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MATERIAL_PATY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMaterialPatyManager mng = new HisMaterialPatyManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_MATERIAL_PATY> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_PATY> result = new ApiResultObject<HIS_MATERIAL_PATY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMaterialPatyManager mng = new HisMaterialPatyManager(param.CommonParam);
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
