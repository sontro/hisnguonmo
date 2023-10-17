using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.TreatmentBedRoomList;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentBedRoomList
{
    class TreatmentBedRoomListFactory
    {
        internal static ITreatmentBedRoomList MakeITreatmentBedRoomList(CommonParam param, object[] data)
        {
            ITreatmentBedRoomList result = null;
            try
            {
                result = new TreatmentBedRoomListBehavior(param, data);
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
