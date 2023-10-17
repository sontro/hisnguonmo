using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationCreate
{
    [KeyboardAction("bbtnSearch", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "bbtnSearch", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnRefesh", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "bbtnRefesh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("SAVE", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "SAVE", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("btnTimKiemS_", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "btnTimKiemS_", KeyStroke = XKeys.Control | XKeys.H)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<DesktopToolContext>
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
