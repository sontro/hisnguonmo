using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class BiinCFG
    {
        private const string IS_SEND_BIIN = "MOS.IS_AUTO_SEND_TEST_RESULT_TO_BIIN";
        private static bool? isAutoSendTestResultToBiin;
        public static bool IS_AUTO_SEND_SEND_BIIN
        {
            get
            {
                if (!isAutoSendTestResultToBiin.HasValue)
                {
                    isAutoSendTestResultToBiin = ConfigUtil.GetIntConfig(IS_SEND_BIIN) == 1;
                }

                return isAutoSendTestResultToBiin.Value;
            }
        }

        public static void Reload()
        {
            isAutoSendTestResultToBiin = ConfigUtil.GetIntConfig(IS_SEND_BIIN) == 1;
        }
    }
}
