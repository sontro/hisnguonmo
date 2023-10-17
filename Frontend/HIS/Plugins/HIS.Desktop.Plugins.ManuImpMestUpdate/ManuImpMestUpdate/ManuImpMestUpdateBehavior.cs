using HIS.Desktop.Plugins.ManuImpMestUpdate.Run;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ManuImpMestUpdate.ManuImpMestUpdate
{
    class ManuImpMestUpdateBehavior : Tool<IDesktopToolContext>, IManuImpMestUpdate
    {
        object[] entity;

        internal ManuImpMestUpdateBehavior()
            : base()
        {

        }

        internal ManuImpMestUpdateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IManuImpMestUpdate.Run()
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
                    //return new frmManuImpMestUpdate(moduleData, impMestId);
                    return new frmImpMestUpdate(moduleData, impMestId);
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
