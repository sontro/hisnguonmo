using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleEdit
{
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.ExpMestSaleEdit.UCExpMestSaleEdit", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnSave", "HIS.Desktop.Plugins.ExpMestSaleEdit.UCExpMestSaleEdit", "BtnSave", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnSavePrint", "HIS.Desktop.Plugins.ExpMestSaleEdit.UCExpMestSaleEdit", "BtnSavePrint", KeyStroke = XKeys.Control | XKeys.I)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.ExpMestSaleEdit.UCExpMestSaleEdit", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("FocusPresCode", "HIS.Desktop.Plugins.ExpMestSaleEdit.UCExpMestSaleEdit", "FocusPresCode", KeyStroke = XKeys.F2)]
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
