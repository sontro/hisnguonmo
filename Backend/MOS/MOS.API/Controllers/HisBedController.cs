using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBed;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBedController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBedFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBedFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BED>> result = new ApiResultObject<List<HIS_BED>>(null);
                if (param != null)
                {
                    HisBedManager mng = new HisBedManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_BED> param)
        {
            try
            {
                ApiResultObject<HIS_BED> result = new ApiResultObject<HIS_BED>(null);
                if (param != null)
                {
                    HisBedManager mng = new HisBedManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_BED> param)
        {
            try
            {
                ApiResultObject<HIS_BED> result = new ApiResultObject<HIS_BED>(null);
                if (param != null)
                {
                    HisBedManager mng = new HisBedManager(param.CommonParam);
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
                    HisBedManager mng = new HisBedManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_BED> param)
        {
            try
            {
                ApiResultObject<HIS_BED> result = new ApiResultObject<HIS_BED>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBedManager mng = new HisBedManager(param.CommonParam);
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
            ApiResultObject<HIS_BED> result = null;
            if (param != null && param.ApiData != null)
            {
                HisBedManager mng = new HisBedManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }


        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_BED>> param)
        {
            try
            {
                ApiResultObject<List<HIS_BED>> result = new ApiResultObject<List<HIS_BED>>(null);
                if (param != null)
                {
                    HisBedManager mng = new HisBedManager(param.CommonParam);
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
        [ActionName("UpdateMap")]
        public ApiResult UpdateMap(ApiParam<List<HIS_BED>> param)
        {
            try
            {
                ApiResultObject<List<HIS_BED>> result = new ApiResultObject<List<HIS_BED>>(null);
                if (param != null)
                {
                    HisBedManager mng = new HisBedManager(param.CommonParam);
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
