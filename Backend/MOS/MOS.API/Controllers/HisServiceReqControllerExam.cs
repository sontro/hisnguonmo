using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceReqController : BaseApiController
    {
        [ActionName("ExamAddition")]
        public ApiResult ExamAddition(ApiParam<HisServiceReqExamAdditionSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_SERVICE_REQ> result = new ApiResultObject<V_HIS_SERVICE_REQ>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.ExamAddition(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [ActionName("ExamChange")]
        public ApiResult ExamChange(ApiParam<HisServiceReqExamChangeSDO> param)
        {
            try
            {
                ApiResultObject<HisServiceReqResultSDO> result = new ApiResultObject<HisServiceReqResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.ExamChange(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [ActionName("ExamRegister")]
        public ApiResult ExamRegister(ApiParam<HisServiceReqExamRegisterSDO> param)
        {
            try
            {
                ApiResultObject<HisServiceReqExamRegisterResultSDO> result = new ApiResultObject<HisServiceReqExamRegisterResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.ExamRegister(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [ActionName("ExamRegisterKiosk")]
        public ApiResult ExamRegisterKiosk(ApiParam<HisExamRegisterKioskSDO> param)
        {
            try
            {
                ApiResultObject<HisServiceReqExamRegisterResultSDO> result = new ApiResultObject<HisServiceReqExamRegisterResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.ExamRegisterKiosk(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [ActionName("ExamRegisterDkk")]
        public ApiResult ExamRegisterDkk(ApiParam<HisExamRegisterDkkSDO> param)
        {
            try
            {
                ApiResultObject<HisServiceReqExamRegisterResultSDO> result = new ApiResultObject<HisServiceReqExamRegisterResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.ExamRegisterDkk(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Phuc vu tich hop TW can tho
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ActionName("RegisterKiosk")]
        public ApiResult RegisterKiosk(ApiParam<HisRegisterKioskSDO> param)
        {
            try
            {
                ApiResultObject<HisServiceReqExamRegisterResultSDO> result = new ApiResultObject<HisServiceReqExamRegisterResultSDO>(null);
                HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                result = mng.ExamRegisterKiosk(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [ActionName("ExamUpdate")]
        public ApiResult ExamUpdate(ApiParam<HisServiceReqExamUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HisServiceReqExamUpdateResultSDO> result = new ApiResultObject<HisServiceReqExamUpdateResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.ExamUpdate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [ActionName("ChangeMain")]
        public ApiResult ChangeMain(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.ChangeMain(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
