using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisSampleList
{
    [KeyboardAction("SearchData", "LIS.Desktop.Plugins.LisSampleList.Run.UCLisSampleList", "SearchData", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("RefreshData", "LIS.Desktop.Plugins.LisSampleList.Run.UCLisSampleList", "RefreshData", KeyStroke = XKeys.Control | XKeys.R)]
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
