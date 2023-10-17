using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBedRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisBedRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBedRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBedRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BED_ROOM>> result = new ApiResultObject<List<HIS_BED_ROOM>>(null);
                if (param != null)
                {
                    HisBedRoomManager mng = new HisBedRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisBedRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisBedRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BED_ROOM>> result = new ApiResultObject<List<V_HIS_BED_ROOM>>(null);
                if (param != null)
                {
                    HisBedRoomManager mng = new HisBedRoomManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBedRoomView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisBedRoomView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BED_ROOM_1>> result = new ApiResultObject<List<V_HIS_BED_ROOM_1>>(null);
                if (param != null)
                {
                    HisBedRoomManager mng = new HisBedRoomManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
        public ApiResult Create(ApiParam<HisBedRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisBedRoomSDO> result = new ApiResultObject<HisBedRoomSDO>(null);
                if (param != null)
                {
                    HisBedRoomManager mng = new HisBedRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisBedRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisBedRoomSDO> result = new ApiResultObject<HisBedRoomSDO>(null);
                if (param != null)
                {
                    HisBedRoomManager mng = new HisBedRoomManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_BED_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisBedRoomManager mng = new HisBedRoomManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_BED_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_BED_ROOM> result = new ApiResultObject<HIS_BED_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBedRoomManager mng = new HisBedRoomManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HisBedRoomSDO>> param)
        {
            try
            {
                ApiResultObject<List<HisBedRoomSDO>> result = new ApiResultObject<List<HisBedRoomSDO>>(null);
                if (param != null)
                {
                    HisBedRoomManager mng = new HisBedRoomManager(param.CommonParam);
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

    }
}
