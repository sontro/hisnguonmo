using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsThread
{
    public class PacsThreadSender
    {
        private static bool IS_RUNNING = false;

        public static void Run()
        {
            try
            {
                if (IS_RUNNING)
                {
                    LogSystem.Info("Tien PacsSancyThreadSender trinh dang duoc chay. Khong cho phep tao tien trinh moi");
                    return;
                }
                IS_RUNNING = true;

                List<PacsOrderData> data = GetDataProcessor.Run();
                List<string> sqls = new List<string>();
                if (data != null && data.Count > 0)
                {
                    CommonParam param = new CommonParam();
                    IPacsProcessor processor = PacsFactory.GetProcessor(param);
                    foreach (PacsOrderData order in data)
                    {
                        processor.SendOrder(order, ref sqls);
                    }

                    processor.UpdateStatus(data, sqls);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            IS_RUNNING = false;
        }
    }
}
