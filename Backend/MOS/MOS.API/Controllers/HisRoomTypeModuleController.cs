using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRoomTypeModule;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisRoomTypeModuleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRoomTypeModuleFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRoomTypeModuleFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> result = new ApiResultObject<List<HIS_ROOM_TYPE_MODULE>>(null);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ROOM_TYPE_MODULE> param)
        {
            try
            {
                ApiResultObject<HIS_ROOM_TYPE_MODULE> result = new ApiResultObject<HIS_ROOM_TYPE_MODULE>(null);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_ROOM_TYPE_MODULE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> result = new ApiResultObject<List<HIS_ROOM_TYPE_MODULE>>(null);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ROOM_TYPE_MODULE> param)
        {
            try
            {
                ApiResultObject<HIS_ROOM_TYPE_MODULE> result = new ApiResultObject<HIS_ROOM_TYPE_MODULE>(null);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
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
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_ROOM_TYPE_MODULE> result = new ApiResultObject<HIS_ROOM_TYPE_MODULE>(null);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
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
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_ROOM_TYPE_MODULE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CopyByModule")]
        public ApiResult CopyByModule(ApiParam<HisRotyModuleCopyByModuleSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> result = new ApiResultObject<List<HIS_ROOM_TYPE_MODULE>>(null);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
                    result = mng.CopyByModule(param.ApiData);
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
        [ActionName("CopyByRoomType")]
        public ApiResult CopyByRoomType(ApiParam<HisRotyModuleCopyByRoomTypeSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_ROOM_TYPE_MODULE>> result = new ApiResultObject<List<HIS_ROOM_TYPE_MODULE>>(null);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
                    result = mng.CopyByRoomType(param.ApiData);
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
