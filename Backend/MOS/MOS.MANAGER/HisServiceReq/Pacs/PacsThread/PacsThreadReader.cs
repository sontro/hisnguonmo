using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsThread
{
    class PacsThreadReader
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

                CommonParam param = new CommonParam();
                IPacsReadProcessor processor = PacsFactory.GetReadProcessor(param);
                processor.ReadResult();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            IS_RUNNING = false;
        }
    }
}
