using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestBCSCreate
{
    [KeyboardAction("bbtnSearch", "HIS.Desktop.Plugins.ExpMestBCSCreate.Run.UCExpMestBCSCreate", "bbtnSearch", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnRefesh", "HIS.Desktop.Plugins.ExpMestBCSCreate.Run.UCExpMestBCSCreate", "bbtnRefesh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("bbtnTongHop", "HIS.Desktop.Plugins.ExpMestBCSCreate.Run.UCExpMestBCSCreate", "bbtnTongHop", KeyStroke = XKeys.Control | XKeys.T)]
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
