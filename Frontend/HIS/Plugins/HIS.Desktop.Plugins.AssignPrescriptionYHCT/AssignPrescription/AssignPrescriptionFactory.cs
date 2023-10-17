using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.ADO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT.AssignPrescription
{
    class AssignPrescriptionFactory
    {
        internal static IAssignPrescription MakeIAssignPrescription(CommonParam param, object[] data)
        {
            IAssignPrescription result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            AssignPrescriptionADO AssignPrescriptionADO = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is AssignPrescriptionADO)
                            {
                                AssignPrescriptionADO = (AssignPrescriptionADO)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                        }

                        //Bắt buộc các chỗ sử dụng module này phải truyền vào phòng đang làm việc (roomId)
                        if (AssignPrescriptionADO == null) throw new NullReferenceException("AssignPrescriptionADO");
                        if (moduleData == null) throw new NullReferenceException("moduleData");
                        if (moduleData.RoomId <= 0) throw new NullReferenceException("moduleData.RoomId = " + moduleData.RoomId);

                        result = new AssignPrescriptionBehavior(moduleData, param, (AssignPrescriptionADO)AssignPrescriptionADO);
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
