using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    //Ko thay doi ten class nay de ko phai thay doi URI cua cac API tich hop voi pacs
    public partial class HisPacsServiceReqController : BaseApiController
    {
        [HttpPost]
        [ActionName("Start")]
        public ApiResult Start(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.PacsStart(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Unstart")]
        public ApiResult Unstart(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.PacsUnstart(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateResult")]
        public ApiResult UpdateResult(ApiParam<HisPacsResultTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.UpdateResult(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateResultHl7")]
        [AllowAnonymous]
        public ApiResult UpdateResultHl7(PacsHl7TDO param)
        {
            try
            {
                PacsHl7TDO result = new PacsHl7TDO();
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.UpdateResultHl7(param);
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
