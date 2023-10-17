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

namespace MOS.MANAGER.AosAccount
{
    class AosAccountGet : BusinessBase
    {
        internal AosAccountGet()
            : base()
        {

        }

        internal AosAccountGet(CommonParam paramGet)
            : base(paramGet)
        {


        }

        internal AosGetBalanceInfoSDO GetBalanceInfo(string serviceCode, string theBranchCode)
        {
            try
            {
                string hashKey = ConfigurationManager.AppSettings["Inventec.The.HashKey"];

                AosGetBalanceInfoSDO input = new AosGetBalanceInfoSDO();
                input.BranchCode = theBranchCode;
                input.ClientRequestId = Guid.NewGuid().ToString();
                input.ServiceCode = serviceCode;

                string dataHash = String.Join("|", new List<string>() { input.AccountCode, input.BranchCode, input.CardCode, input.ServiceCode, input.ClientRequestId });

                input.CheckSumData = Inventec.Common.HashUtil.HashProcessor.HashSHA256(dataHash, hashKey);
                AosGetBalanceInfoSDO result = MOS.ApiConsumerManager.ApiConsumerStore.AosConsumer.Get<AosGetBalanceInfoSDO>(true, "/api/AosAccount/GetBalanceInfo", param, input);
                LogSystem.Info("/api/AosAccount/GetBalanceInfo");
                LogSystem.Info(LogUtil.TraceData("Input", input));
                LogSystem.Info(LogUtil.TraceData("Output", result));

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
