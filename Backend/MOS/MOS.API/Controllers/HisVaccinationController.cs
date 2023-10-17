using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisVaccination;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisVaccinationController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisVaccinationFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisVaccinationFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_VACCINATION>> result = new ApiResultObject<List<HIS_VACCINATION>>(null);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
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
        [ActionName("AssignCreate")]
        public ApiResult AssignCreate(ApiParam<HisVaccinationAssignSDO> param)
        {
            try
            {
                ApiResultObject<VaccinationResultSDO> result = new ApiResultObject<VaccinationResultSDO>(null);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
                    result = mng.AssignCreate(param.ApiData);
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
        [ActionName("AssignUpdate")]
        public ApiResult AssignUpdate(ApiParam<HisVaccinationAssignSDO> param)
        {
            try
            {
                ApiResultObject<VaccinationResultSDO> result = new ApiResultObject<VaccinationResultSDO>(null);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
                    result = mng.AssignUpdate(param.ApiData);
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
        [ActionName("Process")]
        public ApiResult Process(ApiParam<HisVaccinationProcessSDO> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
                    result = mng.Process(param.ApiData);
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
        [ActionName("ChangeMedicine")]
        public ApiResult ChangeMedicine(ApiParam<HisVaccinationChangeMedicineSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST_MEDICINE> result = new ApiResultObject<HIS_EXP_MEST_MEDICINE>(null);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
                    result = mng.ChangeMedicine(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_VACCINATION> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HisVaccinationSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
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
                ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
                if (param != null && param.ApiData != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
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
            ApiResultObject<HIS_VACCINATION> result = null;
            if (param != null && param.ApiData != null)
            {
                HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }


        [HttpPost]
        [ActionName("UpdateReactInfo")]
        public ApiResult UpdateReactInfo(ApiParam<HisVaccReactInfoSDO> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
                    result = mng.UpdateReactInfo(param.ApiData);
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
        [ActionName("UnReact")]
        public ApiResult UnReact(ApiParam<HIS_VACCINATION> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION> result = new ApiResultObject<HIS_VACCINATION>(null);
                if (param != null)
                {
                    HisVaccinationManager mng = new HisVaccinationManager(param.CommonParam);
                    result = mng.UnReact(param.ApiData);
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
