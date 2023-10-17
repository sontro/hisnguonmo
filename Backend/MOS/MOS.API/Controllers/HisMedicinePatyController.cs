using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicinePaty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMedicinePatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicinePatyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicinePatyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_PATY>> result = new ApiResultObject<List<HIS_MEDICINE_PATY>>(null);
                if (param != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetOfLast")]
        public ApiResult GetOfLast(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_PATY>> result = new ApiResultObject<List<HIS_MEDICINE_PATY>>(null);
                if (param != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
                    result = mng.GetOfLast(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicinePatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMedicinePatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICINE_PATY>> result = new ApiResultObject<List<V_HIS_MEDICINE_PATY>>(null);
                if (param != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEDICINE_PATY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_PATY> result = new ApiResultObject<HIS_MEDICINE_PATY>(null);
                if (param != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_MEDICINE_PATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_PATY>> result = new ApiResultObject<List<HIS_MEDICINE_PATY>>(null);
                if (param != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEDICINE_PATY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_PATY> result = new ApiResultObject<HIS_MEDICINE_PATY>(null);
                if (param != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MEDICINE_PATY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_MEDICINE_PATY> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_PATY> result = new ApiResultObject<HIS_MEDICINE_PATY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
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
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicinePatyFilterQuery>), "param")]
        [ActionName("GetZip")]
        public ApiResultZip GetZip(ApiParam<HisMedicinePatyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_PATY>> result = new ApiResultObject<List<HIS_MEDICINE_PATY>>(null);
                if (param != null)
                {
                    HisMedicinePatyManager mng = new HisMedicinePatyManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
                }
                return new ApiResultZip(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
