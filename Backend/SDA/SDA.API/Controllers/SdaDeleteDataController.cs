using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaDeleteDataController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaDeleteData.Get.SdaDeleteDataFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaDeleteData.Get.SdaDeleteDataFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_DELETE_DATA>> result = new ApiResultObject<List<SDA_DELETE_DATA>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaDeleteDataManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_DELETE_DATA> resultData = managerContainer.Run<List<SDA_DELETE_DATA>>();
                    result = PackResult<List<SDA_DELETE_DATA>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_DELETE_DATA> param)
        {
            try
            {
                ApiResultObject<SDA_DELETE_DATA> result = new ApiResultObject<SDA_DELETE_DATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaDeleteDataManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DELETE_DATA resultData = managerContainer.Run<SDA_DELETE_DATA>();
                    result = PackResult<SDA_DELETE_DATA>(resultData);
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
        public ApiResult Update(ApiParam<SDA_DELETE_DATA> param)
        {
            try
            {
                ApiResultObject<SDA_DELETE_DATA> result = new ApiResultObject<SDA_DELETE_DATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaDeleteDataManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DELETE_DATA resultData = managerContainer.Run<SDA_DELETE_DATA>();
                    result = PackResult<SDA_DELETE_DATA>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_DELETE_DATA> param)
        {
            try
            {
                ApiResultObject<SDA_DELETE_DATA> result = new ApiResultObject<SDA_DELETE_DATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaDeleteDataManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DELETE_DATA resultData = managerContainer.Run<SDA_DELETE_DATA>();
                    result = PackResult<SDA_DELETE_DATA>(resultData);
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
                    SDA_DELETE_DATA data = new SDA_DELETE_DATA();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaDeleteDataManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
