using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockMatyList
{
    [KeyboardAction("Save", "HIS.Desktop.Plugins.MediStockMatyList.UCMediStockMatyList", "Save", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("ImportShortcut", "HIS.Desktop.Plugins.MediStockMatyList.UCMediStockMatyList", "ImportShortcut", KeyStroke = XKeys.Control | XKeys.I)]
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
