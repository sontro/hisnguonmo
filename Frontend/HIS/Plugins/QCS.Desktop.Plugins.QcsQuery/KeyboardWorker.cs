using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCS.Desktop.Plugins.QcsQuery
{
    [KeyboardAction("bbtnCtrlS_ItemClick", "QCS.Desktop.Plugins.QcsQuery.Sql.UCSql", "bbtnCtrlS_ItemClick", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("bbtnCtrlE_ItemClick", "QCS.Desktop.Plugins.QcsQuery.Sql.UCSql", "bbtnCtrlE_ItemClick", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("bbtnCtrlP_ItemClick", "QCS.Desktop.Plugins.QcsQuery.Sql.UCSql", "bbtnCtrlP_ItemClick", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("bbtnCtrlA_ItemClick", "QCS.Desktop.Plugins.QcsQuery.Sql.UCSql", "bbtnCtrlA_ItemClick", KeyStroke = XKeys.Control | XKeys.A)]
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
