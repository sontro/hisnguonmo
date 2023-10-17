using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HoreHandover
{
    [KeyboardAction("FIND", "HIS.Desktop.Plugins.HoreHandover.UCHoreHandover", "FIND", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("REFRESH", "HIS.Desktop.Plugins.HoreHandover.UCHoreHandover", "REFRESH", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("SAVE", "HIS.Desktop.Plugins.HoreHandover.UCHoreHandover", "SAVE", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("PRINT", "HIS.Desktop.Plugins.HoreHandover.UCHoreHandover", "PRINT", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("NEW", "HIS.Desktop.Plugins.HoreHandover.UCHoreHandover", "NEW", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("FOCUS", "HIS.Desktop.Plugins.HoreHandover.UCHoreHandover", "FOCUS", KeyStroke = XKeys.F2)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<DesktopToolContext>
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
