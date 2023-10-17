using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisVaccinationExam;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisVaccinationExamController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisVaccinationExamFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisVaccinationExamFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_VACCINATION_EXAM>> result = new ApiResultObject<List<HIS_VACCINATION_EXAM>>(null);
                if (param != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
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
        [ActionName("Finish")]
        public ApiResult Finish(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION_EXAM> result = new ApiResultObject<HIS_VACCINATION_EXAM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
                    result = mng.Finish(param.ApiData);
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
        [ActionName("CancelFinish")]
        public ApiResult CancelFinish(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION_EXAM> result = new ApiResultObject<HIS_VACCINATION_EXAM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
                    result = mng.CancelFinish(param.ApiData);
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
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisVaccinationExamSDO> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION_EXAM> result = new ApiResultObject<HIS_VACCINATION_EXAM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
                    result = mng.UpdateSdo(param.ApiData);
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
        [ActionName("Treat")]
        public ApiResult Treat(ApiParam<HisVaccinationExamTreatSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
                    result = mng.Treat(param.ApiData);
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
        [ActionName("Appointment")]
        public ApiResult Appointment(ApiParam<HisVaccinationAppointmentSDO> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_VACC_APPOINTMENT>> result = new ApiResultObject<List<V_HIS_VACC_APPOINTMENT>>(null);
                if (param != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
                    result = mng.Appointment(param.ApiData);
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
        public ApiResult Delete(ApiParam<HisVaccinationExamDeleteSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION_EXAM> result = new ApiResultObject<HIS_VACCINATION_EXAM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
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
            ApiResultObject<HIS_VACCINATION_EXAM> result = null;
            if (param != null && param.ApiData != null)
            {
                HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Register")]
        public ApiResult Register(ApiParam<HisPatientVaccinationSDO> param)
        {
            try
            {
                ApiResultObject<VaccinationRegisterResultSDO> result = new ApiResultObject<VaccinationRegisterResultSDO>(null);
                if (param != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
                    result = mng.Register(param.ApiData);
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
