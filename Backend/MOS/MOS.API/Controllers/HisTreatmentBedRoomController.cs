using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatmentBedRoom;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisTreatmentBedRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentBedRoomFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTreatmentBedRoomFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT_BED_ROOM>> result = new ApiResultObject<List<HIS_TREATMENT_BED_ROOM>>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentBedRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisTreatmentBedRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM>> result = new ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM>>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentBedRoomLViewFilterQuery>), "param")]
        [ActionName("GetLView")]
        public ApiResult GetView(ApiParam<HisTreatmentBedRoomLViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<L_HIS_TREATMENT_BED_ROOM>> result = new ApiResultObject<List<L_HIS_TREATMENT_BED_ROOM>>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
                    result = mng.GetLView(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentBedRoomView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisTreatmentBedRoomView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM_1>> result = new ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM_1>>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewCurrentIn")]
        public ApiResult GetViewCurrentIn(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM>> result = new ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM>>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
                    result = mng.GetViewCurrentIn(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_TREATMENT_BED_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT_BED_ROOM> result = new ApiResultObject<HIS_TREATMENT_BED_ROOM>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
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
        [ActionName("CreateSdo")]
        public ApiResult CreateSdo(ApiParam<HisTreatmentBedRoomSDO> param)
        {
            try
            {
                ApiResultObject<HisTreatmentBedRoomSDO> result = new ApiResultObject<HisTreatmentBedRoomSDO>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
                    result = mng.CreateSdo(param.ApiData);
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
        [ActionName("Remove")]
        public ApiResult Remove(ApiParam<HIS_TREATMENT_BED_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT_BED_ROOM> result = new ApiResultObject<HIS_TREATMENT_BED_ROOM>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
                    result = mng.Remove(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_TREATMENT_BED_ROOM> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_TREATMENT_BED_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT_BED_ROOM> result = new ApiResultObject<HIS_TREATMENT_BED_ROOM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
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
        [ActionName("UpdateTime")]
        public ApiResult UpdateTime(ApiParam<HIS_TREATMENT_BED_ROOM> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT_BED_ROOM> result = new ApiResultObject<HIS_TREATMENT_BED_ROOM>(null);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
                    result = mng.UpdateTime(param.ApiData);
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
        [ActionName("SetObservedTime")]
        public ApiResult SetObservedTime(ApiParam<ObservedTimeSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentBedRoomManager mng = new HisTreatmentBedRoomManager(param.CommonParam);
                    result = mng.SetObservedTime(param.ApiData);
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
