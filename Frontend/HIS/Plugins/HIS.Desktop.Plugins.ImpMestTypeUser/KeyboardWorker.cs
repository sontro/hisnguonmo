using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestTypeUser
{
    [KeyboardAction("Save", "HIS.Desktop.Plugins.ImpMestTypeUser.UCImpMestTypeUser", "Save", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("FindImpMestType", "HIS.Desktop.Plugins.ImpMestTypeUser.UCImpMestTypeUser", "FindImpMestType", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("FindAccount", "HIS.Desktop.Plugins.ImpMestTypeUser.UCImpMestTypeUser", "FindAccount", KeyStroke = XKeys.Control | XKeys.D)]
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
