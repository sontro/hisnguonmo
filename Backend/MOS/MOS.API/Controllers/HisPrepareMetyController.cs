using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPrepareMety;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisPrepareMetyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPrepareMetyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPrepareMetyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PREPARE_METY>> result = new ApiResultObject<List<HIS_PREPARE_METY>>(null);
                if (param != null)
                {
                    HisPrepareMetyManager mng = new HisPrepareMetyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_PREPARE_METY> param)
        {
            try
            {
                ApiResultObject<HIS_PREPARE_METY> result = new ApiResultObject<HIS_PREPARE_METY>(null);
                if (param != null)
                {
                    HisPrepareMetyManager mng = new HisPrepareMetyManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_PREPARE_METY> param)
        {
            try
            {
                ApiResultObject<HIS_PREPARE_METY> result = new ApiResultObject<HIS_PREPARE_METY>(null);
                if (param != null)
                {
                    HisPrepareMetyManager mng = new HisPrepareMetyManager(param.CommonParam);
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
                    HisPrepareMetyManager mng = new HisPrepareMetyManager(param.CommonParam);
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
                ApiResultObject<HIS_PREPARE_METY> result = new ApiResultObject<HIS_PREPARE_METY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPrepareMetyManager mng = new HisPrepareMetyManager(param.CommonParam);
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
            ApiResultObject<HIS_PREPARE_METY> result = null;
            if (param != null && param.ApiData != null)
            {
                HisPrepareMetyManager mng = new HisPrepareMetyManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
