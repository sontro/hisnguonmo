using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SamplePathologyReq
{
    [KeyboardAction("bbtnSearch_ItemClick", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "bbtnSearch_ItemClick", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnF2_ItemClick", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "bbtnF2_ItemClick", KeyStroke =  XKeys.F2)]
    [KeyboardAction("bbtnF3_ItemClick", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "bbtnF3_ItemClick", KeyStroke = XKeys.F3)]
    [KeyboardAction("bbtnCall_ItemClick", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "bbtnCall_ItemClick", KeyStroke = XKeys.F6)]
    [KeyboardAction("bbtnCallBack_ItemClick", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "bbtnCallBack_ItemClick", KeyStroke =  XKeys.F7)]
    [KeyboardAction("barSave_ItemClick", "HIS.Desktop.Plugins.BaseCompensationCreate.UCBaseCompensationCreate", "barSave_ItemClick", KeyStroke = XKeys.Control | XKeys.S)]
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
