using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestChmsCreate
{
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.ExpMestChmsCreate.UCExpMestChmsCreate", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnSave", "HIS.Desktop.Plugins.ExpMestChmsCreate.UCExpMestChmsCreate", "BtnSave", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnUpdate", "HIS.Desktop.Plugins.ExpMestChmsCreate.UCExpMestChmsCreate", "BtnUpdate", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.ExpMestChmsCreate.UCExpMestChmsCreate", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("BtnPrintt", "HIS.Desktop.Plugins.ExpMestChmsCreate.UCExpMestChmsCreate", "BtnPrintt", KeyStroke = XKeys.Control | XKeys.P)]
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
