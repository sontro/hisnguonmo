using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    [KeyboardAction("Save", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "Save", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("Search", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "Search", KeyStroke = XKeys.F5)]
    [KeyboardAction("End", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "End", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("Print", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "Print", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("CaptureImage1", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "CaptureImage1", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("ChupHinhClick", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "ChupHinhClick", KeyStroke = XKeys.F2)]
    [KeyboardAction("ChupHinhClick1", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "ChupHinhClick1", KeyStroke = XKeys.F11)]
    [KeyboardAction("AssignService", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "AssignService", KeyStroke = XKeys.F9)]
    [KeyboardAction("AssignPre", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "AssignPre", KeyStroke = XKeys.F8)]
    //[KeyboardAction("SaveNPrint", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "SaveNPrint", KeyStroke = XKeys.Control | XKeys.I)]

    //[KeyboardAction("UCServiceExecuteBtnService", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "UCServiceExecuteBtnService", KeyStroke = XKeys.F9)]
    //[KeyboardAction("UCServiceExecuteBtnPrescription", "HIS.Desktop.Plugins.ServiceExecute.UCServiceExecute", "UCServiceExecuteBtnPrescription", KeyStroke = XKeys.F8)]
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
