using Inventec.Core;
using System;

namespace HIS.Desktop.Plugins.MaterialTypeCreate.MaterialTypeCreate
{
    class MaterialTypeCreateFactory
    {
        internal static IMaterialTypeCreate MakeIMaterialType(CommonParam param, object[] data)
        {
            IMaterialTypeCreate result = null;
            try
            {

                result = new MaterialTypeCreateBehavior(param, data);

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
