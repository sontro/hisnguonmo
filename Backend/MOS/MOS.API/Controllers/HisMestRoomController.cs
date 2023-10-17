using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMestRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_ROOM>> result = new ApiResultObject<List<HIS_MEST_ROOM>>(null);
                if (param != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMestRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMestRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_ROOM>> result = new ApiResultObject<List<V_HIS_MEST_ROOM>>(null);
                if (param != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEST_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_ROOM> result = new ApiResultObject<HIS_MEST_ROOM>(null);
                if (param != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_MEST_ROOM>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_ROOM>> result = new ApiResultObject<List<HIS_MEST_ROOM>>(null);
                if (param != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEST_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_ROOM> result = new ApiResultObject<HIS_MEST_ROOM>(null);
                if (param != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MEST_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_MEST_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_ROOM> result = new ApiResultObject<HIS_MEST_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
        [ActionName("CopyByMediStock")]
        public ApiResult CopyByMediStock(ApiParam<HisMestRoomCopyByMediStockSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_ROOM>> result = new ApiResultObject<List<HIS_MEST_ROOM>>(null);
                if (param != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
                    result = mng.CopyByMediStock(param.ApiData);
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
        public ApiResult CopyByRoom(ApiParam<HisMestRoomCopyByRoomSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_ROOM>> result = new ApiResultObject<List<HIS_MEST_ROOM>>(null);
                if (param != null)
                {
                    HisMestRoomManager mng = new HisMestRoomManager(param.CommonParam);
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
