using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaLanguageController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaLanguage.Get.SdaLanguageFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaLanguage.Get.SdaLanguageFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_LANGUAGE>> result = new ApiResultObject<List<SDA_LANGUAGE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaLanguageManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_LANGUAGE> resultData = managerContainer.Run<List<SDA_LANGUAGE>>();
                    result = PackResult<List<SDA_LANGUAGE>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_LANGUAGE> param)
        {
            try
            {
                ApiResultObject<SDA_LANGUAGE> result = new ApiResultObject<SDA_LANGUAGE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaLanguageManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_LANGUAGE resultData = managerContainer.Run<SDA_LANGUAGE>();
                    result = PackResult<SDA_LANGUAGE>(resultData);
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
        public ApiResult Update(ApiParam<SDA_LANGUAGE> param)
        {
            try
            {
                ApiResultObject<SDA_LANGUAGE> result = new ApiResultObject<SDA_LANGUAGE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaLanguageManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_LANGUAGE resultData = managerContainer.Run<SDA_LANGUAGE>();
                    result = PackResult<SDA_LANGUAGE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_LANGUAGE> param)
        {
            try
            {
                ApiResultObject<SDA_LANGUAGE> result = new ApiResultObject<SDA_LANGUAGE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaLanguageManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_LANGUAGE resultData = managerContainer.Run<SDA_LANGUAGE>();
                    result = PackResult<SDA_LANGUAGE>(resultData);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    SDA_LANGUAGE data = new SDA_LANGUAGE();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaLanguageManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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

        [HttpPost]
        [ActionName("UpdateList")]
        public ApiResult Update(ApiParam<List<SDA_LANGUAGE>> param)
        {
            try
            {
                ApiResultObject<List<SDA_LANGUAGE>> result = new ApiResultObject<List<SDA_LANGUAGE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaLanguageManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_LANGUAGE> resultData = managerContainer.Run<List<SDA_LANGUAGE>>();
                    result = PackResult<List<SDA_LANGUAGE>>(resultData);
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
