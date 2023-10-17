using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HoreHandoverList
{
    [KeyboardAction("FIND", "HIS.Desktop.Plugins.HoreHandoverList.UCHoreHandoverList", "FIND", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("REFRESH", "HIS.Desktop.Plugins.HoreHandoverList.UCHoreHandoverList", "REFRESH", KeyStroke = XKeys.Control | XKeys.R)]
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
