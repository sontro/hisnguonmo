using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SchedulerJob
{
    [KeyboardAction("Search", "HIS.Desktop.Plugins.SchedulerJob.SchedulerJob.UCMedcineTypeList", "Search", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refesh", "HIS.Desktop.Plugins.SchedulerJob.SchedulerJob.UCMedcineTypeList", "Refesh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.SchedulerJob.MecicineTypeUpdate.frmMedicineTypeUpdate", "Save", KeyStroke = XKeys.Control | XKeys.S)]
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
