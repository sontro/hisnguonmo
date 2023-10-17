using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CompensationCreateSelect
{
    [KeyboardAction("FIND", "HIS.Desktop.Plugins.CompensationCreateSelect", "FIND", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("REFRESH", "HIS.Desktop.Plugins.CompensationCreateSelect", "REFRESH", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("SAVE", "HIS.Desktop.Plugins.CompensationCreateSelect", "SAVE", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("NEW", "HIS.Desktop.Plugins.CompensationCreateSelect", "NEW", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("bbtnSearch", "HIS.Desktop.Plugins.CompensationCreateSelect", "bbtnSearch", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnRefesh", "HIS.Desktop.Plugins.CompensationCreateSelect", "bbtnRefesh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("bbtnTongHop", "HIS.Desktop.Plugins.CompensationCreateSelect", "bbtnTongHop", KeyStroke = XKeys.Control | XKeys.T)]
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
