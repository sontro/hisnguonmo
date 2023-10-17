using HIS.Desktop.ADO;
//using HIS.Desktop.Plugins.KidneyShiftSchedule.ADO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KidneyShiftSchedule.KidneyShift
{
    class KidneyShiftFactory
    {
        internal static IKidneyShift MakeIAssignService(CommonParam param, object[] data)
        {
            IKidneyShift result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            KidneyShiftScheduleADO kidneyShiftScheduleADO = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            if (data[i] is KidneyShiftScheduleADO)
                            {
                                kidneyShiftScheduleADO = (KidneyShiftScheduleADO)data[i];
                            }
                        }

                        //- Bắt buộc các chỗ sử dụng module này phải truyền vào phòng đang làm việc (roomId)
                        if (moduleData == null) throw new NullReferenceException("moduleData");
                        if (moduleData.RoomId <= 0) throw new NullReferenceException("moduleData.RoomId = " + moduleData.RoomId);

                        result = new KidneyShiftBehavior(param, moduleData, kidneyShiftScheduleADO);
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
