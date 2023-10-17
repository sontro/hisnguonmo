using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportRevenue
{
    [KeyboardAction("BtnChoiceFile", "HIS.Desktop.Plugins.ImportRevenue.UCImportRevenue", "BtnChoiceFile", KeyStroke = XKeys.Control | XKeys.C)]
    [KeyboardAction("BtnImport", "HIS.Desktop.Plugins.ImportRevenue.UCImportRevenue", "BtnImport", KeyStroke = XKeys.Control | XKeys.I)]
    [KeyboardAction("BtnRefresh", "HIS.Desktop.Plugins.ImportRevenue.UCImportRevenue", "BtnRefresh", KeyStroke = XKeys.Control | XKeys.R)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<IDesktopToolContext>
    {
        public KeyboardWorker()
            : base()
        {

        }

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
