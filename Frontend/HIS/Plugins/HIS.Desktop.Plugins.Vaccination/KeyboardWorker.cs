using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.Vaccination
{
    [KeyboardAction("Search", "HIS.Desktop.Plugins.Vaccination.UCVaccination", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.Vaccination.UCVaccination", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("Focus", "HIS.Desktop.Plugins.Vaccination.UCVaccination", KeyStroke = XKeys.F2)]
    [KeyboardAction("SaveExtend", "HIS.Desktop.Plugins.Vaccination.UCVaccination", KeyStroke = XKeys.Alt | XKeys.Shift | XKeys.S)]
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
