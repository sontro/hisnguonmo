using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestAggregate
{
    [KeyboardAction("keyF2FocusedKeyWord", "HIS.Desktop.Plugins.ImpMestAggregate.UCImpMestAggregate", "keyF2FocusedKeyWord", KeyStroke = XKeys.F2)]
    [KeyboardAction("bbtnSearch123", "HIS.Desktop.Plugins.ImpMestAggregate.UCImpMestAggregate", "bbtnSearch123", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnRefesh123", "HIS.Desktop.Plugins.ImpMestAggregate.UCImpMestAggregate", "bbtnRefesh123", KeyStroke = XKeys.Control | XKeys.R)]
    //[KeyboardAction("bbtnAggrImpMest123", "HIS.Desktop.Plugins.ImpMestAggregate.UCImpMestAggregate", "bbtnAggrImpMest123", KeyStroke = XKeys.Control | XKeys.T)]
    [KeyboardAction("bbtnAggrOddImpMest123", "HIS.Desktop.Plugins.ImpMestAggregate.UCImpMestAggregate", "bbtnAggrOddImpMest123", KeyStroke = XKeys.Control | XKeys.T)]
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
