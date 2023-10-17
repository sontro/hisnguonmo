using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisRoche;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Test
{
    partial class HisServiceReqTestUpdate : BusinessBase
    {
        public bool UpdateResult(string resultMessage)
        {
            if (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2)
            {
                return new LisRocheProcessor().UpdateResult(resultMessage, false);
            }
            return false;
        }
    }
}
