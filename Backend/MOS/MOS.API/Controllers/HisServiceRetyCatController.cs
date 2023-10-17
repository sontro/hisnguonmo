using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceRetyCat;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisServiceRetyCatController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceRetyCatFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServiceRetyCatFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_RETY_CAT>> result = new ApiResultObject<List<HIS_SERVICE_RETY_CAT>>(null);
                if (param != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceRetyCatViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServiceRetyCatViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_RETY_CAT>> result = new ApiResultObject<List<V_HIS_SERVICE_RETY_CAT>>(null);
                if (param != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERVICE_RETY_CAT> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_RETY_CAT> result = new ApiResultObject<HIS_SERVICE_RETY_CAT>(null);
                if (param != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_SERVICE_RETY_CAT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_RETY_CAT>> result = new ApiResultObject<List<HIS_SERVICE_RETY_CAT>>(null);
                if (param != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERVICE_RETY_CAT> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_RETY_CAT> result = new ApiResultObject<HIS_SERVICE_RETY_CAT>(null);
                if (param != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SERVICE_RETY_CAT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_SERVICE_RETY_CAT> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_RETY_CAT> result = new ApiResultObject<HIS_SERVICE_RETY_CAT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
        public ApiResult CopyByService(ApiParam<HisServiceRetyCatCopyByServiceSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_RETY_CAT>> result = new ApiResultObject<List<HIS_SERVICE_RETY_CAT>>(null);
                if (param != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
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
        [ActionName("CopyByRetyCat")]
        public ApiResult CopyByRetyCat(ApiParam<HisServiceRetyCatCopyByRetyCatSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_RETY_CAT>> result = new ApiResultObject<List<HIS_SERVICE_RETY_CAT>>(null);
                if (param != null)
                {
                    HisServiceRetyCatManager mng = new HisServiceRetyCatManager(param.CommonParam);
                    result = mng.CopyByRetyCat(param.ApiData);
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
