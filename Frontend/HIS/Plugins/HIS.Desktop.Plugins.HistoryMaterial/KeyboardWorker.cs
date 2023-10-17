using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HistoryMaterial
{
    [KeyboardAction("Search", "HIS.Desktop.Plugins.HistoryMaterial.HistoryMaterial.UC_HistoryMaterial", "Search", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Export", "HIS.Desktop.Plugins.HistoryMaterial.HistoryMaterial.UC_HistoryMaterial", "Export", KeyStroke = XKeys.Control | XKeys.O)]
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
