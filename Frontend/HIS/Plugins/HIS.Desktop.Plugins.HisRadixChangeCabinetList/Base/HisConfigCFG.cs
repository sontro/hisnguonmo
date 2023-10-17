using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisRadixChangeCabinetList.Base
{
    class HisConfigCFG
    {
        /// <summary>
        /// Phải thanh toán mới cho phép thực xuất phiếu xuất bán
        /// </summary>
        private const string CONFIG_KEY__EXP_MEST_SALE__MUST_BILL = "MOS.EXP_MEST.EXPORT_SALE.MUST_BILL";
        private const string CONFIG_KEY__ALLOW_THER_LOGINNAME = "HIS.HIS_TRANSACTION.TRANSACTION_CANCEL.ALLOW_OTHER_LOGINNAME";

        internal static bool EXPORT_SALE__MUST_BILL;
        internal static bool CANCEL_ALLOW_OTHER_LOGINNAME;

        internal static void LoadConfig()
        {
            try
            {
                EXPORT_SALE__MUST_BILL = HisConfigs.Get<string>(CONFIG_KEY__EXP_MEST_SALE__MUST_BILL) == "1";
                CANCEL_ALLOW_OTHER_LOGINNAME = HisConfigs.Get<string>(CONFIG_KEY__ALLOW_THER_LOGINNAME) == "1";
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
