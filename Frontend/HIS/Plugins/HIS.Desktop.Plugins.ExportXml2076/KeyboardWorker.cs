using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXml2076
{
    [KeyboardAction("BtnFind", "HIS.Desktop.Plugins.ExportXml2076.UCExportXml2076", "BtnFind", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnExportXml", "HIS.Desktop.Plugins.ExportXml2076.UCExportXml2076", "BtnExportXml", KeyStroke = XKeys.Control | XKeys.E)]
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
