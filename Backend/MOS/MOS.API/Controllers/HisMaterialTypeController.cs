using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisMaterialType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMaterialTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMaterialTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMaterialTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MATERIAL_TYPE>> result = new ApiResultObject<List<HIS_MATERIAL_TYPE>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMaterialTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MATERIAL_TYPE>> result = new ApiResultObject<List<V_HIS_MATERIAL_TYPE>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialTypeStockViewFilter>), "param")]
        [ActionName("GetInStockMaterialType")]
        public ApiResult GetInStockMaterialType(ApiParam<HisMaterialTypeStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMaterialTypeInStockSDO>> result = new ApiResultObject<List<HisMaterialTypeInStockSDO>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
                    result = mng.GetInStockMaterialType(param.ApiData);
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
        [ActionName("GetMaterialTypeForEmr")]
        public ApiResult GetMaterialTypeForEmr(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<GetMaterialTypeForEmrResultSDO>> result = new ApiResultObject<List<GetMaterialTypeForEmrResultSDO>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
                    result = mng.GetMaterialTypeForEmr(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_MATERIAL_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_MATERIAL_TYPE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MATERIAL_TYPE>> result = new ApiResultObject<List<HIS_MATERIAL_TYPE>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MATERIAL_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MATERIAL_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_MATERIAL_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        public ApiResult Unlock(ApiParam<HIS_MATERIAL_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisMaterialTypeSDO> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMaterialTypeViewFilterQuery>), "param")]
        [ActionName("GetViewZip")]
        public ApiResultZip GetViewZip(ApiParam<HisMaterialTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MATERIAL_TYPE>> result = new ApiResultObject<List<V_HIS_MATERIAL_TYPE>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialTypeView1SDOFilter>), "param")]
        [ActionName("GetPriceLists")]
        public ApiResult GetPriceLists(ApiParam<HisMaterialTypeView1SDOFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMaterialTypeView1SDO>> result = new ApiResultObject<List<HisMaterialTypeView1SDO>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        public ApiResult CreateParent(ApiParam<HIS_MATERIAL_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMatyStockWithImpStockViewFilter>), "param")]
        [ActionName("GetInStockMaterialTypeWithImpStock")]
        public ApiResult GetInStockMaterialTypeWithImpStock(ApiParam<HisMatyStockWithImpStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMaterialTypeInStockSDO>> result = new ApiResultObject<List<HisMaterialTypeInStockSDO>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
                    result = mng.GetInStockMaterialTypeWithImpStock(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMatyStockWithBaseInfoViewFilter>), "param")]
        [ActionName("GetInStockMaterialTypeWithBaseInfo")]
        public ApiResult GetInStockMaterialTypeWithBaseInfo(ApiParam<HisMatyStockWithBaseInfoViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMaterialTypeInStockSDO>> result = new ApiResultObject<List<HisMaterialTypeInStockSDO>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
                    result = mng.GetInStockMaterialTypeWithBaseInfo(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialTypeHospitalViewFilter>), "param")]
        [ActionName("GetInHospitalMaterialType")]
        public ApiResult GetInHospitalMaterialType(ApiParam<HisMaterialTypeHospitalViewFilter> param)
        {
            try
            {
                ApiResultObject<MaterialTypeInHospitalSDO> result = new ApiResultObject<MaterialTypeInHospitalSDO>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
                    result = mng.GetInHospitalMaterialType(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialTypeViewFilterQuery>), "param")]
        [ActionName("GetViewDynamic")]
        public ApiResult GetViewDynamic(ApiParam<HisMaterialTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HisMaterialTypeViewDTO>> result = new ApiResultObject<List<HisMaterialTypeViewDTO>>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("UpdateMap")]
        public ApiResult UpdateMap(ApiParam<HisMaterialTypeUpdateMapSDO> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
                if (param != null)
                {
                    HisMaterialTypeManager mng = new HisMaterialTypeManager(param.CommonParam);
                    result = mng.UpdateMap(param.ApiData);
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
