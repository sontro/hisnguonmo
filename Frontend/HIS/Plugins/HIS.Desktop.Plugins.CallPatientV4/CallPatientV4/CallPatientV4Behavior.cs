using Inventec.Core;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.CallPatientV4;
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

namespace HIS.Desktop.Plugins.CallPatientV4.CallPatientV4
{
    public sealed class CallPatientV4Behavior : Tool<IDesktopToolContext>, ICallPatientV4
    {
        object[] entity;
        public CallPatientV4Behavior()
            : base()
        {

        }

        public CallPatientV4Behavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICallPatientV4.Run()
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
