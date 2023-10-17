using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestOtherExport
{
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.ExpMestOtherExport.UCExpMestOtherExport", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnSave", "HIS.Desktop.Plugins.ExpMestOtherExport.UCExpMestOtherExport", "BtnSave", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.ExpMestOtherExport.UCExpMestOtherExport", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("FocusPresCode", "HIS.Desktop.Plugins.ExpMestOtherExport.UCExpMestOtherExport", "FocusPresCode", KeyStroke = XKeys.F2)]
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
