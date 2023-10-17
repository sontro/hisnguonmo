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
        [ApiParamFilter(typeof(ApiParam<HisMediRecordView2FilterQuery>), "param")]
        [ActionName("GetView2")]
        public ApiResult GetView2(ApiParam<HisMediRecordView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_RECORD_2>> result = new ApiResultObject<List<V_HIS_MEDI_RECORD_2>>(null);
                if (param != null)
                {
                    HisMediRecordManager mng = new HisMediRecordManager(param.CommonParam);
                    result = mng.GetView2(param.ApiData);
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