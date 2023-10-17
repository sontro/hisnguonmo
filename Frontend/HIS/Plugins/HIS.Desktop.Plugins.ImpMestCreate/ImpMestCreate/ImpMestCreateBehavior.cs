using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.ImpMestCreate
{
    class ImpMestCreateBehavior : Tool<IDesktopToolContext>, IImpMestCreate
    {
        object[] entity;

        internal ImpMestCreateBehavior()
            : base()
        {

        }

        internal ImpMestCreateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IImpMestCreate.Run()
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
                    return new UCImpMestCreate(moduleData, moduleData.RoomTypeId, moduleData.RoomId);
                }
                else
                {
                    return new UCImpMestCreate(moduleData);
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
