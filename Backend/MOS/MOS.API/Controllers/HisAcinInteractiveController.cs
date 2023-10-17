using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAcinInteractive;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisAcinInteractiveController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAcinInteractiveFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAcinInteractiveFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACIN_INTERACTIVE>> result = new ApiResultObject<List<HIS_ACIN_INTERACTIVE>>(null);
                if (param != null)
                {
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisAcinInteractiveViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisAcinInteractiveViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ACIN_INTERACTIVE>> result = new ApiResultObject<List<V_HIS_ACIN_INTERACTIVE>>(null);
                if (param != null)
                {
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ACIN_INTERACTIVE> param)
        {
            try
            {
                ApiResultObject<HIS_ACIN_INTERACTIVE> result = new ApiResultObject<HIS_ACIN_INTERACTIVE>(null);
                if (param != null)
                {
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_ACIN_INTERACTIVE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACIN_INTERACTIVE>> result = new ApiResultObject<List<HIS_ACIN_INTERACTIVE>>(null);
                if (param != null)
                {
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ACIN_INTERACTIVE> param)
        {
            try
            {
                ApiResultObject<HIS_ACIN_INTERACTIVE> result = new ApiResultObject<HIS_ACIN_INTERACTIVE>(null);
                if (param != null)
                {
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
        public ApiResult UpdateList(ApiParam<List<HIS_ACIN_INTERACTIVE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACIN_INTERACTIVE>> result = new ApiResultObject<List<HIS_ACIN_INTERACTIVE>>(null);
                if (param != null)
                {
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ACIN_INTERACTIVE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_ACIN_INTERACTIVE> param)
        {
            try
            {
                ApiResultObject<HIS_ACIN_INTERACTIVE> result = new ApiResultObject<HIS_ACIN_INTERACTIVE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAcinInteractiveManager mng = new HisAcinInteractiveManager(param.CommonParam);
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
