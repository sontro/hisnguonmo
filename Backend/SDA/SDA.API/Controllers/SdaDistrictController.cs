using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaDistrictController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaDistrict.Get.SdaDistrictFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaDistrict.Get.SdaDistrictFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_DISTRICT>> result = new ApiResultObject<List<SDA_DISTRICT>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_DISTRICT> resultData = managerContainer.Run<List<SDA_DISTRICT>>();
                    result = PackResult<List<SDA_DISTRICT>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_DISTRICT> param)
        {
            try
            {
                ApiResultObject<SDA_DISTRICT> result = new ApiResultObject<SDA_DISTRICT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DISTRICT resultData = managerContainer.Run<SDA_DISTRICT>();
                    result = PackResult<SDA_DISTRICT>(resultData);
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
        public ApiResult Update(ApiParam<SDA_DISTRICT> param)
        {
            try
            {
                ApiResultObject<SDA_DISTRICT> result = new ApiResultObject<SDA_DISTRICT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DISTRICT resultData = managerContainer.Run<SDA_DISTRICT>();
                    result = PackResult<SDA_DISTRICT>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_DISTRICT> param)
        {
            try
            {
                ApiResultObject<SDA_DISTRICT> result = new ApiResultObject<SDA_DISTRICT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DISTRICT resultData = managerContainer.Run<SDA_DISTRICT>();
                    result = PackResult<SDA_DISTRICT>(resultData);
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
        public ApiResult Delete(ApiParam<SDA_DISTRICT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
