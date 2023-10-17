using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisService
{
    [KeyboardAction("BtnSearch", "HIS.Desktop.Plugins.HisService.UC_HisService", "BtnSearch", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.HisService.UC_HisService", "BtnAdd", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("BtnEdit", "HIS.Desktop.Plugins.HisService.UC_HisService", "BtnEdit", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnRefresh", "HIS.Desktop.Plugins.HisService.UC_HisService", "BtnRefresh", KeyStroke = XKeys.Control | XKeys.R)]
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
