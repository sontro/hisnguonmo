using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.PaylaterDeposit
{
    class RequestPaylaterProcessor : BusinessBase
    {
        public static string Run(HisTransReqDepositData data)
        {
            try
            {
                if (data.Treatment != null && data.Branch != null && data.SereServs != null && data.SereServs.Count > 0)
                {
                    decimal amount = data.SereServs.Sum(o => o.VIR_PATIENT_PRICE.Value);
                    YTT.SDO.YttHisDepositSDO sdo = new YTT.SDO.YttHisDepositSDO();
                    sdo.ServiceCode = data.ServiceCode;
                    sdo.BranchCode = data.Branch != null ? data.Branch.THE_BRANCH_CODE : "";
                    sdo.Amount = (long)amount;

                    CommonParam param = new CommonParam();
                    var apiResult = ApiConsumerManager.ApiConsumerStore.YttConsumer.Post<YTT.SDO.YttHisDepositResultSDO>(true, "api/YttTransfer/HisDepositPaylater", param, sdo);

                    if (apiResult != null && apiResult.IsCreateTransReqSuccess && apiResult.ReqTransCode != null)
                    {
                        return apiResult.ReqTransCode;
                    }
                    else
                    {
                        string desc = apiResult != null ? apiResult.ResultDesc : "";
                        LogSystem.Warn("api/YttTransfer/HisDepositPaylater that bai." + apiResult.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
            return null;
        }
    }
}
