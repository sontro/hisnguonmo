using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpBloodChmsCreate
{
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.ExpBloodChmsCreate.UCExpBloodChmsCreate", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnSave", "HIS.Desktop.Plugins.ExpBloodChmsCreate.UCExpBloodChmsCreate", "BtnSave", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnUpdate", "HIS.Desktop.Plugins.ExpBloodChmsCreate.UCExpBloodChmsCreate", "BtnUpdate", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.ExpBloodChmsCreate.UCExpBloodChmsCreate", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
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
