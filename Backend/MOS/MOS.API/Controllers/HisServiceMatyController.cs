using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceMaty;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisServiceMatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceMatyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServiceMatyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceMatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServiceMatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_MATY>> result = new ApiResultObject<List<V_HIS_SERVICE_MATY>>(null);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERVICE_MATY> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_MATY> result = new ApiResultObject<HIS_SERVICE_MATY>(null);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_SERVICE_MATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_SERVICE_MATY> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_MATY> result = new ApiResultObject<HIS_SERVICE_MATY>(null);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<HIS_SERVICE_MATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
                    result = mng.UpdateList(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_SERVICE_MATY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
                    result = mng.DeleteList(param.ApiData);
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
        public ApiResult ChangeLock(ApiParam<HIS_SERVICE_MATY> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_MATY> result = new ApiResultObject<HIS_SERVICE_MATY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
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
        [ActionName("CopyByService")]
        public ApiResult CopyByService(ApiParam<HisServiceMatyCopyByServiceSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
                    result = mng.CopyByService(param.ApiData);
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
        [ActionName("CopyByMaty")]
        public ApiResult CopyByMaty(ApiParam<HisServiceMatyCopyByMatySDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);
                if (param != null)
                {
                    HisServiceMatyManager mng = new HisServiceMatyManager(param.CommonParam);
                    result = mng.CopyByMaty(param.ApiData);
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
