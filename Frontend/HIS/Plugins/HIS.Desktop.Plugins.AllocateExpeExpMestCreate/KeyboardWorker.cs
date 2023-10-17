using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateExpeExpMestCreate
{
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.AllocateExpeExpMestCreate.UCAllocateExpeExpMestCreate", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnSave", "HIS.Desktop.Plugins.AllocateExpeExpMestCreate.UCAllocateExpeExpMestCreate", "BtnSave", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.AllocateExpeExpMestCreate.UCAllocateExpeExpMestCreate", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("FocusPresCode", "HIS.Desktop.Plugins.AllocateExpeExpMestCreate.UCAllocateExpeExpMestCreate", "FocusPresCode", KeyStroke = XKeys.F2)]
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
