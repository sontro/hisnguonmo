using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaModuleFieldController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaModuleField.Get.SdaModuleFieldFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaModuleField.Get.SdaModuleFieldFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_MODULE_FIELD>> result = new ApiResultObject<List<SDA_MODULE_FIELD>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaModuleFieldManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_MODULE_FIELD> resultData = managerContainer.Run<List<SDA_MODULE_FIELD>>();
                    result = PackResult<List<SDA_MODULE_FIELD>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_MODULE_FIELD> param)
        {
            try
            {
                ApiResultObject<SDA_MODULE_FIELD> result = new ApiResultObject<SDA_MODULE_FIELD>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaModuleFieldManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_MODULE_FIELD resultData = managerContainer.Run<SDA_MODULE_FIELD>();
                    result = PackResult<SDA_MODULE_FIELD>(resultData);
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
        public ApiResult Update(ApiParam<SDA_MODULE_FIELD> param)
        {
            try
            {
                ApiResultObject<SDA_MODULE_FIELD> result = new ApiResultObject<SDA_MODULE_FIELD>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaModuleFieldManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_MODULE_FIELD resultData = managerContainer.Run<SDA_MODULE_FIELD>();
                    result = PackResult<SDA_MODULE_FIELD>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_MODULE_FIELD> param)
        {
            try
            {
                ApiResultObject<SDA_MODULE_FIELD> result = new ApiResultObject<SDA_MODULE_FIELD>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaModuleFieldManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_MODULE_FIELD resultData = managerContainer.Run<SDA_MODULE_FIELD>();
                    result = PackResult<SDA_MODULE_FIELD>(resultData);
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
                    SDA_MODULE_FIELD data = new SDA_MODULE_FIELD();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaModuleFieldManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
