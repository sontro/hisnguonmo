using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.VitaminAList.VitaminAList
{
    class VitaminAListBehavior : Tool<IDesktopToolContext>, IVitaminAList
    {
        object[] entity;
        internal VitaminAListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IVitaminAList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long vitaminATypeId = 0;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (entity[i] is long)
                        {
                            vitaminATypeId = (long)entity[i];
                        }
                    }
                }
                if (moduleData != null)
                {
                    return new UCVitaminAList(moduleData);
                }
                else
                {
                    return null;
                }
                //return new UCVitaminAList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
