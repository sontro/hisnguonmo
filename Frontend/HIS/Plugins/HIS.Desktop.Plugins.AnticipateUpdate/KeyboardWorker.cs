using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.AnticipateUpdate
{
    [KeyboardAction("Add", "HIS.Desktop.Plugins.AnticipateUpdate.UCAnticipateUpdate", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.AnticipateUpdate.UCAnticipateUpdate", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("New", "HIS.Desktop.Plugins.AnticipateUpdate.UCAnticipateUpdate", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("FocusUseTime", "HIS.Desktop.Plugins.AnticipateUpdate.UCAnticipateUpdate", KeyStroke = XKeys.Control | XKeys.Digit1)]
    [KeyboardAction("Print", "HIS.Desktop.Plugins.AnticipateUpdate.UCAnticipateUpdate", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("Update", "HIS.Desktop.Plugins.AnticipateUpdate.UCAnticipateUpdate", KeyStroke = XKeys.Control | XKeys.U)]
    [KeyboardAction("Discard", "HIS.Desktop.Plugins.AnticipateUpdate.UCAnticipateUpdate", KeyStroke = XKeys.Control | XKeys.D)]
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
