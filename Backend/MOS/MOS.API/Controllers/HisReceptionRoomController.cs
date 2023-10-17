using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisReceptionRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisReceptionRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisReceptionRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisReceptionRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_RECEPTION_ROOM>> result = new ApiResultObject<List<HIS_RECEPTION_ROOM>>(null);
                if (param != null)
                {
                    HisReceptionRoomManager mng = new HisReceptionRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisReceptionRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisReceptionRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_RECEPTION_ROOM>> result = new ApiResultObject<List<V_HIS_RECEPTION_ROOM>>(null);
                if (param != null)
                {
                    HisReceptionRoomManager mng = new HisReceptionRoomManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisReceptionRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisReceptionRoomSDO> result = new ApiResultObject<HisReceptionRoomSDO>(null);
                if (param != null)
                {
                    HisReceptionRoomManager mng = new HisReceptionRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisReceptionRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisReceptionRoomSDO> result = new ApiResultObject<HisReceptionRoomSDO>(null);
                if (param != null)
                {
                    HisReceptionRoomManager mng = new HisReceptionRoomManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_RECEPTION_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisReceptionRoomManager mng = new HisReceptionRoomManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_RECEPTION_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_RECEPTION_ROOM> result = new ApiResultObject<HIS_RECEPTION_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisReceptionRoomManager mng = new HisReceptionRoomManager(param.CommonParam);
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
