using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisWellPlate
{
    [KeyboardAction("bbtnFindx", "HIS.Desktop.Plugins.LisWellPlate.Run.UCLisWellPlate", "bbtnFindx", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnSavex", "HIS.Desktop.Plugins.LisWellPlate.Run.UCLisWellPlate", "bbtnSavex", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("bbtnNewx", "HIS.Desktop.Plugins.LisWellPlate.Run.UCLisWellPlate", "bbtnNewx", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("bbtnKTDMx", "HIS.Desktop.Plugins.LisWellPlate.Run.UCLisWellPlate", "bbtnKTDMx", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("bbtnKTGMx", "HIS.Desktop.Plugins.LisWellPlate.Run.UCLisWellPlate", "bbtnKTGMx", KeyStroke = XKeys.Control | XKeys.D)]
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
