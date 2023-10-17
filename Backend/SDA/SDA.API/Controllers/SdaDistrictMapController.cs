using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaDistrictMapController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaDistrictMap.Get.SdaDistrictMapFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaDistrictMap.Get.SdaDistrictMapFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_DISTRICT_MAP>> result = new ApiResultObject<List<SDA_DISTRICT_MAP>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictMapManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_DISTRICT_MAP> resultData = managerContainer.Run<List<SDA_DISTRICT_MAP>>();
                    result = PackResult<List<SDA_DISTRICT_MAP>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_DISTRICT_MAP> param)
        {
            try
            {
                ApiResultObject<SDA_DISTRICT_MAP> result = new ApiResultObject<SDA_DISTRICT_MAP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictMapManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DISTRICT_MAP resultData = managerContainer.Run<SDA_DISTRICT_MAP>();
                    result = PackResult<SDA_DISTRICT_MAP>(resultData);
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
        public ApiResult Update(ApiParam<SDA_DISTRICT_MAP> param)
        {
            try
            {
                ApiResultObject<SDA_DISTRICT_MAP> result = new ApiResultObject<SDA_DISTRICT_MAP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictMapManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DISTRICT_MAP resultData = managerContainer.Run<SDA_DISTRICT_MAP>();
                    result = PackResult<SDA_DISTRICT_MAP>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_DISTRICT_MAP> param)
        {
            try
            {
                ApiResultObject<SDA_DISTRICT_MAP> result = new ApiResultObject<SDA_DISTRICT_MAP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictMapManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_DISTRICT_MAP resultData = managerContainer.Run<SDA_DISTRICT_MAP>();
                    result = PackResult<SDA_DISTRICT_MAP>(resultData);
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
                    SDA_DISTRICT_MAP data = new SDA_DISTRICT_MAP();
                    data.ID = param.ApiData;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaDistrictMapManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
