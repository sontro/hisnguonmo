using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.Nerves
{
    [KeyboardAction("SaveShortCut", "TYT.Desktop.Plugins.Nerves.TYTNervesControl", "SaveShortCut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("FinishShortCut", "TYT.Desktop.Plugins.Nerves.TYTNervesControl", "FinishShortCut", KeyStroke = XKeys.Control | XKeys.E)]
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
