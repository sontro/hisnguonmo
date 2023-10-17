using Inventec.Core;
using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAR.API.Controllers
{
    public partial class SarPrintTypeCfgController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarPrintTypeCfg.Get.SarPrintTypeCfgViewFilterQuery>), "param")]
        [ActionName("GetView")]
        [AllowAnonymous]
        public ApiResult GetView(ApiParam<SAR.MANAGER.Core.SarPrintTypeCfg.Get.SarPrintTypeCfgViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SAR_PRINT_TYPE_CFG>> result = new ApiResultObject<List<V_SAR_PRINT_TYPE_CFG>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SAR_PRINT_TYPE_CFG> resultData = managerContainer.Run<List<V_SAR_PRINT_TYPE_CFG>>();
                    result = PackResult<List<V_SAR_PRINT_TYPE_CFG>>(resultData);
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
        public ApiResult CreateList(ApiParam<List<SAR_PRINT_TYPE_CFG>> param)
        {
            try
            {
                ApiResultObject<List<SAR_PRINT_TYPE_CFG>> result = new ApiResultObject<List<SAR_PRINT_TYPE_CFG>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<SAR_PRINT_TYPE_CFG>> param)
        {
            try
            {
                ApiResultObject<List<SAR_PRINT_TYPE_CFG>> result = new ApiResultObject<List<SAR_PRINT_TYPE_CFG>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
        [ActionName("ChangeLockByControl")]
        public ApiResult ChangeLockByControl(ApiParam<SAR.SDO.ChangeLockByControlSDO> param)
        {
            try
            {
                ApiResultObject<SAR.SDO.ChangeLockByControlSDO> result = new ApiResultObject<SAR.SDO.ChangeLockByControlSDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeCfgManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR.SDO.ChangeLockByControlSDO resultData = managerContainer.Run<SAR.SDO.ChangeLockByControlSDO>();
                    result = PackResult<SAR.SDO.ChangeLockByControlSDO>(resultData);
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