using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateLostExpMestCreate.AllocateLostExpMestCreate
{
    class AllocateLostExpMestCreateBehavior : Tool<IDesktopToolContext>, IAllocateLostExpMestCreate
    {
        object[] entity;

        internal AllocateLostExpMestCreateBehavior()
            : base()
        {

        }

        internal AllocateLostExpMestCreateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IAllocateLostExpMestCreate.Run()
        {
            object result = null;
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
                    return new UCAllocateLostExpMestCreate(moduleData.RoomTypeId, moduleData.RoomId);
                }
                else
                {
                    return null;
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
