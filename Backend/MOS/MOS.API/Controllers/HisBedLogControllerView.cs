using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBedLog;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBedLogController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBedLogViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisBedLogViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BED_LOG>> result = new ApiResultObject<List<V_HIS_BED_LOG>>(null);
                if (param != null)
                {
                    HisBedLogManager mng = new HisBedLogManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBedLogView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisBedLogView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BED_LOG_1>> result = new ApiResultObject<List<V_HIS_BED_LOG_1>>(null);
                if (param != null)
                {
                    HisBedLogManager mng = new HisBedLogManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisBedLogView4FilterQuery>), "param")]
        [ActionName("GetView4")]
        public ApiResult GetView4(ApiParam<HisBedLogView4FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BED_LOG_4>> result = new ApiResultObject<List<V_HIS_BED_LOG_4>>(null);
                if (param != null)
                {
                    HisBedLogManager mng = new HisBedLogManager(param.CommonParam);
                    result = mng.GetView4(param.ApiData);
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
