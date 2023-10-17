using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisExpMestMedicineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestMedicineFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExpMestMedicineFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST_MEDICINE>> result = new ApiResultObject<List<HIS_EXP_MEST_MEDICINE>>(null);
                if (param != null)
                {
                    HisExpMestMedicineManager mng = new HisExpMestMedicineManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestMedicineViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisExpMestMedicineViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE>>(null);
                if (param != null)
                {
                    HisExpMestMedicineManager mng = new HisExpMestMedicineManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewByTreatmentId")]
        public ApiResult GetViewByTreatmentId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE>>(null);
                if (param != null)
                {
                    HisExpMestMedicineManager mng = new HisExpMestMedicineManager(param.CommonParam);
                    result = mng.GetViewByTreatmentId(param.ApiData);
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
        [ActionName("UpdateCommonInfo")]
        public ApiResult UpdateCommonInfo(ApiParam<HIS_EXP_MEST_MEDICINE> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST_MEDICINE> result = new ApiResultObject<HIS_EXP_MEST_MEDICINE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestMedicineManager mng = new HisExpMestMedicineManager(param.CommonParam);
                    result = mng.UpdateCommonInfo(param.ApiData);
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
        [ActionName("Used")]
        public ApiResult Used(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST_MEDICINE> result = new ApiResultObject<HIS_EXP_MEST_MEDICINE>(null);
                if (param != null)
                {
                    HisExpMestMedicineManager mng = new HisExpMestMedicineManager(param.CommonParam);
                    result = mng.Used(param.ApiData);
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
        [ActionName("Unused")]
        public ApiResult Unused(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST_MEDICINE> result = new ApiResultObject<HIS_EXP_MEST_MEDICINE>(null);
                if (param != null)
                {
                    HisExpMestMedicineManager mng = new HisExpMestMedicineManager(param.CommonParam);
                    result = mng.Unused(param.ApiData);
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
