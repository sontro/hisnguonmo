using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.InfantInformationList.InfantInformationList
{
    class InfantInformationListBehavior : Tool<IDesktopToolContext>, IInfantInformationList
    {
        object[] entity;
        internal InfantInformationListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IInfantInformationList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                        }
                    }
                }

                return new UCInfantInformationList(moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);  
                return null;
            }
        }
    }
}
