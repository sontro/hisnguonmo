using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrSignDocumentList
{
    [KeyboardAction("Search", "EMR.Desktop.Plugins.EmrSignDocumentList.UcEmrSignDocumentList", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refreshs", "EMR.Desktop.Plugins.EmrSignDocumentList.UcEmrSignDocumentList", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Signs", "EMR.Desktop.Plugins.EmrSignDocumentList.UcEmrSignDocumentList", KeyStroke = XKeys.Control | XKeys.S)]
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
