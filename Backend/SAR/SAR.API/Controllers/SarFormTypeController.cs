using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarFormTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarFormType.Get.SarFormTypeFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarFormType.Get.SarFormTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_FORM_TYPE>> result = new ApiResultObject<List<SAR_FORM_TYPE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormTypeManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_FORM_TYPE> resultData = managerContainer.Run<List<SAR_FORM_TYPE>>();
                    result = PackResult<List<SAR_FORM_TYPE>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_FORM_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_TYPE> result = new ApiResultObject<SAR_FORM_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormTypeManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_TYPE resultData = managerContainer.Run<SAR_FORM_TYPE>();
                    result = PackResult<SAR_FORM_TYPE>(resultData);
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
        public ApiResult Update(ApiParam<SAR_FORM_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_TYPE> result = new ApiResultObject<SAR_FORM_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormTypeManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_TYPE resultData = managerContainer.Run<SAR_FORM_TYPE>();
                    result = PackResult<SAR_FORM_TYPE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_FORM_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_TYPE> result = new ApiResultObject<SAR_FORM_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormTypeManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_TYPE resultData = managerContainer.Run<SAR_FORM_TYPE>();
                    result = PackResult<SAR_FORM_TYPE>(resultData);
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
                    SAR_FORM_TYPE data = new SAR_FORM_TYPE();
                    data.ID = param.ApiData;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormTypeManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
