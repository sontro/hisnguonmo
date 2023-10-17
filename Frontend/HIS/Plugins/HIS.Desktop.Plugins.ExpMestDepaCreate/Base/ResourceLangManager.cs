using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestDepaCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpMestDepaCreate { get; set; }
        internal static ResourceManager LanguageFrmPrintByCondition { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpMestDepaCreate = new ResourceManager("HIS.Desktop.Plugins.ExpMestDepaCreate.Resources.Lang", typeof(UCExpMestDepaCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            try
            {
                LanguageFrmPrintByCondition = new ResourceManager("HIS.Desktop.Plugins.ExpMestDepaCreate.Resources.Lang", typeof(Print.frmPrintByCondition).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
