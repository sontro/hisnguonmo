using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DepartmentTranReceive
{
    [KeyboardAction("bbtnSearch", "HIS.Desktop.Plugins.DepartmentTranReceive.UCDepartmentTranReceive", "bbtnSearch", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnReload", "HIS.Desktop.Plugins.DepartmentTranReceive.UCDepartmentTranReceive", "bbtnReload", KeyStroke = XKeys.Control | XKeys.R)]
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
