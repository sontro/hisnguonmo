using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisRationSumList
{
    [KeyboardAction("Search", "HIS.Desktop.Plugins.HisRationSumList.HisRationSumList.UcHisRationSumList", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refreshs", "HIS.Desktop.Plugins.HisRationSumList.HisRationSumList.UcHisRationSumList", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Print", "HIS.Desktop.Plugins.HisRationSumList.HisRationSumList.UcHisRationSumList", KeyStroke = XKeys.Control | XKeys.P)]
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
