using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceRetyCat
{
    [KeyboardAction("FindShortcutReportTypeCat", "HIS.Desktop.Plugins.HisServiceRetyCat.UCServiceRetyCat", "FindShortcutReportTypeCat", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("FindShortcutService", "HIS.Desktop.Plugins.HisServiceRetyCat.UCServiceRetyCat", "FindShortcutService", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("SaveShortcut", "HIS.Desktop.Plugins.HisServiceRetyCat.UCServiceRetyCat", "SaveShortcut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("ImportShortcut", "HIS.Desktop.Plugins.HisServiceRetyCat.UCServiceRetyCat", "ImportShortcut", KeyStroke = XKeys.Control | XKeys.I)]
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
