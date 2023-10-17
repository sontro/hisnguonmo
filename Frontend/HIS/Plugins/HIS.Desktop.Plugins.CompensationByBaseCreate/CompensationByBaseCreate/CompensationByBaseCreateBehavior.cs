using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CompensationByBaseCreate.CompensationByBaseCreate
{
    class CompensationByBaseCreateBehavior : Tool<IDesktopToolContext>, ICompensationByBaseCreate
    {
        object[] entity;

        internal CompensationByBaseCreateBehavior()
            : base()
        {

        }

        internal CompensationByBaseCreateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ICompensationByBaseCreate.Run()
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
                    return new UCCompensationByBaseCreate(moduleData);
                }
                else
                    return null;
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
