using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarFormFieldController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarFormField.Get.SarFormFieldFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarFormField.Get.SarFormFieldFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_FORM_FIELD>> result = new ApiResultObject<List<SAR_FORM_FIELD>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormFieldManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_FORM_FIELD> resultData = managerContainer.Run<List<SAR_FORM_FIELD>>();
                    result = PackResult<List<SAR_FORM_FIELD>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_FORM_FIELD> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_FIELD> result = new ApiResultObject<SAR_FORM_FIELD>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormFieldManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_FIELD resultData = managerContainer.Run<SAR_FORM_FIELD>();
                    result = PackResult<SAR_FORM_FIELD>(resultData);
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
        public ApiResult Update(ApiParam<SAR_FORM_FIELD> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_FIELD> result = new ApiResultObject<SAR_FORM_FIELD>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormFieldManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_FIELD resultData = managerContainer.Run<SAR_FORM_FIELD>();
                    result = PackResult<SAR_FORM_FIELD>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_FORM_FIELD> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_FIELD> result = new ApiResultObject<SAR_FORM_FIELD>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormFieldManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_FIELD resultData = managerContainer.Run<SAR_FORM_FIELD>();
                    result = PackResult<SAR_FORM_FIELD>(resultData);
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
        public ApiResult Delete(ApiParam<SAR_FORM_FIELD> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormFieldManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
