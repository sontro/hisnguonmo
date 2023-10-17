using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.HisExportMestMedicine
{
    [KeyboardAction("Search", "HIS.Desktop.Plugins.HisExportMestMedicine.UCHisExportMestMedicine", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refreshs", "HIS.Desktop.Plugins.HisExportMestMedicine.UCHisExportMestMedicine", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("FocusExpCode", "HIS.Desktop.Plugins.HisExportMestMedicine.UCHisExportMestMedicine", KeyStroke = XKeys.F2)]
    [KeyboardAction("Export", "HIS.Desktop.Plugins.HisExportMestMedicine.UCHisExportMestMedicine", KeyStroke = XKeys.Control | XKeys.E)]

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
