using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTreatmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentLViewFilterQuery>), "param")]
        [ActionName("GetLView")]
        public ApiResult GetLView(ApiParam<HisTreatmentLViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<L_HIS_TREATMENT>> result = new ApiResultObject<List<L_HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetLView(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentLView1FilterQuery>), "param")]
        [ActionName("GetLView1")]
        public ApiResult GetLView1(ApiParam<HisTreatmentLView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<L_HIS_TREATMENT_1>> result = new ApiResultObject<List<L_HIS_TREATMENT_1>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetLView1(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }


        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentLView2FilterQuery>), "param")]
        [ActionName("GetLView2")]
        public ApiResult GetLView2(ApiParam<HisTreatmentLView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<L_HIS_TREATMENT_2>> result = new ApiResultObject<List<L_HIS_TREATMENT_2>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetLView2(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentLView3FilterQuery>), "param")]
        [ActionName("GetLView3")]
        public ApiResult GetLView3(ApiParam<HisTreatmentLView3FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<L_HIS_TREATMENT_3>> result = new ApiResultObject<List<L_HIS_TREATMENT_3>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetLView3(param.ApiData);
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
