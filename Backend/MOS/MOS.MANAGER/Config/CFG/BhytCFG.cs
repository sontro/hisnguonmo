using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config.CFG
{
    class BhytCFG
    {
        private const string CHECK_HEIN_CARD_BHXH__ADDRESS_CFG = "HIS.CHECK_HEIN_CARD.BHXH__ADDRESS";

        private static string checkHeinCardBhxhAddress;
        public static string CHECK_HEIN_CARD_BHXH__ADDRESS
        {
            get
            {
                if (checkHeinCardBhxhAddress == null)
                {
                    checkHeinCardBhxhAddress = ConfigUtil.GetStrConfig(CHECK_HEIN_CARD_BHXH__ADDRESS_CFG);
                }

                return checkHeinCardBhxhAddress;
            }
        }

        public static void Reload()
        {
            checkHeinCardBhxhAddress = ConfigUtil.GetStrConfig(CHECK_HEIN_CARD_BHXH__ADDRESS_CFG);
        }
    }
}
