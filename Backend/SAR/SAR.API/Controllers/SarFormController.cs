using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;
using SAR.SDO;

namespace SAR.API.Controllers
{
    public partial class SarFormController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarForm.Get.SarFormFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarForm.Get.SarFormFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_FORM>> result = new ApiResultObject<List<SAR_FORM>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_FORM> resultData = managerContainer.Run<List<SAR_FORM>>();
                    result = PackResult<List<SAR_FORM>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_FORM> param)
        {
            try
            {
                ApiResultObject<SAR_FORM> result = new ApiResultObject<SAR_FORM>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM resultData = managerContainer.Run<SAR_FORM>();
                    result = PackResult<SAR_FORM>(resultData);
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
        public ApiResult Update(ApiParam<SAR_FORM> param)
        {
            try
            {
                ApiResultObject<SAR_FORM> result = new ApiResultObject<SAR_FORM>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM resultData = managerContainer.Run<SAR_FORM>();
                    result = PackResult<SAR_FORM>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_FORM> param)
        {
            try
            {
                ApiResultObject<SAR_FORM> result = new ApiResultObject<SAR_FORM>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM resultData = managerContainer.Run<SAR_FORM>();
                    result = PackResult<SAR_FORM>(resultData);
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
                    SAR_FORM data = new SAR_FORM();
                    data.ID = param.ApiData;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
        [ActionName("CreateOrUpdate")]
        public ApiResult CreateOrUpdate(ApiParam<SarFormCreateOrUpdateSDO> param)
        {
            try
            {
                ApiResultObject<SarFormCreateOrUpdateSDO> result = new ApiResultObject<SarFormCreateOrUpdateSDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SarFormCreateOrUpdateSDO resultData = managerContainer.Run<SarFormCreateOrUpdateSDO>();
                    result = PackResult<SarFormCreateOrUpdateSDO>(resultData);
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
