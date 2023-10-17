using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestConnectDeviceSample
{
    [KeyboardAction("BtnConnect", "HIS.Desktop.Plugins.TestConnectDeviceSample.UCTestConnectDeviceSample", "Connect", KeyStroke = XKeys.Control | XKeys.C)]
    [KeyboardAction("BtnDisconnect", "HIS.Desktop.Plugins.TestConnectDeviceSample.UCTestConnectDeviceSample", "Disconnect", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("BtnRefresh", "HIS.Desktop.Plugins.TestConnectDeviceSample.UCTestConnectDeviceSample", "Refresh", KeyStroke = XKeys.Control | XKeys.R)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<IDesktopToolContext>
    {
        public KeyboardWorker() { }

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
