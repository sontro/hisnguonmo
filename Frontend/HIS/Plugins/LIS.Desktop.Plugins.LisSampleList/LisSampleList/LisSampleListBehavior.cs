using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using LIS.Desktop.Plugins.LisSampleList.Run;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIS.Desktop.Plugins.LisSampleList.LisSampleList
{
    class LisSampleListBehavior : Tool<IDesktopToolContext>, ILisSampleList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        public LisSampleListBehavior()
            : base()
        {
        }

        public LisSampleListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ILisSampleList.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module && currentModule == null)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }

                    result = new UCLisSampleList(currentModule);
                }
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
