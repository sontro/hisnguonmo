using Inventec.Core;
using MOS.API.Base;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class statisticController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(HipoApiParam<List<List<string>>>), "params", "param")]
        [ActionName("kham_benh")]
        [AllowAnonymous]
        public ApiResult GetTreatmentCounter(HipoApiParam<List<List<string>>> param)
        {
            try
            {
                HipoApiResult<List<HisTreatmentCounterSDO>> result = new HipoApiResult<List<HisTreatmentCounterSDO>>(null);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    var apiResult = mng.GetTreatmentCounter(param.data);
                    result.SetValue(apiResult.Data, !param.CommonParam.HasException, param.CommonParam.Messages);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(HipoApiParam<List<List<string>>>), "params", "param")]
        [ActionName("finance_report")]
        [AllowAnonymous]
        public ApiResult GetFinanceReport(HipoApiParam<List<List<string>>> param)
        {
            try
            {
                HipoApiResult<List<HipoFinanceReportSDO>> result = new HipoApiResult<List<HipoFinanceReportSDO>>(null);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    var apiResult = mng.GetFinanceReport(param.data);
                    result.SetValue(apiResult.Data, !param.CommonParam.HasException, param.CommonParam.Messages);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<List<List<string>>>), "param")]
        [ActionName("KhamBenh")]
        [AllowAnonymous]
        public ApiResult GetCountTreatment(ApiParam<List<List<string>>> param)
        {
            try
            {
                ApiResultObject<List<HisTreatmentCounterSDO>> result = new ApiResultObject<List<HisTreatmentCounterSDO>>(null);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    //List<HisTreatmentCounterSDO> apiResult = mng.GetTreatmentCounter(param.ApiData);
                    //result.SetValue(apiResult, !param.CommonParam.HasException, param.CommonParam);
                    result = mng.GetTreatmentCounter(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<List<List<string>>>), "param")]
        [ActionName("FinanceReport")]
        [AllowAnonymous]
        public ApiResult GetFinance(ApiParam<List<List<string>>> param)
        {
            try
            {
                ApiResultObject<List<HipoFinanceReportSDO>> result = new ApiResultObject<List<HipoFinanceReportSDO>>(null);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    //var apiResult = mng.GetFinanceReport(param.ApiData);
                    //result.SetValue(apiResult, !param.CommonParam.HasException, param.CommonParam);
                    result = mng.GetFinanceReport(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}