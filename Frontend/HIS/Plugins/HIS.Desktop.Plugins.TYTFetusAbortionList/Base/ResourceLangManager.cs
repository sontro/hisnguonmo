using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.FetusAbortionList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTYTFetusAbortionList { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTYTFetusAbortionList = new ResourceManager("TYT.Desktop.Plugins.FetusAbortionList.Resources.Lang", typeof(TYT.Desktop.Plugins.FetusAbortionList.UCTYTFetusAbortionList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
