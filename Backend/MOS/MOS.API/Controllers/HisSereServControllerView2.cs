using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
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
        [ApiParamFilter(typeof(ApiParam<HisSereServView2FilterQuery>), "param")]
        [ActionName("GetView2")]
        public ApiResult GetView(ApiParam<HisSereServView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_2>> result = null;
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
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
