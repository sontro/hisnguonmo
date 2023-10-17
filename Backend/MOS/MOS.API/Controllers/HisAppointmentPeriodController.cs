using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisAppointmentPeriod;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisAppointmentPeriodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAppointmentPeriodFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAppointmentPeriodFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_APPOINTMENT_PERIOD>> result = new ApiResultObject<List<HIS_APPOINTMENT_PERIOD>>(null);
                if (param != null)
                {
                    HisAppointmentPeriodManager mng = new HisAppointmentPeriodManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisAppointmentPeriodCountByDateFilter>), "param")]
        [ActionName("GetCountByDate")]
        public ApiResult GetCountByDate(ApiParam<HisAppointmentPeriodCountByDateFilter> param)
        {
            try
            {
                ApiResultObject<List<HisAppointmentPeriodCountByDateSDO>> result = new ApiResultObject<List<HisAppointmentPeriodCountByDateSDO>>(null);
                if (param != null)
                {
                    HisAppointmentPeriodManager mng = new HisAppointmentPeriodManager(param.CommonParam);
                    result = mng.GetCountByDate(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_APPOINTMENT_PERIOD> param)
        {
            try
            {
                ApiResultObject<HIS_APPOINTMENT_PERIOD> result = new ApiResultObject<HIS_APPOINTMENT_PERIOD>(null);
                if (param != null)
                {
                    HisAppointmentPeriodManager mng = new HisAppointmentPeriodManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_APPOINTMENT_PERIOD> param)
        {
            try
            {
                ApiResultObject<HIS_APPOINTMENT_PERIOD> result = new ApiResultObject<HIS_APPOINTMENT_PERIOD>(null);
                if (param != null)
                {
                    HisAppointmentPeriodManager mng = new HisAppointmentPeriodManager(param.CommonParam);
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
                    HisAppointmentPeriodManager mng = new HisAppointmentPeriodManager(param.CommonParam);
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
                ApiResultObject<HIS_APPOINTMENT_PERIOD> result = new ApiResultObject<HIS_APPOINTMENT_PERIOD>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAppointmentPeriodManager mng = new HisAppointmentPeriodManager(param.CommonParam);
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
            ApiResultObject<HIS_APPOINTMENT_PERIOD> result = null;
            if (param != null && param.ApiData != null)
            {
                HisAppointmentPeriodManager mng = new HisAppointmentPeriodManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
