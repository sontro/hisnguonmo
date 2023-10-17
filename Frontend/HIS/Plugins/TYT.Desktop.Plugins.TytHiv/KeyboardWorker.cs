using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace TYT.Desktop.Plugins.TytHiv
{
    [KeyboardAction("Search", "TYT.Desktop.Plugins.TytHiv.UC_TytHiv", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refreshs", "TYT.Desktop.Plugins.TytHiv.UC_TytHiv", KeyStroke = XKeys.Control | XKeys.R)]

    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    class KeyboardWorker:Tool<IDesktopToolContext>
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
