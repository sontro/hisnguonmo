using Inventec.Core;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisInventec;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisRoche;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisLabconn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor
{
    class LisFactory
    {
        public static ILisProcessor GetProcessor(CommonParam param)
        {
            if (Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.INVENTEC)
            {
                return new LisInventecProcessor(param);
            }
            else if (Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.ROCHE)
            {
                return new LisRocheProcessor(param);
            }
            else if (Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.LABCONN)
            {
                return new LisLabconnProcessor(param);
            }
            return null;
        }
    }
}
