using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCarerCardBorrow
{
    [KeyboardAction("BtnAddShortcut", "HIS.Desktop.Plugins.HisCarerCardBorrow.Run.UCCarerCardBorrow", "BtnAddShortcut", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnFind", "HIS.Desktop.Plugins.HisCarerCardBorrow.Run.UCCarerCardBorrow", "BtnFind", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnRefresh", "HIS.Desktop.Plugins.HisCarerCardBorrow.Run.UCCarerCardBorrow", "BtnRefresh", KeyStroke = XKeys.Control | XKeys.R)]
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
