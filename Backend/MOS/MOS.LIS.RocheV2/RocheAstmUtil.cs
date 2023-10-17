using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    public class RocheAstmUtil
    {
        internal static string NVL(string input)
        {
            return input != null ? input : "";
        }

        public static RocheAstmBaseMessage DetechReceivingMessage(string messageStr, string sendingAppCode, string receivingAppCode)
        {
            RocheAstmBaseMessage message = null;
            try
            {
                message = new RocheAstmSampleSeenMessage(sendingAppCode, receivingAppCode, messageStr);
            }
            catch (Exception ex)
            {
            }

            if (message == null)
            {
                try
                {
                    message = new RocheAstmResultMessage(sendingAppCode, receivingAppCode, messageStr);
                }
                catch (Exception ex)
                {
                }
            }

            return message;
        }
    }
}
