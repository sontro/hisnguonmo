using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStoreV2
{
    [KeyboardAction("bbtnSEARCH", "HIS.Desktop.Plugins.MedicalStoreV2.UCMedicalStore", "bbtnSEARCH", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnSAVE", "HIS.Desktop.Plugins.MedicalStoreV2.UCMedicalStore", "bbtnSAVE", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("keyF2Focused", "HIS.Desktop.Plugins.MedicalStoreV2.UCMedicalStore", "keyF2Focused", KeyStroke = XKeys.F2)]
    [KeyboardAction("bbtnSEARCHMediRecord", "HIS.Desktop.Plugins.MedicalStoreV2.UCMedicalStore", "bbtnSEARCHMediRecord", KeyStroke = XKeys.Control | XKeys.Shift | XKeys.F)]
    [KeyboardAction("keyF3Focused", "HIS.Desktop.Plugins.MedicalStoreV2.UCMedicalStore", "keyF3Focused", KeyStroke = XKeys.F3)]
    [KeyboardAction("bbtnImportStore", "HIS.Desktop.Plugins.MedicalStoreV2.UCMedicalStore", "bbtnImportStore", KeyStroke = XKeys.Control | XKeys.I)]
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
