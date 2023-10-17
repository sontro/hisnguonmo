using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PayClinicalResult
{
    [KeyboardAction("FindShortcut", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "FindShortcut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("ExecuteShortcut", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "ExecuteShortcut", KeyStroke = XKeys.Control | XKeys.X)]
    [KeyboardAction("AssignServiceShortcut", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "AssignServiceShortcut", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("AssignPrescriptionShortcut", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "AssignPrescriptionShortcut", KeyStroke = XKeys.Control | XKeys.K)]
    [KeyboardAction("BordereauShortcut", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "BordereauShortcut", KeyStroke = XKeys.F5)]
    [KeyboardAction("CallShorcut", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "CallShorcut", KeyStroke = XKeys.F6)]
    [KeyboardAction("ReCallShorcut", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "ReCallShorcut", KeyStroke = XKeys.F7)]
    [KeyboardAction("123321", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "123321", KeyStroke = XKeys.F11)]
    [KeyboardAction("FindFocus", "HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom", "FindFocus", KeyStroke = XKeys.F2)]
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
