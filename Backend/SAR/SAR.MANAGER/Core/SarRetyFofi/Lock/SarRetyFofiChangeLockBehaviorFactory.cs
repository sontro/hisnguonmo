using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi.Lock
{
    class SarRetyFofiChangeLockBehaviorFactory
    {
        internal static ISarRetyFofiChangeLock MakeISarRetyFofiChangeLock(CommonParam param, object data)
        {
            ISarRetyFofiChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(SAR_RETY_FOFI))
                {
                    result = new SarRetyFofiChangeLockBehaviorEv(param, (SAR_RETY_FOFI)data);
                }
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__FactoryKhoiTaoDoiTuongThatBai);
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
