using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.Dashboard.Filter;
using MOS.Dashboard.DDO;
using MOS.Dashboard.HisServiceReq;
using MOS.Dashboard.HisSereServ;
using MOS.Dashboard.HisTreatment;
using MOS.Dashboard.HisDepartment;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisDashboardController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<ServiceReqGeneralByDepaFilter>), "param")]
        [ActionName("GetGeneralServiceReq")]
        public ApiResult GetGeneralServiceReq(ApiParam<ServiceReqGeneralByDepaFilter> param)
        {
            try
            {
                ApiResultObject<List<ServiceReqGeneralByDepaDDO>> result = new ApiResultObject<List<ServiceReqGeneralByDepaDDO>>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.GetGeneralByDepartment(param.ApiData);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<SereServGeneralByDepaFilter>), "param")]
        [ActionName("GetGeneralSereServ")]
        public ApiResult GetGeneralSereServ(ApiParam<SereServGeneralByDepaFilter> param)
        {
            try
            {
                ApiResultObject<List<SereServGeneralByDepaDDO>> result = new ApiResultObject<List<SereServGeneralByDepaDDO>>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.GetGeneralByDepartment(param.ApiData);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<SereServGeneralByDepaFilter>), "param")]
        [ActionName("GetGeneralTestSereServ")]
        public ApiResult GetGeneralTestSereServ(ApiParam<SereServGeneralByDepaFilter> param)
        {
            try
            {
                ApiResultObject<List<SereServGeneralByDepaDDO>> result = new ApiResultObject<List<SereServGeneralByDepaDDO>>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.GetGeneralTestByDepartment(param.ApiData);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<TreatmentIcdFilter>), "param")]
        [ActionName("GetGeneralTreatmentTopIcd")]
        public ApiResult GetGeneralTreatmentTopIcd(ApiParam<TreatmentIcdFilter> param)
        {
            try
            {
                ApiResultObject<List<TreatmentIcdDDO>> result = new ApiResultObject<List<TreatmentIcdDDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetGeneralTopIcd(param.ApiData);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<ServiceReqExamDateFilter>), "param")]
        [ActionName("GetGeneralExamByDate")]
        public ApiResult GetGeneralExamByDate(ApiParam<ServiceReqExamDateFilter> param)
        {
            try
            {
                ApiResultObject<List<ServiceReqExamDateDDO>> result = new ApiResultObject<List<ServiceReqExamDateDDO>>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.GetGeneralExamDate(param.ApiData);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<DepartmentFilter>), "param")]
        [ActionName("GetDepartment")]
        public ApiResult GetDepartment(ApiParam<DepartmentFilter> param)
        {
            try
            {
                ApiResultObject<List<DepartmentDDO>> result = new ApiResultObject<List<DepartmentDDO>>(null);
                if (param != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<TreatmentTimeFilter>), "param")]
        [ActionName("GetTreatmentWithTime")]
        public ApiResult GetTreatmentWithTime(ApiParam<TreatmentTimeFilter> param)
        {
            try
            {
                ApiResultObject<List<TreatmentTimeDDO>> result = new ApiResultObject<List<TreatmentTimeDDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetWithTime(param.ApiData);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<TreatmentTimeAvgFilter>), "param")]
        [ActionName("GetGeneralTreatmentTimeAvg")]
        public ApiResult GetGeneralTreatmentTimeAvg(ApiParam<TreatmentTimeAvgFilter> param)
        {
            try
            {
                ApiResultObject<List<TreatmentTimeAvgDDO>> result = new ApiResultObject<List<TreatmentTimeAvgDDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetTimeAvg(param.ApiData);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<TreatmentRegisterByTimeFilter>), "param")]
        [ActionName("GetTreatmentRegisterByTime")]
        public ApiResult GetTreatmentRegisterByTime(ApiParam<TreatmentRegisterByTimeFilter> param)
        {
            try
            {
                ApiResultObject<List<TreatmentRegisterByTimeDDO>> result = new ApiResultObject<List<TreatmentRegisterByTimeDDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetRegisterByTime(param.ApiData);
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
