using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RadixChangeCabinet
{
    [KeyboardAction("Them", "HIS.Desktop.Plugins.RadixChangeCabinet.UCRadixChangeCabinet", "Them", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("Luu", "HIS.Desktop.Plugins.RadixChangeCabinet.UCRadixChangeCabinet", "Luu", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("Moi", "HIS.Desktop.Plugins.RadixChangeCabinet.UCRadixChangeCabinet", "Moi", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("In", "HIS.Desktop.Plugins.RadixChangeCabinet.UCRadixChangeCabinet", "In", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("FocusF2", "HIS.Desktop.Plugins.RadixChangeCabinet.UCRadixChangeCabinet", "FocusF2", KeyStroke = XKeys.F2)]
    [KeyboardAction("Sua", "HIS.Desktop.Plugins.RadixChangeCabinet.UCRadixChangeCabinet", "Sua", KeyStroke = XKeys.Control | XKeys.E)]
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
