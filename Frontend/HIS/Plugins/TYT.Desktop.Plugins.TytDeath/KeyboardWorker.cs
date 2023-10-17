using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace TYT.Desktop.Plugins.TytDeath
{
    [KeyboardAction("Search", "TYT.Desktop.Plugins.TytDeath.UC_TytDeath", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refreshs", "TYT.Desktop.Plugins.TytDeath.UC_TytDeath", KeyStroke = XKeys.Control | XKeys.R)]

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
