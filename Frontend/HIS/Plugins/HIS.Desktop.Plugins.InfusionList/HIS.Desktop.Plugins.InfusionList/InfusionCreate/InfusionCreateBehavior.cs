using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.InfusionCreate;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;


using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.InfusionCreate.InfusionCreate;


namespace Inventec.Desktop.Plugins.InfusionCreate.InfusionCreate
{
 public sealed class InfusionCreateBehavior : Tool<IDesktopToolContext>, IInfusionCreate
 {
  InfusionCreateADO data = null;
  Inventec.Desktop.Common.Modules.Module moduleData = null;
  public InfusionCreateBehavior()
   : base()
  {
  }

  public InfusionCreateBehavior(CommonParam param,Inventec.Desktop.Common.Modules.Module moduleData, InfusionCreateADO data)
   : base()
  {
   this.data = data;
   this.moduleData = moduleData;
  }

  object IInfusionCreate.Run()
  {
   try
   {
    return new frmInfusionCreate(moduleData, data);
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
