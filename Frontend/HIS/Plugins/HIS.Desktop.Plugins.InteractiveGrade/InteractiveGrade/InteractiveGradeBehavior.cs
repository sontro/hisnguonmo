using HIS.Desktop.Common;
using HIS.Desktop.Plugins.InteractiveGrade.Run;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InteractiveGrade
{
    class InteractiveGradeBehavior : BusinessBase, IInteractiveGrade
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData;
        internal InteractiveGradeBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IInteractiveGrade.Run()
        {
            try
            {
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
                if (moduleData != null)
                {
                    return new frmInteractiveGrade(moduleData);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Error("moduleData is null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
