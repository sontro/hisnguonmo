using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrViewerList
{
    [KeyboardAction("Search", "EMR.Desktop.Plugins.EmrViewerList.UcEmrViewerList", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refreshs", "EMR.Desktop.Plugins.EmrViewerList.UcEmrViewerList", KeyStroke = XKeys.Control | XKeys.R)]
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
