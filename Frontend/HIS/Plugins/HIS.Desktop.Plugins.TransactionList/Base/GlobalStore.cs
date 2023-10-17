using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList.Base
{
    class GlobalStore
    {
        internal static List<string> TypeFilters = new List<string>();

        internal void LoadTypeFilter()
        {
            try
            {
                TypeFilters = new List<string>();
                TypeFilters.Add(TOI_TAO);
                TypeFilters.Add(PHONG);
                TypeFilters.Add(TAT_CA);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal string TOI_TAO
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmTransactionList.ToiTao", Base.ResourceLangManager.LanguageFrmTransactionList, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
            }
        }
        internal string PHONG
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmTransactionList.Phong", Base.ResourceLangManager.LanguageFrmTransactionList, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
            }
        }
        
        internal string TAT_CA
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmTransactionList.TatCa", Base.ResourceLangManager.LanguageFrmTransactionList, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
            }
        }
       

        internal const short IS_TRUE = 1;
    }
}
