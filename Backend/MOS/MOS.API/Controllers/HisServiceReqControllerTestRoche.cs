using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    //Ko thay doi ten class nay de ko phai thay doi URI cua cac API tich hop voi Roche
    public partial class HisTestServiceReqController : BaseApiController
    {
        [HttpPost]
        [ActionName("ResendOrderToRoche")]
        public ApiResult ResendOrderToRoche(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    //result = mng.ResendOrderToRoche(param.ApiData);Bo vi ham nay dang khong check vien phi cac kieu hay khong
                    result = mng.RequestOrder(param.ApiData);
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
        [ActionName("UpdateRocheResult")]
        [AllowAnonymous]
        public ApiResult UpdateRocheResult(ApiParam<string> param)
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
    }
}
