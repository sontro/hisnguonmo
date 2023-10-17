using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConnectionTest
{
    [KeyboardAction("SEARCH", "HIS.Desktop.Plugins.ConnectionTest.UC_ConnectionTest", "SEARCH", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("PRINT", "HIS.Desktop.Plugins.ConnectionTest.UC_ConnectionTest", "PRINT", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("SAVE", "HIS.Desktop.Plugins.ConnectionTest.UC_ConnectionTest", "SAVE", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("FocusF1", "HIS.Desktop.Plugins.ConnectionTest.UC_ConnectionTest", "FocusF1", KeyStroke = XKeys.F1)]
    [KeyboardAction("FocusF2", "HIS.Desktop.Plugins.ConnectionTest.UC_ConnectionTest", "FocusF2", KeyStroke = XKeys.F2)]
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
