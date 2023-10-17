using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisPatientTypeAlterController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<DHisPatientTypeAlter1Filter>), "param")]
        [ActionName("GetDHisPatientTypeAlter1")]
        public ApiResult GetDHisPatientTypeAlter1(ApiParam<DHisPatientTypeAlter1Filter> param)
        {
            try
            {
                ApiResultObject<List<D_HIS_PATIENT_TYPE_ALTER_1>> result = new ApiResultObject<List<D_HIS_PATIENT_TYPE_ALTER_1>>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
                    result = mng.GetDHisPatientTypeAlter1(param.ApiData);
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
