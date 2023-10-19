using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog.Lock
{
    class AcsActivityLogChangeLockBehaviorFactory
    {
        internal static IAcsActivityLogChangeLock MakeIAcsActivityLogChangeLock(CommonParam param, object data)
        {
            IAcsActivityLogChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ACTIVITY_LOG))
                {
                    result = new AcsActivityLogChangeLockBehaviorEv(param, (ACS_ACTIVITY_LOG)data);
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
