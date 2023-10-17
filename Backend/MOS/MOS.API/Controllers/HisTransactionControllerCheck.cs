using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTransactionController : BaseApiController
    {
        [HttpPost]
        [ActionName("CheckDeposit")]
        public ApiResult CheckDeposit(ApiParam<HisTransactionDepositSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CheckDeposit(param.ApiData);
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
        [ActionName("CheckRepay")]
        public ApiResult CheckRepay(ApiParam<HisTransactionRepaySDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CheckRepay(param.ApiData);
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
        [ActionName("CheckBill")]
        public ApiResult CheckBill(ApiParam<HisTransactionBillSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CheckBill(param.ApiData);
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
        [ActionName("CheckCancel")]
        public ApiResult CheckCancel(ApiParam<HisTransactionCancelSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CheckCancel(param.ApiData);
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
        [ActionName("CheckBillTwoBook")]
        public ApiResult CheckBillTwoBook(ApiParam<HisTransactionBillTwoBookSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CheckBillTwoBook(param.ApiData);
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
