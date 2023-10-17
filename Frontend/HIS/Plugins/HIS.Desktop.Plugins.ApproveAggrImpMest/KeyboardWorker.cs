using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveAggrImpMest
{
    [KeyboardAction("Search", "HIS.Desktop.Plugins.ApproveAggrImpMest.ApproveAggrImpMest.UCMedcineTypeList", "Search", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refesh", "HIS.Desktop.Plugins.ApproveAggrImpMest.ApproveAggrImpMest.UCMedcineTypeList", "Refesh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.ApproveAggrImpMest.MecicineTypeUpdate.frmMedicineTypeUpdate", "Save", KeyStroke = XKeys.Control | XKeys.S)]
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
