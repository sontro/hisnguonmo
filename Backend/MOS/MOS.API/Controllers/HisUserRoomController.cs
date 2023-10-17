using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisUserRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisUserRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisUserRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisUserRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_USER_ROOM>> result = new ApiResultObject<List<HIS_USER_ROOM>>(null);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisUserRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        [AllowAnonymous]
        public ApiResult GetView(ApiParam<HisUserRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_USER_ROOM>> result = new ApiResultObject<List<V_HIS_USER_ROOM>>(null);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisUserRoomViewFilterQuery>), "param")]
        [ActionName("GetViewZip")]
        public ApiResultZip GetViewZip(ApiParam<HisUserRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_USER_ROOM>> result = new ApiResultObject<List<V_HIS_USER_ROOM>>(null);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_USER_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_USER_ROOM> result = new ApiResultObject<HIS_USER_ROOM>(null);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_USER_ROOM>> param)
        {
            try
            {
                ApiResultObject<List<HIS_USER_ROOM>> result = new ApiResultObject<List<HIS_USER_ROOM>>(null);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_USER_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_USER_ROOM> result = new ApiResultObject<HIS_USER_ROOM>(null);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_USER_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_USER_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_USER_ROOM> result = new ApiResultObject<HIS_USER_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
        [ActionName("CopyByLoginname")]
        public ApiResult CopyByLoginname(ApiParam<HisUserRoomCopyByLoginnameSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_USER_ROOM>> result = new ApiResultObject<List<HIS_USER_ROOM>>(null);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
                    result = mng.CopyByLoginname(param.ApiData);
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
        public ApiResult CopyByRoom(ApiParam<HisUserRoomCopyByRoomSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_USER_ROOM>> result = new ApiResultObject<List<HIS_USER_ROOM>>(null);
                if (param != null)
                {
                    HisUserRoomManager mng = new HisUserRoomManager(param.CommonParam);
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
