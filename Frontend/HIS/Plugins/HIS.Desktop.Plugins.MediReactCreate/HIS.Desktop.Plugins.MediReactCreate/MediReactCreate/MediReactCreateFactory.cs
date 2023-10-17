using HIS.Desktop.ADO;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.MediReactCreate;
using HIS.Desktop.Plugins.MediReactCreate.MediReactCreate;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inventec.Desktop.Plugins.MediReactCreate.MediReactCreate
{
 class MediReactCreateFactory
 {
  internal static IMediReactCreate MakeIMediReactCreate(CommonParam param, object[] data)
  {
   IMediReactCreate result = null;
   Inventec.Desktop.Common.Modules.Module moduleData = null;
   MediReactCreateADO MediReactCreateADO = null;
   long MediReactSumId = 0;
   try
   {
    //#region Test
    //MediReactCreateADO = new MediReactCreateADO();
    //MediReactCreateADO.MediReactSumId = 1;
    //MediReactCreateADO.treatmentId = 4630;
    //#endregion
    if (data.GetType() == typeof(object[]))
    {
     if (data != null && data.Count() > 0)
     {
      for (int i = 0; i < data.Count(); i++)
      {
       if (data[i] is long)
       {
        MediReactSumId = (long)data[i];
       }
       else if (data[i] is Inventec.Desktop.Common.Modules.Module)
       {
        moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
       }
       }
      MediReactCreateADO = new MediReactCreateADO();
      MediReactCreateADO.MediReactSumId = MediReactSumId;
      if (moduleData != null && MediReactCreateADO != null)
      {
       
MOS.Filter.HisMediReactSumFilter hisMediReactSumFilter = new MOS.Filter.HisMediReactSumFilter();
hisMediReactSumFilter.ID = MediReactSumId;
MediReactCreateADO.treatmentId = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<HIS_MEDI_REACT_SUM>>("/api/HisMediReactSum/Get", ApiConsumers.MosConsumer, hisMediReactSumFilter, param).First().TREATMENT_ID;
       result = new MediReactCreateBehavior(param, moduleData,(MediReactCreateADO)MediReactCreateADO);
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
