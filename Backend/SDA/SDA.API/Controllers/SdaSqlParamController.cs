using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaSqlParamController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaSqlParam.Get.SdaSqlParamFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaSqlParam.Get.SdaSqlParamFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_SQL_PARAM>> result = new ApiResultObject<List<SDA_SQL_PARAM>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlParamManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_SQL_PARAM> resultData = managerContainer.Run<List<SDA_SQL_PARAM>>();
                    result = PackResult<List<SDA_SQL_PARAM>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_SQL_PARAM> param)
        {
            try
            {
                ApiResultObject<SDA_SQL_PARAM> result = new ApiResultObject<SDA_SQL_PARAM>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlParamManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_SQL_PARAM resultData = managerContainer.Run<SDA_SQL_PARAM>();
                    result = PackResult<SDA_SQL_PARAM>(resultData);
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
        public ApiResult Update(ApiParam<SDA_SQL_PARAM> param)
        {
            try
            {
                ApiResultObject<SDA_SQL_PARAM> result = new ApiResultObject<SDA_SQL_PARAM>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlParamManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_SQL_PARAM resultData = managerContainer.Run<SDA_SQL_PARAM>();
                    result = PackResult<SDA_SQL_PARAM>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_SQL_PARAM> param)
        {
            try
            {
                ApiResultObject<SDA_SQL_PARAM> result = new ApiResultObject<SDA_SQL_PARAM>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlParamManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_SQL_PARAM resultData = managerContainer.Run<SDA_SQL_PARAM>();
                    result = PackResult<SDA_SQL_PARAM>(resultData);
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
                    SDA_SQL_PARAM data = new SDA_SQL_PARAM();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlParamManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
