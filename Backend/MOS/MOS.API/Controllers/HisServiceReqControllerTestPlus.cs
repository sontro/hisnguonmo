using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.Filter;
using MOS.MANAGER.HisServiceReq;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTestServiceReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(TestResultI3DrugsFilter), "param")]
        [ActionName("GetResultByPatient")]
        [AllowAnonymous]
        public ApiResult GetResultByPatient(TestResultI3DrugsFilter param)
        {
            try
            {
                ApiResultObject<List<TestResultI3DrugsTDO>> result = new ApiResultObject<List<TestResultI3DrugsTDO>>(null);
                CommonParam commonParam = new CommonParam();
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(commonParam);
                    result = mng.GetResultForI3Drugs(param);
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