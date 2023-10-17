using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.ApproveKskTreatment
{
    [KeyboardAction("Search", "HIS.Desktop.Plugins.ApproveKskTreatment.UCApproveKskTreatment", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("ApprovalKskContract", "HIS.Desktop.Plugins.ApproveKskTreatment.UCApproveKskTreatment", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("UnApprovalKskContract", "HIS.Desktop.Plugins.ApproveKskTreatment.UCApproveKskTreatment", KeyStroke = XKeys.Control | XKeys.H)]
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
