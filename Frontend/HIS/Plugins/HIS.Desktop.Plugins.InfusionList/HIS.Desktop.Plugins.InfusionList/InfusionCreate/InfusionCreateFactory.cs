using HIS.Desktop.ADO;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.InfusionCreate;
using HIS.Desktop.Plugins.InfusionCreate.InfusionCreate;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inventec.Desktop.Plugins.InfusionCreate.InfusionCreate
{
 class InfusionCreateFactory
 {
  internal static IInfusionCreate MakeIInfusionCreate(CommonParam param, object[] data)
  {
   IInfusionCreate result = null;
   Inventec.Desktop.Common.Modules.Module moduleData = null;
   InfusionCreateADO InfusionCreateADO = null;
   long InfusionSumId = 0;
   try
   {
    //#region Test
    //InfusionCreateADO = new InfusionCreateADO();
    //InfusionCreateADO.InfusionSumId = 421;
    //InfusionCreateADO.treatmentId = 4630;
    //#endregion
    if (data.GetType() == typeof(object[]))
    {
     if (data != null && data.Count() > 0)
     {
      for (int i = 0; i < data.Count(); i++)
      {
       if (data[i] is long)
       {
        InfusionSumId = (long)data[i];
       }
       else if (data[i] is Inventec.Desktop.Common.Modules.Module)
       {
        moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
       }
      }

      if (moduleData != null && InfusionSumId != 0)
      {
       InfusionCreateADO = new InfusionCreateADO();
       InfusionCreateADO.InfusionSumId = InfusionSumId;

       HisInfusionFilter filter = new HisInfusionFilter();
       filter.ID = InfusionSumId;



       InfusionCreateADO.treatmentId = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<HIS_INFUSION_SUM>>("/api/HisInfusionSum/Get", ApiConsumers.MosConsumer, filter, param).First().TREATMENT_ID;
       result = new InfusionCreateBehavior(param, moduleData, (InfusionCreateADO)InfusionCreateADO);
      }

     }
    }

    if (result == null) throw new NullReferenceException();
   }
   catch (NullReferenceException ex)
   {
    Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
    result = null;
   }
   catch (Exception ex)
   {
    Inventec.Common.Logging.LogSystem.Error(ex);
    result = null;
   }
   return result;
  }
 }
}
