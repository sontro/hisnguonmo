using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisMedicineType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMedicineTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicineTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_TYPE>> result = new ApiResultObject<List<HIS_MEDICINE_TYPE>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMedicineTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICINE_TYPE>> result = new ApiResultObject<List<V_HIS_MEDICINE_TYPE>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineTypeStockViewFilter>), "param")]
        [ActionName("GetInStockMedicineType")]
        public ApiResult GetInStockMedicineType(ApiParam<HisMedicineTypeStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMedicineTypeInStockSDO>> result = new ApiResultObject<List<HisMedicineTypeInStockSDO>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.GetInStockMedicineType(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_MEDICINE_TYPE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_TYPE>> result = new ApiResultObject<List<HIS_MEDICINE_TYPE>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
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
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisMedicineTypeSDO> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.UpdateSdo(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
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
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<HIS_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.Lock(param.ApiData);
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
        [ActionName("Unlock")]
        public ApiResult Unlock(ApiParam<HIS_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.Unlock(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineTypeViewFilterQuery>), "param")]
        [ActionName("GetViewZip")]
        public ApiResultZip GetViewZip(ApiParam<HisMedicineTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICINE_TYPE>> result = new ApiResultObject<List<V_HIS_MEDICINE_TYPE>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
                }
                return new ApiResultZip(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineTypeView1SDOFilter>), "param")]
        [ActionName("GetPriceLists")]
        public ApiResult GetPriceLists(ApiParam<HisMedicineTypeView1SDOFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMedicineTypeView1SDO>> result = new ApiResultObject<List<HisMedicineTypeView1SDO>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.GetPriceLists(param.ApiData);
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
        [ActionName("CreateParent")]
        public ApiResult CreateParent(ApiParam<HIS_MEDICINE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.CreateParent(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMetyStockWithImpStockViewFilter>), "param")]
        [ActionName("GetInStockMedicineTypeWithImpStock")]
        public ApiResult GetInStockMedicineTypeWithImpStock(ApiParam<HisMetyStockWithImpStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMedicineTypeInStockSDO>> result = new ApiResultObject<List<HisMedicineTypeInStockSDO>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.GetInStockMedicineTypeWithImpStock(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMetyStockWithBaseInfoViewFilter>), "param")]
        [ActionName("GetInStockMedicineTypeWithBaseInfo")]
        public ApiResult GetInStockMedicineTypeWithBaseInfo(ApiParam<HisMetyStockWithBaseInfoViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMedicineTypeInStockSDO>> result = new ApiResultObject<List<HisMedicineTypeInStockSDO>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.GetInStockMedicineTypeWithBaseInfo(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineTypeHospitalViewFilter>), "param")]
        [ActionName("GetInHospitalMedicineType")]
        public ApiResult GetInHospitalMedicineType(ApiParam<HisMedicineTypeHospitalViewFilter> param)
        {
            try
            {
                ApiResultObject<MedicineTypeInHospitalSDO> result = new ApiResultObject<MedicineTypeInHospitalSDO>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.GetInHospitalMedicineType(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineTypeViewFilterQuery>), "param")]
        [ActionName("GetViewDynamic")]
        public ApiResult GetViewDynamic(ApiParam<HisMedicineTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HisMedicineTypeViewDTO>> result = new ApiResultObject<List<HisMedicineTypeViewDTO>>(null);
                if (param != null)
                {
                    HisMedicineTypeManager mng = new HisMedicineTypeManager(param.CommonParam);
                    result = mng.GetViewDynamic(param.ApiData);
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
