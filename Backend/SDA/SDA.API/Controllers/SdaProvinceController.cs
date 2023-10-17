using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaProvinceController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaProvince.Get.SdaProvinceFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaProvince.Get.SdaProvinceFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_PROVINCE>> result = new ApiResultObject<List<SDA_PROVINCE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_PROVINCE> resultData = managerContainer.Run<List<SDA_PROVINCE>>();
                    result = PackResult<List<SDA_PROVINCE>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_PROVINCE> param)
        {
            try
            {
                ApiResultObject<SDA_PROVINCE> result = new ApiResultObject<SDA_PROVINCE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_PROVINCE resultData = managerContainer.Run<SDA_PROVINCE>();
                    result = PackResult<SDA_PROVINCE>(resultData);
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
        public ApiResult Update(ApiParam<SDA_PROVINCE> param)
        {
            try
            {
                ApiResultObject<SDA_PROVINCE> result = new ApiResultObject<SDA_PROVINCE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_PROVINCE resultData = managerContainer.Run<SDA_PROVINCE>();
                    result = PackResult<SDA_PROVINCE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_PROVINCE> param)
        {
            try
            {
                ApiResultObject<SDA_PROVINCE> result = new ApiResultObject<SDA_PROVINCE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_PROVINCE resultData = managerContainer.Run<SDA_PROVINCE>();
                    result = PackResult<SDA_PROVINCE>(resultData);
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
        public ApiResult Delete(ApiParam<SDA_PROVINCE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
