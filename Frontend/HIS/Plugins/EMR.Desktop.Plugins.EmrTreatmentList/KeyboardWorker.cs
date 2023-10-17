using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrTreatmentList
{
    [KeyboardAction("Search", "EMR.Desktop.Plugins.EmrTreatmentList.UcEmrTreatmentList", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refreshs", "EMR.Desktop.Plugins.EmrTreatmentList.UcEmrTreatmentList", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Save", "EMR.Desktop.Plugins.EmrTreatmentList.UcEmrTreatmentList", KeyStroke = XKeys.Control | XKeys.S)]
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
