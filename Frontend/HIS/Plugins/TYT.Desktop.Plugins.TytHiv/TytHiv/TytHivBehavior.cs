using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;

namespace TYT.Desktop.Plugins.TytHiv.TytHiv
{
    class TytHivBehavior : Tool<IDesktopToolContext>, ITytHiv
    {
        object[] entity;
        internal TytHivBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITytHiv.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_PATIENT patient = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (entity[i] is V_HIS_PATIENT)
                        {
                            patient = (V_HIS_PATIENT)entity[i];
                        }
                    }
                }
                if (moduleData != null && patient == null)
                {
                    return new UC_TytHiv(moduleData);
                }
                else if (moduleData != null && patient != null)
                {
                    return new UC_TytHiv(moduleData, patient);
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
