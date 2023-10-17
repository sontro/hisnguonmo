using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestAggrExam.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpMestAggregate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpMestAggregate = new ResourceManager("HIS.Desktop.Plugins.ExpMestAggrExam.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpMestAggrExam.UCExpMestAggrExam).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
