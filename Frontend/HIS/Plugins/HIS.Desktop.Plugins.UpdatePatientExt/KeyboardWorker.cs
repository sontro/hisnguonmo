using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UpdatePatientExt
{
    [KeyboardAction("FindShortCut", "HIS.Desktop.Plugins.UpdatePatientExt.frmUpdatePatientExt", "FindShortCut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("SaveShortCut", "HIS.Desktop.Plugins.UpdatePatientExt.frmUpdatePatientExt", "SaveShortCut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("RefeshShortCut", "HIS.Desktop.Plugins.UpdatePatientExt.frmUpdatePatientExt", "RefeshShortCut", KeyStroke = XKeys.Control | XKeys.R)]
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
