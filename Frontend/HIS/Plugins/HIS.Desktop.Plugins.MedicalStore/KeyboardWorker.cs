using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStore
{
    [KeyboardAction("bbtnSEARCH", "HIS.Desktop.Plugins.MedicalStore.UCMedicalStore", "bbtnSEARCH", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnSAVE", "HIS.Desktop.Plugins.MedicalStore.UCMedicalStore", "bbtnSAVE", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("keyF2Focused", "HIS.Desktop.Plugins.MedicalStore.UCMedicalStore", "keyF2Focused", KeyStroke =  XKeys.F2)]
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
