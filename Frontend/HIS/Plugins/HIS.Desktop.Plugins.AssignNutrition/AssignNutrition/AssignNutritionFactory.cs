using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.AssignNutrition.ADO;
using Inventec.Core;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignNutrition.AssignNutrition
{
    class AssignNutritionFactory
    {
        internal static IAssignNutrition MakeIAssignNutrition(CommonParam param, object[] data)
        {
            IAssignNutrition result = null;
            HisTreatmentBedRoomLViewFilter treatmentBedRoomLViewFilter = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            long treatmentId = 0;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is long)
                            {
                                treatmentId = (long)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is HisTreatmentBedRoomLViewFilter)
                            {
                                treatmentBedRoomLViewFilter = (HisTreatmentBedRoomLViewFilter)data[i];
                            }
                        }

                        if (moduleData == null) throw new NullReferenceException("moduleData");
                        if (moduleData.RoomId <= 0) throw new NullReferenceException("moduleData.RoomId = " + moduleData.RoomId);

                        result = new AssignNutritionBehavior(param, treatmentId, moduleData, treatmentBedRoomLViewFilter);
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
