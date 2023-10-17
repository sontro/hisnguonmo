using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExroRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisExroRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExroRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExroRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
                if (param != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_EXRO_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_EXRO_ROOM> result = new ApiResultObject<HIS_EXRO_ROOM>(null);
                if (param != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_EXRO_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_EXRO_ROOM> result = new ApiResultObject<HIS_EXRO_ROOM>(null);
                if (param != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_EXRO_ROOM> result = new ApiResultObject<HIS_EXRO_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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
            ApiResultObject<HIS_EXRO_ROOM> result = null;
            if (param != null && param.ApiData != null)
            {
                HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_EXRO_ROOM>> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
                if (param != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<HIS_EXRO_ROOM>> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
                if (param != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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
        [ActionName("CopyByExecuteRoom")]
        public ApiResult CopyByExecuteRoom(ApiParam<HisExroRoomCopyByExroSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
                if (param != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
                    result = mng.CopyByExecuteRoom(param.ApiData);
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
        public ApiResult CopyByRoom(ApiParam<HisExroRoomCopyByRoomSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXRO_ROOM>> result = new ApiResultObject<List<HIS_EXRO_ROOM>>(null);
                if (param != null)
                {
                    HisExroRoomManager mng = new HisExroRoomManager(param.CommonParam);
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

    }
}
