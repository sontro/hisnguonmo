using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceReqController : BaseApiController
    {
        [HttpPost]
        [ActionName("OutPatientPresCreateList")]
        public ApiResult OutPatientPresCreateList(ApiParam<List<OutPatientPresSDO>> param)
        {
            try
            {
                ApiResultObject<OutPatientPresResultSDO> result = new ApiResultObject<OutPatientPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.OutPatientPresCreate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("OutPatientPresCreate")]
        public ApiResult OutPatientPresCreate(ApiParam<OutPatientPresSDO> param)
        {
            try
            {
                ApiResultObject<OutPatientPresResultSDO> result = new ApiResultObject<OutPatientPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.OutPatientPresCreate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ExpPresCreateByConfig")]
        public ApiResult ExpPresCreateByConfig(ApiParam<ExpendPresSDO> param)
        {
            try
            {
                ApiResultObject<SubclinicalPresResultSDO> result = new ApiResultObject<SubclinicalPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.ExpPresCreateByConfig(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("OutPatientPresUpdate")]
        public ApiResult OutPatientPresUpdate(ApiParam<OutPatientPresSDO> param)
        {
            try
            {
                ApiResultObject<OutPatientPresResultSDO> result = new ApiResultObject<OutPatientPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.OutPatientPresUpdate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("InPatientPresCreate")]
        public ApiResult InPatientPresCreate(ApiParam<InPatientPresSDO> param)
        {
            try
            {
                ApiResultObject<InPatientPresResultSDO> result = new ApiResultObject<InPatientPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.InPatientPresCreate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("InPatientPresUpdate")]
        public ApiResult InPatientPresUpdate(ApiParam<InPatientPresSDO> param)
        {
            try
            {
                ApiResultObject<InPatientPresResultSDO> result = new ApiResultObject<InPatientPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.InPatientPresUpdate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("SubclinicalPresCreate")]
        public ApiResult SubclinicalPresCreate(ApiParam<SubclinicalPresSDO> param)
        {
            try
            {
                ApiResultObject<SubclinicalPresResultSDO> result = new ApiResultObject<SubclinicalPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.SubclinicalPresCreate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("SubclinicalPresUpdate")]
        public ApiResult SubclinicalPresUpdate(ApiParam<SubclinicalPresSDO> param)
        {
            try
            {
                ApiResultObject<SubclinicalPresResultSDO> result = new ApiResultObject<SubclinicalPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.SubclinicalPresUpdate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("BloodPresCreate")]
        public ApiResult BloodPresCreate(ApiParam<PatientBloodPresSDO> param)
        {
            try
            {
                ApiResultObject<List<PatientBloodPresResultSDO>> result = new ApiResultObject<List<PatientBloodPresResultSDO>>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.BloodPresCreate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("BloodPresUpdate")]
        public ApiResult BloodPresUpdate(ApiParam<PatientBloodPresSDO> param)
        {
            try
            {
                ApiResultObject<PatientBloodPresResultSDO> result = new ApiResultObject<PatientBloodPresResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.BloodPresUpdate(param.ApiData);
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
