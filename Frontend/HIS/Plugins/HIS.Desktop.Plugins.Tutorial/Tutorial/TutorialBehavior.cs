using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Tutorial.Tutorial
{
    class TutorialBehavior : Tool<IDesktopToolContext>, ITutorial
    {
        object[] entity;
        internal TutorialBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITutorial.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                            break;
                        }
                    }
                }
                if (moduleData != null)
                {
                    return new FormTutorial(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
