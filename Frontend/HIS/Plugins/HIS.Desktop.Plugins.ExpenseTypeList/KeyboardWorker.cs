using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseTypeList
{
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.ExpenseTypeList.UCExpenseTypeList", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnEdit", "HIS.Desktop.Plugins.ExpenseTypeList.UCExpenseTypeList", "BtnEdit", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.ExpenseTypeList.UCExpenseTypeList", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
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
