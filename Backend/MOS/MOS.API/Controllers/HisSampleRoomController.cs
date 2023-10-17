using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSampleRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisSampleRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSampleRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSampleRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SAMPLE_ROOM>> result = new ApiResultObject<List<HIS_SAMPLE_ROOM>>(null);
                if (param != null)
                {
                    HisSampleRoomManager mng = new HisSampleRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisSampleRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisSampleRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SAMPLE_ROOM>> result = new ApiResultObject<List<V_HIS_SAMPLE_ROOM>>(null);
                if (param != null)
                {
                    HisSampleRoomManager mng = new HisSampleRoomManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisSampleRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisSampleRoomSDO> result = new ApiResultObject<HisSampleRoomSDO>(null);
                if (param != null)
                {
                    HisSampleRoomManager mng = new HisSampleRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisSampleRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisSampleRoomSDO> result = new ApiResultObject<HisSampleRoomSDO>(null);
                if (param != null)
                {
                    HisSampleRoomManager mng = new HisSampleRoomManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SAMPLE_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSampleRoomManager mng = new HisSampleRoomManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_SAMPLE_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_SAMPLE_ROOM> result = new ApiResultObject<HIS_SAMPLE_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSampleRoomManager mng = new HisSampleRoomManager(param.CommonParam);
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
