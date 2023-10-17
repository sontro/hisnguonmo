using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RehaServiceReqExecute
{
    [KeyboardAction("SaveShortCut", "HIS.Desktop.Plugins.RehaServiceReqExecute.RehaServiceReqExecuteControl", "SaveShortCut", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("FinishShortCut", "HIS.Desktop.Plugins.RehaServiceReqExecute.RehaServiceReqExecuteControl", "FinishShortCut", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("ThemTapShortCut", "HIS.Desktop.Plugins.RehaServiceReqExecute.RehaServiceReqExecuteControl", "ThemTapShortCut", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("AssignServiceShortCut", "HIS.Desktop.Plugins.RehaServiceReqExecute.RehaServiceReqExecuteControl", "AssignServiceShortCut", KeyStroke = XKeys.F9)]
    [KeyboardAction("AssignPreShortCut", "HIS.Desktop.Plugins.RehaServiceReqExecute.RehaServiceReqExecuteControl", "AssignPreShortCut", KeyStroke = XKeys.F8)]
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
