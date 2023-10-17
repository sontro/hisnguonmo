using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestAggrExam
{
    [KeyboardAction("bbtnSearch12345", "HIS.Desktop.Plugins.ExpMestAggrExam.UCExpMestAggrExam", "bbtnSearch12345", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("bbtnRefesh123456", "HIS.Desktop.Plugins.ExpMestAggrExam.UCExpMestAggrExam", "bbtnRefesh123456", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("bbtnAggrExpMest", "HIS.Desktop.Plugins.ExpMestAggrExam.UCExpMestAggrExam", "bbtnAggrExpMest", KeyStroke = XKeys.Control | XKeys.T)]
    [KeyboardAction("f2KeyWordFocused", "HIS.Desktop.Plugins.ExpMestAggrExam.UCExpMestAggrExam", "f2KeyWordFocused", KeyStroke = XKeys.F2)]
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
