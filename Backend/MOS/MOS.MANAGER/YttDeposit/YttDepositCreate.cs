using ACS.EFMODEL.DataModels;
using ACS.Filter;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AOS.EFMODEL.DataModels;
using AOS.Filter;
using AOS.SDO;
using System.Configuration;
using YTT.SDO;

namespace MOS.MANAGER.YttDeposit
{
    class YttDepositCreate : BusinessBase
    {
        public const string SUCCESS = "00";

        internal YttDepositCreate()
            : base()
        {

        }

        internal YttDepositCreate(CommonParam paramGet)
            : base(paramGet)
        {


        }

        internal YttHisDepositResultSDO Create(long amount, string serviceCode, string theBranchCode)
        {
            try
            {
                string hashKey = ConfigurationManager.AppSettings["Inventec.The.HashKey"];

                YttHisDepositSDO input = new YttHisDepositSDO();
                input.Amount = amount;
                input.BranchCode = theBranchCode;
                input.IsCreateReqWhenBalanceIsInsufficient = false;
                input.ServiceCode = serviceCode;
                input.ClientTraceCode = Guid.NewGuid().ToString();

                string dataHash = String.Join("|", new List<string>() { input.Amount.ToString(), input.BranchCode, input.CardCode, input.IsCreateReqWhenBalanceIsInsufficient.ToString(), input.ServiceCode, input.ClientTraceCode });

                input.CheckSum = Inventec.Common.HashUtil.HashProcessor.HashSHA256(dataHash, hashKey);
                YttHisDepositResultSDO result = MOS.ApiConsumerManager.ApiConsumerStore.YttConsumer.Post<YttHisDepositResultSDO>(true, "/api/YttTransfer/HisDeposit", param, input);
                if (result == null || result.ResultCode != SUCCESS)
                {
                    string desc = result != null ? result.ResultDesc : "";
                    LogSystem.Warn("api/YttTransfer/HisDeposit that bai.");
                    LogSystem.Warn(LogUtil.TraceData("Input", input));
                    LogSystem.Warn(LogUtil.TraceData("Output", result));
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
