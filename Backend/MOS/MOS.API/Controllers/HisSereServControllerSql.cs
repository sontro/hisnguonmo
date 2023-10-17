using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSereServController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<DHisSereServ2Filter>), "param")]
        [ActionName("GetDHisSereServ2")]
        public ApiResult GetDHisSereServ2(ApiParam<DHisSereServ2Filter> param)
        {
            try
            {
                ApiResultObject<List<DHisSereServ2>> result = null;
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.GetDHisSereServ2(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisSereServMinDurationFilter>), "param")]
        [ActionName("GetExceedMinDuration")]
        public ApiResult GetExceedMinDuration(ApiParam<HisSereServMinDurationFilter> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV>> result = null;
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.GetExceedMinDuration(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisSereServRecentPtttFilter>), "param")]
        [ActionName("GetRecentPttt")]
        public ApiResult GetRecentPttt(ApiParam<HisSereServRecentPtttFilter> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV>> result = null;
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.GetRecentPttt(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisSereServCountFilter>), "param")]
        [ActionName("GetCount")]
        public ApiResult GetCount(ApiParam<HisSereServCountFilter> param)
        {
            try
            {
                ApiResultObject<HisSereServCountSDO> result = null;
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.GetCount(param.ApiData);
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
