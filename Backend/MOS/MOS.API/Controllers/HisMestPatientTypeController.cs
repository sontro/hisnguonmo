using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPatientType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMestPatientTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPatientTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestPatientTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> result = new ApiResultObject<List<HIS_MEST_PATIENT_TYPE>>(null);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMestPatientTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMestPatientTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_PATIENT_TYPE>> result = new ApiResultObject<List<V_HIS_MEST_PATIENT_TYPE>>(null);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEST_PATIENT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_PATIENT_TYPE> result = new ApiResultObject<HIS_MEST_PATIENT_TYPE>(null);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEST_PATIENT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_PATIENT_TYPE> result = new ApiResultObject<HIS_MEST_PATIENT_TYPE>(null);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MEST_PATIENT_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_MEST_PATIENT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_PATIENT_TYPE> result = new ApiResultObject<HIS_MEST_PATIENT_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_MEST_PATIENT_TYPE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> result = new ApiResultObject<List<HIS_MEST_PATIENT_TYPE>>(null);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        [ActionName("CopyByMediStock")]
        public ApiResult CopyByMediStock(ApiParam<HisMestPatientTypeCopyByMediStockSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> result = new ApiResultObject<List<HIS_MEST_PATIENT_TYPE>>(null);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
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
        [ActionName("CopyByPatientType")]
        public ApiResult CopyByPatientType(ApiParam<HisMestPatientTypeCopyByPatientTypeSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> result = new ApiResultObject<List<HIS_MEST_PATIENT_TYPE>>(null);
                if (param != null)
                {
                    HisMestPatientTypeManager mng = new HisMestPatientTypeManager(param.CommonParam);
                    result = mng.CopyByPatientType(param.ApiData);
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
