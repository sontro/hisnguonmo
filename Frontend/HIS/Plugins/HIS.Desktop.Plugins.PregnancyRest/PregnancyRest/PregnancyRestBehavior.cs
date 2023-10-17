using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PregnancyRest.PregnancyRest
{
    class PregnancyRestBehavior : Tool<IDesktopToolContext>, IPregnancyRest
    {
        object[] entity;

        internal PregnancyRestBehavior()
            : base()
        {

        }

        internal PregnancyRestBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IPregnancyRest.Run()
        {
            long treatmentId = 0;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is long)
                            treatmentId = (long)item;
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new FormPregnancyRest(treatmentId, moduleData);
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
