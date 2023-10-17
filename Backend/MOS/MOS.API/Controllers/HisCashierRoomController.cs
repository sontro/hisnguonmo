using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCashierRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisCashierRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCashierRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCashierRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CASHIER_ROOM>> result = new ApiResultObject<List<HIS_CASHIER_ROOM>>(null);
                if (param != null)
                {
                    HisCashierRoomManager mng = new HisCashierRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisCashierRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisCashierRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CASHIER_ROOM>> result = new ApiResultObject<List<V_HIS_CASHIER_ROOM>>(null);
                if (param != null)
                {
                    HisCashierRoomManager mng = new HisCashierRoomManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisCashierRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisCashierRoomSDO> result = new ApiResultObject<HisCashierRoomSDO>(null);
                if (param != null)
                {
                    HisCashierRoomManager mng = new HisCashierRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisCashierRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisCashierRoomSDO> result = new ApiResultObject<HisCashierRoomSDO>(null);
                if (param != null)
                {
                    HisCashierRoomManager mng = new HisCashierRoomManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_CASHIER_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisCashierRoomManager mng = new HisCashierRoomManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_CASHIER_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_CASHIER_ROOM> result = new ApiResultObject<HIS_CASHIER_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisCashierRoomManager mng = new HisCashierRoomManager(param.CommonParam);
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
