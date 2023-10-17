using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ReturnMicrobiologicalResults
{
    [KeyboardAction("SEARCH", "HIS.Desktop.Plugins.ReturnMicrobiologicalResults.UC_ReturnMicrobiologicalResults", "SEARCH", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("PRINT", "HIS.Desktop.Plugins.ReturnMicrobiologicalResults.UC_ReturnMicrobiologicalResults", "PRINT", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("SAVE", "HIS.Desktop.Plugins.ReturnMicrobiologicalResults.UC_ReturnMicrobiologicalResults", "SAVE", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("FocusF1", "HIS.Desktop.Plugins.ReturnMicrobiologicalResults.UC_ReturnMicrobiologicalResults", "FocusF1", KeyStroke = XKeys.F1)]
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
