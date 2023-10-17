using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestServiceExecute
{
    [KeyboardAction("Save", "HIS.Desktop.Plugins.TestServiceExecute.UCServiceExecute", "Save", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("End", "HIS.Desktop.Plugins.TestServiceExecute.UCServiceExecute", "End", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("Print", "HIS.Desktop.Plugins.TestServiceExecute.UCServiceExecute", "Print", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("AssignService", "HIS.Desktop.Plugins.TestServiceExecute.UCServiceExecute", "AssignService", KeyStroke = XKeys.F9)]
    [KeyboardAction("AssignPre", "HIS.Desktop.Plugins.TestServiceExecute.UCServiceExecute", "AssignPre", KeyStroke = XKeys.F8)]
    //[KeyboardAction("UCServiceExecuteBtnService", "HIS.Desktop.Plugins.TestServiceExecute.UCServiceExecute", "UCServiceExecuteBtnService", KeyStroke = XKeys.F9)]
    //[KeyboardAction("UCServiceExecuteBtnPrescription", "HIS.Desktop.Plugins.TestServiceExecute.UCServiceExecute", "UCServiceExecuteBtnPrescription", KeyStroke = XKeys.F8)]
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
