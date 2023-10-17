using Inventec.Common.Logging;
using Inventec.Core;
using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.SdaCustomizeButton;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaCustomizeButtonController : BaseApiController
    {
        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<SDA_CUSTOMIZE_BUTTON>> param)
        {
            try
            {
                ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> result = new ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<SDA_CUSTOMIZE_BUTTON>> param)
        {
            try
            {
                ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> result = new ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
                    result = mng.UpdateList(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    SdaCustomizeButtonManager mng = new SdaCustomizeButtonManager(param.CommonParam);
                    result = mng.DeleteList(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
