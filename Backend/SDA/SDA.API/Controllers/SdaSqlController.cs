using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaSqlController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaSql.Get.SdaSqlFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaSql.Get.SdaSqlFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_SQL>> result = new ApiResultObject<List<SDA_SQL>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_SQL> resultData = managerContainer.Run<List<SDA_SQL>>();
                    result = PackResult<List<SDA_SQL>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_SQL> param)
        {
            try
            {
                ApiResultObject<SDA_SQL> result = new ApiResultObject<SDA_SQL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_SQL resultData = managerContainer.Run<SDA_SQL>();
                    result = PackResult<SDA_SQL>(resultData);
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
        public ApiResult Update(ApiParam<SDA_SQL> param)
        {
            try
            {
                ApiResultObject<SDA_SQL> result = new ApiResultObject<SDA_SQL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_SQL resultData = managerContainer.Run<SDA_SQL>();
                    result = PackResult<SDA_SQL>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_SQL> param)
        {
            try
            {
                ApiResultObject<SDA_SQL> result = new ApiResultObject<SDA_SQL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_SQL resultData = managerContainer.Run<SDA_SQL>();
                    result = PackResult<SDA_SQL>(resultData);
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
                    SDA_SQL data = new SDA_SQL();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaSqlManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
