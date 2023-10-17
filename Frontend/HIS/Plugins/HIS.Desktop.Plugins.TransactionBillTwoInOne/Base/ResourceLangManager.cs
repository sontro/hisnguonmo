using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionBillTwoInOne { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionBillTwoInOne = new ResourceManager("HIS.Desktop.Plugins.TransactionBillTwoInOne.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionBillTwoInOne.frmTransactionBillTwoInOne).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
