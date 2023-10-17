using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSurgRemuDetail;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisSurgRemuDetailController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSurgRemuDetailFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSurgRemuDetailFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SURG_REMU_DETAIL>> result = new ApiResultObject<List<HIS_SURG_REMU_DETAIL>>(null);
                if (param != null)
                {
                    HisSurgRemuDetailManager mng = new HisSurgRemuDetailManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SURG_REMU_DETAIL> param)
        {
            try
            {
                ApiResultObject<HIS_SURG_REMU_DETAIL> result = new ApiResultObject<HIS_SURG_REMU_DETAIL>(null);
                if (param != null)
                {
                    HisSurgRemuDetailManager mng = new HisSurgRemuDetailManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SURG_REMU_DETAIL> param)
        {
            try
            {
                ApiResultObject<HIS_SURG_REMU_DETAIL> result = new ApiResultObject<HIS_SURG_REMU_DETAIL>(null);
                if (param != null)
                {
                    HisSurgRemuDetailManager mng = new HisSurgRemuDetailManager(param.CommonParam);
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
                    HisSurgRemuDetailManager mng = new HisSurgRemuDetailManager(param.CommonParam);
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
                ApiResultObject<HIS_SURG_REMU_DETAIL> result = new ApiResultObject<HIS_SURG_REMU_DETAIL>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSurgRemuDetailManager mng = new HisSurgRemuDetailManager(param.CommonParam);
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
            ApiResultObject<HIS_SURG_REMU_DETAIL> result = null;
            if (param != null && param.ApiData != null)
            {
                HisSurgRemuDetailManager mng = new HisSurgRemuDetailManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
