using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedBsty.BedBsty
{
    class BedBstyBehavior : Tool<IDesktopToolContext>, IBedBsty
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal BedBstyBehavior()
            : base()
        {

        }

        internal BedBstyBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IBedBsty.Run()
        {
            object result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                result = new UC_BedBsty(moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
