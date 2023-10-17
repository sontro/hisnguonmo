using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CashierRoomDepartment
{
  [Inventec.Desktop.Core.Actions.KeyboardAction("FindCashierRoom", "HIS.Desktop.Plugins.CashierRoomDepartment.UCCashierRoomDepartment", "FindCashierRoom", KeyStroke = Inventec.Desktop.Core.XKeys.Control | Inventec.Desktop.Core.XKeys.F)]
  [Inventec.Desktop.Core.Actions.KeyboardAction("FindDepartment", "HIS.Desktop.Plugins.CashierRoomDepartment.UCCashierRoomDepartment","FindDepartment", KeyStroke = Inventec.Desktop.Core.XKeys.Control | Inventec.Desktop.Core.XKeys.D)]
  [Inventec.Desktop.Core.Actions.KeyboardAction("FindSave", "HIS.Desktop.Plugins.CashierRoomDepartment.UCCashierRoomDepartment", "FindSave", KeyStroke = Inventec.Desktop.Core.XKeys.Control | Inventec.Desktop.Core.XKeys.S)]
  public sealed class KeyboardWorker : Inventec.Desktop.Core.Tools.Tool<Inventec.Desktop.Core.IDesktopToolContext>
  {
    public KeyboardWorker() : base() { }

    public override Inventec.Desktop.Core.Actions.IActionSet Actions
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
