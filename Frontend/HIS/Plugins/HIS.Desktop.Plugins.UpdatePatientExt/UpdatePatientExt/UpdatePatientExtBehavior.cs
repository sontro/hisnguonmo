using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.UpdatePatientExt;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.UpdatePatientExt.UpdatePatientExt
{
    public sealed class UpdatePatientExtBehavior : Tool<IDesktopToolContext>, IUpdatePatientExt
    {
        object[] entity;
        long patientId;
        public UpdatePatientExtBehavior()
            : base()
        {
        }

        public UpdatePatientExtBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IUpdatePatientExt.Run()
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
                        if (entity[i] is long)
                        {
                            patientId = (long)entity[i];
                        }
                    }
                }
                if (moduleData != null)
                {
                    return new frmUpdatePatientExt(moduleData, patientId);
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
