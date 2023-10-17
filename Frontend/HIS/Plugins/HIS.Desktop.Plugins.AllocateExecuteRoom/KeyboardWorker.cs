using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateExecuteRoom
{

    [KeyboardAction("Add", "HIS.Desktop.Plugins.AllocateExecuteRoom.UCAllocateExecuteRoom", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.AllocateExecuteRoom.UCAllocateExecuteRoom", KeyStroke = XKeys.F5)]
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
