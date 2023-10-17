using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisContactPointList
{
    [KeyboardAction("BtnSearch", "HIS.Desktop.Plugins.HisContactPointList.UCHisContactPointList", "BtnSearch", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnRefreshs", "HIS.Desktop.Plugins.HisContactPointList.UCHisContactPointList", "BtnRefreshs", KeyStroke = XKeys.Control | XKeys.R)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<DesktopToolContext>
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
