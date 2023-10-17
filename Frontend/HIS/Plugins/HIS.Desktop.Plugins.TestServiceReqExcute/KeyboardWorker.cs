using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestServiceReqExcute
{
    [KeyboardAction("Save", "HIS.Desktop.Plugins.TestServiceReqExcute.UCTestServiceReqExcute", "Save", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("Print", "HIS.Desktop.Plugins.TestServiceReqExcute.UCTestServiceReqExcute", "Print", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("Finish", "HIS.Desktop.Plugins.TestServiceReqExcute.UCTestServiceReqExcute", "Finish", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("KeDonThuoc", "HIS.Desktop.Plugins.TestServiceReqExcute.UCTestServiceReqExcute", "KeDonThuoc", KeyStroke = XKeys.F8)]
    [KeyboardAction("LuuVaIn", "HIS.Desktop.Plugins.TestServiceReqExcute.UCTestServiceReqExcute", "LuuVaIn", KeyStroke = XKeys.Control | XKeys.I)]
    [KeyboardAction("LuuVaInVaKetThuc", "HIS.Desktop.Plugins.TestServiceReqExcute.UCTestServiceReqExcute", "LuuVaInVaKetThuc", KeyStroke = XKeys.Control | XKeys.K)]
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
