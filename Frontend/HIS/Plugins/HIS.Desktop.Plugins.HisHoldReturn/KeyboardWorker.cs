using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisHoldReturn
{
    [KeyboardAction("SaveHoldReturnShortcut", "HIS.Desktop.Plugins.HisHoldReturn.HoldReturn.UCHoldReturn", "SaveHoldReturnShortcut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("SearchHoldReturnShortcut", "HIS.Desktop.Plugins.HisHoldReturn.HoldReturn.UCHoldReturn", "SearchHoldReturnShortcut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("NewHoldReturnShortcut", "HIS.Desktop.Plugins.HisHoldReturn.HoldReturn.UCHoldReturn", "NewHoldReturnShortcut", KeyStroke = XKeys.Control | XKeys.N)]   
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
