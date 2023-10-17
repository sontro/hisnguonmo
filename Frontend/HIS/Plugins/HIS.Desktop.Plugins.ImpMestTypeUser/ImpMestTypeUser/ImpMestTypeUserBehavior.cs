using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestTypeUser.ImpMestTypeUser
{
    class ImpMestTypeUserBehavior : Tool<IDesktopToolContext>, IImpMestTypeUser
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        public ImpMestTypeUserBehavior()
            : base()
        {
        }

        public ImpMestTypeUserBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IImpMestTypeUser.Run()
        {
            try
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
                return new UCImpMestTypeUser(moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
