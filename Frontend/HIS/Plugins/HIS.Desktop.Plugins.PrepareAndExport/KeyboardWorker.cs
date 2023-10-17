using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PrepareAndExport
{
    [KeyboardAction("TaiLai", "HIS.Desktop.Plugins.PrepareAndExport.Run.frmPrepareAndExport", "TaiLai", KeyStroke = XKeys.F4)]
    [KeyboardAction("InDon", "HIS.Desktop.Plugins.PrepareAndExport.Run.frmPrepareAndExport", "InDon", KeyStroke = XKeys.F5)]
    [KeyboardAction("DaPhatThuoc", "HIS.Desktop.Plugins.PrepareAndExport.Run.frmPrepareAndExport", "DaPhatThuoc", KeyStroke = XKeys.F9)]
    [KeyboardAction("VangMat", "HIS.Desktop.Plugins.PrepareAndExport.Run.frmPrepareAndExport", "VangMat", KeyStroke = XKeys.F10)]
    [KeyboardAction("Goi", "HIS.Desktop.Plugins.PrepareAndExport.Run.frmPrepareAndExport", "Goi", KeyStroke = XKeys.F8)]
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
