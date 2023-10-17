using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
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
        [ActionName("EpaymentDeposit")]
        public ApiResult EpaymentDeposit(ApiParam<EpaymentDepositSD> param)
        {
            try
            {
                ApiResultObject<EpaymentDepositResultSDO> result = new ApiResultObject<EpaymentDepositResultSDO>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.EpaymentDeposit(param.ApiData);
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
        [ActionName("EpaymentBill")]
        public ApiResult EpaymentBill(ApiParam<EpaymentBillSDO> param)
        {
            try
            {
                ApiResultObject<EpaymentBillResultSDO> result = new ApiResultObject<EpaymentBillResultSDO>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.EpaymentBill(param.ApiData);
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
