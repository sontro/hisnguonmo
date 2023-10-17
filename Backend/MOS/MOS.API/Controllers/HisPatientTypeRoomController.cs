using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientTypeRoom;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisPatientTypeRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPatientTypeRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>> result = new ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>>(null);
                if (param != null)
                {
                    HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_PATIENT_TYPE_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = new ApiResultObject<HIS_PATIENT_TYPE_ROOM>(null);
                if (param != null)
                {
                    HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_PATIENT_TYPE_ROOM>> param)
        {
            try
            {
                ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>> result = new ApiResultObject<List<HIS_PATIENT_TYPE_ROOM>>(null);
                if (param != null)
                {
                    HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_PATIENT_TYPE_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = new ApiResultObject<HIS_PATIENT_TYPE_ROOM>(null);
                if (param != null)
                {
                    HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
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
                    HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
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
                    HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = new ApiResultObject<HIS_PATIENT_TYPE_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
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
            ApiResultObject<HIS_PATIENT_TYPE_ROOM> result = null;
            if (param != null && param.ApiData != null)
            {
                HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
