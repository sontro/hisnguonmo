using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.FetusAbortion.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTYTFetusAbortion { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTYTFetusAbortion = new ResourceManager("TYT.Desktop.Plugins.FetusAbortion.Resources.Lang", typeof(TYT.Desktop.Plugins.FetusAbortion.frmTYTFetusAbortion).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
