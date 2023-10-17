using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Patient
{
    [KeyboardAction("Refesh", "HIS.Desktop.Plugins.Patient.UCListPatient", "Refesh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("btnSearchKey", "HIS.Desktop.Plugins.Patient.UCListPatient", "btnSearchKey", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Import", "HIS.Desktop.Plugins.Patient.UCListPatient", "Import", KeyStroke = XKeys.Control | XKeys.I)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.Patient.UCListPatient", "Save", KeyStroke = XKeys.Control | XKeys.S)]
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
