using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.HisAssignBlood
{
    //[KeyboardAction("Refesh", "HIS.Desktop.Plugins.AssignPrescriptionPK.frmAssignPrescription", "Refesh", KeyStroke = XKeys.Control | XKeys.R)]
    //[KeyboardAction("Save", "HIS.Desktop.Plugins.AssignPrescriptionPK.frmAssignPrescription", "Search", KeyStroke = XKeys.Control | XKeys.F)]
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
