using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InsuranceExpertise
{
    [KeyboardAction("BtnFind", "HIS.Desktop.Plugins.InsuranceExpertise.UCInsuranceExpertise", "BtnFind", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnApproval", "HIS.Desktop.Plugins.InsuranceExpertise.UCInsuranceExpertise", "BtnApproval", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("BtnCancelApproval", "HIS.Desktop.Plugins.InsuranceExpertise.UCInsuranceExpertise", "BtnCancelApproval", KeyStroke = XKeys.Control | XKeys.C)]
    [KeyboardAction("BtnLockHein", "HIS.Desktop.Plugins.InsuranceExpertise.UCInsuranceExpertise", "BtnLockHein", KeyStroke = XKeys.Control | XKeys.L)]
    [KeyboardAction("BtnUnlockHein", "HIS.Desktop.Plugins.InsuranceExpertise.UCInsuranceExpertise", "BtnUnlockHein", KeyStroke = XKeys.Control | XKeys.O)]
    [KeyboardAction("BtnFocusTreatmentCode", "HIS.Desktop.Plugins.InsuranceExpertise.UCInsuranceExpertise", "BtnFocusTreatmentCode", KeyStroke = XKeys.F2)]
    [KeyboardAction("BtnLuuTru", "HIS.Desktop.Plugins.InsuranceExpertise.UCInsuranceExpertise", "BtnLuuTru", KeyStroke = XKeys.F5)]


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
