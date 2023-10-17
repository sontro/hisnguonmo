using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaProvinceMapController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaProvinceMap.Get.SdaProvinceMapFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaProvinceMap.Get.SdaProvinceMapFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_PROVINCE_MAP>> result = new ApiResultObject<List<SDA_PROVINCE_MAP>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceMapManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_PROVINCE_MAP> resultData = managerContainer.Run<List<SDA_PROVINCE_MAP>>();
                    result = PackResult<List<SDA_PROVINCE_MAP>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_PROVINCE_MAP> param)
        {
            try
            {
                ApiResultObject<SDA_PROVINCE_MAP> result = new ApiResultObject<SDA_PROVINCE_MAP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceMapManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_PROVINCE_MAP resultData = managerContainer.Run<SDA_PROVINCE_MAP>();
                    result = PackResult<SDA_PROVINCE_MAP>(resultData);
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
        public ApiResult Update(ApiParam<SDA_PROVINCE_MAP> param)
        {
            try
            {
                ApiResultObject<SDA_PROVINCE_MAP> result = new ApiResultObject<SDA_PROVINCE_MAP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceMapManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_PROVINCE_MAP resultData = managerContainer.Run<SDA_PROVINCE_MAP>();
                    result = PackResult<SDA_PROVINCE_MAP>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_PROVINCE_MAP> param)
        {
            try
            {
                ApiResultObject<SDA_PROVINCE_MAP> result = new ApiResultObject<SDA_PROVINCE_MAP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceMapManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_PROVINCE_MAP resultData = managerContainer.Run<SDA_PROVINCE_MAP>();
                    result = PackResult<SDA_PROVINCE_MAP>(resultData);
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
                    SDA_PROVINCE_MAP data = new SDA_PROVINCE_MAP();
                    data.ID = param.ApiData;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceMapManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
