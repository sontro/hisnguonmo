using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Desktop.Plugins.AcsModuleRole
{
    [KeyboardAction("FindShortcut1", "ACS.Desktop.Plugins.AcsModuleRole.UCAcsModuleRole", "FindShortcut1", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("FindShortcut2", "ACS.Desktop.Plugins.AcsModuleRole.UCAcsModuleRole", "FindShortcut2", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("SaveShortcut", "ACS.Desktop.Plugins.AcsModuleRole.UCAcsModuleRole", "SaveShortcut", KeyStroke = XKeys.Control | XKeys.S)]
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
