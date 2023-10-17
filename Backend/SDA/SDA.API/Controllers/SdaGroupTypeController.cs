using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaGroupTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaGroupType.Get.SdaGroupTypeFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaGroupType.Get.SdaGroupTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_GROUP_TYPE>> result = new ApiResultObject<List<SDA_GROUP_TYPE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaGroupTypeManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_GROUP_TYPE> resultData = managerContainer.Run<List<SDA_GROUP_TYPE>>();
                    result = PackResult<List<SDA_GROUP_TYPE>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_GROUP_TYPE> param)
        {
            try
            {
                ApiResultObject<SDA_GROUP_TYPE> result = new ApiResultObject<SDA_GROUP_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaGroupTypeManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_GROUP_TYPE resultData = managerContainer.Run<SDA_GROUP_TYPE>();
                    result = PackResult<SDA_GROUP_TYPE>(resultData);
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
        public ApiResult Update(ApiParam<SDA_GROUP_TYPE> param)
        {
            try
            {
                ApiResultObject<SDA_GROUP_TYPE> result = new ApiResultObject<SDA_GROUP_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaGroupTypeManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_GROUP_TYPE resultData = managerContainer.Run<SDA_GROUP_TYPE>();
                    result = PackResult<SDA_GROUP_TYPE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_GROUP_TYPE> param)
        {
            try
            {
                ApiResultObject<SDA_GROUP_TYPE> result = new ApiResultObject<SDA_GROUP_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaGroupTypeManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_GROUP_TYPE resultData = managerContainer.Run<SDA_GROUP_TYPE>();
                    result = PackResult<SDA_GROUP_TYPE>(resultData);
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
        public ApiResult Delete(ApiParam<SDA_GROUP_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaGroupTypeManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
