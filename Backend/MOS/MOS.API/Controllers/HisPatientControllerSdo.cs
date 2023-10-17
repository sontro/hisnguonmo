using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisPatient;
using MOS.SDO;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisPatientController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetSdoByCardServiceCode")]
        public ApiResult GetSdoByCardServiceCode(ApiParam<string> param)
        {
            ApiResultObject<HisCardPatientSDO> result = new ApiResultObject<HisCardPatientSDO>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetSdoByCardServiceCode(param.ApiData);
            }

            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientAdvanceFilter>), "param")]
        [ActionName("GetSdoAdvance")]
        public ApiResult GetSdoAdvance(ApiParam<HisPatientAdvanceFilter> param)
        {
            ApiResultObject<List<HisPatientSDO>> result = new ApiResultObject<List<HisPatientSDO>>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetSdoAdvance(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetCardBalance")]
        public ApiResult GetCardBalance(ApiParam<long> param)
        {
            ApiResultObject<decimal?> result = new ApiResultObject<decimal?>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetCardBalance(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<CardBalanceFilter>), "param")]
        [ActionName("GetCardBalanceBySpecified")]
        public ApiResult GetCardBalanceBySpecified(ApiParam<CardBalanceFilter> param)
        {
            ApiResultObject<decimal?> result = new ApiResultObject<decimal?>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetCardBalance(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetPreviousWarning")]
        public ApiResult GetPreviousWarning(ApiParam<long> param)
        {
            ApiResultObject<HisPatientWarningSDO> result = new ApiResultObject<HisPatientWarningSDO>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetPreviousWarning(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetPreviousPrescription")]
        public ApiResult GetPreviousPrescription(ApiParam<long> param)
        {
            ApiResultObject<List<HisPreviousPrescriptionSDO>> result = new ApiResultObject<List<HisPreviousPrescriptionSDO>>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetPreviousPrescription(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetPreviousPrescriptionDetail")]
        public ApiResult GetPreviousPrescriptionDetail(ApiParam<long> param)
        {
            ApiResultObject<List<HisPreviousPrescriptionDetailSDO>> result = new ApiResultObject<List<HisPreviousPrescriptionDetailSDO>>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetPreviousPrescriptionDetail(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisPatientUpdateSDO> param)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.UpdateSdo(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Recognition")]
        public ApiResult Recognition(ApiParam<RecognitionSDO> param)
        {
            ApiResultObject<RecognitionResultSDO> result = new ApiResultObject<RecognitionResultSDO>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.Recognition(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("UpdateCard")]
        public ApiResult UpdateCard(ApiParam<HisPatientUpdateCardSDO> param)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.UpdateCard(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }


        [HttpPost]
        [ActionName("UpdateClassify")]
        public ApiResult UpdateClassify(ApiParam<HisPatientUpdateClassifySDO> param)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.UpdateClassify(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientAdvanceFilter>), "param")]
        [ActionName("GetInformationForKiosk")]
        public ApiResult GetInformationForKiosk(ApiParam<HisPatientAdvanceFilter> param)
        {
            ApiResultObject<HisPatientForKioskSDO> result = new ApiResultObject<HisPatientForKioskSDO>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetInformationForKiosk(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

    }
}