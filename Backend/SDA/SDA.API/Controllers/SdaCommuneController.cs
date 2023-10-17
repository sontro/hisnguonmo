using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaCommuneController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaCommune.Get.SdaCommuneFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaCommune.Get.SdaCommuneFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_COMMUNE>> result = new ApiResultObject<List<SDA_COMMUNE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaCommuneManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_COMMUNE> resultData = managerContainer.Run<List<SDA_COMMUNE>>();
                    result = PackResult<List<SDA_COMMUNE>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_COMMUNE> param)
        {
            try
            {
                ApiResultObject<SDA_COMMUNE> result = new ApiResultObject<SDA_COMMUNE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaCommuneManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_COMMUNE resultData = managerContainer.Run<SDA_COMMUNE>();
                    result = PackResult<SDA_COMMUNE>(resultData);
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
        public ApiResult Update(ApiParam<SDA_COMMUNE> param)
        {
            try
            {
                ApiResultObject<SDA_COMMUNE> result = new ApiResultObject<SDA_COMMUNE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaCommuneManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_COMMUNE resultData = managerContainer.Run<SDA_COMMUNE>();
                    result = PackResult<SDA_COMMUNE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_COMMUNE> param)
        {
            try
            {
                ApiResultObject<SDA_COMMUNE> result = new ApiResultObject<SDA_COMMUNE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaCommuneManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_COMMUNE resultData = managerContainer.Run<SDA_COMMUNE>();
                    result = PackResult<SDA_COMMUNE>(resultData);
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
        public ApiResult Delete(ApiParam<SDA_COMMUNE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaCommuneManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
