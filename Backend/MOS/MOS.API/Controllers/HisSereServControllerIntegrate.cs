using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.TDO;
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
        [HttpPost]
        [ActionName("UpdateServiceExecute")]
        public ApiResult UpdateServiceExecute(ApiParam<HisUpdateServiceExecuteTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateServiceExecute(param.ApiData);
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
