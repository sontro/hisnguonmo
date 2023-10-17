using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.HisAccountBookList
{
    [KeyboardAction("Refesh", "HIS.Desktop.Plugins.HisAccountBookList.UCHisAccountBookList", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Search", "HIS.Desktop.Plugins.HisAccountBookList.UCHisAccountBookList", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.HisAccountBookList.UCHisAccountBookList", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("Add", "HIS.Desktop.Plugins.HisAccountBookList.UCHisAccountBookList", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("Edit", "HIS.Desktop.Plugins.HisAccountBookList.UCHisAccountBookList", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("Cancel", "HIS.Desktop.Plugins.HisAccountBookList.UCHisAccountBookList", KeyStroke = XKeys.Control | XKeys.B)]
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
