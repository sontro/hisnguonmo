using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisSampleAggregation
{
    [KeyboardAction("CreateAggrSample", "HIS.Desktop.Plugins.LisSampleAggregation.Run.UCLisSampleAggregation", "CreateAggrSample", KeyStroke = XKeys.F2)]
    [KeyboardAction("SearchData", "HIS.Desktop.Plugins.LisSampleAggregation.Run.UCLisSampleAggregation", "SearchData", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Print", "HIS.Desktop.Plugins.LisSampleAggregation.Run.UCLisSampleAggregation", "Print", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("SetTrayShortcut", "HIS.Desktop.Plugins.LisSampleAggregation.Run.UCLisSampleAggregation", "SetTrayShortcut", KeyStroke = XKeys.F5)]
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
