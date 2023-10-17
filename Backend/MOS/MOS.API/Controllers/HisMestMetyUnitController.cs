using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestMetyUnit;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMestMetyUnitController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestMetyUnitFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestMetyUnitFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_METY_UNIT>> result = new ApiResultObject<List<HIS_MEST_METY_UNIT>>(null);
                if (param != null)
                {
                    HisMestMetyUnitManager mng = new HisMestMetyUnitManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMestMetyUnitViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMestMetyUnitViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_METY_UNIT>> result = new ApiResultObject<List<V_HIS_MEST_METY_UNIT>>(null);
                if (param != null)
                {
                    HisMestMetyUnitManager mng = new HisMestMetyUnitManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEST_METY_UNIT> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_METY_UNIT> result = new ApiResultObject<HIS_MEST_METY_UNIT>(null);
                if (param != null)
                {
                    HisMestMetyUnitManager mng = new HisMestMetyUnitManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEST_METY_UNIT> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_METY_UNIT> result = new ApiResultObject<HIS_MEST_METY_UNIT>(null);
                if (param != null)
                {
                    HisMestMetyUnitManager mng = new HisMestMetyUnitManager(param.CommonParam);
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
                    HisMestMetyUnitManager mng = new HisMestMetyUnitManager(param.CommonParam);
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
                ApiResultObject<HIS_MEST_METY_UNIT> result = new ApiResultObject<HIS_MEST_METY_UNIT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMestMetyUnitManager mng = new HisMestMetyUnitManager(param.CommonParam);
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
            ApiResultObject<HIS_MEST_METY_UNIT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMestMetyUnitManager mng = new HisMestMetyUnitManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
