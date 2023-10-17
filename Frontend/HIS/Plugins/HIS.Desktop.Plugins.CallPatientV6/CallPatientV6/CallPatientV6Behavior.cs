using Inventec.Core;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.CallPatientV6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.SDO;

namespace HIS.Desktop.Plugins.CallPatientV6.CallPatientV6
{
    public sealed class CallPatientV6Behavior : Tool<IDesktopToolContext>, ICallPatientV6
    {
        object[] entity;
        public CallPatientV6Behavior()
            : base()
        {

        }

        public CallPatientV6Behavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICallPatientV6.Run()
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
