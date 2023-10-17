using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SampleRoomPartialKXN
{
    [KeyboardAction("BtnCreateSample", "HIS.Desktop.Plugins.SampleRoomPartialKXN.UCSampleRoomPartialKXN", "BtnCreateSample", KeyStroke = XKeys.Control | XKeys.M)]
    [KeyboardAction("BtnFind", "HIS.Desktop.Plugins.SampleRoomPartialKXN.UCSampleRoomPartialKXN", "BtnFind", KeyStroke = XKeys.Control | XKeys.F)]
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
