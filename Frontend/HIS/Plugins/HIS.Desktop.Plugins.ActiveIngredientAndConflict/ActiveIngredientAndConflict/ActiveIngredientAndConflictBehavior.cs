using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ActiveIngredientAndConflict
{
    class ActiveIngredientAndConflictBehavior : Tool<IDesktopToolContext>, IActiveIngredientAndConflict
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal ActiveIngredientAndConflictBehavior()
            : base()
        {

        }

        internal ActiveIngredientAndConflictBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IActiveIngredientAndConflict.Run()
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

                result = new UC_ActiveIngredientAndConflict(moduleData);
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
