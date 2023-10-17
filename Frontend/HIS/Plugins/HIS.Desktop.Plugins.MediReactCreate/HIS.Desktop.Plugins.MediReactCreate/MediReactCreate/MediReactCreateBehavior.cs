using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.MediReactCreate;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;


using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.MediReactCreate.MediReactCreate;


namespace Inventec.Desktop.Plugins.MediReactCreate.MediReactCreate
{
 public sealed class MediReactCreateBehavior : Tool<IDesktopToolContext>, IMediReactCreate
 {
  MediReactCreateADO data = null;
  Inventec.Desktop.Common.Modules.Module moduleData = null;
  public MediReactCreateBehavior()
   : base()
  {
  }

  public MediReactCreateBehavior(CommonParam param,Inventec.Desktop.Common.Modules.Module moduleData, MediReactCreateADO data)
   : base()
  {
   this.data = data;
   this.moduleData = moduleData;
  }

  object IMediReactCreate.Run()
  {
   try
   {
    return new frmMediReactCreate(moduleData, data);
   }
   catch (Exception ex)
   {
    Inventec.Common.Logging.LogSystem.Error(ex);
    //param.HasException = true;
    return null;
   }
  }
 }
}
