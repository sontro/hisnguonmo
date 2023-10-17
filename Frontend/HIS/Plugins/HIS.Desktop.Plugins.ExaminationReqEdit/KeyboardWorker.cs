using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.ExaminationReqEdit
{
    [KeyboardAction("Search", "HIS.Desktop.Plugins.ExaminationReqEdit.UCExaminationReqEdit", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("Refreshs", "HIS.Desktop.Plugins.ExaminationReqEdit.UCExaminationReqEdit", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("FocusServiceReqCode", "HIS.Desktop.Plugins.ExaminationReqEdit.UCExaminationReqEdit", KeyStroke = XKeys.F2)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.ExaminationReqEdit.UCExaminationReqEdit", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("Print", "HIS.Desktop.Plugins.ExaminationReqEdit.UCExaminationReqEdit", KeyStroke = XKeys.Control | XKeys.P)]

    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    class KeyboardWorker:Tool<IDesktopToolContext>
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
