using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.DepaExpMestList.DepaExpMestList
{
    class DepaExpMestListBehavior : Tool<IDesktopToolContext>, IDepaExpMestList
    {
        object[] entity;

        internal DepaExpMestListBehavior()
            : base()
        {

        }

        internal DepaExpMestListBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IDepaExpMestList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
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
                if (moduleData != null)
                {
                    return new UCDepaExpMestList(moduleData.RoomId, moduleData.RoomTypeId);
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
