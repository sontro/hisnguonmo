using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisDepartmentTran;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisDepartmentTranController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDepartmentTranFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDepartmentTranFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DEPARTMENT_TRAN>> result = new ApiResultObject<List<HIS_DEPARTMENT_TRAN>>(null);
                if (param != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisDepartmentTranViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisDepartmentTranViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DEPARTMENT_TRAN>> result = new ApiResultObject<List<V_HIS_DEPARTMENT_TRAN>>(null);
                if (param != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisDepartmentTranLastFilter>), "param")]
        [ActionName("GetLastByTreatmentId")]
        public ApiResult GetLastByTreatmentId(ApiParam<HisDepartmentTranLastFilter> param)
        {
            try
            {
                ApiResultObject<V_HIS_DEPARTMENT_TRAN> result = new ApiResultObject<V_HIS_DEPARTMENT_TRAN>(null);
                if (param != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
                    result = mng.GetLastByTreatmentId(param.ApiData);
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
        [ActionName("GetFirstByTreatmentId")]
        public ApiResult GetFirstByTreatmentId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<V_HIS_DEPARTMENT_TRAN> result = new ApiResultObject<V_HIS_DEPARTMENT_TRAN>(null);
                if (param != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
                    result = mng.GetFirstByTreatmentId(param.ApiData);
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
        public ApiResult Create(ApiParam<HisDepartmentTranSDO> param)
        {
            try
            {
                ApiResultObject<HIS_DEPARTMENT_TRAN> result = new ApiResultObject<HIS_DEPARTMENT_TRAN>(null);
                if (param != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
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
        [ActionName("Hospitalize")]
        public ApiResult Hospitalize(ApiParam<HisDepartmentTranHospitalizeSDO> param)
        {
            try
            {
                ApiResultObject<HisDepartmentTranHospitalizeResultSDO> result = new ApiResultObject<HisDepartmentTranHospitalizeResultSDO>(null);
                if (param != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
                    result = mng.Hospitalize(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_DEPARTMENT_TRAN> param)
        {
            try
            {
                ApiResultObject<HIS_DEPARTMENT_TRAN> result = new ApiResultObject<HIS_DEPARTMENT_TRAN>(null);
                if (param != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
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
        [ActionName("Receive")]
        public ApiResult Receive(ApiParam<HisDepartmentTranReceiveSDO> param)
        {
            try
            {
                ApiResultObject<HIS_DEPARTMENT_TRAN> result = new ApiResultObject<HIS_DEPARTMENT_TRAN>(null);
                if (param != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
                    result = mng.Receive(param.ApiData);
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
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_DEPARTMENT_TRAN> param)
        {
            try
            {
                ApiResultObject<HIS_DEPARTMENT_TRAN> result = new ApiResultObject<HIS_DEPARTMENT_TRAN>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDepartmentTranManager mng = new HisDepartmentTranManager(param.CommonParam);
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
