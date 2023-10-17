using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaNationalController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaNational.Get.SdaNationalFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaNational.Get.SdaNationalFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_NATIONAL>> result = new ApiResultObject<List<SDA_NATIONAL>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_NATIONAL> resultData = managerContainer.Run<List<SDA_NATIONAL>>();
                    result = PackResult<List<SDA_NATIONAL>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_NATIONAL> param)
        {
            try
            {
                ApiResultObject<SDA_NATIONAL> result = new ApiResultObject<SDA_NATIONAL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_NATIONAL resultData = managerContainer.Run<SDA_NATIONAL>();
                    result = PackResult<SDA_NATIONAL>(resultData);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<SDA_NATIONAL>> param)
        {
            try
            {
                ApiResultObject<List<SDA_NATIONAL>> result = new ApiResultObject<List<SDA_NATIONAL>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_NATIONAL> resultData = managerContainer.Run<List<SDA_NATIONAL>>();
                    result = PackResult<List<SDA_NATIONAL>>(resultData);
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
        public ApiResult Update(ApiParam<SDA_NATIONAL> param)
        {
            try
            {
                ApiResultObject<SDA_NATIONAL> result = new ApiResultObject<SDA_NATIONAL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_NATIONAL resultData = managerContainer.Run<SDA_NATIONAL>();
                    result = PackResult<SDA_NATIONAL>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_NATIONAL> param)
        {
            try
            {
                ApiResultObject<SDA_NATIONAL> result = new ApiResultObject<SDA_NATIONAL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_NATIONAL resultData = managerContainer.Run<SDA_NATIONAL>();
                    result = PackResult<SDA_NATIONAL>(resultData);
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
        public ApiResult Delete(ApiParam<SDA_NATIONAL> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
