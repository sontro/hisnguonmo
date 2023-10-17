using Inventec.Core;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.CallPatientVer5;
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

namespace HIS.Desktop.Plugins.CallPatientVer5.CallPatient
{
    public sealed class CallPatientBehavior : Tool<IDesktopToolContext>, ICallPatient
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module module;
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
                foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        this.module = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                return new frmChooseRoomForWaitingScreen(module);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
