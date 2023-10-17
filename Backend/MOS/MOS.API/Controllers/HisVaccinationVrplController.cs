using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisVaccinationVrpl;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisVaccinationVrplController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisVaccinationVrplFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisVaccinationVrplFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_VACCINATION_VRPL>> result = new ApiResultObject<List<HIS_VACCINATION_VRPL>>(null);
                if (param != null)
                {
                    HisVaccinationVrplManager mng = new HisVaccinationVrplManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_VACCINATION_VRPL> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION_VRPL> result = new ApiResultObject<HIS_VACCINATION_VRPL>(null);
                if (param != null)
                {
                    HisVaccinationVrplManager mng = new HisVaccinationVrplManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_VACCINATION_VRPL> param)
        {
            try
            {
                ApiResultObject<HIS_VACCINATION_VRPL> result = new ApiResultObject<HIS_VACCINATION_VRPL>(null);
                if (param != null)
                {
                    HisVaccinationVrplManager mng = new HisVaccinationVrplManager(param.CommonParam);
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
                    HisVaccinationVrplManager mng = new HisVaccinationVrplManager(param.CommonParam);
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
                ApiResultObject<HIS_VACCINATION_VRPL> result = new ApiResultObject<HIS_VACCINATION_VRPL>(null);
                if (param != null && param.ApiData != null)
                {
                    HisVaccinationVrplManager mng = new HisVaccinationVrplManager(param.CommonParam);
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
            ApiResultObject<HIS_VACCINATION_VRPL> result = null;
            if (param != null && param.ApiData != null)
            {
                HisVaccinationVrplManager mng = new HisVaccinationVrplManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
