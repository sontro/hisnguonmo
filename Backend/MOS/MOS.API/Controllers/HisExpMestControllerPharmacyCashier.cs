using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisExpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<DHisTransExpFilter>), "param")]
        [ActionName("PharmacyCashierGet")]
        public ApiResult PharmacyCashierGet(ApiParam<DHisTransExpFilter> param)
        {
            try
            {
                ApiResultObject<List<DHisTransExpSDO>> result = new ApiResultObject<List<DHisTransExpSDO>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.PharmacyCashierGet(param.ApiData);
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
        [ActionName("PharmacyCashierPay")]
        public ApiResult PharmacyCashierPay(ApiParam<PharmacyCashierSDO> param)
        {
            try
            {
                ApiResultObject<PharmacyCashierResultSDO> result = new ApiResultObject<PharmacyCashierResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.PharmacyCashierPay(param.ApiData);
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
        [ActionName("PharmacyCashierExpInvoice")]
        public ApiResult PharmacyCashierExpInvoice(ApiParam<PharmacyCashierExpInvoiceSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.PharmacyCashierExpInvoice(param.ApiData);
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
        [ActionName("PharmacyCashierExpCancel")]
        public ApiResult PharmacyCashierExpCancel(ApiParam<PharmacyCashierExpCancelSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.PharmacyCashierExpCancel(param.ApiData);
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
