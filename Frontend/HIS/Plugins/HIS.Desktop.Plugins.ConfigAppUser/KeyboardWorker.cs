using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfigAppUser
{
    [KeyboardAction("FindShortcut", "HIS.Desktop.Plugins.ConfigAppUser.UCConfigAppUser", "FindShortcut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("SaveShortcut", "HIS.Desktop.Plugins.ConfigAppUser.UCConfigAppUser", "SaveShortcut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("RestoreShortcut", "HIS.Desktop.Plugins.ConfigAppUser.UCConfigAppUser", "RestoreShortcut", KeyStroke = XKeys.Control | XKeys.R)]
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
