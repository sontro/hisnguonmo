using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisServiceRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServiceRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServiceRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_ROOM>> result = new ApiResultObject<List<V_HIS_SERVICE_ROOM>>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERVICE_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_ROOM> result = new ApiResultObject<HIS_SERVICE_ROOM>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_SERVICE_ROOM>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERVICE_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_ROOM> result = new ApiResultObject<HIS_SERVICE_ROOM>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        public ApiResult UpdateList(ApiParam<List<HIS_SERVICE_ROOM>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SERVICE_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_SERVICE_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_ROOM> result = new ApiResultObject<HIS_SERVICE_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        public ApiResult CopyByService(ApiParam<HisServiceRoomCopyByServiceSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
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
        [ActionName("CopyByRoom")]
        public ApiResult CopyByRoom(ApiParam<HisServiceRoomCopyByRoomSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
                    result = mng.CopyByRoom(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceRoomViewFilterQuery>), "param")]
        [ActionName("GetViewZip")]
        public ApiResultZip GetViewZip(ApiParam<HisServiceRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_ROOM>> result = new ApiResultObject<List<V_HIS_SERVICE_ROOM>>(null);
                if (param != null)
                {
                    HisServiceRoomManager mng = new HisServiceRoomManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
                }
                return new ApiResultZip(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
