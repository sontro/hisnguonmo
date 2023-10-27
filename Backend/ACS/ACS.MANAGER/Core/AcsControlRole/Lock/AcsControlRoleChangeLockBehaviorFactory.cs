using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole.Lock
{
    class AcsControlRoleChangeLockBehaviorFactory
    {
        internal static IAcsControlRoleChangeLock MakeIAcsControlRoleChangeLock(CommonParam param, object data)
        {
            IAcsControlRoleChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(ACS_CONTROL_ROLE))
                {
                    result = new AcsControlRoleChangeLockBehaviorEv(param, (ACS_CONTROL_ROLE)data);
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
