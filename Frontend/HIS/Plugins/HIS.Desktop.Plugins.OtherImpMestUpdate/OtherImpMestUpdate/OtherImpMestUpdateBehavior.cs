using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherImpMestUpdate.OtherImpMestUpdate
{
    class OtherImpMestUpdateBehavior : Tool<IDesktopToolContext>, IOtherImpMestUpdate
    {
        object[] entity;

        internal OtherImpMestUpdateBehavior()
            : base()
        {

        }

        internal OtherImpMestUpdateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IOtherImpMestUpdate.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long impMestId = 0;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is long)
                        {
                            impMestId = (long)entity[i];
                        }
                    }
                }
                if (moduleData != null && impMestId > 0)
                {
                    return new frmOtherImpMestUpdate(moduleData, impMestId);
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
