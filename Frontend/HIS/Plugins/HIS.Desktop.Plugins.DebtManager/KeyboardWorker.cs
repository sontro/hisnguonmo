using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DebtManager
{

    [KeyboardAction("bbtnFind", "HIS.Desktop.Plugins.DebtManager.UCDebtManager", "bbtnFind", KeyStroke = XKeys.Control | XKeys.F)]

    [KeyboardAction("bbtnDebt", "HIS.Desktop.Plugins.DebtManager.UCDebtManager", "bbtnDebt", KeyStroke = XKeys.Control | XKeys.T)]
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
