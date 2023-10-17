using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;
using SDA.SDO;

namespace SDA.API.Controllers
{
    public partial class SdaLicenseController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaLicense.Get.SdaLicenseFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaLicense.Get.SdaLicenseFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_LICENSE>> result = new ApiResultObject<List<SDA_LICENSE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaLicenseManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_LICENSE> resultData = managerContainer.Run<List<SDA_LICENSE>>();
                    result = PackResult<List<SDA_LICENSE>>(resultData);
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
        [ActionName("Create")]
        public ApiResult Create(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<SDA_LICENSE> result = new ApiResultObject<SDA_LICENSE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaLicenseManager), "CreateT", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_LICENSE resultData = managerContainer.Run<SDA_LICENSE>();
                    result = PackResult<SDA_LICENSE>(resultData);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<SdaLicenseSDO> param)
        {
            try
            {
                ApiResultObject<SDA_LICENSE> result = new ApiResultObject<SDA_LICENSE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaLicenseManager), "UpdateT", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_LICENSE resultData = managerContainer.Run<SDA_LICENSE>();
                    result = PackResult<SDA_LICENSE>(resultData);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<SDA_LICENSE> param)
        {
            try
            {
                ApiResultObject<SDA_LICENSE> result = new ApiResultObject<SDA_LICENSE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaLicenseManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_LICENSE resultData = managerContainer.Run<SDA_LICENSE>();
                    result = PackResult<SDA_LICENSE>(resultData);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<SDA_LICENSE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaLicenseManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    bool resultData = managerContainer.Run<bool>();
                    result = PackResult<bool>(resultData);
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
