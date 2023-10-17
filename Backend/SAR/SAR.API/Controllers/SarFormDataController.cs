using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarFormDataController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarFormData.Get.SarFormDataFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarFormData.Get.SarFormDataFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_FORM_DATA>> result = new ApiResultObject<List<SAR_FORM_DATA>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormDataManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_FORM_DATA> resultData = managerContainer.Run<List<SAR_FORM_DATA>>();
                    result = PackResult<List<SAR_FORM_DATA>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_FORM_DATA> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_DATA> result = new ApiResultObject<SAR_FORM_DATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormDataManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_DATA resultData = managerContainer.Run<SAR_FORM_DATA>();
                    result = PackResult<SAR_FORM_DATA>(resultData);
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
        public ApiResult CreateList(ApiParam<List<SAR_FORM_DATA>> param)
        {
            try
            {
                ApiResultObject<List<SAR_FORM_DATA>> result = new ApiResultObject<List<SAR_FORM_DATA>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormDataManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_FORM_DATA> resultData = managerContainer.Run<List<SAR_FORM_DATA>>();
                    result = PackResult<List<SAR_FORM_DATA>>(resultData);
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
        public ApiResult Update(ApiParam<SAR_FORM_DATA> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_DATA> result = new ApiResultObject<SAR_FORM_DATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormDataManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_DATA resultData = managerContainer.Run<SAR_FORM_DATA>();
                    result = PackResult<SAR_FORM_DATA>(resultData);
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
        public ApiResult UpdateList(ApiParam<List<SAR_FORM_DATA>> param)
        {
            try
            {
                ApiResultObject<List<SAR_FORM_DATA>> result = new ApiResultObject<List<SAR_FORM_DATA>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormDataManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_FORM_DATA> resultData = managerContainer.Run<List<SAR_FORM_DATA>>();
                    result = PackResult<List<SAR_FORM_DATA>>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_FORM_DATA> param)
        {
            try
            {
                ApiResultObject<SAR_FORM_DATA> result = new ApiResultObject<SAR_FORM_DATA>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormDataManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_FORM_DATA resultData = managerContainer.Run<SAR_FORM_DATA>();
                    result = PackResult<SAR_FORM_DATA>(resultData);
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
                    SAR_FORM_DATA data = new SAR_FORM_DATA();
                    data.ID = param.ApiData;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormDataManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    List<SAR_FORM_DATA> listData = new List<SAR_FORM_DATA>();
                    if (param.ApiData != null && param.ApiData.Count > 0)
                    {
                        foreach (var item in param.ApiData)
                        {
                            SAR_FORM_DATA data = new SAR_FORM_DATA();
                            data.ID = item;
                            listData.Add(data);
                        }
                    }
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormDataManager), "Delete", new object[] { param.CommonParam }, new object[] { listData });
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
