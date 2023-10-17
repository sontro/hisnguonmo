using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPrepare;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisPrepareController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPrepareFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPrepareFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PREPARE>> result = new ApiResultObject<List<HIS_PREPARE>>(null);
                if (param != null)
                {
                    HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisPrepareSDO> param)
        {
            try
            {
                ApiResultObject<HisPrepareResultSDO> result = new ApiResultObject<HisPrepareResultSDO>(null);
                if (param != null)
                {
                    HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisPrepareSDO> param)
        {
            try
            {
                ApiResultObject<HisPrepareResultSDO> result = new ApiResultObject<HisPrepareResultSDO>(null);
                if (param != null)
                {
                    HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
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
        [ActionName("Approve")]
        public ApiResult Approve(ApiParam<HisPrepareApproveSDO> param)
        {
            try
            {
                ApiResultObject<HisPrepareResultSDO> result = new ApiResultObject<HisPrepareResultSDO>(null);
                if (param != null)
                {
                    HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
                    result = mng.Approve(param.ApiData);
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
        [ActionName("ApproveList")]
        public ApiResult ApproveList(ApiParam<HisPrepareApproveListSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_PREPARE>> result = new ApiResultObject<List<HIS_PREPARE>>(null);
                if (param != null)
                {
                    HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
                    result = mng.ApproveList(param.ApiData);
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
        [ActionName("Unapprove")]
        public ApiResult Unapprove(ApiParam<HisPrepareSDO> param)
        {
            try
            {
                ApiResultObject<HIS_PREPARE> result = new ApiResultObject<HIS_PREPARE>(null);
                if (param != null)
                {
                    HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
                    result = mng.Unapprove(param.ApiData);
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
                    HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
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
                ApiResultObject<HIS_PREPARE> result = new ApiResultObject<HIS_PREPARE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
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
            ApiResultObject<HIS_PREPARE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisPrepareManager mng = new HisPrepareManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
