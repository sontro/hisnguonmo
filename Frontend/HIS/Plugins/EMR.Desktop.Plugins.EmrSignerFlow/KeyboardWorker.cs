using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrSignerFlow
{
    [KeyboardAction("FindShortcut1", "EMR.Desktop.Plugins.EmrSignerFlow.UC_EmrSignerFlow", "FindShortcut1", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("FindShortcut2", "EMR.Desktop.Plugins.EmrSignerFlow.UC_EmrSignerFlow", "FindShortcut2", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("SaveShortcut", "EMR.Desktop.Plugins.EmrSignerFlow.UC_EmrSignerFlow", "SaveShortcut", KeyStroke = XKeys.Control | XKeys.S)]
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
