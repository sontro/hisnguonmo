using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisExecuteRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisExecuteRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExecuteRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExecuteRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXECUTE_ROOM>> result = new ApiResultObject<List<HIS_EXECUTE_ROOM>>(null);
                if (param != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisExecuteRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisExecuteRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXECUTE_ROOM>> result = new ApiResultObject<List<V_HIS_EXECUTE_ROOM>>(null);
                if (param != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisExecuteRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisExecuteRoomSDO> result = new ApiResultObject<HisExecuteRoomSDO>(null);
                if (param != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisExecuteRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisExecuteRoomSDO> result = new ApiResultObject<HisExecuteRoomSDO>(null);
                if (param != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_EXECUTE_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_EXECUTE_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_EXECUTE_ROOM> result = new ApiResultObject<HIS_EXECUTE_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HisExecuteRoomSDO>> param)
        {
            try
            {
                ApiResultObject<List<HisExecuteRoomSDO>> result = new ApiResultObject<List<HisExecuteRoomSDO>>(null);
                if (param != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExecuteRoomAppointedFilter>), "param")]
        [ActionName("GetCountAppointed")]
        public ApiResult GetCountAppointed(ApiParam<HisExecuteRoomAppointedFilter> param)
        {
            try
            {
                ApiResultObject<List<HisExecuteRoomAppointedSDO>> result = new ApiResultObject<List<HisExecuteRoomAppointedSDO>>(null);
                if (param != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
                    result = mng.GetCountAppointed(param.ApiData);
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
