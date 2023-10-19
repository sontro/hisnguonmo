using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole.Lock
{
    class AcsRoleChangeLockBehaviorFactory
    {
        internal static IAcsRoleChangeLock MakeIAcsRoleChangeLock(CommonParam param, object data)
        {
            IAcsRoleChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ROLE))
                {
                    result = new AcsRoleChangeLockBehaviorEv(param, (ACS_ROLE)data);
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
