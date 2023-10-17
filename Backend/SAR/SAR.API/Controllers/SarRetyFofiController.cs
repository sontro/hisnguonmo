using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarRetyFofiController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarRetyFofi.Get.SarRetyFofiFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarRetyFofi.Get.SarRetyFofiFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_RETY_FOFI>> result = new ApiResultObject<List<SAR_RETY_FOFI>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarRetyFofiManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_RETY_FOFI> resultData = managerContainer.Run<List<SAR_RETY_FOFI>>();
                    result = PackResult<List<SAR_RETY_FOFI>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_RETY_FOFI> param)
        {
            try
            {
                ApiResultObject<SAR_RETY_FOFI> result = new ApiResultObject<SAR_RETY_FOFI>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarRetyFofiManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_RETY_FOFI resultData = managerContainer.Run<SAR_RETY_FOFI>();
                    result = PackResult<SAR_RETY_FOFI>(resultData);
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
        public ApiResult Update(ApiParam<SAR_RETY_FOFI> param)
        {
            try
            {
                ApiResultObject<SAR_RETY_FOFI> result = new ApiResultObject<SAR_RETY_FOFI>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarRetyFofiManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_RETY_FOFI resultData = managerContainer.Run<SAR_RETY_FOFI>();
                    result = PackResult<SAR_RETY_FOFI>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_RETY_FOFI> param)
        {
            try
            {
                ApiResultObject<SAR_RETY_FOFI> result = new ApiResultObject<SAR_RETY_FOFI>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarRetyFofiManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_RETY_FOFI resultData = managerContainer.Run<SAR_RETY_FOFI>();
                    result = PackResult<SAR_RETY_FOFI>(resultData);
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
        public ApiResult Delete(ApiParam<SAR_RETY_FOFI> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarRetyFofiManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<SAR_RETY_FOFI>> param)
        {
            try
            {
                ApiResultObject<List<SAR_RETY_FOFI>> result = new ApiResultObject<List<SAR_RETY_FOFI>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarRetyFofiManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_RETY_FOFI> resultData = managerContainer.Run<List<SAR_RETY_FOFI>>();
                    result = PackResult<List<SAR_RETY_FOFI>>(resultData);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<SAR_RETY_FOFI>> param)
        {
            try
            {
                ApiResultObject<List<SAR_RETY_FOFI>> result = new ApiResultObject<List<SAR_RETY_FOFI>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarRetyFofiManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_RETY_FOFI> resultData = managerContainer.Run<List<SAR_RETY_FOFI>>();
                    result = PackResult<List<SAR_RETY_FOFI>>(resultData);
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
