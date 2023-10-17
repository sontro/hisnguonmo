using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTreatmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentWithPatientTypeInfoFilter>), "param")]
        [ActionName("GetTreatmentWithPatientTypeInfoSdo")]
        public ApiResult GetTreatmentWithPatientTypeInfoSdo(ApiParam<HisTreatmentWithPatientTypeInfoFilter> param)
        {
            try
            {
                ApiResultObject<List<HisTreatmentWithPatientTypeInfoSDO>> result = new ApiResultObject<List<HisTreatmentWithPatientTypeInfoSDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetTreatmentWithPatientTypeInfoSdo(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentCounterAndPriceFilter>), "param")]
        [ActionName("GetTreatmentCounterAndPriceByTime")]
        public ApiResult GetTreatmentCounterAndPriceByTime(ApiParam<HisTreatmentCounterAndPriceFilter> param)
        {
            try
            {
                ApiResultObject<HisTreatmentCounterAndPriceSDO> result = new ApiResultObject<HisTreatmentCounterAndPriceSDO>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetTreatmentCounterAndPriceByTime(param.ApiData);
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
        [ActionName("GetMissingInvoiceInfoMaterialByTreatmentId")]
        public ApiResult GetMissingInvoiceInfoMaterialByTreatmentId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<MissingInvoiceInfoMaterialSDO>> result = new ApiResultObject<List<MissingInvoiceInfoMaterialSDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetMissingInvoiceInfoMaterialByTreatmentId(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentForEmrSDO>), "param")]
        [ActionName("GetForEmr")]
        public ApiResult GetForEmr(ApiParam<HisTreatmentForEmrSDO> param)
        {
            try
            {
                ApiResultObject<HisTreatmentForEmrSDO> result = new ApiResultObject<HisTreatmentForEmrSDO>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetForEmr(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentForRecordCheckingFilter>), "param")]
        [ActionName("GetInfoForRecordChecking")]
        public ApiResult GetInfoForRecordChecking(ApiParam<HisTreatmentForRecordCheckingFilter> param)
        {
            try
            {
                ApiResultObject<HisTreatmentForRecordCheckingSDO> result = new ApiResultObject<HisTreatmentForRecordCheckingSDO>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetInfoForRecordChecking(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetCardServiceCodeByDepartment")]
        [AllowAnonymous]
        public ApiResult GetCardServiceCodeByDepartment(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<List<string>> result = new ApiResultObject<List<string>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetCardServiceCodeByDepartment(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentRationNotApproveFilter>), "param")]
        [ActionName("GetRationNotApprove")]
        public ApiResult GetRationNotApprove(ApiParam<HisTreatmentRationNotApproveFilter> param)
        {
            try
            {
                ApiResultObject<List<HisTreatmentRationNotApproveSDO>> result = new ApiResultObject<List<HisTreatmentRationNotApproveSDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetRationNotApprove(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentMedicineForEmrFilter>), "param")]
        [ActionName("GetMedicineForEmr")]
        public ApiResult GetMedicineForEmr(ApiParam<HisTreatmentMedicineForEmrFilter> param)
        {
            try
            {
                ApiResultObject<List<HisTreatmentMedicineTDO>> result = new ApiResultObject<List<HisTreatmentMedicineTDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetMedicineForEmr(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetClinicalDetailForEmr")]
        public ApiResult GetClinicalDetailForEmr(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<HisTreatmentClinicalDetailForEmrTDO> result = new ApiResultObject<HisTreatmentClinicalDetailForEmrTDO>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetClinicalDetailForEmr(param.ApiData);
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
