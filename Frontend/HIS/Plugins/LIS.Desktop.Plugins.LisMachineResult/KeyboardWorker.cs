using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisMachineResult
{
    [KeyboardAction("FindShortCut", "LIS.Desktop.Plugins.LisMachineResult.LisMachineResult.UcLisMachineResult", "FindShortCut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("SaveShortCut", "LIS.Desktop.Plugins.LisMachineResult.LisMachineResult.UcLisMachineResult", "SaveShortCut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("EditShortCut", "LIS.Desktop.Plugins.LisMachineResult.LisMachineResult.UcLisMachineResult", "EditShortCut", KeyStroke = XKeys.Control | XKeys.D)]
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
