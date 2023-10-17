using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PeriodDepartmentList
{
    [KeyboardAction("BtnFind", "HIS.Desktop.Plugins.PeriodDepartmentList.UCPeriodDepartmentList", "BtnFind", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnRefresh", "HIS.Desktop.Plugins.PeriodDepartmentList.UCPeriodDepartmentList", "BtnRefresh", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("BtnAdd", "HIS.Desktop.Plugins.PeriodDepartmentList.UCPeriodDepartmentList", "BtnAdd", KeyStroke = XKeys.Control | XKeys.A)]
    [KeyboardAction("BtnEdit", "HIS.Desktop.Plugins.PeriodDepartmentList.UCPeriodDepartmentList", "BtnEdit", KeyStroke = XKeys.Control | XKeys.E)]
    [KeyboardAction("BtnNew", "HIS.Desktop.Plugins.PeriodDepartmentList.UCPeriodDepartmentList", "BtnNew", KeyStroke = XKeys.Control | XKeys.N)]
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
