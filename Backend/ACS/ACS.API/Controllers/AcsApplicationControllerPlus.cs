using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Manager;
using ACS.SDO;
using Inventec.Backend.MANAGER;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsApplicationController : BaseApiController
    {
        [HttpPost]
        [ActionName("CreateSdo")]
        public ApiResult CreateSdo(ApiParam<AcsApplicationWithDataSDO> param)
        {
            try
            {
                ApiResultObject<AcsApplicationWithDataSDO> result = new ApiResultObject<AcsApplicationWithDataSDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    AcsApplicationWithDataSDO resultData = managerContainer.Run<AcsApplicationWithDataSDO>();
                    result = PackResult<AcsApplicationWithDataSDO>(resultData);
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
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<AcsApplicationWithDataSDO> param)
        {
            try
            {
                ApiResultObject<AcsApplicationWithDataSDO> result = new ApiResultObject<AcsApplicationWithDataSDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    AcsApplicationWithDataSDO resultData = managerContainer.Run<AcsApplicationWithDataSDO>();
                    result = PackResult<AcsApplicationWithDataSDO>(resultData);
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
