using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaTranslateController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaTranslate.Get.SdaTranslateFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaTranslate.Get.SdaTranslateFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_TRANSLATE>> result = new ApiResultObject<List<SDA_TRANSLATE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaTranslateManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_TRANSLATE> resultData = managerContainer.Run<List<SDA_TRANSLATE>>();
                    result = PackResult<List<SDA_TRANSLATE>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_TRANSLATE> param)
        {
            try
            {
                ApiResultObject<SDA_TRANSLATE> result = new ApiResultObject<SDA_TRANSLATE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaTranslateManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_TRANSLATE resultData = managerContainer.Run<SDA_TRANSLATE>();
                    result = PackResult<SDA_TRANSLATE>(resultData);
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
        public ApiResult Update(ApiParam<SDA_TRANSLATE> param)
        {
            try
            {
                ApiResultObject<SDA_TRANSLATE> result = new ApiResultObject<SDA_TRANSLATE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaTranslateManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_TRANSLATE resultData = managerContainer.Run<SDA_TRANSLATE>();
                    result = PackResult<SDA_TRANSLATE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_TRANSLATE> param)
        {
            try
            {
                ApiResultObject<SDA_TRANSLATE> result = new ApiResultObject<SDA_TRANSLATE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaTranslateManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_TRANSLATE resultData = managerContainer.Run<SDA_TRANSLATE>();
                    result = PackResult<SDA_TRANSLATE>(resultData);
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
                    SDA_TRANSLATE data = new SDA_TRANSLATE();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaTranslateManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
