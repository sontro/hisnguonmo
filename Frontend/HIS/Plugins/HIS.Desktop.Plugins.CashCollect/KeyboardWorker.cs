using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CashCollect
{
    [KeyboardAction("FindShortcut", "HIS.Desktop.Plugins.CashCollect.UCCashCollect", "FindShortcut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("EditShortcut", "HIS.Desktop.Plugins.CashCollect.UCCashCollect", "EditShortcut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("AddShortcut", "HIS.Desktop.Plugins.CashCollect.UCCashCollect", "AddShortcut", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("CancelShortcut", "HIS.Desktop.Plugins.CashCollect.UCCashCollect", "CancelShortcut", KeyStroke = XKeys.Control | XKeys.C)]
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
