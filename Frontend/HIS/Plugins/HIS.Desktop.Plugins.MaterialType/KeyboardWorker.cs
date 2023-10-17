using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialType
{
    [KeyboardAction("New", "HIS.Desktop.Plugins.MaterialType.MaterialTypeList.UCMaterialTypeList", "New", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("Reload", "HIS.Desktop.Plugins.MaterialType.MaterialTypeList.UCMaterialTypeList", "Reload", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Search", "HIS.Desktop.Plugins.MaterialType.MaterialTypeList.UCMaterialTypeList", "Search", KeyStroke = XKeys.Control | XKeys.R)]
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
