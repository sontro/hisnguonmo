using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod
{
    [KeyboardAction("TaiLai", "HIS.Desktop.Plugins.MediStockPeriod.UCMediStockPeriod", "TaiLai", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Find", "HIS.Desktop.Plugins.MediStockPeriod.UCMediStockPeriod", "Find", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.MediStockPeriod.UCMediStockPeriod", "Save", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("InAn", "HIS.Desktop.Plugins.MediStockPeriod.UCMediStockPeriod", "InAn", KeyStroke = XKeys.Control | XKeys.P)]
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
