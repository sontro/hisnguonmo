using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBedRoomList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmHisBedRoomList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmHisBedRoomList = new ResourceManager("HIS.Desktop.Plugins.HisBedRoomList.Resources.Lang", typeof(HIS.Desktop.Plugins.HisBedRoomList.frmHisBedRoomList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
