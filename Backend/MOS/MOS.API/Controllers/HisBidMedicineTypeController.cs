using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBidMedicineType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBidMedicineTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBidMedicineTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBidMedicineTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BID_MEDICINE_TYPE>> result = new ApiResultObject<List<HIS_BID_MEDICINE_TYPE>>(null);
                if (param != null)
                {
                    HisBidMedicineTypeManager mng = new HisBidMedicineTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisBidMedicineTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisBidMedicineTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BID_MEDICINE_TYPE>> result = new ApiResultObject<List<V_HIS_BID_MEDICINE_TYPE>>(null);
                if (param != null)
                {
                    HisBidMedicineTypeManager mng = new HisBidMedicineTypeManager(param.CommonParam);
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
        
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_BID_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_BID_MEDICINE_TYPE> result = new ApiResultObject<HIS_BID_MEDICINE_TYPE>(null);
                if (param != null)
                {
                    HisBidMedicineTypeManager mng = new HisBidMedicineTypeManager(param.CommonParam);
                    result = mng.Create(param.ApiData);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_BID_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_BID_MEDICINE_TYPE> result = new ApiResultObject<HIS_BID_MEDICINE_TYPE>(null);
                if (param != null)
                {
                    HisBidMedicineTypeManager mng = new HisBidMedicineTypeManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HIS_BID_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisBidMedicineTypeManager mng = new HisBidMedicineTypeManager(param.CommonParam);
                    result = mng.Delete(param.ApiData);
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
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<HIS_BID_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_BID_MEDICINE_TYPE> result = new ApiResultObject<HIS_BID_MEDICINE_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBidMedicineTypeManager mng = new HisBidMedicineTypeManager(param.CommonParam);
                    result = mng.ChangeLock(param.ApiData);
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
