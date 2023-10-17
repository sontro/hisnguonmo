using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ResourceMessage
{
    public class GetResource
    {
        static System.Resources.ResourceManager languageMessage = new System.Resources.ResourceManager("HIS.Desktop.Plugins.Library.ResourceMessage.Resources.Message.Lang", System.Reflection.Assembly.GetExecutingAssembly());

        public static string Get(string key)
        {
            try
            {
                return Inventec.Common.Resource.Get.Value(key, languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return "";
        }
    }
}
