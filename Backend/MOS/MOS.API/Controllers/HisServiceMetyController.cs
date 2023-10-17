using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceMety;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisServiceMetyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceMetyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServiceMetyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceMetyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServiceMetyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_METY>> result = new ApiResultObject<List<V_HIS_SERVICE_METY>>(null);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERVICE_METY> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_METY> result = new ApiResultObject<HIS_SERVICE_METY>(null);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_SERVICE_METY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERVICE_METY> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_METY> result = new ApiResultObject<HIS_SERVICE_METY>(null);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        public ApiResult UpdateList(ApiParam<List<HIS_SERVICE_METY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SERVICE_METY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_SERVICE_METY> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_METY> result = new ApiResultObject<HIS_SERVICE_METY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        public ApiResult CopyByService(ApiParam<HisServiceMetyCopyByServiceSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
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
        [ActionName("CopyByMety")]
        public ApiResult CopyByMety(ApiParam<HisServiceMetyCopyByMetySDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);
                if (param != null)
                {
                    HisServiceMetyManager mng = new HisServiceMetyManager(param.CommonParam);
                    result = mng.CopyByMety(param.ApiData);
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
