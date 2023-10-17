using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ROOM>> result = new ApiResultObject<List<HIS_ROOM>>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ROOM>> result = new ApiResultObject<List<V_HIS_ROOM>>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisRoomCounterViewFilterQuery>), "param")]
        [ActionName("GetCounterView")]
        public ApiResult GetCounterView(ApiParam<HisRoomCounterViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ROOM_COUNTER>> result = new ApiResultObject<List<V_HIS_ROOM_COUNTER>>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
                    result = mng.GetCounterView(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisRoomCounter1ViewFilterQuery>), "param")]
        [ActionName("GetCounter1View")]
        public ApiResult GetCounter1View(ApiParam<HisRoomCounter1ViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ROOM_COUNTER_1>> result = new ApiResultObject<List<V_HIS_ROOM_COUNTER_1>>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
                    result = mng.GetCounter1View(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisRoomCounterLViewFilterQuery>), "param")]
        [ActionName("GetCounterLView")]
        public ApiResult GetCounterLView(ApiParam<HisRoomCounterLViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<L_HIS_ROOM_COUNTER>> result = new ApiResultObject<List<L_HIS_ROOM_COUNTER>>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
                    result = mng.GetCounterLView(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_ROOM> result = new ApiResultObject<HIS_ROOM>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_ROOM> result = new ApiResultObject<HIS_ROOM>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
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
        [ActionName("UpdateResponsibleUser")]
        public ApiResult UpdateResponsibleUser(ApiParam<List<UpdateResponsibleUserSDO>> param)
        {
            try
            {
                ApiResultObject<List<HIS_ROOM>> result = new ApiResultObject<List<HIS_ROOM>>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
                    result = mng.UpdateResponsibleUser(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_ROOM> result = new ApiResultObject<HIS_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRoomCounterLView1FilterQuery>), "param")]
        [ActionName("GetCounterLView1")]
        public ApiResult GetCounterLView1(ApiParam<HisRoomCounterLView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<L_HIS_ROOM_COUNTER_1>> result = new ApiResultObject<List<L_HIS_ROOM_COUNTER_1>>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
                    result = mng.GetCounterLView1(param.ApiData);
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
        [ActionName("UpdateJsonPrintId")]
        public ApiResult UpdateJsonPrintId(ApiParam<HisRoomSDO> param)
        {
            try
            {
                ApiResultObject<HIS_ROOM> result = new ApiResultObject<HIS_ROOM>(null);
                if (param != null)
                {
                    HisRoomManager mng = new HisRoomManager(param.CommonParam);
                    result = mng.UpdateJsonPrintId(param.ApiData);
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
