using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineService;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMedicineServiceController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineServiceFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicineServiceFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_SERVICE>> result = new ApiResultObject<List<HIS_MEDICINE_SERVICE>>(null);
                if (param != null)
                {
                    HisMedicineServiceManager mng = new HisMedicineServiceManager(param.CommonParam);
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
        
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<List<HIS_MEDICINE_SERVICE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_SERVICE>> result = new ApiResultObject<List<HIS_MEDICINE_SERVICE>>(null);
                if (param != null)
                {
                    HisMedicineServiceManager mng = new HisMedicineServiceManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<List<HIS_MEDICINE_SERVICE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_SERVICE>> result = new ApiResultObject<List<HIS_MEDICINE_SERVICE>>(null);
                if (param != null)
                {
                    HisMedicineServiceManager mng = new HisMedicineServiceManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMedicineServiceManager mng = new HisMedicineServiceManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_SERVICE> result = new ApiResultObject<HIS_MEDICINE_SERVICE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMedicineServiceManager mng = new HisMedicineServiceManager(param.CommonParam);
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
		
		[HttpPost]
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_MEDICINE_SERVICE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMedicineServiceManager mng = new HisMedicineServiceManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CreateOrUpdate")]
        public ApiResult CreateOrUpdate(ApiParam<List<HIS_MEDICINE_SERVICE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_SERVICE>> result = new ApiResultObject<List<HIS_MEDICINE_SERVICE>>(null);
                if (param != null)
                {
                    HisMedicineServiceManager mng = new HisMedicineServiceManager(param.CommonParam);
                    result = mng.CreateOrUpdate(param.ApiData);
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
