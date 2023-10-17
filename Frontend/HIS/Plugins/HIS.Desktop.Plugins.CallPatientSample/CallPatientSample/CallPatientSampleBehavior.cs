using Inventec.Core;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.CallPatientSample;
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

namespace HIS.Desktop.Plugins.CallPatientSample.CallPatientSample
{
    public sealed class CallPatientSampleBehavior : Tool<IDesktopToolContext>, ICallPatientSample
    {
        object[] entity;
        public CallPatientSampleBehavior()
            : base()
        {

        }

        public CallPatientSampleBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICallPatientSample.Run()
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
