using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ExecuteBedRoomSummary;

namespace HIS.Desktop.Plugins.ExecuteBedRoomSummary.BedRoom
{
    class BedRoomFactory
    {
        internal static IBedRoom MakeIBedRoom(CommonParam param, object data)
        {
            IBedRoom result = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    result = new BedRoomBahavior(param, (object[])data);
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
