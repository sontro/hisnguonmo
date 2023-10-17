using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaReligionController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaReligion.Get.SdaReligionFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaReligion.Get.SdaReligionFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_RELIGION>> result = new ApiResultObject<List<SDA_RELIGION>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaReligionManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_RELIGION> resultData = managerContainer.Run<List<SDA_RELIGION>>();
                    result = PackResult<List<SDA_RELIGION>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_RELIGION> param)
        {
            try
            {
                ApiResultObject<SDA_RELIGION> result = new ApiResultObject<SDA_RELIGION>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaReligionManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_RELIGION resultData = managerContainer.Run<SDA_RELIGION>();
                    result = PackResult<SDA_RELIGION>(resultData);
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
        public ApiResult Update(ApiParam<SDA_RELIGION> param)
        {
            try
            {
                ApiResultObject<SDA_RELIGION> result = new ApiResultObject<SDA_RELIGION>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaReligionManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_RELIGION resultData = managerContainer.Run<SDA_RELIGION>();
                    result = PackResult<SDA_RELIGION>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_RELIGION> param)
        {
            try
            {
                ApiResultObject<SDA_RELIGION> result = new ApiResultObject<SDA_RELIGION>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaReligionManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_RELIGION resultData = managerContainer.Run<SDA_RELIGION>();
                    result = PackResult<SDA_RELIGION>(resultData);
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
        public ApiResult Delete(ApiParam<SDA_RELIGION> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaReligionManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
