using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.HisRationSum
{
    [KeyboardAction("FocusKeyword", "HIS.Desktop.Plugins.HisRationSum.UCHisRationSum", KeyStroke = XKeys.F2)]
    [KeyboardAction("DuyetChot", "HIS.Desktop.Plugins.HisRationSum.UCHisRationSum", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("TimRationSum", "HIS.Desktop.Plugins.HisRationSum.UCHisRationSum", KeyStroke = XKeys.Control | XKeys.Shift | XKeys.F)]
    [KeyboardAction("TimSQ", "HIS.Desktop.Plugins.HisRationSum.UCHisRationSum", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("InTongHop", "HIS.Desktop.Plugins.HisRationSum.UCHisRationSum", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("RefreshAll", "HIS.Desktop.Plugins.HisRationSum.UCHisRationSum", KeyStroke = XKeys.Control | XKeys.R)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    class KeyboardWorker : Tool<IDesktopToolContext>
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
