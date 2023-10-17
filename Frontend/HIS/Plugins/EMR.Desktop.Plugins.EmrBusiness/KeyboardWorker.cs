using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace EMR.Desktop.Plugins.EmrBusiness
{
    [KeyboardAction("BtnRest", "EMR.Desktop.Plugins.EmrTreatmentList.UcEmrTreatmentList", "BtnRest", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnEdit", "EMR.Desktop.Plugins.EmrBusiness.UC_EmrBusiness", "BtnEdit", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnAdd", "EMR.Desktop.Plugins.EmrBusiness.UC_EmrBusiness", "BtnAdd", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("BtnRest", "EMR.Desktop.Plugins.EmrBusiness.UC_EmrBusiness", "BtnRest", KeyStroke = XKeys.Control | XKeys.R)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    class KeyboardWorker : Tool<DesktopToolContext>
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
