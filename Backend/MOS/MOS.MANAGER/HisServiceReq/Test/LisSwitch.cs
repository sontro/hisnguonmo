using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread;
using MOS.MANAGER.HisServiceReq.Test.LisSenderV1;
using MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test
{
    public class LisSwitch
    {
        public static void Sender()
        {
            try
            {
                if (Lis2CFG.IS_SEND_REQUEST_LABCONN && Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2 && Lis2CFG.LIS_INTEGRATION_TYPE == Lis2CFG.LisIntegrationType.LABCONN)
                {
                    LogSystem.Info("Batdau SenttoLabconn");
                    LisThreadToLabconnSender.Run();
                }
                else if (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2)
                {
                    LisThreadSender.Run();
                }
                else
                {
                    LisBatchSender.Send();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static void Reader()
        {
            try
            {
                if (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2)
                {
                    LisThreadReader.Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
