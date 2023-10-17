using Inventec.Core;
using System;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.MedicineTypeCreate
{
    class MedicineTypeCreateFactory
    {
        internal static IMedicineTypeCreate MakeIMaterialType(CommonParam param, object[] data)
        {
            IMedicineTypeCreate result = null;
            try
            {

                result = new MedicineTypeCreateBehavior(param, data);

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
