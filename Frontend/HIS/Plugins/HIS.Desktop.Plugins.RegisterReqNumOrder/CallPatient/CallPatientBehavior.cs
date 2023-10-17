using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;


namespace HIS.Desktop.Plugins.RegisterReqNumOrder
{
    public sealed class CallPatientBehavior : Tool<IDesktopToolContext>, ICallPatient
    {
        object[] entity;
        public CallPatientBehavior()
            : base()
        {

        }

        public CallPatientBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICallPatient.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;
                foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        module = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                return new frmChooseRoomForWaitingScreen(module);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
