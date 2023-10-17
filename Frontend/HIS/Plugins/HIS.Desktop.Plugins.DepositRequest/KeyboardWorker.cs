using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DepositRequest
{
    [KeyboardAction("Save", "HIS.Desktop.Plugins.DepositRequest.UCDepositRequest", "Save", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("Search", "HIS.Desktop.Plugins.DepositRequest.UCDepositRequest", "Search", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Print", "HIS.Desktop.Plugins.DepositRequest.UCDepositRequest", "Print", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("SavePrint", "HIS.Desktop.Plugins.DepositRequest.UCDepositRequest", "SavePrint", KeyStroke = XKeys.Control | XKeys.I)]
    [KeyboardAction("HotkeyF2", "HIS.Desktop.Plugins.DepositRequest.UCDepositRequest", "HotkeyF2", KeyStroke = XKeys.F2)]
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
