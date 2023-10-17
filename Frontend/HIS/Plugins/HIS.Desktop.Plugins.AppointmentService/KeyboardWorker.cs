using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AppointmentService
{
    [KeyboardAction("SaveShortCut", "HIS.Desktop.Plugins.AppointmentService.AppointmentServiceControl", "SaveShortCut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("FinishShortCut", "HIS.Desktop.Plugins.AppointmentService.AppointmentServiceControl", "FinishShortCut", KeyStroke = XKeys.Control | XKeys.E)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<IDesktopToolContext>
    {
        public KeyboardWorker() : base() { }

        public override IActionSet Actions
        {
            get
            {
                return base.Actions;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
