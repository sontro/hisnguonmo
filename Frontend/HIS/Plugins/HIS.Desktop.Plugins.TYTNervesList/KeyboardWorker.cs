using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.NervesList
{
    [KeyboardAction("FindShortcut", "TYT.Desktop.Plugins.NervesList.UCTYTNervesList", "FindShortcut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("RefeshShortcut", "TYT.Desktop.Plugins.NervesList.UCTYTNervesList", "RefeshShortcut", KeyStroke = XKeys.Control | XKeys.R)]
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
