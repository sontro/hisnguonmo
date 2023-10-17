using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VitaminA
{
    [KeyboardAction("FindShortCut", "HIS.Desktop.Plugins.VitaminA.UCVitaminA", "FindShortCut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("RefeshShortcut", "HIS.Desktop.Plugins.VitaminA.UCVitaminA", "RefeshShortcut", KeyStroke = XKeys.Control | XKeys.R)]
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
