using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.InfantInformationList
{
    [KeyboardAction("Refesh", "HIS.Desktop.Plugins.InfantInformationList.UCInfantInformationList", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Search", "HIS.Desktop.Plugins.InfantInformationList.UCInfantInformationList", KeyStroke = XKeys.Control | XKeys.F)]
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
