using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHoreHoha;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisHoreHohaController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHoreHohaFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisHoreHohaFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_HORE_HOHA>> result = new ApiResultObject<List<HIS_HORE_HOHA>>(null);
                if (param != null)
                {
                    HisHoreHohaManager mng = new HisHoreHohaManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_HORE_HOHA> param)
        {
            try
            {
                ApiResultObject<HIS_HORE_HOHA> result = new ApiResultObject<HIS_HORE_HOHA>(null);
                if (param != null)
                {
                    HisHoreHohaManager mng = new HisHoreHohaManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_HORE_HOHA> param)
        {
            try
            {
                ApiResultObject<HIS_HORE_HOHA> result = new ApiResultObject<HIS_HORE_HOHA>(null);
                if (param != null)
                {
                    HisHoreHohaManager mng = new HisHoreHohaManager(param.CommonParam);
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
                    HisHoreHohaManager mng = new HisHoreHohaManager(param.CommonParam);
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
                ApiResultObject<HIS_HORE_HOHA> result = new ApiResultObject<HIS_HORE_HOHA>(null);
                if (param != null && param.ApiData != null)
                {
                    HisHoreHohaManager mng = new HisHoreHohaManager(param.CommonParam);
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
            ApiResultObject<HIS_HORE_HOHA> result = null;
            if (param != null && param.ApiData != null)
            {
                HisHoreHohaManager mng = new HisHoreHohaManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
