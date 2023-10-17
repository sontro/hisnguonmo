using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseList
{
    [KeyboardAction("BtnFind", "HIS.Desktop.Plugins.ExpenseList.UCExpenseList", "BtnFind", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnRefresh", "HIS.Desktop.Plugins.ExpenseList.UCExpenseList", "BtnRefresh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.ExpenseList.UCExpenseList", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnEdit", "HIS.Desktop.Plugins.ExpenseList.UCExpenseList", "BtnEdit", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.ExpenseList.UCExpenseList", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
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
