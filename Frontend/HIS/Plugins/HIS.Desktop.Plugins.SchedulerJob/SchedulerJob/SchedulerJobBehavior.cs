using HIS.Desktop.Controls.Session;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.SchedulerJob.SchedulerJob
{
    class SchedulerJobBehavior : Tool<IDesktopToolContext>, ISchedulerJob
    {
        object[] entity;

        internal SchedulerJobBehavior()
            : base()
        {

        }

        internal SchedulerJobBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ISchedulerJob.Run()
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
                    return new UCSchedulerJob(moduleData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        private void ReleaseBeforeClose(object uc)
        {
            try
            {
                MethodInfo methodInfo = (uc as UserControl).GetType().GetMethod("FromClosingEvent");
                methodInfo.Invoke(uc, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
