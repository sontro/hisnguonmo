using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.TYTFetusExamList
{
    [KeyboardAction("Refesh", "TYT.Desktop.Plugins.TYTFetusExamList.UCListTYTFetusExamList", "Refesh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Search", "TYT.Desktop.Plugins.TYTFetusExamList.UCListTYTFetusExamList", "Search", KeyStroke = XKeys.Control | XKeys.F)]

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
