using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InterconnectionPrescription.InterconnectionPrescription
{
    class InterconnectionPrescriptionBehavior : Tool<IDesktopToolContext>, IInterconnectionPrescription
    {
        long entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;

        internal InterconnectionPrescriptionBehavior()
            : base()
        {

        }

        internal InterconnectionPrescriptionBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data)
            : base()
        {
            entity = data;
            moduleData = module;
        }

        object IInterconnectionPrescription.Run()
        {
            object result = null;
            try
            {
                result = new frmInterconnectionPrescription(moduleData, entity);
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
