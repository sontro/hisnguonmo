using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ReportTypeList
{
    [KeyboardAction("Refesh", "HIS.Desktop.Plugins.ReportTypeList.UCReportTypeList", "Refesh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Search", "HIS.Desktop.Plugins.ReportTypeList.UCReportTypeList", "Search", KeyStroke = XKeys.Control | XKeys.F)]
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
