using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD130
{
    [KeyboardAction("BtnFind", "HIS.Desktop.Plugins.ExportXmlQD130.UCExportXml", "BtnFind", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnUnLock", "HIS.Desktop.Plugins.ExportXmlQD130.UCExportXml", "BtnUnLock", KeyStroke = XKeys.Control | XKeys.O)]
    [KeyboardAction("BtnLock", "HIS.Desktop.Plugins.ExportXmlQD130.UCExportXml", "BtnLock", KeyStroke = XKeys.Control | XKeys.L)]
    [KeyboardAction("BtnExportXml", "HIS.Desktop.Plugins.ExportXmlQD130.UCExportXml", "BtnExportXml", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("FocusTreatmentCode", "HIS.Desktop.Plugins.ExportXmlQD130.UCExportXml", "FocusTreatmentCode", KeyStroke = XKeys.F2)]
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
