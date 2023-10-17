using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXml
{
    [KeyboardAction("BtnFind", "HIS.Desktop.Plugins.ExportXml.UCExportXml", "BtnFind", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnExportXml", "HIS.Desktop.Plugins.ExportXml.UCExportXml", "BtnExportXml", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("FocusTreatmentCode", "HIS.Desktop.Plugins.ExportXml.UCExportXml", "FocusTreatmentCode", KeyStroke = XKeys.F2)]
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
