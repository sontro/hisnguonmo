using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Debate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguagefrmDebate { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguagefrmDebate = new ResourceManager("HIS.Desktop.Plugins.Debate.Resources.Lang", typeof(HIS.Desktop.Plugins.Debate.frmDebate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
