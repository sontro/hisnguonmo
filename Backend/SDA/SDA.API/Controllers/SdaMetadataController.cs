using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaMetadataController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaMetadata.Get.SdaMetadataFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaMetadata.Get.SdaMetadataFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_METADATA>> result = new ApiResultObject<List<SDA_METADATA>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaMetadataManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_METADATA> resultData = managerContainer.Run<List<SDA_METADATA>>();
                    result = PackResult<List<SDA_METADATA>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_METADATA> param)
        {
            try
            {
                ApiResultObject<SDA_METADATA> result = new ApiResultObject<SDA_METADATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaMetadataManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_METADATA resultData = managerContainer.Run<SDA_METADATA>();
                    result = PackResult<SDA_METADATA>(resultData);
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
        public ApiResult Create(ApiParam<List<SDA_METADATA>> param)
        {
            try
            {
                ApiResultObject<List<SDA_METADATA>> result = new ApiResultObject<List<SDA_METADATA>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaMetadataManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_METADATA> resultData = managerContainer.Run<List<SDA_METADATA>>();
                    result = PackResult<List<SDA_METADATA>>(resultData);
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
        public ApiResult Update(ApiParam<SDA_METADATA> param)
        {
            try
            {
                ApiResultObject<SDA_METADATA> result = new ApiResultObject<SDA_METADATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaMetadataManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_METADATA resultData = managerContainer.Run<SDA_METADATA>();
                    result = PackResult<SDA_METADATA>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_METADATA> param)
        {
            try
            {
                ApiResultObject<SDA_METADATA> result = new ApiResultObject<SDA_METADATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaMetadataManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_METADATA resultData = managerContainer.Run<SDA_METADATA>();
                    result = PackResult<SDA_METADATA>(resultData);
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
                    SDA_METADATA data = new SDA_METADATA();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaMetadataManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
