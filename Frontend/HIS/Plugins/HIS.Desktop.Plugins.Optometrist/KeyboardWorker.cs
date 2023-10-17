using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Optometrist
{
    [KeyboardAction("OptometristSave", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "OptometristSave", KeyStroke = XKeys.Control | XKeys.S)]
    //[KeyboardAction("End", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "End", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("OptometristPrint", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "OptometristPrint", KeyStroke = XKeys.Control | XKeys.P)]
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
