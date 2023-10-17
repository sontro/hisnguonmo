using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalWarehouse
{
    [KeyboardAction("bbtnSEARCH", "HIS.Desktop.Plugins.MedicalWarehouse.UCMedicalWarehouse", "bbtnSEARCH", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnSAVE", "HIS.Desktop.Plugins.MedicalWarehouse.UCMedicalWarehouse", "bbtnSAVE", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("keyF2Focused", "HIS.Desktop.Plugins.MedicalWarehouse.UCMedicalWarehouse", "keyF2Focused", KeyStroke =  XKeys.F2)]
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
