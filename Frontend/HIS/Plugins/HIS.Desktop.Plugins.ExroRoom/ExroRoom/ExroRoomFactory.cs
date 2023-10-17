using System;
using System.Collections.Generic;
using System.Linq;
using Inventec.Core;

namespace HIS.Desktop.Plugins.ExroRoom.ExroRoom
{
    internal class ExroRoomFactory
    {
        internal static IExroRoom MakeIMedicineTypeRoom(CommonParam param, object[] data)
        {
            IExroRoom result = null;
            try
            {
                result = new ExroRoomBehavior(param, data);
                if (result == null)
                {
                    throw new NullReferenceException();
                }
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
