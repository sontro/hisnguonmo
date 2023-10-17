using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KidneyShiftSchedule
{
    [KeyboardAction("AddKidneyShiftShortcut", "HIS.Desktop.Plugins.KidneyShiftSchedule.KidneyShift.UCKidneyShift", "AddKidneyShiftShortcut", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("SearchServiceReqKidneyShiftShortcut", "HIS.Desktop.Plugins.KidneyShiftSchedule.KidneyShift.UCKidneyShift", "SearchServiceReqKidneyShiftShortcut", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("PrintServiceReqKidneyShiftShortcut", "HIS.Desktop.Plugins.KidneyShiftSchedule.KidneyShift.UCKidneyShift", "PrintServiceReqKidneyShiftShortcut", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("SearchPatientInBedRoomShortcut", "HIS.Desktop.Plugins.KidneyShiftSchedule.KidneyShift.UCKidneyShift", "SearchPatientInBedRoomShortcut", KeyStroke = XKeys.Control | XKeys.Shift | XKeys.F)]
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
