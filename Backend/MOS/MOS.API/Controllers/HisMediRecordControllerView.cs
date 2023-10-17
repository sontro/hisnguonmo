using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediRecord;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMediRecordController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediRecordViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMediRecordViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_RECORD>> result = new ApiResultObject<List<V_HIS_MEDI_RECORD>>(null);
                if (param != null)
                {
                    HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
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
    }
}
