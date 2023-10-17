using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarPrintTypeCfgController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarPrintTypeCfg.Get.SarPrintTypeCfgFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarPrintTypeCfg.Get.SarPrintTypeCfgFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_PRINT_TYPE_CFG>> result = new ApiResultObject<List<SAR_PRINT_TYPE_CFG>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_PRINT_TYPE_CFG> resultData = managerContainer.Run<List<SAR_PRINT_TYPE_CFG>>();
                    result = PackResult<List<SAR_PRINT_TYPE_CFG>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_PRINT_TYPE_CFG> param)
        {
            try
            {
                ApiResultObject<SAR_PRINT_TYPE_CFG> result = new ApiResultObject<SAR_PRINT_TYPE_CFG>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_PRINT_TYPE_CFG resultData = managerContainer.Run<SAR_PRINT_TYPE_CFG>();
                    result = PackResult<SAR_PRINT_TYPE_CFG>(resultData);
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
        public ApiResult Update(ApiParam<SAR_PRINT_TYPE_CFG> param)
        {
            try
            {
                ApiResultObject<SAR_PRINT_TYPE_CFG> result = new ApiResultObject<SAR_PRINT_TYPE_CFG>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_PRINT_TYPE_CFG resultData = managerContainer.Run<SAR_PRINT_TYPE_CFG>();
                    result = PackResult<SAR_PRINT_TYPE_CFG>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_PRINT_TYPE_CFG> param)
        {
            try
            {
                ApiResultObject<SAR_PRINT_TYPE_CFG> result = new ApiResultObject<SAR_PRINT_TYPE_CFG>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_PRINT_TYPE_CFG resultData = managerContainer.Run<SAR_PRINT_TYPE_CFG>();
                    result = PackResult<SAR_PRINT_TYPE_CFG>(resultData);
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
                    SAR_PRINT_TYPE_CFG data = new SAR_PRINT_TYPE_CFG();
                    data.ID = param.ApiData;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
                    List<SAR_PRINT_TYPE_CFG> listData = new List<SAR_PRINT_TYPE_CFG>();
                    if (param.ApiData != null && param.ApiData.Count > 0)
                    {
                        foreach (var item in param.ApiData)
                        {
                            SAR_PRINT_TYPE_CFG data = new SAR_PRINT_TYPE_CFG();
                            data.ID = item;
                            listData.Add(data);
                        }
                    }
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "Delete", new object[] { param.CommonParam }, new object[] { listData });
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
