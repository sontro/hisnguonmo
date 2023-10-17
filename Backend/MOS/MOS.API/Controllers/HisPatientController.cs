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
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisPatientController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPatientFilterQuery> param)
        {
            ApiResultObject<List<HIS_PATIENT>> result = new ApiResultObject<List<HIS_PATIENT>>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.Get(param.ApiData);
            }

            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisPatientViewFilterQuery> param)
        {
            ApiResultObject<List<V_HIS_PATIENT>> result = new ApiResultObject<List<V_HIS_PATIENT>>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetView(param.ApiData);
            }

            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisPatientView1FilterQuery> param)
        {
            ApiResultObject<List<V_HIS_PATIENT_1>> result = new ApiResultObject<List<V_HIS_PATIENT_1>>();
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.GetView1(param.ApiData);
            }

            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_PATIENT> param)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.Create(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult Create(ApiParam<List<HIS_PATIENT>> param)
        {
            ApiResultObject<List<HIS_PATIENT>> result = new ApiResultObject<List<HIS_PATIENT>>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.CreateList(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("RegisterProfile")]
        public ApiResult RegisterProfile(ApiParam<HisPatientProfileSDO> param)
        {
            ApiResultObject<HisPatientProfileSDO> result = new ApiResultObject<HisPatientProfileSDO>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.RegisterProfile(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_PATIENT> param)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.Update(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("UpdateImageByCard")]
        public ApiResult UpdateImageByCard(ApiParam<object> param)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            if (param!=null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.UpdateImageByCard();
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("RegisterVitaminA")]
        public ApiResult RegisterVitaminA(ApiParam<HisPatientVitaminASDO> param)
        {
            ApiResultObject<HisPatientVitaminASDO> result = new ApiResultObject<HisPatientVitaminASDO>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.RegisterVitaminA(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Follow")]
        public ApiResult Follow(ApiParam<HIS_PATIENT> param)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.Follow(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Unfollow")]
        public ApiResult Unfollow(ApiParam<HIS_PATIENT> param)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);
            if (param != null)
            {
                HisPatientManager mng = new HisPatientManager(param.CommonParam);
                result = mng.Unfollow(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}